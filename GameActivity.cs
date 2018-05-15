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
using System.Timers;


namespace TooLearnAndroid 
{
    [Activity(Label = "Game", Theme = "@style/Theme.DesignDemo")]
    public class GameActivity : Activity
    {
        SqlConnection con = new SqlConnection("Data Source='" + Program.source + "' ; Initial Catalog='" + Program.db + "'; User ID='" + Program.id + "';Password='" + Program.password + "'");

        private static TcpClient _client = new TcpClient();
        private const int _buffer_size = 2048;
        private byte[] _buffer = new byte[_buffer_size];
        private string _IPAddress = Program.serverIP;
        private const int _PORT = 13000;

        string GameType = LobbyActivity.GameType;
        public static string correctanswer = "", points = "", Pname = "";
        string time;
        int convertedtime;
        string Total;

        Timer timer;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.activity_game);
            RunOnUiThread(() => StartConnect());
            RunOnUiThread(() => RulesOnLoadActivity());


            var enterans = FindViewById<Button>(Resource.Id.button5);
            enterans.Click += EnterAnswer;
            var pchoice1 = FindViewById<Button>(Resource.Id.button1);
            pchoice1.Click += Choice1;
            var pchoice2 = FindViewById<Button>(Resource.Id.button2);
            pchoice2.Click += Choice2;
            var pchoice3 = FindViewById<Button>(Resource.Id.button3);
            pchoice3.Click += Choice3;
            var pchoice4 = FindViewById<Button>(Resource.Id.button4);
            pchoice4.Click += Choice4;
            var truechoice = FindViewById<Button>(Resource.Id.button6);
            truechoice.Click += TrueChoice;
            var falsechoice = FindViewById<Button>(Resource.Id.button7);
            falsechoice.Click += FalseChoice;
            var timersec = FindViewById<TextView>(Resource.Id.textView5);
        }

        public override void OnBackPressed()
        {
            AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);
            alertDialog.SetTitle("Quitting the Game");
            alertDialog.SetMessage("Are you sure?");
            alertDialog.SetPositiveButton("Ok", (senderAlert, args) =>
            {
                Intent intent = new Intent(this, typeof(LobbyActivity));
                StartActivity(intent);
            });

            alertDialog.SetNegativeButton("Cancel", delegate
            {
                alertDialog.Dispose();
                Toast.MakeText(this, "Cancelled!", ToastLength.Short).Show();
            });
            Dialog dialog = alertDialog.Create();
            dialog.Show();
        }

        private string TimerFormat(int secs)
        {
            TimeSpan t = TimeSpan.FromSeconds(secs);
            return string.Format("{0:D2}:{1:D2}:{2:D2}",
                (int)t.TotalHours,
                t.Minutes,
                t.Seconds);
        }

       
        public void FalseChoice(object sender, EventArgs e)
        {
            var question = FindViewById<TextView>(Resource.Id.textView12);
            var timerlabel = FindViewById<TextView>(Resource.Id.textView3);
            var timertext = FindViewById<TextView>(Resource.Id.textView5);
            var truebutton = FindViewById<Button>(Resource.Id.button6);
            var falsebutton = FindViewById<Button>(Resource.Id.button7);

            var falsechoice = FindViewById<Button>(Resource.Id.button7).Text;
            
            var correct = FindViewById<TextView>(Resource.Id.textView13);

            var wrong = FindViewById<TextView>(Resource.Id.textView14);

            var scorepts = FindViewById<TextView>(Resource.Id.textView4);

            string feed = validate(falsechoice);

            int score;

            if (feed == "Correct")
            {
                score = Convert.ToInt32(scorepts.Text);
                score = score + Convert.ToInt32(points);
                scorepts.Text = score.ToString();
                RunOnUiThread(() => question.Visibility = ViewStates.Gone);
                RunOnUiThread(() => truebutton.Enabled = false);
                RunOnUiThread(() => falsebutton.Enabled = false);
                RunOnUiThread(() => correct.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Gone);
                SendScore(scorepts.Text);
                RunOnUiThread(() => timertext.Visibility = ViewStates.Invisible);
                RunOnUiThread(() => timerlabel.Visibility = ViewStates.Invisible);
                timer.Stop();
            }

            else
            {
                RunOnUiThread(() => question.Visibility = ViewStates.Gone);
                RunOnUiThread(() => truebutton.Enabled = false);
                RunOnUiThread(() => falsebutton.Enabled = false);
                RunOnUiThread(() => correct.Visibility = ViewStates.Gone);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Text = "Incorrect! The right answer is " + correctanswer.ToUpper());
                RunOnUiThread(() => timertext.Visibility = ViewStates.Invisible);
                RunOnUiThread(() => timerlabel.Visibility = ViewStates.Invisible);
                timer.Stop();
            }
        }

        public void TrueChoice(object sender, EventArgs e)
        {
            var question = FindViewById<TextView>(Resource.Id.textView12);
            var timerlabel = FindViewById<TextView>(Resource.Id.textView3);
            var timertext = FindViewById<TextView>(Resource.Id.textView5);
            var truebutton = FindViewById<Button>(Resource.Id.button6);
            var falsebutton = FindViewById<Button>(Resource.Id.button7);

            var truechoice = FindViewById<Button>(Resource.Id.button6).Text;

            var correct = FindViewById<TextView>(Resource.Id.textView13);

            var wrong = FindViewById<TextView>(Resource.Id.textView14);

            var scorepts = FindViewById<TextView>(Resource.Id.textView4);

            string feed = validate(truechoice);
            int score;

            if (feed == "Correct")
            {
                score = Convert.ToInt32(scorepts.Text);
                score = score + Convert.ToInt32(points);
                scorepts.Text = score.ToString();
                RunOnUiThread(() => question.Visibility = ViewStates.Gone);
                RunOnUiThread(() => truebutton.Enabled = false);
                RunOnUiThread(() => falsebutton.Enabled = false);
                RunOnUiThread(() => correct.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Gone);
                SendScore(scorepts.Text);
                RunOnUiThread(() => timertext.Visibility = ViewStates.Invisible);
                RunOnUiThread(() => timerlabel.Visibility = ViewStates.Invisible);
                timer.Stop();
            }

            else
            {
                RunOnUiThread(() => question.Visibility = ViewStates.Gone);
                RunOnUiThread(() => truebutton.Enabled = false);
                RunOnUiThread(() => falsebutton.Enabled = false);
                RunOnUiThread(() => correct.Visibility = ViewStates.Gone);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Text = "Incorrect! The right answer is " + correctanswer.ToUpper());
                RunOnUiThread(() => timertext.Visibility = ViewStates.Invisible);
                RunOnUiThread(() => timerlabel.Visibility = ViewStates.Invisible);
                timer.Stop();
            }
        }
            
        public void Choice1(object sender, EventArgs e)
        {
            var question = FindViewById<TextView>(Resource.Id.textView12);
            var timerlabel = FindViewById<TextView>(Resource.Id.textView3);
            var timertext = FindViewById<TextView>(Resource.Id.textView5);
            var choice1 = FindViewById<Button>(Resource.Id.button1);
            var choice2 = FindViewById<Button>(Resource.Id.button2);
            var choice3 = FindViewById<Button>(Resource.Id.button3);
            var choice4 = FindViewById<Button>(Resource.Id.button4);

            var correct = FindViewById<TextView>(Resource.Id.textView13);

            var wrong = FindViewById<TextView>(Resource.Id.textView14);

            var scorepts = FindViewById<TextView>(Resource.Id.textView4);

            string feed = validate("A");
            int score;

            if (feed == "Correct")
            {
                score = Convert.ToInt32(scorepts.Text);
                score = score + Convert.ToInt32(points);
                scorepts.Text = score.ToString();
                RunOnUiThread(() => question.Visibility = ViewStates.Gone);
                RunOnUiThread(() => choice1.Enabled = false);
                RunOnUiThread(() => choice2.Enabled = false);
                RunOnUiThread(() => choice3.Enabled = false);
                RunOnUiThread(() => choice4.Enabled = false);
                RunOnUiThread(() => correct.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Gone);
                SendScore(scorepts.Text);
                RunOnUiThread(() => timertext.Visibility = ViewStates.Invisible);
                RunOnUiThread(() => timerlabel.Visibility = ViewStates.Invisible);
                timer.Stop();
            }

            else
            {
                RunOnUiThread(() => question.Visibility = ViewStates.Gone);
                RunOnUiThread(() => choice1.Enabled = false);
                RunOnUiThread(() => choice2.Enabled = false);
                RunOnUiThread(() => choice3.Enabled = false);
                RunOnUiThread(() => choice4.Enabled = false);
                RunOnUiThread(() => correct.Visibility = ViewStates.Gone);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Text = "Incorrect! The right answer is " + correctanswer.ToUpper());
                RunOnUiThread(() => timertext.Visibility = ViewStates.Invisible);
                RunOnUiThread(() => timerlabel.Visibility = ViewStates.Invisible);
                timer.Stop();
            }
        }

        public void Choice2(object sender, EventArgs e)
        {
            var question = FindViewById<TextView>(Resource.Id.textView12);
            var timerlabel = FindViewById<TextView>(Resource.Id.textView3);
            var timertext = FindViewById<TextView>(Resource.Id.textView5);
            var choice1 = FindViewById<Button>(Resource.Id.button1);
            var choice2 = FindViewById<Button>(Resource.Id.button2);
            var choice3 = FindViewById<Button>(Resource.Id.button3);
            var choice4 = FindViewById<Button>(Resource.Id.button4);

            var correct = FindViewById<TextView>(Resource.Id.textView13);

            var wrong = FindViewById<TextView>(Resource.Id.textView14);

            var scorepts = FindViewById<TextView>(Resource.Id.textView4);
            string feed = validate("B");
            int score;

            if (feed == "Correct")
            {
                score = Convert.ToInt32(scorepts.Text);
                score = score + Convert.ToInt32(points);
                scorepts.Text = score.ToString();
                RunOnUiThread(() => question.Visibility = ViewStates.Gone);
                RunOnUiThread(() => choice1.Enabled = false);
                RunOnUiThread(() => choice2.Enabled = false);
                RunOnUiThread(() => choice3.Enabled = false);
                RunOnUiThread(() => choice4.Enabled = false);
                RunOnUiThread(() => correct.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Gone);
                SendScore(scorepts.Text);
                RunOnUiThread(() => timertext.Visibility = ViewStates.Invisible);
                RunOnUiThread(() => timerlabel.Visibility = ViewStates.Invisible);
                timer.Stop();
            }

            else
            {
                RunOnUiThread(() => question.Visibility = ViewStates.Gone);
                RunOnUiThread(() => choice1.Enabled = false);
                RunOnUiThread(() => choice2.Enabled = false);
                RunOnUiThread(() => choice3.Enabled = false);
                RunOnUiThread(() => choice4.Enabled = false);
                RunOnUiThread(() => correct.Visibility = ViewStates.Gone);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Text = "Incorrect! The right answer is " + correctanswer.ToUpper());
                RunOnUiThread(() => timertext.Visibility = ViewStates.Invisible);
                RunOnUiThread(() => timerlabel.Visibility = ViewStates.Invisible);
                timer.Stop();
            }
        }

        public void Choice3(object sender, EventArgs e)
        {
            var question = FindViewById<TextView>(Resource.Id.textView12);
            var timerlabel = FindViewById<TextView>(Resource.Id.textView3);
            var timertext = FindViewById<TextView>(Resource.Id.textView5);
            var choice1 = FindViewById<Button>(Resource.Id.button1);
            var choice2 = FindViewById<Button>(Resource.Id.button2);
            var choice3 = FindViewById<Button>(Resource.Id.button3);
            var choice4 = FindViewById<Button>(Resource.Id.button4);

            var correct = FindViewById<TextView>(Resource.Id.textView13);

            var wrong = FindViewById<TextView>(Resource.Id.textView14);

            var scorepts = FindViewById<TextView>(Resource.Id.textView4);
            string feed = validate("C");
            int score;

            if (feed == "Correct")
            {

                score = Convert.ToInt32(scorepts.Text);
                score = score + Convert.ToInt32(points);
                scorepts.Text = score.ToString();
                RunOnUiThread(() => question.Visibility = ViewStates.Gone);
                RunOnUiThread(() => choice1.Enabled = false);
                RunOnUiThread(() => choice2.Enabled = false);
                RunOnUiThread(() => choice3.Enabled = false);
                RunOnUiThread(() => choice4.Enabled = false);
                RunOnUiThread(() => correct.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Gone);
                SendScore(scorepts.Text);
                RunOnUiThread(() => timertext.Visibility = ViewStates.Invisible);
                RunOnUiThread(() => timerlabel.Visibility = ViewStates.Invisible);
                timer.Stop();
            }

            else
            {
                RunOnUiThread(() => question.Visibility = ViewStates.Gone);
                RunOnUiThread(() => choice1.Enabled = false);
                RunOnUiThread(() => choice2.Enabled = false);
                RunOnUiThread(() => choice3.Enabled = false);
                RunOnUiThread(() => choice4.Enabled = false);
                RunOnUiThread(() => correct.Visibility = ViewStates.Gone);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Text = "Incorrect! The right answer is " + correctanswer.ToUpper());
                RunOnUiThread(() => timertext.Visibility = ViewStates.Invisible);
                RunOnUiThread(() => timerlabel.Visibility = ViewStates.Invisible);
                timer.Stop();
            }
        }


        public void Choice4(object sender, EventArgs e)
        {
            var question = FindViewById<TextView>(Resource.Id.textView12);
            var timerlabel = FindViewById<TextView>(Resource.Id.textView3);
            var timertext = FindViewById<TextView>(Resource.Id.textView5);
            var choice1 = FindViewById<Button>(Resource.Id.button1);
            var choice2 = FindViewById<Button>(Resource.Id.button2);
            var choice3 = FindViewById<Button>(Resource.Id.button3);
            var choice4 = FindViewById<Button>(Resource.Id.button4);

            var correct = FindViewById<TextView>(Resource.Id.textView13);

            var wrong = FindViewById<TextView>(Resource.Id.textView14);

            var scorepts = FindViewById<TextView>(Resource.Id.textView4);
            string feed = validate("D");
            int score;

            if (feed == "Correct")
            {
                score = Convert.ToInt32(scorepts.Text);
                score = score + Convert.ToInt32(points);
                scorepts.Text = score.ToString();
                RunOnUiThread(() => question.Visibility = ViewStates.Gone);
                RunOnUiThread(() => choice1.Enabled = false);
                RunOnUiThread(() => choice2.Enabled = false);
                RunOnUiThread(() => choice3.Enabled = false);
                RunOnUiThread(() => choice4.Enabled = false);
                RunOnUiThread(() => correct.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Gone);
                SendScore(scorepts.Text);
                RunOnUiThread(() => timertext.Visibility = ViewStates.Invisible);
                RunOnUiThread(() => timerlabel.Visibility = ViewStates.Invisible);
                timer.Stop();
            }

            else
            {
                RunOnUiThread(() => question.Visibility = ViewStates.Gone);
                RunOnUiThread(() => choice1.Enabled = false);
                RunOnUiThread(() => choice2.Enabled = false);
                RunOnUiThread(() => choice3.Enabled = false);
                RunOnUiThread(() => choice4.Enabled = false);
                RunOnUiThread(() => correct.Visibility = ViewStates.Gone);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Text = "Incorrect! The right answer is " + correctanswer.ToUpper());
                RunOnUiThread(() => timertext.Visibility = ViewStates.Invisible);
                RunOnUiThread(() => timerlabel.Visibility = ViewStates.Invisible);
                timer.Stop();
            }
        }

        public void EnterAnswer(object sender, EventArgs e)
        {
            var question = FindViewById<TextView>(Resource.Id.textView12);
            var timerlabel = FindViewById<TextView>(Resource.Id.textView3);
            var timertext = FindViewById<TextView>(Resource.Id.textView5);
            var enterans = FindViewById<Button>(Resource.Id.button5);
            var shortanswer = FindViewById<EditText>(Resource.Id.editText1);

            var correct = FindViewById<TextView>(Resource.Id.textView13);

            var wrong = FindViewById<TextView>(Resource.Id.textView14);

            var scorepts = FindViewById<TextView>(Resource.Id.textView4);
            string feed = validateSA(shortanswer.Text);
            int score;

            if (feed == "Correct")
            {
                score = Convert.ToInt32(scorepts.Text);
                score = score + Convert.ToInt32(points);
                scorepts.Text = score.ToString();
                RunOnUiThread(() => question.Visibility = ViewStates.Gone);
                RunOnUiThread(() => enterans.Enabled = false);
                RunOnUiThread(() => shortanswer.Enabled = false);
                RunOnUiThread(() => correct.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Gone);
                SendScore(scorepts.Text);
                RunOnUiThread(() => timertext.Visibility = ViewStates.Invisible);
                RunOnUiThread(() => timerlabel.Visibility = ViewStates.Invisible);
                timer.Stop();
            }

            else
            {
                RunOnUiThread(() => question.Visibility = ViewStates.Gone);
                RunOnUiThread(() => enterans.Enabled = false);
                RunOnUiThread(() => shortanswer.Enabled = false);
                RunOnUiThread(() => correct.Visibility = ViewStates.Gone);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Text = "Incorrect! The right answer is " + correctanswer.ToUpper());
                RunOnUiThread(() => timertext.Visibility = ViewStates.Invisible);
                RunOnUiThread(() => timerlabel.Visibility = ViewStates.Invisible);
                timer.Stop();
            }

            RunOnUiThread(() => shortanswer.Text = "");

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
                byte[] buffer = System.Text.Encoding.ASCII.GetBytes("(SCORE),"+Pname+",("+message+")");

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

                //RunOnUiThread(() => Toast.MakeText(this, "START CONNECT!", ToastLength.Short).Show());
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
                RunOnUiThread(() => _client.Client.BeginReceive(_buffer, 0, _buffer_size, SocketFlags.None, BeginReceiveCallback, _client
                    ));
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString() + "ERROR SA RECEIVE", ToastLength.Short).Show();

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

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var question = FindViewById<TextView>(Resource.Id.textView12);
            var timesup = FindViewById<TextView>(Resource.Id.textView3);
            var timertxt = FindViewById<TextView>(Resource.Id.textView5);

            var enterans = FindViewById<Button>(Resource.Id.button5);
            var shortans = FindViewById<EditText>(Resource.Id.editText1);
            var truechoice = FindViewById<Button>(Resource.Id.button6);
            var falsechoice = FindViewById<Button>(Resource.Id.button7);

            var choice1 = FindViewById<Button>(Resource.Id.button1);
            var choice2 = FindViewById<Button>(Resource.Id.button2);
            var choice3 = FindViewById<Button>(Resource.Id.button3);
            var choice4 = FindViewById<Button>(Resource.Id.button4);

            convertedtime--;

            if (convertedtime == 0)
            {

                timer.Stop();
                RunOnUiThread(() => timertxt.Visibility = ViewStates.Invisible);
                RunOnUiThread(() => timesup.Text = "Times up!");
                RunOnUiThread(() => timesup.Visibility = ViewStates.Visible);
                RunOnUiThread(() => choice1.Enabled = false);
                RunOnUiThread(() => choice2.Enabled = false);
                RunOnUiThread(() => choice3.Enabled = false);
                RunOnUiThread(() => choice4.Enabled = false);//MC

                RunOnUiThread(() => truechoice.Enabled = false);//TF
                RunOnUiThread(() => falsechoice.Enabled = false);

                RunOnUiThread(() => shortans.Enabled = false);//SA
                RunOnUiThread(() => enterans.Enabled = false);


            }
            else
            {
                RunOnUiThread(() => timertxt.Text = TimerFormat(convertedtime));
                RunOnUiThread(() => timesup.Visibility = ViewStates.Visible);
                RunOnUiThread(() => timertxt.Visibility = ViewStates.Visible);
                RunOnUiThread(() => question.Visibility = ViewStates.Visible);

                RunOnUiThread(() => choice1.Enabled = true);
                RunOnUiThread(() => choice2.Enabled = true);
                RunOnUiThread(() => choice3.Enabled = true);
                RunOnUiThread(() => choice4.Enabled = true);//MC

                RunOnUiThread(() => truechoice.Enabled = true);//TF
                RunOnUiThread(() => falsechoice.Enabled = true);

                RunOnUiThread(() => shortans.Enabled = true);//SA
                RunOnUiThread(() => enterans.Enabled = true);

            }

        }

        private void BeginReceiveCallback(IAsyncResult ar)
        {
            try
            {

                timer = new Timer();
                timer.Interval = 1000;
                timer.Elapsed += Timer_Elapsed;


                //(message.Contains("StartGame"))
                var title = FindViewById<TextView>(Resource.Id.textView1);
                var content = FindViewById<TextView>(Resource.Id.textView7);
                var gametype = FindViewById<TextView>(Resource.Id.textView1);
                var scorelabel = FindViewById<TextView>(Resource.Id.textView2);
                var timerlabel = FindViewById<TextView>(Resource.Id.textView3);
                var ptslabel = FindViewById<TextView>(Resource.Id.textView4);
                var timlabel = FindViewById<TextView>(Resource.Id.textView5);

                //(message.Contains("C1o2m3pute"))
                var scoretext = FindViewById<TextView>(Resource.Id.textView8);
                var totalscores = FindViewById<TextView>(Resource.Id.textView9);
                var totalitems = FindViewById<TextView>(Resource.Id.textView10);
                var feedback = FindViewById<TextView>(Resource.Id.textView11);

                //(array[11].ToString() == "Multiple Choice")
                var pquestion = FindViewById<TextView>(Resource.Id.textView12);
                var pchoice1 = FindViewById<Button>(Resource.Id.button1);
                var pchoice2 = FindViewById<Button>(Resource.Id.button2);
                var pchoice3 = FindViewById<Button>(Resource.Id.button3);
                var pchoice4 = FindViewById<Button>(Resource.Id.button4);

                var enterans = FindViewById<Button>(Resource.Id.button5);
                var shortans = FindViewById<EditText>(Resource.Id.editText1);
                var truechoice = FindViewById<Button>(Resource.Id.button6);
                var falsechoice = FindViewById<Button>(Resource.Id.button7);

                var question = FindViewById<TextView>(Resource.Id.textView12);
                var choice1 = FindViewById<Button>(Resource.Id.button1);
                var choice2 = FindViewById<Button>(Resource.Id.button2);
                var choice3 = FindViewById<Button>(Resource.Id.button3);
                var choice4 = FindViewById<Button>(Resource.Id.button4);

                // get the client socket
                TcpClient client = (TcpClient)ar.AsyncState;
                int bytesRead = client.Client.EndReceive(ar);

                string message = System.Text.Encoding.ASCII.GetString(_buffer, 0, bytesRead); //ini si may laman kang message

                if (message.Contains("DISCONNECT"))
                {
                    StartActivity(typeof(MainmenuActivity)); //ThreadHelper.Hide(this);
                    client.Client.Shutdown(SocketShutdown.Both);
                    client.Client.Close();
                }

                else if (message.Contains("StartGame"))
                {
                    RunOnUiThread(() => title.Visibility = ViewStates.Gone);
                    RunOnUiThread(() => content.Visibility = ViewStates.Gone);
                    RunOnUiThread(() => gametype.Visibility = ViewStates.Gone);
                    RunOnUiThread(() => scorelabel.Visibility = ViewStates.Visible);
                    RunOnUiThread(() => timerlabel.Visibility = ViewStates.Visible);
                    RunOnUiThread(() => ptslabel.Visibility = ViewStates.Visible);
                    RunOnUiThread(() => timlabel.Visibility = ViewStates.Visible);
                    RunOnUiThread(() => question.Visibility = ViewStates.Visible);
                    Receive();
                    
                }

                else if (message.Contains("C1o2m3pute"))
                {
                    RunOnUiThread(() => Toast.MakeText(this, "SA COMPUTE FEEDBACK", ToastLength.Long).Show());
                    /*
                    RunOnUiThread(() => scoretext.Visibility = ViewStates.Visible);
                    RunOnUiThread(() => totalscores.Visibility = ViewStates.Visible);
                    RunOnUiThread(() => totalitems.Visibility = ViewStates.Visible);
                    RunOnUiThread(() => feedback.Visibility = ViewStates.Visible);

                    int rawscore = Convert.ToInt32(ptslabel.Text);
                    RunOnUiThread(() => totalscores.Text = rawscore.ToString());
                    RunOnUiThread(() => totalitems.Text = Total);

                    double converted_total = Convert.ToInt32(Total);
                    double converted_rawscore = rawscore;
                    double comp = (converted_rawscore / converted_total) * 100;
                    int compute = Convert.ToInt32(comp);

                    if (compute < Convert.ToInt32("60"))
                    {
                        RunOnUiThread(() => feedback.Text = compute.ToString() + "% You Need Improvement, Study and Play!");
                    }
                    else if (compute == Convert.ToInt32("100"))
                    {
                        RunOnUiThread(() => feedback.Text = compute.ToString() + "% Excellent!");
                    }
                    else
                    {
                        RunOnUiThread(() => feedback.Text = compute.ToString() + "% Not Bad!, aim for a Perfect Score Next Time ");
                    }
                    */
                    Receive();

                }

                else if (message.Contains("PleaseHideThis"))
                {
                    Send("DISCONNECT");
                    //ThreadHelper.Hide(this);
                }

                else
                {


                    var array = message.Split('\n');


                    if (array[11].ToString() == "Multiple Choice")//Item Format
                    {

                        var correct = FindViewById<TextView>(Resource.Id.textView13);
                        var wrong = FindViewById<TextView>(Resource.Id.textView14);

                        RunOnUiThread(() => correct.Visibility = ViewStates.Gone);
                        RunOnUiThread(() => wrong.Visibility = ViewStates.Gone);
                        
                        RunOnUiThread(() => question.Text = array[0]);
                        RunOnUiThread(() => choice1.Text = array[1]);
                        RunOnUiThread(() => choice2.Text = array[2]);
                        RunOnUiThread(() => choice3.Text = array[3]);
                        RunOnUiThread(() => choice4.Text = array[4]);
                        
                        correctanswer = array[5].ToString();  //CorrectAnswer
                        points = array[8];
                        Total = array[10];

                        RunOnUiThread(() => pquestion.Visibility = ViewStates.Visible);
                        RunOnUiThread(() => pchoice1.Visibility = ViewStates.Visible);
                        RunOnUiThread(() => pchoice2.Visibility = ViewStates.Visible);
                        RunOnUiThread(() => pchoice3.Visibility = ViewStates.Visible);
                        RunOnUiThread(() => pchoice4.Visibility = ViewStates.Visible);
                        RunOnUiThread(() => enterans.Visibility = ViewStates.Gone);
                        RunOnUiThread(() => shortans.Visibility = ViewStates.Gone);
                        RunOnUiThread(() => truechoice.Visibility = ViewStates.Gone);
                        RunOnUiThread(() => falsechoice.Visibility = ViewStates.Gone);

                        string str = array[7].ToString();
                        int index = str.IndexOf('(');

                        if (index >= 0)
                        {
                            time = str.Substring(0, index);



                        }
                        else
                        {

                            time = str;
                        }


                        convertedtime = Convert.ToInt32(time);//timer


                        string cut = array[7].ToString();
                        int ind = cut.IndexOf('(');
                        string form;
                        if (ind >= 0)
                        {
                            form = cut.Substring(ind + 1, 7);



                        }
                        else
                        {

                            form = cut;
                        }


                        if (form == "Minutes")
                        {
                            convertedtime = convertedtime * 60;
                        }
                        timer.Start();
                        
                        //this.Invoke(new ThreadStart(delegate () { timer1.Enabled = true; timer1.Start(); }));



                    }
                    else if (array[11].ToString() == "True/False")
                    {
                        var correct = FindViewById<TextView>(Resource.Id.textView13);
                        var wrong = FindViewById<TextView>(Resource.Id.textView14);

                        RunOnUiThread(() => correct.Visibility = ViewStates.Gone);
                        RunOnUiThread(() => wrong.Visibility = ViewStates.Gone);

                        RunOnUiThread(() => question.Text = array[0].ToString());  //Question
                        correctanswer = array[5].ToString();  //CorrectAnswer
                        points = array[8].ToString();
                        Total = array[10].ToString();

                        RunOnUiThread(() => pquestion.Visibility = ViewStates.Visible);
                        RunOnUiThread(() => enterans.Visibility = ViewStates.Gone);
                        RunOnUiThread(() => shortans.Visibility = ViewStates.Gone);
                        RunOnUiThread(() => pchoice1.Visibility = ViewStates.Invisible);
                        RunOnUiThread(() => pchoice2.Visibility = ViewStates.Invisible);
                        RunOnUiThread(() => pchoice3.Visibility = ViewStates.Invisible);
                        RunOnUiThread(() => pchoice4.Visibility = ViewStates.Invisible);
                        RunOnUiThread(() => truechoice.Visibility = ViewStates.Visible);
                        RunOnUiThread(() => falsechoice.Visibility = ViewStates.Visible);

                        string str = array[7].ToString();
                        int index = str.IndexOf('(');

                        if (index >= 0)
                        {
                            time = str.Substring(0, index);



                        }
                        else
                        {

                            time = str;
                        }


                        convertedtime = Convert.ToInt32(time);//timer



                        string cut = array[7].ToString();
                        int ind = cut.IndexOf('(');
                        string form;
                        if (ind >= 0)
                        {
                            form = cut.Substring(ind + 1, 7);



                        }
                        else
                        {

                            form = cut;
                        }


                        if (form == "Minutes")
                        {
                            convertedtime = convertedtime * 60;
                        }
                        timer.Start();
                        //this.Invoke(new ThreadStart(delegate () { timer1.Enabled = true; timer1.Start(); }));
                    }

                    else
                    {

                        var correct = FindViewById<TextView>(Resource.Id.textView13);
                        var wrong = FindViewById<TextView>(Resource.Id.textView14);

                        RunOnUiThread(() => correct.Visibility = ViewStates.Gone);
                        RunOnUiThread(() => wrong.Visibility = ViewStates.Gone);

                        RunOnUiThread(() => question.Text = array[0].ToString());  //Question
                        correctanswer = array[5].ToString(); ;  //CorrectAnswer
                        points = array[8].ToString();
                        Total = array[10].ToString();

                        RunOnUiThread(() => pquestion.Visibility = ViewStates.Visible);
                        RunOnUiThread(() => enterans.Visibility = ViewStates.Visible);
                        RunOnUiThread(() => shortans.Visibility = ViewStates.Visible);
                        RunOnUiThread(() => pchoice1.Visibility = ViewStates.Invisible);
                        RunOnUiThread(() => pchoice2.Visibility = ViewStates.Invisible);
                        RunOnUiThread(() => pchoice3.Visibility = ViewStates.Invisible);
                        RunOnUiThread(() => pchoice4.Visibility = ViewStates.Invisible);
                        RunOnUiThread(() => truechoice.Visibility = ViewStates.Gone);
                        RunOnUiThread(() => falsechoice.Visibility = ViewStates.Gone);

                        string str = array[7].ToString();
                        int index = str.IndexOf('(');

                        if (index >= 0)
                        {
                            time = str.Substring(0, index);



                        }
                        else
                        {

                            time = str;
                        }


                        convertedtime = Convert.ToInt32(time);//timer




                        string cut = array[7].ToString();
                        int ind = cut.IndexOf('(');
                        string form;
                        if (ind >= 0)
                        {
                            form = cut.Substring(ind + 1, 7);



                        }
                        else
                        {

                            form = cut;
                        }


                        if (form == "Minutes")
                        {
                            convertedtime = convertedtime * 60;
                        }
                        timer.Start();
                        //this.Invoke(new ThreadStart(delegate () { timer1.Enabled = true; timer1.Start(); }));

                    }


                    Receive();

                }
            }
            catch (Exception ex)
            {
                RunOnUiThread(() => Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show());
            }
        }

        public void RulesOnLoadActivity()
        {
            var title = FindViewById<TextView>(Resource.Id.textView1);
            var content = FindViewById<TextView>(Resource.Id.textView7);
            string stitle, scontent;

            try
            {
                SqlDataAdapter Name = new SqlDataAdapter("Select fullname from participant where participant_id='" + Program.par_id + "' ", con);
                DataTable dt = new DataTable();
                Name.Fill(dt);
                Pname = dt.Rows[0][0].ToString();

                if (GameType == "QB")
                {
                    stitle = "Quiz Bee";
                    RunOnUiThread(() => title.Text = stitle);
                    using (StreamReader sr = new StreamReader(Application.Context.Assets.Open("QuizBeeRules.txt")))
                    {
                        scontent = sr.ReadToEnd();
                        RunOnUiThread(() => content.Text = scontent);
                        
                    }

                }

                else if (GameType == "PZ")
                {
                    stitle = "Picture Puzzle";
                    RunOnUiThread(() => title.Text = stitle);
                    using (StreamReader sr = new StreamReader(Application.Context.Assets.Open("PicturePuzzleRules.txt")))
                    {
                        scontent = sr.ReadToEnd();
                        RunOnUiThread(() => content.Text = scontent);

                    }
                }

            }
            catch(Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
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