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
        SqlConnection con;
        Spinner spinner;
        ArrayAdapter<String> adapter;
        String[] servers = { };
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.activity_serverconnection);
            spinner = (Spinner)FindViewById<Spinner>(Resource.Id.spinner1);
            load_server();
            adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, servers);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(ConnectActivity);

            Button refresh_button = FindViewById<Button>(Resource.Id.button1);
            refresh_button.Click += RefreshActivity;
            Button connect_button = FindViewById<Button>(Resource.Id.button2);
            

        }

        private void load_server()
        {
            int x = 0;
            DataTable table = SqlDataSourceEnumerator.Instance.GetDataSources();
            foreach (DataRow server in table.Rows)
            {
                var query = server[table.Columns["ServerName"]].ToString();
                servers[x] = query;
                x++;
            }

        }
      

        public void RefreshActivity(object sender, EventArgs e)
        {
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            spinner.Adapter = null;
            load_server();
        }
        
        public void ConnectActivity(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            String DB;
            Object Source;
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            string servername = spinner.SelectedItem.ToString();
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

            if (spinner.SelectedItem != null)
            {
                Source = spinner.SelectedItem.ToString() + ",1433";
                DB = "Toolearn";

                con = new SqlConnection("Data Source='" + Source + "' ; Initial Catalog='" + DB + "'");
                try
                {
                   
                    con.Open();
                    if (con.State == ConnectionState.Open)
                    {

                        con.Close();

                        Program.source = Source.ToString();
                        Program.db = DB;

                        string text = Intent.GetStringExtra("Invidividual");
                        if (text == "Individual")
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
        
    }
}