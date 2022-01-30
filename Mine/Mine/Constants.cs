using SQLite;
using System;
using System.IO;

namespace Mine
{
    public static class Constants
    {
        public const string DatabaseFileName = "TodoSQLLite.db3";

        public const SQLiteOpenFlags Flags = 
            //open database in read write mode
            SQLiteOpenFlags.ReadWrite | 
            //create database if it doesnt exist
            SQLiteOpenFlags.Create | 
            //enable multi threaded database access
            SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get 
            { 
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFileName);
            }
        }

    }
}
