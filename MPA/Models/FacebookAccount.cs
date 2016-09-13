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
using SQLite;

namespace MPA.Models
{
   public class FacebookAccount
    {
        public int UserId { get; set; }
        [PrimaryKey, AutoIncrement]
        public int IdAccount { get; set; }
        [MaxLength(8)]
        public string NameAccount { get; set; }
        public string EmailAccount { get; set; }
        [MaxLength(8)]
        public string PasswordAccount { get; set; }
        public string DescAccount { get; set; }

    }
}