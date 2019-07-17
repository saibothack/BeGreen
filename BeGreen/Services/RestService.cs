using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BeGreen.Models.Category;
using BeGreen.Models.Coupon;
using BeGreen.Models.Orchard;
using BeGreen.Models.Product;
using BeGreen.Models.Settings;
using BeGreen.Models.Terms;
using BeGreen.Models.User;
using BeGreen.Utilities;
using Newtonsoft.Json;

namespace BeGreen.Services
{
    public class RestService : IRestService
    {
        HttpClient client;

        public RestService()
        {
            client = new HttpClient();
        }

        #region

        public async Task<UserData> LoginAsync(User user)
        {
            var uri = new Uri(Constants.urlApi + "processLogin");

            UserData loginResult = new UserData();

            if (App.CurrentConetion())
            {
                try
                {
                    var dataPost = JsonConvert.SerializeObject(user);
                    var content = new StringContent(dataPost, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;
                    response = await client.PostAsync(uri, content);

                    var request = await response.Content.ReadAsStringAsync();
                    loginResult = JsonConvert.DeserializeObject<UserData>(request);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
            }

            return loginResult;

        }

        public async Task<UserData> RegisterAsync(User user)
        {
            var uri = new Uri(Constants.urlApi + "processRegistration");

            UserData registerResult = new UserData();

            if (App.CurrentConetion())
            {
                try
                {
                    var dataPost = JsonConvert.SerializeObject(user);
                    var content = new StringContent(dataPost, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;
                    response = await client.PostAsync(uri, content);

                    var request = await response.Content.ReadAsStringAsync();
                    registerResult = JsonConvert.DeserializeObject<UserData>(request);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
            }

            return registerResult;

        }

        #endregion

        #region "Products"

        public async Task<CategoryData> getAllCategories(int languajeId)
        {
            var uri = new Uri(Constants.urlApi + "allCategories");

            CategoryData allCategory = new CategoryData();

            if (App.CurrentConetion())
            {   
                try
                {
                    var dataPost = JsonConvert.SerializeObject(new
                    {
                        language_id = languajeId
                    });

                    var content = new StringContent(dataPost, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;
                    response = await client.PostAsync(uri, content);

                    var request = await response.Content.ReadAsStringAsync();
                    allCategory = JsonConvert.DeserializeObject<CategoryData>(request);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
            }

            return allCategory;
        }

        public async Task<ProductData> getAllProducts(GetProducts getProducts)
        {
            var uri = new Uri(Constants.urlApi + "getAllProducts");

            ProductData allProducts = new ProductData();

            if (App.CurrentConetion())
            {
                try
                {
                    var dataPost = JsonConvert.SerializeObject(getProducts);

                    var content = new StringContent(dataPost, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;
                    response = await client.PostAsync(uri, content);

                    var request = await response.Content.ReadAsStringAsync();
                    allProducts = JsonConvert.DeserializeObject<ProductData>(request);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
            }

            return allProducts;
        }

        public async Task<object> setLikeProducts(int idProduct, string idCustomer)
        {
            var uri = new Uri(Constants.urlApi + "likeProduct");

            var likeParams = new
            {
                liked_products_id = idProduct,
                liked_customers_id = idCustomer
            };

            var postBody = new object();

            //ProductData allProducts = new ProductData();

            if (App.CurrentConetion())
            {
                try
                {
                    var dataPost = JsonConvert.SerializeObject(likeParams);

                    var content = new StringContent(dataPost, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;
                    response = await client.PostAsync(uri, content);

                    var request = await response.Content.ReadAsStringAsync();
                    postBody = JsonConvert.DeserializeObject<object>(request);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
            }

            return postBody;
        }

        public async Task<object> setUnLikeProducts(int idProduct, string idCustomer)
        {
            var uri = new Uri(Constants.urlApi + "unlikeProduct");

            var likeParams = new
            {
                liked_products_id = idProduct,
                liked_customers_id = idCustomer
            };

            var postBody = new object();

            //ProductData allProducts = new ProductData();

            if (App.CurrentConetion())
            {
                try
                {
                    var dataPost = JsonConvert.SerializeObject(likeParams);

                    var content = new StringContent(dataPost, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;
                    response = await client.PostAsync(uri, content);

                    var request = await response.Content.ReadAsStringAsync();
                    postBody = JsonConvert.DeserializeObject<object>(request);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
            }

            return postBody;
        }

        #endregion

        #region "Orchards"

        public async Task<OrchardData> getAllOrchards(int idLanguaje, int numberPage)
        {
            var uri = new Uri(Constants.urlApi + "getAllNews");

            var allOrchards = new
            {
                language_id = 1,
                page_number = 0,
                is_feature = 0
            };

            OrchardData allOrchardsReq = new OrchardData();

            if (App.CurrentConetion())
            {
                try
                {
                    var dataPost = JsonConvert.SerializeObject(allOrchards);

                    var content = new StringContent(dataPost, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;
                    response = await client.PostAsync(uri, content);

                    var request = await response.Content.ReadAsStringAsync();
                    allOrchardsReq = JsonConvert.DeserializeObject<OrchardData>(request);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
            }

            return allOrchardsReq;
        }

        #endregion

        #region "Coupons"

        public async Task<List<Coupon>> getAllCoupons()
        {
            var uri = new Uri(Constants.urlApi + "getAllCoupon");

            List<Coupon> allCoupons = new List<Coupon>();

            if (App.CurrentConetion())
            {
                try
                {
                    HttpResponseMessage response = null;
                    response = await client.GetAsync(uri);

                    var request = await response.Content.ReadAsStringAsync();
                    allCoupons = JsonConvert.DeserializeObject<List<Coupon>>(request);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
            }

            return allCoupons;
        }

        #endregion

        #region "Configuration"

        public async Task<TermsData> getAllTerms(int languajeId)
        {
            var uri = new Uri(Constants.urlApi + "getAllPages");

            TermsData allTerms = new TermsData();

            var parameters = new
            {
                language_id = languajeId,
            };

            if (App.CurrentConetion())
            {
                try
                {
                    var dataPost = JsonConvert.SerializeObject(parameters);

                    var content = new StringContent(dataPost, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;
                    response = await client.PostAsync(uri, content);

                    var request = await response.Content.ReadAsStringAsync();
                    allTerms = JsonConvert.DeserializeObject<TermsData>(request);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
            }

            return allTerms;
        }

        public async Task<SettingsData> getSettings()
        {
            var uri = new Uri(Constants.urlApi + "siteSetting");

            SettingsData allSettings = new SettingsData();

            if (App.CurrentConetion())
            {
                try
                {
                    HttpResponseMessage response = null;
                    response = await client.GetAsync(uri);

                    var request = await response.Content.ReadAsStringAsync();
                    allSettings = JsonConvert.DeserializeObject<SettingsData>(request);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
            }

            return allSettings;
        }

        #endregion

        public async Task<object> addToOrder(Models.Order.PostOrder postOrder)
        {
            var uri = new Uri(Constants.urlApi + "addToOrder");
            var postBody = new object();

            if (App.CurrentConetion())
            {
                try
                {
                    var dataPost = JsonConvert.SerializeObject(postOrder);

                    var content = new StringContent(dataPost, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;
                    response = await client.PostAsync(uri, content);

                    var request = await response.Content.ReadAsStringAsync();
                    postBody = JsonConvert.DeserializeObject<object>(request);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
            }

            return postBody;
        }

    }
}
