using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace EasyDemo.Droid
{
    [Activity(Label = "EasyDemo", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Activity
    {
        TextView lblTitle;
        protected override void OnCreate(Bundle bundle)
        {
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;
            lblTitle = FindViewById<TextView>(Resource.Id.lblTitle);
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            string userstatus = Intent.GetStringExtra("MyData");
            if (userstatus == null)
            {
                var loginActivity = new Intent(this, typeof(LoginActivity));
                StartActivity(loginActivity);
            }
            else
            {
                //lblTitle.Text = userstatus;
                Console.WriteLine("user variable"+userstatus);
            }
            
        }
    }
}

