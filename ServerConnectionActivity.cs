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

using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Net;
using System.Net.Sockets;

namespace TooLearnAndroid
{
    [Activity(Label = "Server Connection", Theme = "@style/Theme.DesignDemo")]
    public class ServerConnectionActivity : Activity
    {
        string Role = MainActivity.Role;
        SqlConnection con;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.activity_serverconnection);
            var connect = FindViewById<Button>(Resource.Id.button2);
            connect.Click += ConnectActivity;

        }
        
        public void ConnectActivity(object sender, EventArgs e)
        {
            try
            {
                String DB, ID, Password;
                Object Source;
                var servername = FindViewById<EditText>(Resource.Id.editText1).Text;
                Program.serverIP = servername;

                
                try
                {
                    IPHostEntry host = Dns.GetHostEntry(servername); //get the ServerIP
                    foreach (IPAddress ip in host.AddressList)
                    {

                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                        {
                            Program.serverIP = ip.ToString();

                        }
                    }
                }//end try

                catch (SocketException ex)
                {

                    Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
                }
                

                if (servername != null)
                {
                    Source = servername + ",1433";
                    DB = "Toolearn";
                    ID = "Toolearn";
                    Password = "Toolearn";
                    con = new SqlConnection("Data Source='" + Source + "' ; Initial Catalog='" + DB + "'; User ID='" + ID + "';Password='" + Password + "'");
                    try
                    {

                        con.Open();
                        if (con.State == ConnectionState.Open)
                        {

                            con.Close();

                            Program.source = Source.ToString();
                            Program.db = DB;
                            Program.id = ID;
                            Program.password = Password;

                            if (Role == "Individual")
                            {
                                Intent intent = new Intent(this, typeof(SignInActivity));
                                StartActivity(intent);
                            }

                            else
                            {
                                Intent intent = new Intent(this, typeof(GroupSignInActivity));
                                StartActivity(intent);
                            }


                        }


                        else
                        {
                            Toast.MakeText(this, "Connection Failed", ToastLength.Short).Show();
                        }

                    }

                    catch (Exception ex)
                    {
                        Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
                    }


                }

                else
                {

                    Toast.MakeText(this, "No Server Selected", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }

        
    }
}