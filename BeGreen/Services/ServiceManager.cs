using System;
using System.Threading.Tasks;
using BeGreen.Models;

namespace BeGreen.Services
{
    public class ServiceManager
    {
        IRestService restService;

        public ServiceManager(IRestService service)
        {
            restService = service;
        }

        public Task<AllCategory> getAllCategories()
        {
            return restService.getAllCategories();
        }
    }
}
