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
using System.IO;

using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace TooLearnAndroid 
{
    [Activity(Label = "Game", Theme = "@style/Theme.DesignDemo", NoHistory = true)]
    public class GameActivity : Activity
    {
        SqlConnection con = new SqlConnection("Data Source='" + Program.source + "' ; Initial Catalog='" + Program.db + "'; User ID='" + Program.id + "';Password='" + Program.password + "'");

        public static TcpClient _client = new TcpClient();
        public static int _buffer_size = 2048;
        public static byte[] _buffer = new byte[_buffer_size];
        public static string _IPAddress = Program.serverIP;
        public static int _PORT = 1433;

        Timer timer;

        string GameType = LobbyActivity.GameType;
        public static string correctanswer, points, Pname;
        public static string time;
        public static int convertedtime;
        public static String[] array = { };
        public static string Total = "";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_game);
            StartConnect();
            RulesOnLoadActivity();
            var timersec = FindViewById<TextView>(Resource.Id.textView5);
        }

        
        private void Send(string message)
        {
            try
            {
                //Translate the message into its byte form
                byte[] buffer = System.Text.Encoding.ASCII.GetBytes(message + " is ready!");

                //Get a client stream for reading and writing
                NetworkStream stream = _client.GetStream();

                //Send the message to the connected server
                //stream.Write(buffer, 0, buffer.length);
                stream.BeginWrite(buffer, 0, buffer.Length, BeginWriteCallback, stream);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        public static void SendScore(string message)
        {
            try
            {
                //Translate the message into its byte form
                byte[] buffer = System.Text.Encoding.ASCII.GetBytes("(SCORE)," + Pname + "(" + message + ")");

                //Get a client stream for reading and writing
                NetworkStream stream = _client.GetStream();

                //Send the message to the connected server
                //stream.Write(buffer, 0, buffer.length);
                stream.BeginWrite(buffer, 0, buffer.Length, BeginWriteCallback, stream);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, ex.ToString(), ToastLength.Short).Show();
            }
        }

        public static void BeginWriteCallback(IAsyncResult ar)
        {
            NetworkStream stream = (NetworkStream)ar.AsyncState;
            stream.EndWrite(ar);
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

        private string get(string answer)
        {
            string RA = answer;
            string value = "";

            char[] answerchar = RA.ToArray();
            for (int i = 0; i < RA.Count(); i++)
            {
                if ((i % 2) == 0)
                {
                    value += answerchar[i].ToString();
                }

            }

            return value;
        }


        private void BeginReceiveCallback(IAsyncResult ar)
        {
            
            var scorepts = FindViewById<TextView>(Resource.Id.textView4).Text;
            try
            {
                // get the client socket
                TcpClient client = (TcpClient)ar.AsyncState;
                int bytesRead = client.Client.EndReceive(ar);

                string message = System.Text.Encoding.ASCII.GetString(_buffer, 0, bytesRead); //ini si may laman kang message

                if (message.Contains("DISCONNECT"))
                {
                    StartActivity(typeof(MainmenuActivity));
                    client.Client.Shutdown(SocketShutdown.Both);
                    client.Client.Close();
                }

                else if (message.Contains("StartGame"))
                {
                    Receive();

                }

                else if (message.Contains("C1o2m3pute"))
                {
                    FeedbackFragment fragment1 = new FeedbackFragment();
                    FragmentTransaction fragmentTx1 = this.FragmentManager.BeginTransaction();
                    fragmentTx1.Replace(Resource.Id.fragment_container, fragment1);
                    fragmentTx1.Commit();
                }

                else if (message.Contains("PleaseHideThis"))
                {
                    Send("DISCONNECT");
                }

                else
                {


                    array = message.Split('\n');


                    if (array[11].ToString() == "Multiple Choice")//Item Format
                    {

                        MultipleChoiceFragment fragment2 = new MultipleChoiceFragment();
                        FragmentTransaction fragmentTx2 = this.FragmentManager.BeginTransaction();
                        fragmentTx2.Replace(Resource.Id.fragment_container, fragment2);
                        fragmentTx2.Commit();

                        //var timer1 = FindViewById<TextView>(Resource.Id.textView5);
                        //this.Invoke(new ThreadStart(delegate () { timer1.Enabled = true; timer1.Start(); }));



                    }
                    else if (array[11].ToString() == "True/False")
                    {
                        TrueFalseFragment fragment3 = new TrueFalseFragment();
                        FragmentTransaction fragmentTx3 = this.FragmentManager.BeginTransaction();
                        fragmentTx3.Replace(Resource.Id.fragment_container, fragment3);
                        fragmentTx3.Commit();

                        //this.Invoke(new ThreadStart(delegate () { timer1.Enabled = true; timer1.Start(); }));
                    }

                    else
                    {
                        ShortAnswerFragment fragment4 = new ShortAnswerFragment();
                        FragmentTransaction fragmentTx4 = this.FragmentManager.BeginTransaction();
                        fragmentTx4.Replace(Resource.Id.fragment_container, fragment4);
                        fragmentTx4.Commit();

                        //this.Invoke(new ThreadStart(delegate () { timer1.Enabled = true; timer1.Start(); }));

                    }


                    Receive();

                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        public void RulesOnLoadActivity()
        {
            SqlDataAdapter Name = new SqlDataAdapter("Select fullname from participant where participant_id='" + Program.par_id + "' ", con);
            DataTable dt = new DataTable();
            Name.Fill(dt);
            Pname = dt.Rows[0][0].ToString();

            RulesFragment fragment = new RulesFragment();
            FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
            fragmentTx.Replace(Resource.Id.fragment_container, fragment);
            fragmentTx.AddToBackStack(null);
            fragmentTx.Commit();
            

        }

        public static string validateSA(string answer)
        {

            string feed;



            if (correctanswer.ToLower().ToString().Contains(answer.ToLower().ToString()) && answer.Length.Equals(correctanswer.Length - 1))
            {
                feed = "Correct";



            }
            else
            {
                feed = "Wrong";

                Toast.MakeText(Application.Context, answer.Length.ToString() + correctanswer.Length.ToString(), ToastLength.Long).Show();
            }

            return feed;
        }

        public static string validate(string answer)
        {

            string feed;



            if (correctanswer.ToLower().ToString().Contains(answer.ToLower().ToString()))
            {
                feed = "Correct";



            }
            else
            {
                feed = "Wrong";


            }


            return feed;


        }

    }
}