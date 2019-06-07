using System;
using System.Threading.Tasks;
using BeGreen.Models;
using BeGreen.Models.User;

namespace BeGreen.Services
{
    public interface IRestService
    {
        #region

        Task<UserData> LoginAsync(User user);
        Task<UserData> RegisterAsync(User user);

        #endregion

        Task<AllCategory> getAllCategories();
    }
}
