using BucoCoffee.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucoCoffee.Helper
{
    public class FirebaseHelper
    {
        private readonly string TableProductType = "ProductType";

        readonly FirebaseClient _firebase = new FirebaseClient("https://bucocoffee-bd671-default-rtdb.firebaseio.com/");

        public async Task<List<ProductType>> GetAllProductTypes()
        {
            return (await _firebase
                .Child(TableProductType)
                .OnceAsync<ProductType>()).Select(item => new ProductType
                {
                    Title = item.Object.Title,
                    ThemeColor = item.Object.ThemeColor
                }).ToList();
        }

        public async Task AddProductType(string title, string color)
        {
            await _firebase
                .Child(TableProductType)
                .PostAsync(new ProductType()
                {
                    Id = Guid.NewGuid(),
                    Title = title,
                    ThemeColor = color
                });
        }

        public async Task<ProductType> GetProductType(Guid typeId)
        {
            var allProductTypes = await GetAllProductTypes();
            await _firebase
                .Child(TableProductType)
                .OnceAsync<ProductType>();
            return allProductTypes.FirstOrDefault(item => item.Id == typeId);
        }

        public async Task<ProductType> GetProductType(string name)
        {
            var allProductTypes = await GetAllProductTypes();
            await _firebase
                .Child(TableProductType)
                .OnceAsync<ProductType>();
            return allProductTypes.FirstOrDefault(item => item.Title == name);
        }

        public async Task UpdateProductType(Guid productTypeId, string newTitle, string newColor)
        {
            var toUpdateProductType = (await _firebase
                .Child(TableProductType)
                .OnceAsync<ProductType>()).FirstOrDefault(item => item.Object.Id == productTypeId);

            await _firebase
                .Child(TableProductType)
                .Child(toUpdateProductType.Key)
                .PutAsync(new ProductType()
                {
                    Id = productTypeId,
                    Title = newTitle,
                    ThemeColor = newColor
                });
        }

        public async Task DeleteProductType(Guid productTypeId)
        {
            var toDeleteProductType = (await _firebase
                .Child(TableProductType)
                .OnceAsync<ProductType>()).FirstOrDefault(item => item.Object.Id == productTypeId);
            await _firebase.Child(TableProductType).Child(toDeleteProductType.Key).DeleteAsync();
        }
    }
}
