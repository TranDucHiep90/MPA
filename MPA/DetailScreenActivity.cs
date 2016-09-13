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
using SupportToolBar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V4.Widget;
using Android.Content.PM;

namespace MPA
{
    [Activity(Label = "DetailScreenActivity", Theme = "@style/MyTheme", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class DetailScreenActivity : ActionBarActivity
    {
        private SupportToolBar _mToolBar;
        private MyActionBarDrawerToggle _mDrawerToggle;
        private DrawerLayout _mDrawerLayout;
        private ListView _mLeftDrawer;

        private List<string> _mLeftDataSet;
        private ArrayAdapter _mLeftAdapter;
        private int openDrawerTitle = Resource.String.OpenedDrawerTitle;
        private int closedDrawerTitle = Resource.String.ClosedDrawerTitle;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DetailScreenLayout);
            // Create your application here
            _mToolBar = FindViewById<SupportToolBar>(Resource.Id.ToolBarDetail);
            _mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.DrawerLayoutDetail);
            _mLeftDrawer = FindViewById<ListView>(Resource.Id.LeftDrawerDetail);
            _mLeftDrawer.Tag = 0;
            this.SetSupportActionBar(_mToolBar);

            _mDrawerToggle = new MyActionBarDrawerToggle(
                this,
                _mDrawerLayout,
                this.openDrawerTitle,
                this.closedDrawerTitle
                );
            _mDrawerLayout.SetDrawerListener(_mDrawerToggle);

            //Set data for list
            _mLeftDataSet = new List<string>();
            _mLeftDataSet.Add("Home");
            _mLeftDataSet.Add("Your account");
            _mLeftDataSet.Add("Facebook Account");
            _mLeftDataSet.Add("Youtube Account");
            _mLeftDataSet.Add("Google Account");
            _mLeftDataSet.Add("Ebank Account");
            _mLeftDataSet.Add("Setting");
            _mLeftDataSet.Add("About");
            _mLeftDataSet.Add("Help");
            _mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, _mLeftDataSet);
            _mLeftDrawer.Adapter = _mLeftAdapter;
            //header view
               

            
            //header view
            this.SupportActionBar.SetHomeButtonEnabled(true);
            this.SupportActionBar.SetDisplayShowTitleEnabled(true);
            this.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            _mDrawerToggle.SyncState();

            if (bundle != null)
            {
                if (bundle.GetString("DrawerState").Equals("Opened"))
                    SupportActionBar.SetTitle(this.openDrawerTitle);
                else
                    SupportActionBar.SetTitle(this.closedDrawerTitle);
            }
            else
                SupportActionBar.SetTitle(this.closedDrawerTitle);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            _mDrawerToggle.OnOptionsItemSelected(item);
            return base.OnOptionsItemSelected(item);
        }
  
     
    }
}
