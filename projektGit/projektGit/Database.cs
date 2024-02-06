﻿using System;
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

        public Task<List<UserResult>> GetResultsAsync()
        {
            return database.Table<UserResult>().ToListAsync();
        }

        public Task<UserResult> GetUserName(string userName)
        {
            return database.Table<UserResult>().Where(i => i.UserName == userName).FirstOrDefaultAsync();
        }

        public async Task SaveResultAsync(UserResult result)
        {
            var existingResult = await GetUserName(result.UserName);

            if(existingResult !=null)
            {
                if(result.Score > existingResult.Score) 
                {
                    existingResult.TotalTime = result.TotalTime;
                    existingResult.Score = result.Score;
                    await database.UpdateAsync(existingResult);
                }
            }
            else
            {
                await database.InsertAsync(result);
            }

        }
    }
}
