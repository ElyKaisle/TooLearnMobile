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

        private static TcpClient _client = new TcpClient();
        private const int _buffer_size = 2048;
        private byte[] _buffer = new byte[_buffer_size];
        private string _IPAddress = Program.serverIP;
        private const int _PORT = 13000;

        string GameType = LobbyActivity.GameType;
        static string correctanswer = "", points = "", Pname = "";
        string time;
        int convertedtime;
        string Total;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_game);
            RunOnUiThread(() => RulesOnLoadActivity());
            RunOnUiThread(() => StartConnect());

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

        public void FalseChoice(object sender, EventArgs e)
        {
            var falsechoice = FindViewById<Button>(Resource.Id.button7);
            string falsecorrect = "";
            falsechoice.Text = falsecorrect;

            var scorepts = FindViewById<TextView>(Resource.Id.textView4);
            string scorepoints = "";
            scorepts.Text = scorepoints;

            var correct = FindViewById<TextView>(Resource.Id.textView13);
            var wrong = FindViewById<TextView>(Resource.Id.textView14);

            string wrongfeed = "";
            wrong.Text = wrongfeed;

            string feed = validate(falsecorrect);
            int score;

            if (feed == "Correct")
            {

                score = Convert.ToInt32(scorepoints);
                score = score + Convert.ToInt32(points);
                RunOnUiThread(() => scorepoints = score.ToString());
                RunOnUiThread(() => correct.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Gone);

                RunOnUiThread(() => SendScore(scorepoints.ToString()));
            }

            else
            {
                RunOnUiThread(() => correct.Visibility = ViewStates.Gone);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrongfeed = "Wrong! The Right Answer is " + correctanswer.ToUpper());
            }
        }

        public void TrueChoice(object sender, EventArgs e)
        {
            var truechoice = FindViewById<Button>(Resource.Id.button6);
            string truecorrect = "";
            truechoice.Text = truecorrect;

            var scorepts = FindViewById<TextView>(Resource.Id.textView4);
            string scorepoints = "";
            scorepts.Text = scorepoints;

            var correct = FindViewById<TextView>(Resource.Id.textView13);
            var wrong = FindViewById<TextView>(Resource.Id.textView14);
            string wrongfeed = "";
            wrong.Text = wrongfeed;

            string feed = validate(truecorrect);
            int score;

            if (feed == "Correct")
            {

                score = Convert.ToInt32(scorepoints);
                score = score + Convert.ToInt32(points);
                RunOnUiThread(() => scorepoints = score.ToString());
                RunOnUiThread(() => correct.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Gone);

                RunOnUiThread(() => SendScore(scorepoints.ToString()));
            }

            else
            {
                RunOnUiThread(() => correct.Visibility = ViewStates.Gone);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrongfeed = "Wrong! The Right Answer is " + correctanswer.ToUpper());
            }
        }

        public void Choice1(object sender, EventArgs e)
        {
            var scorepts = FindViewById<TextView>(Resource.Id.textView4);
            string scorepoints = FindViewById<TextView>(Resource.Id.textView4).Text;

            var correct = FindViewById<TextView>(Resource.Id.textView13);

            var wrong = FindViewById<TextView>(Resource.Id.textView14);
            var wrongfeed = FindViewById<TextView>(Resource.Id.textView14).Text;

            string feed = validate("A");
            int score;

            if (feed == "Correct")
            {
                int converted = Convert.ToInt32(scorepoints);
                score = converted;
                int convertedpts = Convert.ToInt32(points);
                score = score + convertedpts;

                RunOnUiThread(() => scorepoints = score.ToString());
                RunOnUiThread(() => correct.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Gone);

                RunOnUiThread(() => SendScore(scorepoints.ToString()));
            }

            else
            {
                RunOnUiThread(() => correct.Visibility = ViewStates.Gone);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrongfeed = "Wrong! The Right Answer is " + correctanswer.ToUpper());
            }
        }

        public void Choice2(object sender, EventArgs e)
        {
            var scorepts = FindViewById<TextView>(Resource.Id.textView4);
            string scorepoints = FindViewById<TextView>(Resource.Id.textView4).Text;

            var correct = FindViewById<TextView>(Resource.Id.textView13);

            var wrong = FindViewById<TextView>(Resource.Id.textView14);
            var wrongfeed = FindViewById<TextView>(Resource.Id.textView14).Text;

            string feed = validate("B");
            int score;

            if (feed == "Correct")
            {

                int converted = Convert.ToInt32(scorepoints);
                score = converted;
                int convertedpts = Convert.ToInt32(points);
                score = score + convertedpts;
                RunOnUiThread(() => scorepoints = score.ToString());
                RunOnUiThread(() => correct.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Gone);

                RunOnUiThread(() => SendScore(scorepoints.ToString()));
            }

            else
            {
                RunOnUiThread(() => correct.Visibility = ViewStates.Gone);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrongfeed = "Wrong! The Right Answer is " + correctanswer.ToUpper());
            }
        }

        public void Choice3(object sender, EventArgs e)
        {
            var scorepts = FindViewById<TextView>(Resource.Id.textView4);
            string scorepoints = FindViewById<TextView>(Resource.Id.textView4).Text;

            var correct = FindViewById<TextView>(Resource.Id.textView13);

            var wrong = FindViewById<TextView>(Resource.Id.textView14);
            var wrongfeed = FindViewById<TextView>(Resource.Id.textView14).Text;

            string feed = validate("C");
            int score;

            if (feed == "Correct")
            {
                var converted = Convert.ToInt32(scorepoints);
                score = converted;
                var convertedpts = Convert.ToInt32(points);
                score = score + convertedpts;
                RunOnUiThread(() => scorepoints = score.ToString());
                RunOnUiThread(() => correct.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Gone);

                RunOnUiThread(() => SendScore(scorepoints.ToString()));
            }

            else
            {
                RunOnUiThread(() => correct.Visibility = ViewStates.Gone);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrongfeed = "Wrong! The Right Answer is " + correctanswer.ToUpper());
            }
        }


        public void Choice4(object sender, EventArgs e)
        {
            var scorepts = FindViewById<TextView>(Resource.Id.textView4);
            string scorepoints = FindViewById<TextView>(Resource.Id.textView4).Text;

            var correct = FindViewById<TextView>(Resource.Id.textView13);

            var wrong = FindViewById<TextView>(Resource.Id.textView14);
            var wrongfeed = FindViewById<TextView>(Resource.Id.textView14).Text;

            string feed = validate("D");
            int score;

            if (feed == "Correct")
            {
                var converted = Convert.ToInt32(scorepoints);
                score = converted;
                var convertedpts = Convert.ToInt32(points);
                score = score + convertedpts;
                RunOnUiThread(() => scorepoints = score.ToString());
                RunOnUiThread(() => correct.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Gone);

                RunOnUiThread(() => SendScore(scorepoints.ToString()));
            }

            else
            {
                RunOnUiThread(() => correct.Visibility = ViewStates.Gone);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrongfeed = "Wrong! The Right Answer is " + correctanswer.ToUpper());
            }
        }

        public void EnterAnswer(object sender, EventArgs e)
        {
            var shortans = FindViewById<EditText>(Resource.Id.editText1);
            string shortanswer = "";
            RunOnUiThread(() => shortans.Text = shortanswer);
            var scorepts = FindViewById<TextView>(Resource.Id.textView4);
            string scorepoints = "";
            RunOnUiThread(() => scorepts.Text = scorepoints);

            var correct = FindViewById<TextView>(Resource.Id.textView13);
            var wrong = FindViewById<TextView>(Resource.Id.textView14);
            string wrongfeed = "";
            RunOnUiThread(() => wrong.Text = wrongfeed);

            string feed = validateSA(shortanswer);
            int score;

            if (feed == "Correct")
            {

                score = Convert.ToInt32(scorepoints);
                score = score + Convert.ToInt32(points);
                RunOnUiThread(() => scorepoints = score.ToString());

                RunOnUiThread(() => correct.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Gone);
                
                RunOnUiThread(() => SendScore(scorepoints.ToString()));
            }

            else
            {
                RunOnUiThread(() => correct.Visibility = ViewStates.Gone);
                RunOnUiThread(() => wrong.Visibility = ViewStates.Visible);
                RunOnUiThread(() => wrongfeed = "Wrong! The Right Answer is " + correctanswer.ToUpper());
            }

            RunOnUiThread(() => shortanswer = "");

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
            try
            {
                //(message.Contains("StartGame"))
                var title = FindViewById<TextView>(Resource.Id.textView6);
                var content = FindViewById<TextView>(Resource.Id.textView7);

                //(message.Contains("C1o2m3pute"))
                var pscoretext = FindViewById<TextView>(Resource.Id.textView8);
                var ptotalscores = FindViewById<TextView>(Resource.Id.textView9);
                var ptotalitems = FindViewById<TextView>(Resource.Id.textView10);
                var pfeedback = FindViewById<TextView>(Resource.Id.textView11);

                var scorepts = FindViewById<TextView>(Resource.Id.textView4).Text;
                var totalscores = FindViewById<TextView>(Resource.Id.textView9).Text;
                var totalitems = FindViewById<TextView>(Resource.Id.textView10).Text;
                var feedback = FindViewById<TextView>(Resource.Id.textView11).Text;

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

                string question = FindViewById<TextView>(Resource.Id.textView12).Text;
                string choice1 = FindViewById<Button>(Resource.Id.button1).Text;
                string choice2 = FindViewById<Button>(Resource.Id.button2).Text;
                string choice3 = FindViewById<Button>(Resource.Id.button3).Text;
                string choice4 = FindViewById<Button>(Resource.Id.button4).Text;

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
                    Receive();
                    
                }

                else if (message.Contains("C1o2m3pute"))
                {

                    int rawscore = Convert.ToInt32(scorepts);
                    RunOnUiThread(() => totalscores = rawscore.ToString());
                    RunOnUiThread(() => totalitems = Total);

                    double converted_total = Convert.ToInt32(Total);
                    double converted_rawscore = rawscore;
                    double comp = (converted_rawscore / converted_total) * 100;
                    int compute = Convert.ToInt32(comp);

                    if (compute < Convert.ToInt32("60"))
                    {
                        RunOnUiThread(() => feedback = compute.ToString() + "% You Need Improvement, Study and Play!");
                    }
                    else if (compute == Convert.ToInt32("100"))
                    {
                        RunOnUiThread(() => feedback = compute.ToString() + "% Excellent!");
                    }
                    else
                    {
                        RunOnUiThread(() => feedback = compute.ToString() + "% Not Bad!, aim for a Perfect Score Next Time ");
                    }

                    RunOnUiThread(() => pscoretext.Visibility = ViewStates.Visible);
                    RunOnUiThread(() => ptotalscores.Visibility = ViewStates.Visible);
                    RunOnUiThread(() => ptotalitems.Visibility = ViewStates.Visible);
                    RunOnUiThread(() => pfeedback.Visibility = ViewStates.Visible);

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

                        /*
                        RunOnUiThread(() => Toast.MakeText(this, array[0], ToastLength.Long).Show());
                        RunOnUiThread(() => Toast.MakeText(this, array[1], ToastLength.Long).Show());
                        //RunOnUiThread(() => question = "Howmuch");
                        //RunOnUiThread(() => choice1 = "Yes");
                        RunOnUiThread(() => choice2 = array[2].ToString());
                        RunOnUiThread(() => choice3 = array[3].ToString());
                        RunOnUiThread(() => choice4 = array[4].ToString());
                        */
                        RunOnUiThread(() => question = array[0]);
                        RunOnUiThread(() => choice1 = array[1]);
                        RunOnUiThread(() => choice2 = array[2]);
                        RunOnUiThread(() => choice3 = array[3]);
                        RunOnUiThread(() => choice4 = array[4]);
                        
                        correctanswer = array[5].ToString();  //CorrectAnswer
                        points = array[8];
                        Total = array[10];

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

                        //var timer1 = FindViewById<TextView>(Resource.Id.textView5);
                        //this.Invoke(new ThreadStart(delegate () { timer1.Enabled = true; timer1.Start(); }));



                    }
                    else if (array[11].ToString() == "True/False")
                    {
                        var correct = FindViewById<TextView>(Resource.Id.textView13);
                        var wrong = FindViewById<TextView>(Resource.Id.textView14);

                        RunOnUiThread(() => correct.Visibility = ViewStates.Gone);
                        RunOnUiThread(() => wrong.Visibility = ViewStates.Gone);

                        RunOnUiThread(() => question = array[0].ToString());  //Question
                        correctanswer = array[5].ToString();  //CorrectAnswer
                        points = array[8].ToString();
                        Total = array[10].ToString();

                        RunOnUiThread(() => enterans.Visibility = ViewStates.Gone);
                        RunOnUiThread(() => shortans.Visibility = ViewStates.Gone);
                        RunOnUiThread(() => pchoice1.Visibility = ViewStates.Gone);
                        RunOnUiThread(() => pchoice2.Visibility = ViewStates.Gone);
                        RunOnUiThread(() => pchoice3.Visibility = ViewStates.Gone);
                        RunOnUiThread(() => pchoice4.Visibility = ViewStates.Gone);
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

                        //this.Invoke(new ThreadStart(delegate () { timer1.Enabled = true; timer1.Start(); }));
                    }

                    else
                    {

                        var correct = FindViewById<TextView>(Resource.Id.textView13);
                        var wrong = FindViewById<TextView>(Resource.Id.textView14);

                        RunOnUiThread(() => correct.Visibility = ViewStates.Gone);
                        RunOnUiThread(() => wrong.Visibility = ViewStates.Gone);

                        RunOnUiThread(() => question = array[0].ToString());  //Question
                        correctanswer = array[5].ToString(); ;  //CorrectAnswer
                        points = array[8].ToString();
                        Total = array[10].ToString();

                        RunOnUiThread(() => enterans.Visibility = ViewStates.Visible);
                        RunOnUiThread(() => shortans.Visibility = ViewStates.Visible);
                        RunOnUiThread(() => pchoice1.Visibility = ViewStates.Gone);
                        RunOnUiThread(() => pchoice2.Visibility = ViewStates.Gone);
                        RunOnUiThread(() => pchoice3.Visibility = ViewStates.Gone);
                        RunOnUiThread(() => pchoice4.Visibility = ViewStates.Gone);
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
            var title = FindViewById<TextView>(Resource.Id.textView6);
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