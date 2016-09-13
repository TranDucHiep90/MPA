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
using Java.Lang;
using MPA.Models;

namespace MPA
{
    public class ViewHolder : Java.Lang.Object
    {
        public TextView NameFacebook { get; set; }
        public TextView EmailFacebook { get; set; }
        public TextView IdFacebook { get; set; }
    }

    public class ListViewAdapter:BaseAdapter
    {
        private Activity activity;
        private List<FacebookAccount> listFacebookAccount;
        public ListViewAdapter(Activity activity, List<FacebookAccount> listFacebookAccount)
        {
            this.activity = activity;
            this.listFacebookAccount = listFacebookAccount;
        }

        public override int Count
        {
            get
            {
                return listFacebookAccount.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return listFacebookAccount[position].IdAccount;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.ListViewSingleRow, parent, false);

            TextView txtName = view.FindViewById<TextView>(Resource.Id.NameFacebook);
            TextView txtEmail = view.FindViewById<TextView>(Resource.Id.EmailFacebook);
            TextView txtId = view.FindViewById<TextView>(Resource.Id.IdFacebook);

            txtName.Text = listFacebookAccount[position].NameAccount;
            txtEmail.Text = listFacebookAccount[position].EmailAccount;
            txtId.Text =Constants.BLANK+listFacebookAccount[position].IdAccount;
            return view;
        }
    }
}