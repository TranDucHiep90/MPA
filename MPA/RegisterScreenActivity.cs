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
    [Activity(Label = "RegisterScreenActivity", Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class RegisterScreenActivity : AppCompatActivity
    {
        Android.Support.V7.App.AlertDialog.Builder alert;
        Button _registerButton;
        TextView _LoginLink;
        EditText _nameUserRegister;
        EditText _emailUserRegister;
        EditText _emailRecoveryPasswordRegister;
        EditText _passwordUserRegister;
        EditText _rePasswordUserRegister;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegisterScreenLayout);
            // Initial Value
            _LoginLink = FindViewById<TextView>(Resource.Id.LinkLogin);
            _registerButton = FindViewById<Button>(Resource.Id.RegisterBtn);
            _nameUserRegister = FindViewById<EditText>(Resource.Id.NameUserRegister);
            _emailUserRegister = FindViewById<EditText>(Resource.Id.EmailUserRegister);
            _emailRecoveryPasswordRegister = FindViewById<EditText>(Resource.Id.EmailRecoveryPasswordRegister);
            _passwordUserRegister = FindViewById<EditText>(Resource.Id.PasswordUserRegister);
            _rePasswordUserRegister = FindViewById<EditText>(Resource.Id.RetypePasswordUserRegister);

            //Button create account clicked
            _registerButton.Click += delegate
            {
                if (!validateInput(_nameUserRegister, _emailUserRegister,_emailRecoveryPasswordRegister, _passwordUserRegister, _rePasswordUserRegister))
                    onRegisterFailed();
                else
                    registerSuccess();
            };

            //_LoginLink  click
            _LoginLink.Click += delegate
            {
                Intent intent = new Intent(this, typeof(LoginScreenActivity));
                base.StartActivity(intent);
            };

            //get Extra from Login request
            string emailRegisterSuccessed = Intent.GetStringExtra(Constants.VALID_EMAIL_EXTRA) ?? Constants.BLANK;
            if (!Constants.BLANK.Equals(emailRegisterSuccessed) && emailRegisterSuccessed.Length > 0)
            {
                _emailUserRegister.Text = emailRegisterSuccessed;
                _nameUserRegister.RequestFocusFromTouch();
            }
            else
                return;
        }

        public void registerSuccess()
        {
            Toast.MakeText(this, Constants.REGISTER_SUCCESSED, ToastLength.Short).Show();
            alert = new Android.Support.V7.App.AlertDialog.Builder(this);
            alert.SetTitle("Create account successfully!");
            alert.SetMessage(Constants.CHOICE_ACTIVITY);
            alert.SetPositiveButton(Constants.YES, (senderAlert, args) =>
            {
                Intent intent = new Intent(this, typeof(LoginScreenActivity));
                intent.PutExtra(Constants.VALID_EMAIL_EXTRA, _emailUserRegister.Text);
                base.StartActivity(intent);
            });
            alert.SetNegativeButton(Constants.NO, (senderAlert, args) =>
            {
                base.StartActivity(new Intent(this, typeof(MainActivity)));
            });
            alert.Create().Show();
        }

        public bool validateInput(EditText _nameUser, EditText _emailUser, EditText _emailRecovery, EditText _passwordUser, EditText _rePasswordUser)
        {
            bool valid = true;
            ICharSequence logNameError = new Java.Lang.String(Constants.NAME_INVALID);
            ICharSequence logEmailError = new Java.Lang.String(Constants.EMAIL_INVALID);
            ICharSequence logPasswordError = new Java.Lang.String(Constants.PASSWORD_INVALID);
            ICharSequence logRePasswordError = new Java.Lang.String(Constants.REPASSWORD_INVALID);
            Android.Graphics.Drawables.Drawable resourceDrawable =  Resources.GetDrawable(Resource.Drawable.error);
            if (!CommonUtils.isValidNameField(_nameUser.Text) || Constants.BLANK.Equals(_nameUserRegister.Text))
            {
                _nameUser.SetError(logNameError, resourceDrawable);
                CommonUtils.alertMessageBox(alert, this, "Username invalid", "Request input User's name correctly.", Constants.OK, _nameUserRegister);
                valid = false;
            }
            else if (!CommonUtils.isValidEmailField(_emailUser.Text) || Constants.BLANK.Equals(_emailUserRegister.Text))
            {
                _emailUser.SetError(logEmailError, resourceDrawable);
                CommonUtils.alertMessageBox(alert, this, "Email invalid", "Request input User's email correctly.", Constants.OK, _emailUserRegister);
                valid = false;
            }
            else if (!CommonUtils.isValidEmailField(_emailRecovery.Text) || Constants.BLANK.Equals(_emailRecoveryPasswordRegister.Text))
            {
                _emailRecovery.SetError(logEmailError, resourceDrawable);
                CommonUtils.alertMessageBox(alert, this, "Email recovery invalid", "Request input User's email recovery password correctly.", Constants.OK, _emailRecoveryPasswordRegister);
                valid = false;
            }
            else if (!CommonUtils.isValidPasswordField(_passwordUser.Text) || Constants.BLANK.Equals(_passwordUserRegister.Text))
            {
                _passwordUser.SetError(logPasswordError, resourceDrawable);
                CommonUtils.alertMessageBox(alert, this, "Password invalid", "Request input User's password correctly.", Constants.OK, _passwordUserRegister);
                valid = false;
            }
            else if (!CommonUtils.isValidPasswordField(_rePasswordUser.Text) || Constants.BLANK.Equals(_rePasswordUserRegister.Text))
            {
                _rePasswordUser.SetError(logPasswordError, resourceDrawable);
                CommonUtils.alertMessageBox(alert, this, "Re-Password invalid", "Request input User's re-Password correctly.", Constants.OK, _rePasswordUserRegister);
                valid = false;
            }
            else if (!_passwordUser.Text.SequenceEqual(_rePasswordUser.Text) )
            {
                _rePasswordUser.SetError(logRePasswordError, resourceDrawable);
                CommonUtils.alertMessageBox(alert, this, "Re-Password invalid", "Re-password not matched", Constants.OK, _rePasswordUserRegister);
                valid = false;
            }

            return valid;
        }
        public void onRegisterFailed()
        {
            Toast.MakeText(this, Constants.FIELD_INPUT_INCORRECT, ToastLength.Short).Show();
            _registerButton.Enabled = true;
        }
    }
}