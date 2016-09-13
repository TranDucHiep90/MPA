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
using MPA.Models;
using MPA.DBHelper;

namespace MPA
{
    [Activity(Label = "HallScreenActivity", Theme = "@style/MyTheme", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class HallScreenActivity : ActionBarActivity
    {
        Android.Support.V7.App.AlertDialog.Builder alert;
        private SupportToolBar _mToolBar;
        private MyActionBarDrawerToggle _mDrawerToggle;
        private DrawerLayout _mDrawerLayout;
        private ListView _mLeftDrawer;
        private List<string> _mLeftDataSet;
        private ArrayAdapter _mLeftAdapter;
        private ListView _mListViewData;
        private ImageView _mAddImage;
        //private TextView _mTitleAccountList;
        private int openDrawerTitle = Resource.String.OpenedDrawerTitle;
        private int closedDrawerTitle = Resource.String.ClosedDrawerTitle;

        //Database
        DataBase db;
        List<FacebookAccount> listFacebookAcc = new List<FacebookAccount>();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.HallScreenLayout);
            //Create db
            db = new DataBase();
            db.createDataBase();
            // Create your application here
            _mToolBar = FindViewById<SupportToolBar>(Resource.Id.ToolBar);
            _mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.DrawerLayout);
            _mLeftDrawer = FindViewById<ListView>(Resource.Id.LeftDrawer);
            _mListViewData = FindViewById<ListView>(Resource.Id.ListData);
            _mAddImage = FindViewById<ImageView>(Resource.Id.AddButtonImageHallScreen);
          //  _mTitleAccountList = FindViewById<TextView>(Resource.Id.AccountScreenTitle);

            _mLeftDrawer.Tag = 0;
            this.SetSupportActionBar(_mToolBar);
           
            _mDrawerToggle = new MyActionBarDrawerToggle(
                this, 
                _mDrawerLayout, 
                this.openDrawerTitle,
                this.closedDrawerTitle
                );
            _mDrawerLayout.SetDrawerListener(_mDrawerToggle);

            //Set data for list left menu
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
            _mLeftDataSet.Add("Log out");
            _mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemActivated1, _mLeftDataSet);
            _mLeftDrawer.Adapter = _mLeftAdapter;
            
            //Onitem click
            _mLeftDrawer.ItemClick += OnItemClick;

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
            {
                //This is the first the time the activity is ran
                SupportActionBar.SetTitle(Resource.String.ClosedDrawerTitle);
           }
            //Add icon display and hiden
            //AddIconButtonActionHallScreen();

