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
using Android.Support.V4.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;

namespace TooLearnAndroid
{
    [Activity(Label = "Main Menu", Theme = "@style/Theme.DesignDemo")]
    public class MainmenuActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_mainmenu);
            UsernameOnLoad();
            
            DrawerLayout drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            // Create ActionBarDrawerToggle button and add it to the toolbar  
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);
            drawerLayout.AddDrawerListener(drawerToggle);
            drawerToggle.SyncState();
            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            setupDrawerContent(navigationView); //Calling Function 

            navigationView.NavigationItemSelected += HomeNavigationView_NavigationItemSelected;

            
        }

        public void UsernameOnLoad()
        {
            var username = FindViewById<TextView>(Resource.Id.navheader_username);
        }
        private void HomeNavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            var menuItem = e.MenuItem;
            menuItem.SetChecked(!menuItem.IsChecked);
            switch (menuItem.ItemId)
            {
                case Resource.Id.nav_myaccount:
                    MyAccountFragment fragment = new MyAccountFragment();
                    FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
                    fragmentTx.Replace(Resource.Id.fragment_container, fragment);
                    fragmentTx.Commit(); break;

                case Resource.Id.nav_logout:
                    LogoutFragment fragment1 = new LogoutFragment();
                    FragmentTransaction fragmentTx1 = this.FragmentManager.BeginTransaction();
                    fragmentTx1.Replace(Resource.Id.fragment_container, fragment1);
                    fragmentTx1.Commit(); break;

                case Resource.Id.nav_joinquiz:
                    JoinQuizFragment fragment2 = new JoinQuizFragment();
                    FragmentTransaction fragmentTx2 = this.FragmentManager.BeginTransaction();
                    fragmentTx2.Replace(Resource.Id.fragment_container, fragment2);
                    fragmentTx2.Commit(); break;

                case Resource.Id.nav_settings:
                    SettingsFragment fragment3 = new SettingsFragment();
                    FragmentTransaction fragmentTx3 = this.FragmentManager.BeginTransaction();
                    fragmentTx3.Replace(Resource.Id.fragment_container, fragment3);
                    fragmentTx3.Commit(); break;

                case Resource.Id.nav_manual:
                    ManualFragment fragment4 = new ManualFragment();
                    FragmentTransaction fragmentTx4 = this.FragmentManager.BeginTransaction();
                    fragmentTx4.Replace(Resource.Id.fragment_container, fragment4);
                    fragmentTx4.Commit(); break;

                case Resource.Id.nav_about:
                    AboutFragment fragment5 = new AboutFragment();
                    FragmentTransaction fragmentTx5 = this.FragmentManager.BeginTransaction();
                    fragmentTx5.Replace(Resource.Id.fragment_container, fragment5);
                    fragmentTx5.Commit(); break;
            }
        } 

        void setupDrawerContent(NavigationView navigationView)
        {
            navigationView.NavigationItemSelected += (sender, e) =>
            {
                DrawerLayout drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                e.MenuItem.SetChecked(true);
                drawerLayout.CloseDrawers();
            };
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
          NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.InflateMenu(Resource.Menu.nav_menu); //Navigation Drawer Layout Menu Creation  
            return true;
        }
    }
}