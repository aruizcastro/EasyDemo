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

namespace EasyDemo.Droid
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        Button btnLogin;
        EditText txtUsername;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            txtUsername = FindViewById<EditText>(Resource.Id.txtUsername);
            btnLogin.Click += delegate
            {
                var mainActivity = new Intent(this, typeof(MainActivity));
                mainActivity.PutExtra("MyData", txtUsername.Text);
                StartActivity(mainActivity);
            };


            // Create your application here
        }
    }
}