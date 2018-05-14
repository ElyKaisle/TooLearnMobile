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
            load_server();
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

            var connect = FindViewById<Button>(Resource.Id.button2);
            connect.Click += ConnectActivity;

        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var servername = FindViewById<EditText>(Resource.Id.editText1);
            Spinner spinner = (Spinner)sender;
            var servers = spinner.GetItemAtPosition(e.Position);
            servername.Text = servers.ToString();
        }

        private void load_server()
        {
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            DataTable table = instance.GetDataSources();
            foreach (DataRow server in table.Rows)
            {
                var serverlist = (server[table.Columns["ServerName"]]);
                String[] values = { serverlist.ToString() };
                ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, values);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                RunOnUiThread(() => spinner.Adapter = adapter);
            }
            
        }
        
        public void ConnectActivity(object sender, EventArgs e)
        {
            try
            {
                String DB, ID, Password;
                Object Source;
                var servername = FindViewById<EditText>(Resource.Id.editText1);
                Program.serverIP = servername.Text;

                
                try
                {
                    IPHostEntry host = Dns.GetHostEntry(servername.Text); //get the ServerIP
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
                

                if (servername.Text != null)
                {
                    Source = servername.Text + ",1433";
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