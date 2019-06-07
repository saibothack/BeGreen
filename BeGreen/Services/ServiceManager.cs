using System;
using System.Threading.Tasks;
using BeGreen.Models;
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

        public Task<AllCategory> getAllCategories()
        {
            return restService.getAllCategories();
        }
    }
}
