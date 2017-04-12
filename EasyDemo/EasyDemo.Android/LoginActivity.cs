
using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Widget;
using EasyDemo.Droid.Helper;
using System;
using Firebase;
using Firebase.Iid;
using Android.Views;
using Android.Support.V4.App;
using Android.Gms.Common.Apis;
using Android.Gms.Tasks;
using Firebase.Auth;
using Android.Gms.Common;

namespace EasyDemo.Droid
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : FragmentActivity, GoogleApiClient.IOnConnectionFailedListener,
        View.IOnClickListener, IOnCompleteListener, FirebaseAuth.IAuthStateListener
    {
        private const string Tag = "GoogleLogin";
        private const int RcSignIn = 9001;

        #region View Controls
#pragma warning disable 649
        private Button btnSignIn, btnSignOut, btnRevokeAccess;
        private TextView textViewStatus, textViewDetail;
#pragma warning restore 649
        #endregion

        private FirebaseAuth mAuth;
        private GoogleApiClient mGoogleApiClient;
        
        Button btnLogin;
        EditText txtUsername;
        Connection db;
        string conn = "";
        string usr = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            // Call to "wire up" all our controls autoamticlly
            //this.WireUpViews();
            btnSignIn.SetOnClickListener(this);
            btnSignOut.SetOnClickListener(this);
            btnRevokeAccess.SetOnClickListener(this);


            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            txtUsername = FindViewById<EditText>(Resource.Id.txtUsername);
            db = new Connection(this);


            btnLogin.Click += delegate
            {
                var mainActivity = new Intent(this, typeof(MainActivity));
                addData();
                showDatos();
                mainActivity.PutExtra("status", conn);
                mainActivity.PutExtra("username", usr);
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
                    usr = usern;
                    Console.WriteLine("get string pos 3, userstatus:"+ statusu);
                } while (c.MoveToNext());
            }
        }

        public void OnClick(View v)
        {
            throw new NotImplementedException();
        }

        public void OnComplete(Task task)
        {
            throw new NotImplementedException();
        }

        public void OnAuthStateChanged(FirebaseAuth auth)
        {
            throw new NotImplementedException();
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            throw new NotImplementedException();
        }
    }
}