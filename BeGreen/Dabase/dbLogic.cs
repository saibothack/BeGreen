using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using BeGreen.Models.Orchard;
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
        }

        public void dropTables()
        {
            database.ExecuteAsync("DELETE FROM Settings");
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
    }
}
