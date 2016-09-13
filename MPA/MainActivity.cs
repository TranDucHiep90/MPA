using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Content.PM;

namespace MPA
{
    [Activity(Label = "MPAccount", MainLauncher = true, Icon = "@drawable/LogoSmall", Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        ImageButton _loginBtn;
        ImageButton _registerBtn;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            _loginBtn = FindViewById<ImageButton>(Resource.Id.LogInButton);
            _registerBtn = FindViewById<ImageButton>(Resource.Id.RegisterButton);
            _loginBtn.Click += delegate
            {
                StartActivity(new Intent(this, typeof(LoginScreenActivity)));
            };
            _registerBtn.Click += delegate
            {
                StartActivity(new Intent(this, typeof(RegisterScreenActivity)));
            };
        }
    }
}

