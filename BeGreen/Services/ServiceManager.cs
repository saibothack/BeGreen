using System;
using System.Threading.Tasks;
using BeGreen.Models;
using BeGreen.Models.Category;
using BeGreen.Models.Orchard;
using BeGreen.Models.Product;
using BeGreen.Models.User;

namespace BeGreen.Services
{
    public class ServiceManager
    {
        IRestService restService;

        public ServiceManager(IRestService service)
        {
            restService = service;
        }

        #region "User"

        public Task<UserData> LoginAsync(User user)
        {
            return restService.LoginAsync(user);
        }

        public Task<UserData> RegisterAsync(User user)
        {
            return restService.RegisterAsync(user);
        }

        #endregion

        #region "Products"

        public Task<CategoryData> getAllCategories(int languajeId)
        {
            return restService.getAllCategories(languajeId);
        }

        public Task<ProductData> getAllProducts(GetProducts getProducts)
        {
            return restService.getAllProducts(getProducts);
        }

        public Task<object> setLikeProducts(int idProduct, string idCustomer)
        {
            return restService.setLikeProducts(idProduct, idCustomer);
        }

        public Task<object> setUnLikeProducts(int idProduct, string idCustomer)
        {
            return restService.setUnLikeProducts(idProduct, idCustomer);
        }

        #endregion

        #region "Products"

        public Task<OrchardData> getAllOrchards(int idLanguaje, int numberPage)
        {
            return restService.getAllOrchards(idLanguaje, numberPage);
        }

        #endregion
    }
}
