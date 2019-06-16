using System;
using System.Threading.Tasks;
using BeGreen.Models;
using BeGreen.Models.Category;
using BeGreen.Models.Orchard;
using BeGreen.Models.Product;
using BeGreen.Models.User;

namespace BeGreen.Services
{
    public interface IRestService
    {
        #region

        Task<UserData> LoginAsync(User user);
        Task<UserData> RegisterAsync(User user);

        #endregion

        #region "Products"

        Task<CategoryData> getAllCategories(int languajeId);
        Task<ProductData> getAllProducts(GetProducts getProducts);
        Task<object> setLikeProducts(int idProduct, string idCustomer);
        Task<object> setUnLikeProducts(int idProduct, string idCustomer);

        #endregion

        #region "Orchards"

        Task<OrchardData> getAllOrchards(int idLanguaje, int numberPage);

        #endregion



    }
}
