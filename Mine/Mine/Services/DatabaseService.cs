using System;
using System.Threading.Tasks;
using System.Linq;
using SQLite;
using Mine.Models;
using System.Collections.Generic;

namespace Mine.Services
{
    public class DatabaseService : IDataStore<ItemModel>
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public DatabaseService()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ItemModel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None,typeof(ItemModel)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }

        /// <summary>
        /// Insert new data of Item Model
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(ItemModel item)
        {
            //check if null then return false
            if(item == null)
            {
                return false;
            }

            //insert data
            var result = await Database.InsertAsync(item);

            //if no rows are affected, exit with false
            if(result == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Update item 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(ItemModel item)
        {
            if (item == null)
            {
                return false;
            }

            //update data and store rows affected
            var result = await Database.UpdateAsync(item);

            //if no rows affected, return false;
            if (result == 0)
            {
                return false;
            }

            return true;

        }

        /// <summary>
        /// Delete Item model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string id)
        {   
            //get data based on id
            var data = await ReadAsync(id);

            //if null return false
            if (data == null)
            {
                return false;
            }

            //update data and store rows affected
            var result = await Database.DeleteAsync(data);

            //if no rows affected, return false;
            if (result == 0)
            {
                return false;
            }


            return true;
        }

        /// <summary>
        /// retrieves data based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ItemModel> ReadAsync(string id)
        {
            if(id == null)
            {
                return null;
            }

            //get result based on id
            var result = Database.Table<ItemModel>().FirstOrDefaultAsync(m => m.Id == id);

            return result;
        }

        /// <summary>
        /// Grab result for the index page.
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ItemModel>> IndexAsync(bool forceRefresh = false)
        {
            //gets result from the database
            var result = await Database.Table<ItemModel>().ToListAsync();
            return result;
        }
    }
}
