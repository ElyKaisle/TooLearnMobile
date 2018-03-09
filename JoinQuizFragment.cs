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

using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Data;
using System.Data.SqlClient;

namespace TooLearnAndroid
{
    public class JoinQuizFragment : Fragment
    {
        public static String Session_id, PSession_id; // For Facilitator
        public static int user_id, par_id; // For Participant
        public static int group_id; // For Group
        public static String serverIP;
        public static String source, db, id, password;// For Participant& G
        private TcpClient _client = new TcpClient();
        private const int _buffer_size = 2048;
        private byte[] _buffer = new byte[_buffer_size];
        private string _IPAddress = "127.0.0.1";
        private const int _PORT = 13000;
        Button join;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.fragment_joinquiz, container, false);
            join = view.FindViewById<Button>(Resource.Id.button1);
            join.Click += StartLobbyActivity;
            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
        public void StartLobbyActivity(object sender, EventArgs e)
        {
            //Alternative
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Helper.ConnectionString;

            SqlDataAdapter sda = new SqlDataAdapter("Select Game_Pin From Pincode", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                Toast.MakeText(this.Activity, "No Host Server Detected! Please Wait for the Host to Start", ToastLength.Short).Show();

            }

            else
            {

                string code = dt.Rows[0][0].ToString();

                var text = View.FindViewById<EditText>(Resource.Id.editText1).Text;
                if (code == text)
                {
                    Intent intent = new Intent(this.Activity, typeof(LobbyActivity));
                    StartActivity(intent);
                }

                else if (text == null || text == "")
                {
                    Toast.MakeText(this.Activity, "* Please Enter Code", ToastLength.Short).Show();

                }
                else
                {
                    Toast.MakeText(this.Activity, "* Code is Invalid!", ToastLength.Short).Show();

                }
            }
        }

        static class Helper
        {
            public static string ConnectionString
            {
                get
                {
                    string str = "Data Source='" + source + "' ; Initial Catalog='" + db + "'; User ID='" + id + "';Password='" + password + "'";
                    return str;
                }
            }
        }

        public void StartConnect()
        {
            try
            {
                if (_client.Connected == false)
                {
                    _client = new TcpClient();
                }
                _client.NoDelay = true;

                //Begin connecting to server
                _client.BeginConnect(IPAddress.Parse(_IPAddress), _PORT, BeginConnectCallBack, _client);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this.Activity, ex.ToString(), ToastLength.Short).Show();
            }
        }

        private void BeginConnectCallBack(IAsyncResult ar)
        {
            try
            {
                TcpClient _client = (TcpClient)ar.AsyncState;
                _client.EndConnect(ar);
                Receive();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this.Activity, ex.ToString(), ToastLength.Short).Show();
            }
        }

        private void Receive()
        {
            try
            {
                _client.Client.BeginReceive(_buffer, 0, _buffer_size, SocketFlags.None, BeginReceiveCallback, _client
                    );
            }
            catch (Exception ex)
            {
                Toast.MakeText(this.Activity, ex.ToString(), ToastLength.Short).Show();
            }
        }

        private void BeginReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // get the client socket
                TcpClient client = (TcpClient)ar.AsyncState;
                int bytesRead = client.Client.EndReceive(ar);

                string message = System.Text.Encoding.ASCII.GetString(_buffer, 0, bytesRead);

                if (message.Contains("DISCONNECT"))
                {
                    client.Client.Shutdown(SocketShutdown.Both);
                    client.Client.Close();
                }
                else
                {

                    Receive();

                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this.Activity, ex.ToString(), ToastLength.Short).Show();
            }
        }

    }
}