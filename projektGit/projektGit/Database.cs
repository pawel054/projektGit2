using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;

namespace projektGit
{
    public class Database
    {
        private SQLiteAsyncConnection database;

        public Database(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<UserResult>().Wait();
        }

        public Task<List<UserResul>> GetResultsAsync()
        {
            return database.Table<UserResult>().ToListAsync();
        }
    }
}
