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
using System.IO;

using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Data.SqlClient;
using System.Threading;


namespace TooLearnAndroid
{
    public class RulesFragment : Fragment
    {
        
        string GameType = LobbyActivity.GameType;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            
            View view = inflater.Inflate(Resource.Layout.fragment_rules, container, false);
            StartConnect();
            GameParticipant_Load();
            return view;
            
        }

        public void StartConnect()
        {
            try
            {
                if (GameActivity._client.Connected == false)
                {
                    GameActivity._client = new TcpClient();
                }
                GameActivity._client.NoDelay = true;

                //Begin connecting to server
                GameActivity._client.BeginConnect(IPAddress.Parse(GameActivity._IPAddress), GameActivity._PORT, BeginConnectCallBack, GameActivity._client);
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
                GameActivity._client.Client.BeginReceive(GameActivity._buffer, 0, GameActivity._buffer_size, SocketFlags.None, BeginReceiveCallback, GameActivity._client
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

                string message = System.Text.Encoding.ASCII.GetString(GameActivity._buffer, 0, bytesRead);

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

        public void GameParticipant_Load()
        {
            
                Object y = null;
                Object x = null;
                var rulestitle = y;
                var rulescontentv = x;
                Activity.RunOnUiThread(() => rulestitle = View.FindViewById<TextView>(Resource.Id.textTitle).Text);
                Activity.RunOnUiThread(() => rulescontentv = View.FindViewById<TextView>(Resource.Id.textContent).Text);
                

                if (GameType == "QB")
                {
                    rulestitle = "Quiz Bee";
                    using (StreamReader sr = new StreamReader(Application.Context.Assets.Open("QuizBeeRules.txt")))
                    {
                        rulescontentv = sr.ReadToEnd();
                    }
                }

                else if (GameType == "PZ")
                {
                    rulestitle = "Picture Puzzle";
                    using (StreamReader sr = new StreamReader(Application.Context.Assets.Open("PicturePuzzleRules.txt")))
                    {
                        rulescontentv = sr.ReadToEnd();
                    }
                }
            
            
        }
    }
}