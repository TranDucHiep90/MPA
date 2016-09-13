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

namespace MPA
{
    class CommonUtils
    {
        public static Boolean isValidEmail(String email)
        {
            return Android.Util.Patterns.EmailAddress.Matcher(email).Matches();
        }
        public static Boolean isValidNameField(String name)
        {
            return name.Length >= 3 ? true : false;
        }
        public static Boolean isValidEmailField(String email)
        {
            return isValidEmail(email) ? true : false;
        }
        public static Boolean isValidPasswordField(String password)
        {
            return password.Length >= 4 && password.Length <= 12 ? true : false;
        }

        public static void alertMessageBox(Android.Support.V7.App.AlertDialog.Builder alert, Activity activity, string titleAlert, string message, string buttonPositive, EditText forcusEditText)
        {
            alert = new Android.Support.V7.App.AlertDialog.Builder(activity);
            alert.SetTitle(titleAlert);
            alert.SetMessage(message);
            alert.SetPositiveButton(buttonPositive, (senderAlert, args) =>
            {
                if(forcusEditText != null)
                forcusEditText.RequestFocus();
                return;
            });
            alert.Create().Show();
        }

        internal static void alertMessageBox(object alert)
        {
            throw new NotImplementedException();
        }
    }
}