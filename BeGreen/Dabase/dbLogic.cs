using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using BeGreen.Models.Orchard;
using BeGreen.Models.Product;
using BeGreen.Models.Settings;
using SQLite;

namespace BeGreen.Dabase
{
    public class dbLogic
    {
        readonly SQLiteAsyncConnection database;

        public dbLogic(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);

            //dropTables();

            database.CreateTableAsync<Settings>().Wait();
            database.CreateTableAsync<dbUser>().Wait();
            database.CreateTableAsync<Orchard>().Wait();
            database.CreateTableAsync<ProductDetails>().Wait();
            database.CreateTableAsync<CartProduct>().Wait();
            database.CreateTableAsync<CartProductAttributes>().Wait();
            database.CreateTableAsync<Option>().Wait();
            database.CreateTableAsync<Value>().Wait();
        }

        public void dropTables()
        {
            database.ExecuteAsync("DELETE FROM Settings");
            database.ExecuteAsync("DELETE FROM dbUser");
            database.ExecuteAsync("DELETE FROM Orchard");
            database.ExecuteAsync("DELETE FROM ProductDetails");
            database.ExecuteAsync("DELETE FROM CartProduct");
            database.ExecuteAsync("DELETE FROM CartProductAttributes");
            database.ExecuteAsync("DELETE FROM Option");
            database.ExecuteAsync("DELETE FROM Value");
        }

        public async Task newOrder() {

            var itemsOption = await database.Table<Option>().ToListAsync();

            foreach (var itm in itemsOption) {
                await database.DeleteAsync(itm);
            }

            var itemsValues = await database.Table<Value>().ToListAsync();

            foreach (var itm in itemsValues)
            {
                await database.DeleteAsync(itm);
            }

            var itemsAttributes = await database.Table<CartProductAttributes>().ToListAsync();

            foreach (var itm in itemsAttributes)
            {
                await database.DeleteAsync(itm);
            }

            var itemCart = await database.Table<CartProduct>().ToListAsync();

            foreach (var itm in itemCart)
            {
                var itemsProduct = await database.Table<ProductDetails>().Where(i => i.CartProductID.Equals(itm.ID)).ToListAsync();

                foreach (var itmProd in itemsProduct)
                {
                    await database.DeleteAsync(itmProd);
                }

                await database.DeleteAsync(itm);
            }
        }

        public Task<int> SaveUser(dbUser user)
        {
            return database.InsertAsync(user);
        }

        public Task<List<dbUser>> GetUser()
        {
            return database.Table<dbUser>().ToListAsync();
        }

        #region "Estado"

        public Task<int> SaveSettings(Settings settings)
        {
            return database.InsertAsync(settings);
        }

