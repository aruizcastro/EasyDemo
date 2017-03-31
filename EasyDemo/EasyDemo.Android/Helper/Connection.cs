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
using Android.Database.Sqlite;
using Android.Database;

namespace EasyDemo.Droid.Helper
{
    public class Connection:SQLiteOpenHelper
    {
        public static string ID = "ID";
        public static string USERNAME = "username";
        public static string PASSWORD = "password";
        public static string STATUS = "status";

        public static string DATABASE = "easydata.db";
        public static string TABLE_USERS = "users";
        //https://www.youtube.com/watch?v=0S-bhBM5fJ8

        public Connection(Context context):base(context, DATABASE, null, 1) {

        }

        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL("CREATE TABLE "+TABLE_USERS+" ("+ID+" INTEGER PRIMARY KEY AUTOINCREMENT, "+USERNAME+" TEXT,"+PASSWORD+" TEXT,"+STATUS+" TEXT)");
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL("DROP TABLE IF EXIST "+TABLE_USERS);
            OnCreate(db);

        }

        public void addDatos(String username, String status) {
            ContentValues valusers = new ContentValues();
            valusers.Put(USERNAME,username);
            valusers.Put(PASSWORD,"123");
            valusers.Put(STATUS,status);
            this.WritableDatabase.Insert(TABLE_USERS,null,valusers);
        }
        public ICursor getDatos() {
            string[] columns = { ID, USERNAME, STATUS };
            ICursor c = this.ReadableDatabase.Query(TABLE_USERS,columns,null,null,null,null,null);
            return c;
        }
    }
}