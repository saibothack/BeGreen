using System;
using System.Threading.Tasks;
using BeGreen.Models;
using BeGreen.Models.Category;
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

        #endregion

    }
}