        public async Task SaveSettings(List<Settings> settings)
        {
            try
            {
                foreach (Settings item in settings)
                {
                    await database.InsertAsync(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
        }

        public Task<List<Settings>> GetSettings()
        {
            return database.Table<Settings>().ToListAsync();
        }

        public Task<Settings> GetSettingsById(string setting_id)
        {
            return database.Table<Settings>().Where(i => i.setting_id == setting_id).FirstOrDefaultAsync();
        }

        #endregion

        #region "Orchards"

        public async Task<int> SaveOrchardAsync(Orchard orchard)
        {

            var item = await database.Table<Orchard>().Where(i => i.news_id == orchard.news_id).FirstOrDefaultAsync();
            int idResult = 0;

            if (item == null) {
                idResult = await database.InsertAsync(orchard);
            }

            return idResult;
        }

        public Task<List<Orchard>> GetOrchards()
        {
            return database.Table<Orchard>().ToListAsync();
        }

        public Task<Orchard> GetOrchardsById(string news_id)
        {
            return database.Table<Orchard>().Where(i => i.news_id == news_id).FirstOrDefaultAsync();
        }

        public Task<int> DeleteOrchar(Orchard orchard)
        {
            return database.DeleteAsync(orchard);
        }

        #endregion

        #region "Products"

        public async Task<int> SaveProductsAsync(Product product, int CartProductID = 0)
        {

            ProductDetails productDetails = new ProductDetails();
            int idProducto = 0;
            productDetails = await database.Table<ProductDetails>().Where(i => i.CartProductID.Equals(CartProductID) && i.products_id.Equals(product.products_id)).FirstOrDefaultAsync();

            if (productDetails != null) {

                productDetails.customers_basket_quantity = productDetails.customers_basket_quantity + product.customers_basket_quantity;
                productDetails.products_price = product.products_price;
                productDetails.attributes_price = product.attributes_price;
                productDetails.final_price = product.final_price;
                productDetails.total_price = (double.Parse(productDetails.total_price) + double.Parse(product.total_price)).ToString();
                productDetails.comentProduct = productDetails.comentProduct + ", "+ product.comentProduct;

                await database.UpdateAsync(productDetails);

                idProducto = productDetails.ID;
            }
            else {
                productDetails = new ProductDetails
                {
                    CartProductID = CartProductID,
                    products_id = product.products_id,
                    products_quantity = product.products_quantity,
                    products_image = product.products_image,
                    products_model = product.products_model,
                    products_price = product.products_price,
                    discount_price = product.discount_price,
                    products_date_added = product.products_date_added,
                    products_last_modified = product.products_last_modified,
                    products_date_available = product.products_date_available,
                    products_weight = product.products_weight,
                    products_weight_unit = product.products_weight_unit,
                    products_status = product.products_status,
                    products_ordered = product.products_ordered,
                    products_liked = product.products_liked,
                    language_id = product.language_id,
                    products_name = product.products_name,
                    products_description = product.products_description,
                    products_url = product.products_url,
                    products_viewed = product.products_viewed,
                    products_tax_class_id = product.products_tax_class_id,
                    tax_rates_id = product.tax_rates_id,
                    tax_zone_id = product.tax_zone_id,
                    tax_class_id = product.tax_class_id,
                    tax_priority = product.tax_priority,
                    tax_rate = product.tax_rate,
                    tax_description = product.tax_description,
                    tax_class_title = product.tax_class_title,
                    tax_class_description = product.tax_class_description,
                    categories_id = product.categories_id,
                    categories_name = product.categories_name,
                    categories_image = product.categories_image,
                    categories_icon = product.categories_icon,
                    parent_id = product.parent_id,
                    sort_order = product.sort_order,
                    isLiked = product.isLiked,
                    manufacturers_id = product.manufacturers_id,
                    manufacturers_name = product.manufacturers_name,
                    manufacturers_image = product.manufacturers_image,
                    manufacturers_url = product.manufacturers_url,
                    date_added = product.date_added,
                    last_modified = product.last_modified,
                    isSale_product = product.isSale_product,
                    attributes_price = product.attributes_price,
                    final_price = product.final_price,
                    total_price = product.total_price,
                    customers_basket_quantity = product.customers_basket_quantity,
                    comentProduct = product.comentProduct
                };

                idProducto = await database.InsertAsync(productDetails);
            }

            return idProducto;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            List<Product> products = new List<Product>();
            var produtcs = await database.Table<ProductDetails>().ToListAsync();

            foreach(var item in produtcs)
            {
                Product productDetails = new Product
                {
                    products_id = item.products_id,
                    products_quantity = item.products_quantity,
                    products_image = item.products_image,
                    products_model = item.products_model,
                    products_price = item.products_price,
                    discount_price = item.discount_price,
                    products_date_added = item.products_date_added,
                    products_last_modified = item.products_last_modified,
                    products_date_available = item.products_date_available,
                    products_weight = item.products_weight,
                    products_weight_unit = item.products_weight_unit,
                    products_status = item.products_status,
                    products_ordered = item.products_ordered,
                    products_liked = item.products_liked,
                    language_id = item.language_id,
                    products_name = item.products_name,
                    products_description = item.products_description,
                    products_url = item.products_url,
                    products_viewed = item.products_viewed,
                    products_tax_class_id = item.products_tax_class_id,
                    tax_rates_id = item.tax_rates_id,
                    tax_zone_id = item.tax_zone_id,
                    tax_class_id = item.tax_class_id,
                    tax_priority = item.tax_priority,
                    tax_rate = item.tax_rate,
                    tax_description = item.tax_description,
                    tax_class_title = item.tax_class_title,
                    tax_class_description = item.tax_class_description,
                    categories_id = item.categories_id,
                    categories_name = item.categories_name,
                    categories_image = item.categories_image,
                    categories_icon = item.categories_icon,
                    parent_id = item.parent_id,
                    sort_order = item.sort_order,
                    isLiked = item.isLiked,
                    manufacturers_id = item.manufacturers_id,
                    manufacturers_name = item.manufacturers_name,
                    manufacturers_image = item.manufacturers_image,
                    manufacturers_url = item.manufacturers_url,
                    date_added = item.date_added,
                    last_modified = item.last_modified,
                    isSale_product = item.isSale_product,
                    attributes_price = item.attributes_price,
                    final_price = item.final_price,
                    total_price = item.total_price,
                    customers_basket_quantity = item.customers_basket_quantity,
                    comentProduct = item.comentProduct
                };

                products.Add(productDetails);
            }

            return products;
        }

        public Task<Product> GetProductById(int products_id)
        {
            return database.Table<Product>().Where(i => i.products_id == products_id).FirstOrDefaultAsync();
        }

        public async Task<int> DeleteProducts(Product product)
        {
            var products = await database.Table<ProductDetails>().Where(i => i.products_id == product.products_id && i.isLiked.Equals("1")).ToListAsync();

            foreach (var item in products) {
                await database.DeleteAsync(item);
            }

            return 1;
        }

        #endregion

        #region "Cart Products"

        public async Task<int> SaveCartProductAsync(Models.Cart.CartProduct cartProduct)
        {
            CartProduct cart;
            int idCart = 0;

            cart = new CartProduct
            {
                customersId = cartProduct.customersId,
                customersBasketId = cartProduct.customersBasketId,
                customersBasketDateAdded = cartProduct.customersBasketDateAdded,
                status = true
            };

            idCart = await database.InsertAsync(cart);
            
            await SaveProductsAsync(cartProduct.customersBasketProduct, idCart);
            
            await SaveCartProductAttributesAsync(cartProduct.customersBasketProductAttributes, idCart);

            return idCart;
        }

        public async Task<List<Models.Cart.CartProduct>> GetCartProductAsync()
        {
            List<Models.Cart.CartProduct> cartProducts = new List<Models.Cart.CartProduct>();
            var sourceCart = await database.Table<CartProduct>().ToListAsync();

            foreach (var item in sourceCart) {

                Models.Cart.CartProduct cart = new Models.Cart.CartProduct
                {
                    customersId = item.customersId,
                    customersBasketId = item.customersBasketId,
                    customersBasketDateAdded = item.customersBasketDateAdded
                };

                var itemProduct = await database.Table<ProductDetails>().Where(i => i.CartProductID.Equals(item.ID)).ToListAsync();
                //var itemProductAttribute = await database.Table<CartProductAttributes>().Where(i => i.CartProductID.Equals(item.ID)).ToListAsync();

                var itemProductAttribute = await database.Table<CartProductAttributes>().ToListAsync();

                foreach (var itemAttr in itemProductAttribute) {
                    Models.Cart.CartProductAttributes cartProductAttributes = new Models.Cart.CartProductAttributes();
                    cartProductAttributes.customersBasketId = itemAttr.customersBasketId;
                    cartProductAttributes.productsId= itemAttr.productsId;

                    var itemProductAttributeOption = await database.Table<Option>().Where(i => i.CartProductAttributesID.Equals(itemAttr.ID)).ToListAsync();
                    var itemProductAttributeValues = await database.Table<Value>().Where(i => i.CartProductAttributesID.Equals(itemAttr.ID)).ToListAsync();

                    Models.Cart.Option option = new Models.Cart.Option();

                    foreach (var itemAttVal in itemProductAttributeValues)
                    {
                        Models.Cart.Value value = new Models.Cart.Value {
                            id = itemAttVal.id,
                            price = itemAttVal.price,
                            price_prefix = itemAttVal.price_prefix,
                            value = itemAttVal.value
                        };

                        cartProductAttributes.values.Add(value);
                    }

                    foreach (var itemAttOpt in itemProductAttributeOption) {
                        option.id = itemAttOpt.id;
                        option.name = itemAttOpt.name;
                    }

                    cartProductAttributes.option = option;

                    cart.customersBasketProductAttributes.Add(cartProductAttributes);
                }

                foreach (var itmP in itemProduct) {
                    Product productDetails = new Product
                    {
                        products_id = itmP.products_id,
                        products_quantity = itmP.products_quantity,
                        products_image = itmP.products_image,
                        products_model = itmP.products_model,
                        products_price = itmP.products_price,
                        discount_price = itmP.discount_price,
                        products_date_added = itmP.products_date_added,
                        products_last_modified = itmP.products_last_modified,
                        products_date_available = itmP.products_date_available,
                        products_weight = itmP.products_weight,
                        products_weight_unit = itmP.products_weight_unit,
                        products_status = itmP.products_status,
                        products_ordered = itmP.products_ordered,
                        products_liked = itmP.products_liked,
                        language_id = itmP.language_id,
                        products_name = itmP.products_name,
                        products_description = itmP.products_description,
                        products_url = itmP.products_url,
                        products_viewed = itmP.products_viewed,
                        products_tax_class_id = itmP.products_tax_class_id,
                        tax_rates_id = itmP.tax_rates_id,
                        tax_zone_id = itmP.tax_zone_id,
                        tax_class_id = itmP.tax_class_id,
                        tax_priority = itmP.tax_priority,
                        tax_rate = itmP.tax_rate,
                        tax_description = itmP.tax_description,
                        tax_class_title = itmP.tax_class_title,
                        tax_class_description = itmP.tax_class_description,
                        categories_id = itmP.categories_id,
                        categories_name = itmP.categories_name,
                        categories_image = itmP.categories_image,
                        categories_icon = itmP.categories_icon,
                        parent_id = itmP.parent_id,
                        sort_order = itmP.sort_order,
                        isLiked = itmP.isLiked,
                        manufacturers_id = itmP.manufacturers_id,
                        manufacturers_name = itmP.manufacturers_name,
                        manufacturers_image = itmP.manufacturers_image,
                        manufacturers_url = itmP.manufacturers_url,
                        date_added = itmP.date_added,
                        last_modified = itmP.last_modified,
                        isSale_product = itmP.isSale_product,
                        attributes_price = itmP.attributes_price,
                        final_price = itmP.final_price,
                        total_price = itmP.total_price,
                        customers_basket_quantity = itmP.customers_basket_quantity,
                        comentProduct = itmP.comentProduct
                    };

                    cart.customersBasketProduct = productDetails;
                }

                cartProducts.Add(cart);
            }

            return cartProducts;

        }

        public Task<int> DeleteCartProduct(CartProduct cartProduct)
        {
            return database.DeleteAsync(cartProduct);
        }

        public async Task<int> SaveCartProductAttributesAsync(List<Models.Cart.CartProductAttributes> cartProduct, int CartProductID = 0)
        {
            int idCartProductAttributesAsync = 0;

            foreach (var item in cartProduct)
            {
                CartProductAttributes cartProductAttributes = new CartProductAttributes
                {
                    CartProductID = CartProductID,
                    customersBasketId = item.customersBasketId,
                    productsId = item.productsId
                };

                idCartProductAttributesAsync = await database.InsertAsync(cartProductAttributes);

                await SaveCartProductAttributesOptionAsync(item.option, idCartProductAttributesAsync);

                await SaveCartProductAttributesValue(item.values, idCartProductAttributesAsync);
            }

            return idCartProductAttributesAsync;
        }

        public Task<int> SaveCartProductAttributesOptionAsync(Models.Cart.Option option, int CartProductAttributesID = 0)
        {
            Option saveOption = new Option {
                CartProductAttributesID = CartProductAttributesID,
                id = option.id,
                name = option.name
            };
            
            return database.InsertAsync(saveOption);
        }

        public async Task<int> SaveCartProductAttributesValue(List<Models.Cart.Value> values, int CartProductAttributesID = 0)
        {
            int idCartProductAttributesValue = 0;

            foreach (var item in values)
            {
                Value saveValue = new Value
                {
                    CartProductAttributesID = CartProductAttributesID,
                    id = item.id,
                    value = item.value,
                    price = item.price,
                    price_prefix = item.price_prefix
                };

                idCartProductAttributesValue = await database.InsertAsync(saveValue);
            }

            return idCartProductAttributesValue;
        }

        #endregion
    }
}
