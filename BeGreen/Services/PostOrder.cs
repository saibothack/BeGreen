using BeGreen.Models;
using BeGreen.Models.Cart;
using BeGreen.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeGreen.Services
{
    public class PostOrder
    {
        public async Task<bool> PostOrderAsync(string comment, string address)
        {
            try {
                var datosCarrito = await App.DataBase.GetCartProductAsync();

                var user = await App.DataBase.GetUser();

                double total = 0;

                Models.Order.PostOrder orderDetails = new Models.Order.PostOrder();
                List<PostProducts> orderProductList = new List<PostProducts>();

                // Set Customer Info
                orderDetails.customers_id = int.Parse(user[0].customers_id);

                // Set Shipping  Info
                orderDetails.delivery_street_address = address;

                orderDetails.comments = comment;
                orderDetails.is_coupon_applied = 0;
                orderDetails.coupon_amount = 0.0;
                orderDetails.coupons = null;

                // Set PaymentNonceToken and PaymentMethod
                //orderDetails.setNonce("kjbcsfsgohiuickdsvhcvs");
                orderDetails.payment_method = "PayPal";

                // Set Checkout Price and Products

                foreach (var itemCompra in datosCarrito)
                {

                    var itemProducto = itemCompra.customersBasketProduct;

                    if (itemProducto != null)
                    {
                        PostProducts orderProduct = new PostProducts();

                        // Get current Product Details
                        orderProduct.products_id = itemProducto.products_id;
                        orderProduct.subtotal = itemProducto.total_price;
                        orderProduct.total = itemProducto.total_price;
                        orderProduct.customers_basket_quantity = itemProducto.customers_basket_quantity;

                        // Add current Product to orderProductList
                        orderProductList.Add(orderProduct);
                    }
                }

                orderDetails.productsTotal = total;
                orderDetails.totalPrice = total;

                orderDetails.products = orderProductList;

                await App.oServiceManager.addToOrder(orderDetails);


                await App.DataBase.newOrder();

                return true;

            } catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}