            _mAddImage.Click += delegate
            {
                Toast.MakeText(this, "Add Item for Youtube account", ToastLength.Short).Show();
            };
        }

        private void AddIconButtonActionHallScreen()
        {
            //_mTitleAccountList.Visibility = ViewStates.Invisible;
        }

        private void OnItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            _mDrawerLayout.CloseDrawer(_mLeftDrawer);
            var position = e.Position;//int
                switch (_mLeftDataSet[position])
            {
                case "Home":
                    this.ChooseHomeAction();
                    break;
                case "Your account":
                    this.ChooseYourAccountAction();
                    break;
                case "Facebook Account":
                  //  SupportActionBar.SetTitle(Resource.String.FacebookAccountTitle);
                    this.ChooseFacebookAction();
                    break;
                case "Youtube Account":
                    this.ChooseYoutubeAction();
                    break;
                case "Google Account":
                    this.ChooseGoogleAction();
                    break;
                case "Ebank Account":
                    this.ChooseEbankAction();
                    break;
                case "Setting":
                    this.ChooseSettingAction();
                    break;
                case "About":
                    this.ChooseAboutAction();
                    break;
                case "Help":
                    this.ChooseHelpAction();
                    break;
                case "Log out":
                    this.LogoutAction();
                    break;
                default:
                    Toast.MakeText(this, "Not found element clicked", ToastLength.Short).Show();
                    break;
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    _mDrawerToggle.OnOptionsItemSelected(item);
                    return true;
                case Resource.Id.action_refresh:
                    Toast.MakeText(this, "Hiep refresh", ToastLength.Short).Show();
                    return true;
                case Resource.Id.action_add:
                    //HiepTODO: Should seprate for each type of account when click this add button
                    fnShowCustomAlertDialogFacebookAdd();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        //Home is chosen
        private void ChooseHomeAction()
        {
            Toast.MakeText(this, "Home has been clicked", ToastLength.Short).Show();
            //set back to main
        }
        //Your account is chosen
        private void ChooseYourAccountAction()
        {
            Toast.MakeText(this, "Your account has been clicked", ToastLength.Short).Show();
        }
        //Facebook account is chosen
        private void ChooseFacebookAction()
        {
            Toast.MakeText(this, "Facebook has been clicked", ToastLength.Short).Show();
            //hiep TODO
            loadDataFacebook();
        }

        private void loadDataFacebook()
        {
            //Get data Facebook
            listFacebookAcc = db.GetAllListFacebookAccount();
            var adapterFacebook = new ListViewAdapter(this, listFacebookAcc);
            _mListViewData.Adapter = adapterFacebook;
            //Each row has been clicked
            _mListViewData.ItemClick += (sender, args) =>
            {
                fnShowCustomAlertDialogFacebookUpdate(listFacebookAcc[args.Position]);
            };
            //Handle Add image Icon
            if (listFacebookAcc.Count > 0 && listFacebookAcc != null)
                _mAddImage.Visibility = ViewStates.Invisible;
            else
                _mAddImage.Visibility = ViewStates.Visible;
            _mAddImage.Click += delegate
            {
                //fnShowCustomAlertDialogFacebookAdd();
                Toast.MakeText(this, "Add Item for Facebook account", ToastLength.Short).Show();
            };
        }

        //Youtube account is chosen
        private void ChooseYoutubeAction()
        {
            Toast.MakeText(this, "Youtube has been clicked", ToastLength.Short).Show();
            //hiep TODO
            //data Youtube
            List<string> _mYoutubekData = new List<string>();
            //_mYoutubekData.Add("Item youtube 1");
            //_mYoutubekData.Add("Item youtube 2");

            _mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemActivated1, _mYoutubekData);
            _mListViewData.Adapter = _mLeftAdapter;

            //Handle Add image Icon
            if (_mYoutubekData.Count > 0 && _mYoutubekData != null)
                _mAddImage.Visibility = ViewStates.Invisible;
            else
            {
                _mAddImage.Visibility = ViewStates.Visible;
            }
            _mAddImage.Click += delegate
            {
                Toast.MakeText(this, "Add Item for Youtube account", ToastLength.Short).Show();
            };
        }
        //Google account is chosen
        private void ChooseGoogleAction()
        {
            Toast.MakeText(this, "Google has been clicked", ToastLength.Short).Show();
        }
        //Ebank account is chosen
        private void ChooseEbankAction()
        {
            Toast.MakeText(this, "Ebank has been clicked", ToastLength.Short).Show();
        }
        //setting is chosen
        private void ChooseSettingAction()
        {
            Toast.MakeText(this, "Setting has been clicked", ToastLength.Short).Show();
        }
        //About is chosen
        private void ChooseAboutAction()
        {
            CommonUtils.alertMessageBox(alert, this, "About", "The application is developed by HiepTD3 \n v1.0.0.", Constants.OK, null);
        }
        //Help is chosen
        private void ChooseHelpAction()
        {
            Toast.MakeText(this, "Help has been clicked", ToastLength.Short).Show();
        }
        //Logout account action
        private void LogoutAction()
        {
            base.StartActivity(new Intent(this, typeof(LoginScreenActivity)));
            Finish();
        }
        // Override menu
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        //Add facebook 
        private void fnShowCustomAlertDialogFacebookAdd()
        {
            //Inflate layout
            LayoutInflater layoutInflater = LayoutInflater.From(this);
            View view = layoutInflater.Inflate(Resource.Layout.DetailFacebookAlert, null);

            EditText NameFaceAcc = view.FindViewById<EditText>(Resource.Id.FacebookUserNameAdd);
            EditText EmailFaceAcc = view.FindViewById<EditText>(Resource.Id.FacebookEmailAdd);
            EditText PasswordFaceAcc = view.FindViewById<EditText>(Resource.Id.FacebookPasswordAdd);
            EditText DescFaceAcc = view.FindViewById<EditText>(Resource.Id.FacebookDescAdd);

            alert = new Android.Support.V7.App.AlertDialog.Builder(this);

            // Android.Support.V7.App.AlertDialog builder = new Android.Support.V7.App.AlertDialog.Builder(this).Create();
            alert.SetView(view);
            alert.SetPositiveButton(Constants.ADD, (senderAlert, args) =>
            {
                FacebookAccount faceAcc = new FacebookAccount()
                {
                    NameAccount = NameFaceAcc.Text,
                    EmailAccount = EmailFaceAcc.Text,
                    PasswordAccount = PasswordFaceAcc.Text,
                    DescAccount = DescFaceAcc.Text
                };
                db.AddAnFacebookAccount(faceAcc);
                loadDataFacebook();
                Toast.MakeText(this, "Addition success!", ToastLength.Short).Show();
                this.alert.Dispose();
            });
            alert.SetNegativeButton(Constants.CANCEL, (senderAlert, args) =>
            {
                Toast.MakeText(this, "Cancel", ToastLength.Short).Show();
                this.alert.Dispose();
            });
            alert.Create().Show();
        }
        //Add facebook 
        private void fnShowCustomAlertDialogFacebookUpdate(FacebookAccount facebook)
        {
            //Inflate layout
            LayoutInflater layoutInflater = LayoutInflater.From(this);
            View view = layoutInflater.Inflate(Resource.Layout.DetailFacebookAlert, null);
            EditText NameFaceAcc = view.FindViewById<EditText>(Resource.Id.FacebookUserNameAdd);
            EditText EmailFaceAcc = view.FindViewById<EditText>(Resource.Id.FacebookEmailAdd);
            EditText PasswordFaceAcc = view.FindViewById<EditText>(Resource.Id.FacebookPasswordAdd);
            EditText DescFaceAcc = view.FindViewById<EditText>(Resource.Id.FacebookDescAdd);

            //setter
            NameFaceAcc.Text = facebook.NameAccount;
            EmailFaceAcc.Text = facebook.EmailAccount;
            PasswordFaceAcc.Text = facebook.PasswordAccount;
            DescFaceAcc.Text = facebook.DescAccount;

            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);

            // Android.Support.V7.App.AlertDialog builder = new Android.Support.V7.App.AlertDialog.Builder(this).Create();
            alert.SetView(view);
            alert.SetPositiveButton(Constants.UPDATE, (senderAlert, args) =>
            {
                FacebookAccount faceAcc = new FacebookAccount()
                {
                    IdAccount = facebook.IdAccount,
                    NameAccount = NameFaceAcc.Text,
                    EmailAccount = EmailFaceAcc.Text,
                    PasswordAccount = PasswordFaceAcc.Text,
                    DescAccount = DescFaceAcc.Text
                };
                db.UpdateAnFacebookAccount(faceAcc);
                loadDataFacebook();
                alert.Dispose();
                Toast.MakeText(this, "Update successfully!", ToastLength.Short).Show();
            });
            alert.SetNegativeButton(Constants.CANCEL, (senderAlert, args) =>
            {
                Toast.MakeText(this, "Cancel", ToastLength.Short).Show();
                alert.Dispose();
            });
            alert.SetNeutralButton(Constants.DELETE, (senderAlert, args) =>
            {
                alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                alert.SetTitle("Confirm delete");
                alert.SetMessage("Do you want to delete this account?");
                alert.SetPositiveButton(Constants.YES, (s, a) =>
                {
                    db.DeleteTableFacebookAccount(facebook);
                    loadDataFacebook();
                    Toast.MakeText(this, "Deleted", ToastLength.Short).Show();
                    alert.Dispose();
                });
                alert.SetNegativeButton(Constants.NO, (s, a) =>
                {
                    Toast.MakeText(this, "No button clicked", ToastLength.Short).Show();
                    alert.Dispose();
                    
                });
                alert.Create().Show();
            });
            alert.Create().Show();
        }
    }
}