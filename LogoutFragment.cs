using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace TooLearnAndroid
{
    public class LogoutFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.fragment_logout, container, false);
            var logout = view.FindViewById<Button>(Resource.Id.button1);
            logout.Click += LogOutActivity;
            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
        public void LogOutActivity(object sender, EventArgs e)
        {

            AlertDialog.Builder alertDialog = new AlertDialog.Builder(this.Activity);
            alertDialog.SetTitle("Logout");
            alertDialog.SetMessage("Are you sure?");
            alertDialog.SetPositiveButton("Ok", (senderAlert, args) =>
            {
                Intent intent = new Intent(this.Activity, typeof(SignInActivity));
                StartActivity(intent);
            });

            alertDialog.SetNegativeButton("Cancel", delegate
            {
                alertDialog.Dispose();
                Toast.MakeText(this.Activity, "Cancelled!", ToastLength.Short).Show();
            });
            Dialog dialog = alertDialog.Create();
            dialog.Show();
        }
    }
}