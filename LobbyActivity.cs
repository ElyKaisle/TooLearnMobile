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
using System.Data;
using System.Data.SqlClient;

namespace TooLearnAndroid
{
    [Activity(Label = "Lobby", Theme = "@style/Theme.DesignDemo")]
    public class LobbyActivity : Activity
    {  
        private TcpClient _client = new TcpClient();
        private const int _buffer_size = 2048;
        private byte[] _buffer = new byte[_buffer_size];
        private string _IPAddress = Program.serverIP;
        private const int _PORT = 13000;
        string name;
        public static string GameType = "";

        string me = MainActivity.Role;

        SqlConnection con = new SqlConnection("Data Source='" + Program.source + "' ; Initial Catalog='" + Program.db + "'; User ID='" + Program.id + "';Password='" + Program.password + "'");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.activity_lobby);
            StartConnect();
            LobbyParticipant_Load();
        }
        
        private void LobbyParticipant_Load()
        {
            if (me == "Individual")
            {
                SqlDataAdapter sql = new SqlDataAdapter("Select fullname from participant where participant_id='" + Program.par_id + "'", con);
                DataTable dt = new DataTable();
                sql.Fill(dt);
                name = dt.Rows[0][0].ToString();

                Send(name);
            }

            else if (me == "Group")
            {
                SqlDataAdapter sql = new SqlDataAdapter("Select  group_name from groups where group_id='" + Program.group_id + "'", con);
                DataTable dt = new DataTable();
                sql.Fill(dt);
                name = dt.Rows[0][0].ToString();

                Send(name);
            }
            else
            {
                Send(NicknameActivity.NameFREE);
            }
        }

        private void Send(string message)
        {
            try
            {
                //Translate the message into its byte form
                byte[] buffer = System.Text.Encoding.ASCII.GetBytes(message + " has joined the Lobby.");

                //Get a client stream for reading and writing
                NetworkStream stream = _client.GetStream();

                //Send the message to the connected server
                //stream.Write(buffer, 0, buffer.length);
                stream.BeginWrite(buffer, 0, buffer.Length, BeginWriteCallback, stream);
            }
            catch (Exception ex)
            {
                //ThreadHelper.lsbAddItem(this, lsbWait, ex.ToString());

                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
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
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        private void BeginWriteCallback(IAsyncResult ar)
        {
            NetworkStream stream = (NetworkStream)ar.AsyncState;
            stream.EndWrite(ar);
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
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
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
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }


        private void BeginReceiveCallback(IAsyncResult ar)
        {

            var msg = FindViewById<EditText>(Resource.Id.editText1);
            try
            {
                // get the client socket
                TcpClient client = (TcpClient)ar.AsyncState; ///error forcibly close
                int bytesRead = client.Client.EndReceive(ar);

                string message = System.Text.Encoding.ASCII.GetString(_buffer, 0, bytesRead);

                Receive();

                if (message.Contains("CONFIRM"))
                {
                    RunOnUiThread(() => Toast.MakeText(this, " You are now connected! Please Wait", ToastLength.Long).Show());
                }

                else if (message.Contains("GAMEIPQB"))
                {
                    GameType = "QB";
                    
                    Intent intent = new Intent(this, typeof(GameActivity));
                    StartActivity(intent);

                }

                else if (message.Contains("GAMEIPPZ"))
                {
                    GameType = "PZ";
                    
                    StartActivity(typeof(GameActivity));
                    Intent intent = new Intent(this, typeof(GameActivity));
                    StartActivity(intent);
                    
                }


                else if (message.Contains("GAMEGPQB")) //group
                {

                    //this.Hide();//kaipuhn muna ithread
                    GameType = "QB";
                    //  GameRules GR = new GameRules();
                    // GR.Show();


                    StartActivity(typeof(GameGroupActivity));
                    Intent intent = new Intent(this, typeof(GameGroupActivity));
                    StartActivity(intent);


                }

                else if (message.Contains("GAMEGPPZ"))//group
                {

                    //this.Hide();//kaipuhn muna ithread
                    GameType = "PZ";
                    //  GameRules GR = new GameRules();
                    // GR.Show();

                    StartActivity(typeof(GameGroupActivity));
                    Intent intent = new Intent(this, typeof(GameGroupActivity));
                    StartActivity(intent);


                }


                else if (message.Contains("GAMEFPQB"))
                {

                    //this.Hide();//kaipuhn muna ithread
                    GameType = "QB";
                    //  GameRules GR = new GameRules();
                    // GR.Show();
                    StartActivity(typeof(GameGuestActivity));
                    Intent intent = new Intent(this, typeof(GameGuestActivity));
                    StartActivity(intent);

                }

                else if (message.Contains("GAMEFPPZ"))
                {
                    

                    //this.Hide();//kaipuhn muna ithread
                    GameType = "PZ";
                    //  GameRules GR = new GameRules();
                    // GR.Show();


                    StartActivity(typeof(GameGuestActivity));
                    Intent intent = new Intent(this, typeof(GameGuestActivity));
                    StartActivity(intent);


                }


                else
                {
                    msg.Text = message;
                    Receive();

                }

            }
            
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }

        }

    }
}