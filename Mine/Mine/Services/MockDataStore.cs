using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mine.Models;

namespace Mine.Services
{
    public class MockDataStore : IDataStore<ItemModel>
    {
        readonly List<ItemModel> items;

        public MockDataStore()
        {
            items = new List<ItemModel>()
            {
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Lighting Sword", Description="Wow, cool.", Value = 7},
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Banana Pistol", Description="Don't slip!", Value = 3 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Sack of Rocks", Description="Shiny stones", Value = 2 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Mom's Slippers", Description="The forbidden weapon", Value = 9 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Power of Love", Description="It never fails", Value = 6 },
            };
        }

        public async Task<bool> CreateAsync(ItemModel item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(ItemModel item)
        {
            var oldItem = items.Where((ItemModel arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var oldItem = items.Where((ItemModel arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ItemModel> ReadAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ItemModel>> IndexAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}