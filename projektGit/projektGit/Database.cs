using System;
using System.Collections.Generic;
using System.Text;

namespace projektGit
{
    public class Database
    {
        private SQLiteAsyncConnection(dbPath);

        public Database(string dbPath)
        {
            database = new SQLLiteAsyncConnection(dbPath);
            database.CreateTableAsync<UserResult>().Wait();
        }


    }
}
