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
        public static string NameFREE;
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
            var screenname = FindViewById<EditText>(Resource.Id.editText2);

            if (ipadd.Text == null || ipadd.Text == "" || screenname.Text == null || screenname.Text == "")
            {
                Toast.MakeText(this, "Enter Host IP Address! and Provide a Screen Name!", ToastLength.Long).Show();
            }

            else
            {


                try
                {
                    
                    Program.serverIP = ipadd.Text;
                    //Program.source = ipadd.Text + ",13000";
                   // Program.db = "Toolearn";
                   // Program.id = "Toolearn";
                   // Program.password = "Toolearn";
                    NameFREE = screenname.Text;
                    StartActivity(typeof(LobbyActivity));
                    /*
                    IPHostEntry host = Dns.GetHostEntry(ipadd.Text);
                    foreach (IPAddress ip in host.AddressList)
                    {

                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                        {
                            Program.serverIP = ip.ToString();
                            

                        }
                        
                    }
                    NameFREE = screenname.Text;
                    StartActivity(typeof(LobbyGuestActivity));
                    */
                }//end try

                catch
                {
                    Toast.MakeText(this, "Connection Failed", ToastLength.Short).Show();

                }


            }
        }
    }
}