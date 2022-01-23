using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Mine.Models;
using Mine.Views;

namespace Mine.ViewModels
{
    public class ItemIndexViewModel : BaseViewModel
    {
        public ObservableCollection<ItemModel> DataSet { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemIndexViewModel()
        {
            Title = "Items";
            DataSet = new ObservableCollection<ItemModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<ItemCreatePage, ItemModel>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as ItemModel;
                DataSet.Add(newItem);
                await DataStore.CreateAsync(newItem);
            });

            MessagingCenter.Subscribe<ItemDeletePage, ItemModel>(this, "DeleteItem", async (obj, item) =>
            {
                var data = item as ItemModel;

                await DeleteAsync(data);
            });

            MessagingCenter.Subscribe<ItemUpdatePage, ItemModel>(this, "UpdateItem", async (obj, item) =>
            {
                var data = item as ItemModel;

                await UpdateAsync(data);
            });
        }

        /// <summary>
        /// read an item from the data store
        /// </summary>
        /// <param name="id">ID of the record</param>
        /// <returns>The record from ReadAsync</returns>
        public async Task<ItemModel> ReadAsync(string id)
        {
            var result = await DataStore.ReadAsync(id);

            return result;
        }

        /// <summary>
        /// read an item from the data store
        /// </summary>
        /// <param name="id">ID of the record</param>
        /// <returns>The record from ReadAsync</returns>
        public async Task<bool> UpdateAsync(ItemModel data)
        {
            //check if record exist; if not return null
            var record = await ReadAsync(data.Id);

            if (record == null)
            {
                return false;
            }

            //call to remove data from data store
            var result = await DataStore.UpdateAsync(data);

            var canExecute = LoadItemsCommand.CanExecute(null);
            LoadItemsCommand.Execute(null);

            return result;
        }

        /// <summary>
        /// Delete an item from the data store
        /// </summary>
        /// <param name="data">item model of the item</param>
        /// <returns>false if failed, true if deleted</returns>
        public async Task<bool> DeleteAsync(ItemModel data)
        {
            //check if record exist; if not return null
            var record = await ReadAsync(data.Id);

            if(record == null)
            {
                return false;
            }

            //remove local data set
            DataSet.Remove(data);

            //call to remove data
            var result = await DataStore.DeleteAsync(data.Id);

            return result;
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                DataSet.Clear();
                var items = await DataStore.IndexAsync(true);
                foreach (var item in items)
                {
                    DataSet.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}