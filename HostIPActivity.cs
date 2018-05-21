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

using System.Net;
using System.Net.Sockets;

namespace TooLearnAndroid
{
    [Activity(Label = "Public - Server Connection", Theme = "@style/Theme.DesignDemo")]
    public class HostIPActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_hostIP);
            var join = FindViewById<Button>(Resource.Id.button1);
            join.Click += JoinServerActivity;
            // Create your application here
        }

        public void JoinServerActivity(object sender, EventArgs e)
        {
            var ipadd = FindViewById<EditText>(Resource.Id.editText1);

            if (ipadd.Text == null || ipadd.Text == "")
            {
                Toast.MakeText(this, "Enter Host IP Address!", ToastLength.Long).Show();
            }

            else
            {
                try
                {
                    IPHostEntry host = Dns.GetHostEntry(ipadd.Text);
                    foreach (IPAddress ip in host.AddressList)
                    {

                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                        {
                            Program.serverIP = ip.ToString();
                            

                        }
                        StartActivity(typeof(LobbyActivity));

                    }
                }//end try

                catch
                {
                    Toast.MakeText(this, "Connection Failed", ToastLength.Short).Show();

                }


            }
        }
    }
}