using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using BeGreen.Helpers;
using System.Collections.Generic;
using BeGreen.Models.Category;
using BeGreen.Models.Product;
using BeGreen.Utilities;

namespace BeGreen.Services.Logic
{
    public class ServicesProduct
    {
        public async Task<List<Category>> CategoriesAsync(int languajeId)
        {
            CategoryData data = await App.oServiceManager.getAllCategories(languajeId);

            if (!data.success.Equals("1"))
            {
                await Application.Current.MainPage.DisplayAlert("Notificación", data.message, "Aceptar");
            }

            return data.data;
        }

        public async Task<List<Product>> ProductsAsync(List<Category> SubCategories)
        {
            List<Product> producData = new List<Product>();

            foreach (Category category in SubCategories)
            {
                GetProducts filterProducts = new GetProducts
                {
                    page_number = 0,
                    language_id = 1,
                    categories_id = category.id,
                    type = "a to z"
                };

                ProductData data = await App.oServiceManager.getAllProducts(filterProducts);

                if (!data.success.Equals("1"))
                {
                    await Application.Current.MainPage.DisplayAlert("Notificación", data.message, "Aceptar");
                } else
                {
                    data.product_data.ForEach(x => x.products_image = (Constants.urlApi + x.products_image));
                    producData.AddRange(data.product_data);
                }

            }

            return producData;
        }

    }
}
