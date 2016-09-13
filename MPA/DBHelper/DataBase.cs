using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using Android.Util;
using MPA.Models;

namespace MPA.DBHelper
{
    public class DataBase
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        /*
         * Create Table
         */
        public bool createDataBase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "mpadata.db")))
                {
                    connection.CreateTable<FacebookAccount>();
                    //connect and create other Model
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLite Exception : ", ex.Message);
                return false;
            }
        }

        //ADD an account Facebook
        public bool AddAnFacebookAccount(FacebookAccount facebook)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "mpadata.db")))
                {
                    connection.Insert(facebook);
                    return true;
                }
                
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLite Exception : ", ex.Message);
                return false;
            }
        }
        //Get Facebook account by IdAccount 
        public bool GetFacebookAccountById(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "mpadata.db")))
                {
                    connection.Query<FacebookAccount>("SELECT * FROM FacebookAccount WHERE IdAccount = ?", id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLite Exception : ", ex.Message);
                return false;
            }
        }

        //Get List Facebook Account
        public List<FacebookAccount> GetAllListFacebookAccount()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "mpadata.db")))
                {
                    return connection.Table<FacebookAccount>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLite Exception : ", ex.Message);
                return null;
            }
        }
        //Update an account facebook
        public bool UpdateAnFacebookAccount(FacebookAccount facebook)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "mpadata.db")))
                {
                    connection.Query<FacebookAccount>("UPDATE FacebookAccount SET NameAccount=?, EmailAccount=?, PasswordAccount=?, DescAccount=? WHERE IdAccount=?", facebook.NameAccount, facebook.EmailAccount, facebook.PasswordAccount, facebook.DescAccount, facebook.IdAccount);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLite Exception : ", ex.Message);
                return false;
            }
        }
        //DeleteFacebookTable
        public bool DeleteTableFacebookAccount(FacebookAccount facebook)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "mpadata.db")))
                {
                    connection.Delete(facebook);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLite Exception : ", ex.Message);
                return false;
            }
        }
    }
}