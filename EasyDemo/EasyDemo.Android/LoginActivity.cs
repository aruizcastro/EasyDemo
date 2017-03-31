
using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Widget;
using EasyDemo.Droid.Helper;

namespace EasyDemo.Droid
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        Button btnLogin;
        EditText txtUsername;
        Connection db;

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
                mainActivity.PutExtra("MyData", txtUsername.Text);
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

            }else
            {
                do {
                    usern = c.GetString(1);
                    statusu = c.GetString(3);
                } while (c.MoveToNext());
            }
        }

    }
}