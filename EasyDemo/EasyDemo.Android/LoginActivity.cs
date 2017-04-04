
using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Widget;
using EasyDemo.Droid.Helper;
using System;

namespace EasyDemo.Droid
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        Button btnLogin;
        EditText txtUsername;
        Connection db;
        string conn = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            txtUsername = FindViewById<EditText>(Resource.Id.txtUsername);
            db = new Connection(this);


            btnLogin.Click += delegate
            {
                var mainActivity = new Intent(this, typeof(MainActivity));
                addData();
                showDatos();
                mainActivity.PutExtra("MyData", conn);
                StartActivity(mainActivity);
            };


            // Create your application here
        }

        private void addData()
        {
            string username = txtUsername.Text;
            db.addDatos(username,"in");
            txtUsername.Text = "";

        }
        private void showDatos() {
            string usern = "";
            string statusu = "";
            ICursor c = db.getDatos();
            if (c.MoveToFirst() == false) {
                Console.WriteLine("No dont move to first:");
            }else
            {
                do {
                    usern = c.GetString(1);
                    statusu = c.GetString(2);
                    conn = statusu;
                    Console.WriteLine("get string pos 3, userstatus:");
                } while (c.MoveToNext());
            }
        }

    }
}