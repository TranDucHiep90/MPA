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
    [Activity(Label = "LoginScreen", Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    class LoginScreenActivity : AppCompatActivity
    {
        Android.Support.V7.App.AlertDialog.Builder alert;
        Button _loginButton;
        TextView _signupLink;
        EditText _emailUser;
        EditText _passwordUser;
        TextView _forgotPassword;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginScreenLayout);

            // Initial element
            _loginButton = FindViewById<Button>(Resource.Id.LoginBtn);
            _signupLink = FindViewById<TextView>(Resource.Id.LinkSignUp);
            _emailUser = FindViewById<EditText>(Resource.Id.EmailUser);
            _passwordUser = FindViewById<EditText>(Resource.Id.PasswordUser);
            _forgotPassword = FindViewById<TextView>(Resource.Id.ForgotPassword);
            //Log in button onclick
            _loginButton.Click += delegate
            {
                if (!validateInput())
                {
                    onLoginFailed();
                }
                else
                {
                    _loginButton.Enabled = false; 
                    //Check valid user on USER table, and wait util load successfully
                    if (checkAccountLogin(_emailUser.Text, _passwordUser.Text) == 1)
                    onLoginSuccess();
                    else if (checkAccountLogin(_emailUser.Text, _passwordUser.Text) == 0) // Redirect to reset password
                    {
                        Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                        alert.SetTitle("Password Incorrect");
                        alert.SetMessage("Did you forget your password?");
                        alert.SetPositiveButton(Constants.YES, (senderAlert, args) =>
                        {
                            _loginButton.Enabled = true;
                            Intent intent = new Intent(this, typeof(ForgotPasswordActivity));
                            intent.PutExtra(Constants.VALID_EMAIL_EXTRA, _emailUser.Text);
                            base.StartActivity(intent);
                        });
                        alert.SetNegativeButton(Constants.NO, (senderAlert, args) =>
                        {
                            _loginButton.Enabled = true;
                            return;
                        });
                        alert.Create().Show();
                    }
                    else
                    {
                        alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                        alert.SetTitle(Constants.NOTICE_EMAIL_PASSWORD);
                        alert.SetMessage("Would you want to create new account?");
                        alert.SetPositiveButton(Constants.OK, (senderAlert, args) =>
                        {
                            _loginButton.Enabled = true;
                            Intent intent = new Intent(this, typeof(RegisterScreenActivity));
                            intent.PutExtra(Constants.VALID_EMAIL_EXTRA, _emailUser.Text);
                            base.StartActivity(intent);     
                        });
                        alert.SetNeutralButton(Constants.MAIN_SCREEN, (senderAlert, args) =>
                        {
                            _loginButton.Enabled = true;
                            base.StartActivity(new Intent(this, typeof(MainActivity)));
                            return;
                        });
                        alert.SetNegativeButton(Constants.BACK, (senderAlert, args) =>
                        {
                            _loginButton.Enabled = true;
                            return;
                        });
                        alert.Create().Show();
                    }
                }
            };

            //_signupLink action
            _signupLink.Click += delegate
            {
                Intent intent = new Intent(this, typeof(RegisterScreenActivity));
                base.StartActivity(intent);
            };
            //Forgot password action
            _forgotPassword.Click += delegate
            {
                //redirect to Forgot password activity
                Intent intent = new Intent(this, typeof(ForgotPasswordActivity));
                intent.PutExtra(Constants.VALID_EMAIL_EXTRA, _emailUser.Text);
                base.StartActivity(intent);
            };

            //Extra form register activity
            string emailRegisterSuccessed = Intent.GetStringExtra(Constants.VALID_EMAIL_EXTRA) ?? Constants.BLANK;
            if (!Constants.BLANK.Equals(emailRegisterSuccessed) && emailRegisterSuccessed.Length > 0)
            {
                _emailUser.Text = emailRegisterSuccessed;
                _passwordUser.RequestFocus();
            } else
            {
                return;
            }
        }

        private int checkAccountLogin(string email, string password)
        {
            if (email.Equals(Constants.EMAIL_VALID) && password.Equals(Constants.PASSWORD_VALID))
                return 1;
            else if (email.Equals(Constants.EMAIL_VALID) && !password.Equals(Constants.PASSWORD_VALID)) // Check if account match with account in DB
                return 0;
            else
                return -1;
        }

        public void onLoginFailed()
        {
            Toast.MakeText(this, Constants.FIELD_INPUT_INCORRECT, ToastLength.Short).Show();
            _loginButton.Enabled = true;
        }

        public void onLoginSuccess()
        {
            _loginButton.Enabled = true;
            Toast.MakeText(this, Constants.LOGIN_SUCCESSED, ToastLength.Short).Show();
            //Intent to hall screen
            base.StartActivity(new Intent(this, typeof(HallScreenActivity)));
            //Send info user to screen
        }

        public bool validateInput()
        {
            bool valid = true;
            ICharSequence logEmailError = new Java.Lang.String(Constants.EMAIL_INVALID);
            ICharSequence logPasswordError = new Java.Lang.String(Constants.PASSWORD_INVALID);
            Android.Graphics.Drawables.Drawable resourceDrawable = Resources.GetDrawable(Resource.Drawable.error);
            if (!CommonUtils.isValidEmailField(_emailUser.Text) || Constants.BLANK.Equals(_emailUser.Text))
            {
                _emailUser.SetError(logEmailError, resourceDrawable);
                CommonUtils.alertMessageBox(alert, this, "Email invalid", "Request input User's email correctly.", Constants.OK, _emailUser);
                valid = false;
            }
            else if (!CommonUtils.isValidPasswordField(_passwordUser.Text) || Constants.BLANK.Equals(_passwordUser.Text))
            {
                _passwordUser.SetError(logPasswordError, resourceDrawable);
                CommonUtils.alertMessageBox(alert, this, "Password invalid", "Request input User's password correctly.", Constants.OK, _passwordUser);
                valid = false;
            }
            return valid;
        }
    }
}