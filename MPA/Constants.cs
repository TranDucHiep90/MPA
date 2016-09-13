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
   public static class Constants
    {
        //String Util
        public static string BLANK = "";
        public static string OK = "OK";
        public static string YES = "YES";
        public static string NO = "NO";
        public static string BACK = "BACK";
        public static string CANCEL = "CANCEL";
        public static string DELETE = "DELETE";
        public static string UPDATE = "UPDATE";
        public static string ADD = "ADD";
        public static string DISMISS = "DISMISS";
        public static string RESEND_EMAIL = "RESEND EMAIL";
        public static string MAIN_SCREEN = "MAIN SCREEN";

        //Account valid
        public static string EMAIL_VALID = "mpa@gmail.com";
        public static string EMAIL_RESET_PASSWORD = "mpa@gmail.com";
        public static string PASSWORD_VALID = "admin";

        //Notice
        public static string LOGIN_SUCCESSED = "Login successed";
        public static string REGISTER_SUCCESSED = "Account created successfully";
        public static string LOGIN_FAIL = "Login failed";
        public static string FIELD_INPUT_INCORRECT = "Field input incorrect";
        public static string NOTICE_EMAIL_PASSWORD = "Invalid Email or password.";
        public static string NAME_INVALID = "Name inValid.\n Allowed input more than 3 character";
        public static string EMAIL_INVALID = "Email inValid.\n Example: email@extention.vn";
        public static string PASSWORD_INVALID = "Password inValid. \n Allowed input 4 to 12 characters";
        public static string REPASSWORD_INVALID = "Re-Password not matched.";
        public static string CHOICE_ACTIVITY = "Would you want to login ?";
        public static string VALID_EMAIL_EXTRA = "Email valid";
        public static string RECOVERY_EMAIL_INVALID = "Email recovery inValid.\n Example: email@extention.vn";

    }
}