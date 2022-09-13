using BucoCoffee.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BucoCoffee.Helper
{
    public class FirebaseHelper
    {
        private readonly string TableProductType = "ProductType";
        private readonly string TableProductItem = "ProductItem";

        readonly FirebaseClient _firebase = new FirebaseClient("https://bucocoffee-bd671-default-rtdb.firebaseio.com/");

        #region TableProductType

        public async Task<List<ProductType>> GetAllProductTypes()
        {
            return (await _firebase
                .Child(TableProductType)
                .OnceAsync<ProductType>()).Select(item => new ProductType
                {
                    Id = item.Object.Id,
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

        #endregion

        #region TableProductItem

        public async Task<List<ProductItem>> GetAllProductItems()
        {
            return (await _firebase
                .Child(TableProductItem)
                .OnceAsync<ProductItem>()).Select(item => new ProductItem
                {
                    Id = item.Object.Id,
                    SelectedProductTypeId = item.Object.SelectedProductTypeId,
                    Comment = item.Object.Comment,
                    PackageDate = item.Object.PackageDate,
                    PackingDate = item.Object.PackingDate,
                    PackageAmount = item.Object.PackageAmount,
                    PackerName = item.Object.PackerName,
                    Weight = item.Object.Weight
                }).ToList();
        }

        public async Task AddProductItem(Guid productTypeId, string comment, string pckgDate, string packingDate, int pckgAmount, string packerName, double weight)
        {
            await _firebase
                .Child(TableProductItem)
                .PostAsync(new ProductItem()
                {
                    Id = Guid.NewGuid(),
                    SelectedProductTypeId = productTypeId,
                    Comment = comment,
                    PackageDate = pckgDate,
                    PackingDate = packingDate,
                    PackageAmount = pckgAmount,
                    PackerName = packerName,
                    Weight = weight
                });
        }

        public async Task<ProductItem> GetProductItem(Guid productId)
        {
            var allProductItems = await GetAllProductItems();
            await _firebase
                .Child(TableProductItem)
                .OnceAsync<ProductItem>();
            return allProductItems.FirstOrDefault(item => item.Id == productId);
        }

        public async Task UpdateProductitem(Guid productItemId, Guid productTypeId, string comment, string pckgDate, string packingDate, int pckgAmount, string packerName, double weight)
        {
            var toUpdateProductitem = (await _firebase
                .Child(TableProductItem)
                .OnceAsync<ProductItem>()).FirstOrDefault(item => item.Object.Id == productItemId);

            await _firebase
                .Child(TableProductItem)
                .Child(toUpdateProductitem.Key)
                .PutAsync(new ProductItem()
                {
                    Id = productItemId,
                    SelectedProductTypeId = productTypeId,
                    Comment = comment,
                    PackageDate = pckgDate,
                    PackingDate = packingDate,
                    PackageAmount = pckgAmount,
                    PackerName = packerName,
                    Weight = weight
                });
        }

        public async Task DeleteProductitem(Guid productItemId)
        {
            var toDeleteProductItem = (await _firebase
                .Child(TableProductItem)
                .OnceAsync<ProductItem>()).FirstOrDefault(item => item.Object.Id == productItemId);
            await _firebase.Child(TableProductItem).Child(toDeleteProductItem.Key).DeleteAsync();
        }

        #endregion

    }
}
