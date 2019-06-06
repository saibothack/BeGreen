using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BeGreen.Models;
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

        public async Task<AllCategory> getAllCategories()
        {
            var uri = new Uri(Constants.urlApi + "allCategories");

            AllCategory allCategory = new AllCategory();

            if (App.CurrentConetion())
            {
                try
                {
                    var dataPost = JsonConvert.SerializeObject(new
                    {
                        language_id = 1
                    });

                    var content = new StringContent(dataPost, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = null;
                    response = await client.PostAsync(uri, content);

                    var request = await response.Content.ReadAsStringAsync();
                    allCategory = JsonConvert.DeserializeObject<AllCategory>(request);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
            }

            return allCategory;
        }
    }
}
