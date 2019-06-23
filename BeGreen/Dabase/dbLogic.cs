using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using BeGreen.Models.Orchard;
using BeGreen.Models.Product;
using BeGreen.Models.Settings;
using SQLite;

namespace BeGreen.Dabase
{
    public class dbLogic
    {
        readonly SQLiteAsyncConnection database;

        public dbLogic(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);

            database.CreateTableAsync<Settings>().Wait();
            database.CreateTableAsync<Orchard>().Wait();
            database.CreateTableAsync<Product>().Wait();
        }

        public void dropTables()
        {
            database.ExecuteAsync("DELETE FROM Settings");
            database.ExecuteAsync("DELETE FROM Orchard");
            database.ExecuteAsync("DELETE FROM Product");
        }

        #region "Estado"

        public Task<int> SaveSettings(Settings settings)
        {
            return database.InsertAsync(settings);
        }

        public async Task SaveSettings(List<Settings> settings)
        {
            try
            {
                foreach (Settings item in settings)
                {
                    await database.InsertAsync(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
        }

        public Task<List<Settings>> GetSettings()
        {
            return database.Table<Settings>().ToListAsync();
        }

        public Task<Settings> GetSettingsById(string setting_id)
        {
            return database.Table<Settings>().Where(i => i.setting_id == setting_id).FirstOrDefaultAsync();
        }

        #endregion

        #region "Orchards"

        public Task<int> SaveOrchard(Orchard orchard)
        {
            return database.InsertAsync(orchard);
        }

        public Task<List<Orchard>> GetOrchards()
        {
            return database.Table<Orchard>().ToListAsync();
        }

        public Task<Orchard> GetOrchardsById(string news_id)
        {
            return database.Table<Orchard>().Where(i => i.news_id == news_id).FirstOrDefaultAsync();
        }

        public Task<int> DeleteOrchar(Orchard orchard)
        {
            return database.DeleteAsync(orchard);
        }

        #endregion

        #region "Products"

        public Task<int> SaveProducts(Product product)
        {
            return database.InsertAsync(product);
        }

        public Task<List<Product>> GetProducts()
        {
            return database.Table<Product>().ToListAsync();
        }

        public Task<Product> GetProductById(int products_id)
        {
            return database.Table<Product>().Where(i => i.products_id == products_id).FirstOrDefaultAsync();
        }

        public Task<int> DeleteProducts(Product product)
        {
            return database.DeleteAsync(product);
        }

        #endregion
    }
}
