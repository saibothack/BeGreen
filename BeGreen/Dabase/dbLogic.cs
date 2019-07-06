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
            database.ExecuteAsync("DELETE FROM Orchard");
            database.ExecuteAsync("DELETE FROM Product");
            database.ExecuteAsync("DELETE FROM CartProduct");
            database.ExecuteAsync("DELETE FROM CartProductAttributes");
            database.ExecuteAsync("DELETE FROM Option");
            database.ExecuteAsync("DELETE FROM Value");
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

        public Task<int> SaveProducts(Product product, int CartProductID = 0)
        {
            ProductDetails productDetails = new ProductDetails
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

            return database.InsertAsync(productDetails);
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
            CartProduct cart = new CartProduct
            {
                customersId = cartProduct.customersId,
                customersBasketId = cartProduct.customersBasketId,
                customersBasketDateAdded = cartProduct.customersBasketDateAdded
            };

            var idCart = await database.InsertAsync(cart);

            await SaveProducts(cartProduct.customersBasketProduct, idCart);

            await SaveCartProductAttributesAsync(cartProduct.customersBasketProductAttributes, idCart);

            return idCart;
        }

        public async Task<List<Models.Cart.CartProduct>> GetCartProductAsync()
        {
            List<Models.Cart.CartProduct> cartProducts = new List<Models.Cart.CartProduct>();
            var sourceCart = await database.Table<CartProduct>().ToListAsync();

            foreach(var item in sourceCart) {

                var itemProduct = await database.Table<ProductDetails>().Where(i => i.CartProductID.Equals(item.ID)).FirstOrDefaultAsync();

                Product productDetails = new Product
                {
                    products_id = itemProduct.products_id,
                    products_quantity = itemProduct.products_quantity,
                    products_image = itemProduct.products_image,
                    products_model = itemProduct.products_model,
                    products_price = itemProduct.products_price,
                    discount_price = itemProduct.discount_price,
                    products_date_added = itemProduct.products_date_added,
                    products_last_modified = itemProduct.products_last_modified,
                    products_date_available = itemProduct.products_date_available,
                    products_weight = itemProduct.products_weight,
                    products_weight_unit = itemProduct.products_weight_unit,
                    products_status = itemProduct.products_status,
                    products_ordered = itemProduct.products_ordered,
                    products_liked = itemProduct.products_liked,
                    language_id = itemProduct.language_id,
                    products_name = itemProduct.products_name,
                    products_description = itemProduct.products_description,
                    products_url = itemProduct.products_url,
                    products_viewed = itemProduct.products_viewed,
                    products_tax_class_id = itemProduct.products_tax_class_id,
                    tax_rates_id = itemProduct.tax_rates_id,
                    tax_zone_id = itemProduct.tax_zone_id,
                    tax_class_id = itemProduct.tax_class_id,
                    tax_priority = itemProduct.tax_priority,
                    tax_rate = itemProduct.tax_rate,
                    tax_description = itemProduct.tax_description,
                    tax_class_title = itemProduct.tax_class_title,
                    tax_class_description = itemProduct.tax_class_description,
                    categories_id = itemProduct.categories_id,
                    categories_name = itemProduct.categories_name,
                    categories_image = itemProduct.categories_image,
                    categories_icon = itemProduct.categories_icon,
                    parent_id = itemProduct.parent_id,
                    sort_order = itemProduct.sort_order,
                    isLiked = itemProduct.isLiked,
                    manufacturers_id = itemProduct.manufacturers_id,
                    manufacturers_name = itemProduct.manufacturers_name,
                    manufacturers_image = itemProduct.manufacturers_image,
                    manufacturers_url = itemProduct.manufacturers_url,
                    date_added = itemProduct.date_added,
                    last_modified = itemProduct.last_modified,
                    isSale_product = itemProduct.isSale_product,
                    attributes_price = itemProduct.attributes_price,
                    final_price = itemProduct.final_price,
                    total_price = itemProduct.total_price,
                    customers_basket_quantity = itemProduct.customers_basket_quantity,
                    comentProduct = itemProduct.comentProduct
                };


                Models.Cart.CartProduct cart = new Models.Cart.CartProduct
                {
                    customersId = item.customersId,
                    customersBasketId = item.customersBasketId,
                    customersBasketDateAdded = item.customersBasketDateAdded,
                    customersBasketProduct = productDetails
                };

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
