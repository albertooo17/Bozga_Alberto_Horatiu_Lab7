using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bozga_Alberto_Horatiu_Lab7.Models;
using System.Collections;

namespace Bozga_Alberto_Horatiu_Lab7.Data
{
    public class ShoppingListDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public ShoppingListDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ShopList>().Wait();
            _database.CreateTableAsync<Product>().Wait();
            _database.CreateTableAsync<ListProduct>().Wait();
        }
        public Task<int> SaveProductAsync(Product product)
        {
            if (product.ID != 0)
            {
                return _database.UpdateAsync(product);
            }
            else
            {
                return _database.InsertAsync(product);
            }
        }
        public Task<int> DeleteProductAsync(Product product)
        {
            return _database.DeleteAsync(product);
        }
        public Task<List<Product>> GetProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }

        internal Task DeleteShopListAsync(ShopList slist)
        {
            throw new NotImplementedException();
        }

        internal Task SaveShopListAsync(ShopList slist)
        {
            throw new NotImplementedException();
        }

        internal Task<IEnumerable> GetShopListsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveListProductAsync(ListProduct listp)
        {
            if (listp.ID != 0)
            {
                return _database.UpdateAsync(listp);
            }
            else
            {
                return _database.InsertAsync(listp);
            }
        }

        public Task<List<Product>> GetListProductsAsync(int shoplistid)
        {
            return _database.QueryAsync<Product>(
            "select P.ID, P.Description from Product P"
            + " inner join ListProduct LP"
            + " on P.ID = LP.ProductID where LP.ShopListID = ?",
            shoplistid);
        }
        public Task<int> DeleteListProductAsync(int shopListId, int productId)
        {
            return _database.ExecuteAsync(
                "DELETE FROM ListProduct WHERE ShopListID = ? AND ProductID = ?",
                shopListId, productId);
        }

    }
}