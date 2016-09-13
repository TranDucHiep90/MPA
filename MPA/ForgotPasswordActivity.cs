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
using Android.Support.V7.App;
using Java.Lang;
using Android.Content.PM;

namespace MPA
{
    [Activity(Label = "ForgotPasswordActivity", Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class ForgotPasswordActivity : AppCompatActivity
    {
        Android.Support.V7.App.AlertDialog.Builder alert;
        TextView _emailUserLogin;
        EditText _emailUserForgot;
        EditText _recoveryPasswordUserEmail;
        Button _sentRequestBtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ForgotPasswordScreenLayout);

            //initial editext
            _emailUserLogin = FindViewById<TextView>(Resource.Id.EmailUserLogin);
            _emailUserForgot = FindViewById<EditText>(Resource.Id.EmailUserForgot);
            _recoveryPasswordUserEmail = FindViewById<EditText>(Resource.Id.EmailUserRecoveryPassword);
            _sentRequestBtn = FindViewById<Button>(Resource.Id.SendRequestBtn);

            //Request reset password
            _sentRequestBtn.Click += delegate
            {
                if (validateInput())
                {
                    alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                    alert.SetTitle("Email revovery sent successfully!");
                    alert.SetMessage("Please click RESEND EMAIL if you don't receive email recovery password. \n Or dismiss this message.");
                   
                    alert.SetPositiveButton(Constants.RESEND_EMAIL, (senderAlert, args) =>
                    {
                        Toast.MakeText(this, "Resend email button click", ToastLength.Short).Show();
                        return;
                    });
                    alert.SetNegativeButton(Constants.DISMISS, (senderAlert, args) =>
                    {
                        Toast.MakeText(this, "Dismiss button click", ToastLength.Short).Show();
                        return;
                    });
                    alert.Create().Show();
                }
                else Toast.MakeText(this, Constants.FIELD_INPUT_INCORRECT, ToastLength.Short).Show();

            };
            //get Extra from Login request
            string emailRecovery = Intent.GetStringExtra(Constants.VALID_EMAIL_EXTRA) ?? Constants.BLANK;
            if (!Constants.BLANK.Equals(emailRecovery))
            {
                _emailUserLogin.Text = emailRecovery;
                _emailUserForgot.Text = _emailUserLogin.Text;
                _emailUserForgot.Visibility = Android.Views.ViewStates.Gone;
                _recoveryPasswordUserEmail.RequestFocus();
            }
            else
            {
                _emailUserLogin.Visibility = Android.Views.ViewStates.Invisible;    
                _emailUserForgot.Visibility = Android.Views.ViewStates.Visible;
                _emailUserForgot.RequestFocus();
            }
        }

        //Check valid Input
        public bool validateInput()
        {
            bool valid = true;
            ICharSequence logEmailError = new Java.Lang.String(Constants.EMAIL_INVALID);
            ICharSequence logRecoveryEmailError = new Java.Lang.String(Constants.RECOVERY_EMAIL_INVALID);
            Android.Graphics.Drawables.Drawable resourceDrawable = Resources.GetDrawable(Resource.Drawable.error);

            if (!CommonUtils.isValidEmailField(_recoveryPasswordUserEmail.Text) || Constants.BLANK.Equals(_recoveryPasswordUserEmail.Text))
            {
                _recoveryPasswordUserEmail.SetError(logRecoveryEmailError, resourceDrawable);
                CommonUtils.alertMessageBox(alert, this, "Email recovery invalid", "Request input User's email recovery password correctly.", Constants.OK, _recoveryPasswordUserEmail);
                valid = false;
            }
            else if (!CommonUtils.isValidEmailField(_emailUserForgot.Text) || Constants.BLANK.Equals(_emailUserForgot.Text))
            {
                _emailUserForgot.SetError(logEmailError, resourceDrawable);
                CommonUtils.alertMessageBox(alert, this, "Email invalid", "Request input User's email correctly.", Constants.OK, _emailUserForgot);
                valid = false;
            }
            else
            {
                _emailUserLogin.Text = _emailUserForgot.Text;
                if (_emailUserLogin.Text.Equals(Constants.EMAIL_VALID) && _recoveryPasswordUserEmail.Text.Equals(Constants.EMAIL_RESET_PASSWORD)) 
                    valid = true;
                else
                {
                    // email not found in DB request user register account
                    alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                    alert.SetTitle("Email recovery invalid");
                    alert.SetMessage("Did you forget your email recovery password?");
                    alert.SetPositiveButton(Constants.YES, (senderAlert, args) =>
                    {
                        Intent intent = new Intent(this, typeof(LoginScreenActivity));
                        intent.PutExtra(Constants.VALID_EMAIL_EXTRA,_emailUserLogin.Text);
                        base.StartActivity(intent);
                        _emailUserLogin.RequestFocus();
                        return;
                    });
                    alert.SetNegativeButton(Constants.NO, (senderAlert, args) =>
                    {
                        _emailUserLogin.RequestFocus();
                        return;
                    });
                    alert.Create().Show();
                    valid = false;
                }
            }
            return valid;
        }
    }
}