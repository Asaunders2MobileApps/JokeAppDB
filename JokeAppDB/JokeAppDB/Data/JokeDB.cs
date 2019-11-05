using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace JokeAppDB
{
    public class JokeDB
    {
        readonly SQLiteAsyncConnection database;

        public JokeDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<JokeItem>().Wait();
        }

        public Task<List<JokeItem>> GetItemsAsync()
        {
            return database.Table<JokeItem>().ToListAsync();
        }

        public Task<List<JokeItem>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<JokeItem>("SELECT * FROM [JokeItem] WHERE [Done] = 0");
        }

        public Task<JokeItem> GetItemAsync(int id)
        {
            return database.Table<JokeItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(JokeItem item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
    }
}
