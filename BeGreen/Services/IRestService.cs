using System;
using System.Threading.Tasks;
using BeGreen.Models;

namespace BeGreen.Services
{
    public interface IRestService
    {
        Task<AllCategory> getAllCategories();
    }
}
