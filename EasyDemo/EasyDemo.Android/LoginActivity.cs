
using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Widget;
using EasyDemo.Droid.Helper;
using System;
using Firebase;
using Android.Views;
using Android.Support.V4.App;
using Android.Gms.Common.Apis;
using Android.Gms.Tasks;
using Firebase.Auth;
using Android.Gms.Common;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Util;
using System.Diagnostics;

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
            btnSignIn = FindViewById<Button>(Resource.Id.btnSignIn);
            btnSignOut = FindViewById<Button>(Resource.Id.btnSignOut);
            btnRevokeAccess = FindViewById< Button > (Resource.Id.btnRevokeAccess);

            btnSignIn.SetOnClickListener(this);
            btnSignOut.SetOnClickListener(this);
            btnRevokeAccess.SetOnClickListener(this);

            /*----------firebase ---------*/
            // Setup our firebase options then init
            FirebaseOptions o = new FirebaseOptions.Builder()
                .SetApiKey(GetString(Resource.String.ApiKey))
                .SetApplicationId(GetString(Resource.String.ApplicationId))
                .SetDatabaseUrl(GetString(Resource.String.DatabaseUrl))
                .Build();
            FirebaseApp fa = FirebaseApp.InitializeApp(this, o, Application.PackageName);

            // Configure Google Sign In
            GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                    .RequestIdToken(GetString(Resource.String.ServerClientId))
                    .RequestId()
                    .RequestEmail()
                    .Build();

            // Build our api client
            mGoogleApiClient = new GoogleApiClient.Builder(this)
               .EnableAutoManage(this, this)
               .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
               .Build();

            // Get the auth instance so we can add to it
            mAuth = FirebaseAuth.GetInstance(fa);
            /*-----firebase----------*/

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
            switch (v.Id)
            {
                case Resource.Id.btnSignIn: SignIn(); break;
                case Resource.Id.btnSignOut: SignOut(); break;
                case Resource.Id.btnRevokeAccess: RevokeAccess(); break;
                default:

                    // Not handled or unkonwn
                    Log.Debug(Tag, "OnClick:" + v.Id);
                    Debugger.Break();
                    break;
            }
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


        private void SignIn()
        {
            Intent signInIntent = Auth.GoogleSignInApi.GetSignInIntent(mGoogleApiClient);
            StartActivityForResult(signInIntent, RcSignIn);
        }

        private void SignOut()
        {
            // Firebase sign out
            mAuth.SignOut();

            // Google sign out
            Auth.GoogleSignInApi.SignOut(mGoogleApiClient)
                .SetResultCallback(new ResultCallback<IResult>(delegate
                {
                    Log.Debug(Tag, "Auth.GoogleSignInApi.SignOut");
                    UpdateUi();
                }));
        }

        private void RevokeAccess()
        {
            // Firebase sign out
            mAuth.SignOut();

            // Google revoke access
            Auth.GoogleSignInApi.RevokeAccess(mGoogleApiClient)
                .SetResultCallback(new ResultCallback<IResult>(delegate
                {
                    Log.Debug(Tag, "Auth.GoogleSignInApi.RevokeAccess");
                    UpdateUi();
                }));
        }
        private void UpdateUi(FirebaseUser user = null)
        {
            // Check if null so we don't have to rewrite everything twice
            var b = user != null;
            //textViewStatus.SetText(b ? user.Email ?? "No Email" : "Signed Out");
            //textViewDetail.SetText(b ? user.Uid : "");
            btnSignIn.Visibility = b ? ViewStates.Gone : ViewStates.Visible;
            btnSignOut.Visibility = b ? ViewStates.Visible : ViewStates.Gone;
        }

    }
}