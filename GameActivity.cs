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
using System.Configuration;

namespace TooLearnAndroid //MAGHILING KA SA BABA FOR PALATANDAAN ||  private void bunifuFlatButton2_Click(object sender, EventArgs e) nexttarget to fix
{
    [Activity(Label = "GameActivity")]
    public class GameActivity : Activity
    {/*
        private TcpClient _client = new TcpClient();
        private const int _buffer_size = 2048;
        private byte[] _buffer = new byte[_buffer_size];
        private string _IPAddress = Program.serverIP;
        private const int _PORT = 13000;

        string GameType = LobbyActivity.GameType;

        string correctanswer, points;

        string time;
        int convertedtime;
        //GOING TO FIX LATER CONFIG MANAGER
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_game);
            var choice1 = FindViewById<Button>(Resource.Id.button1);
            var choice2 = FindViewById<Button>(Resource.Id.button2);
            var choice3 = FindViewById<Button>(Resource.Id.button3);
            var choice4 = FindViewById<Button>(Resource.Id.button4);
            var bunichoice1 = FindViewById<Button>(Resource.Id.button5).Text;
            var bunichoice2 = FindViewById<Button>(Resource.Id.button6).Text;
            var bunichoice3 = FindViewById<Button>(Resource.Id.button7).Text;
            var bunichoice4 = FindViewById<Button>(Resource.Id.button8).Text;
            var rightansw = FindViewById<Button>(Resource.Id.button9);

            rightansw.Click += EnterAnswerActivity;
            choice1.Click += delegate
            {
                Send(bunichoice1);
            };

            choice2.Click += delegate
            {
                Send(bunichoice2);
            };

            choice3.Click += delegate
            {
                Send(bunichoice3);
            };

            choice4.Click += delegate
            {
                Send(bunichoice4);
            };

            StartConnect();
            GameParticipant_Load();
        }

        public void EnterAnswerActivity(object sender, EventArgs e)
        {
            var shortanswer = FindViewById<EditText>(Resource.Id.editText1);
            var numberofscore = FindViewById<TextView>(Resource.Id.textView3).Text;
            var correct = FindViewById<TextView>(Resource.Id.textView4).Text;
            var wrong = FindViewById<TextView>(Resource.Id.textView5).Text;
            string shortans = shortanswer.Text;
            string feed = validate(shortans);
            int score;

            if (feed == "Correct")
            {

                score = Convert.ToInt32(numberofscore);
                score = score + Convert.ToInt32(points);
                numberofscore = score.ToString();
                panel3.Visible = true; // GOING TO FIX THE PANEL LATER
                panel2.Visible = false;
                correct = "Correct!";
            }

            else
            {
                panel2.Visible = true;
                panel3.Visible = false;
                wrong = "Wrong, the Right Answer is " + correctanswer;
            }
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

                    ThreadHelper.PanelOut(this, panel1, false);
                    Receive();

                }

                else //else if start game visible false
                {
                    




                    var array = message.Split('\n');


                    if (array[10].ToString() == "Multiple Choice")//Item Format
                    {
                        ThreadHelper.PanelOut(this, panel2, false);
                        ThreadHelper.PanelOut(this, panel3, false);

                        ThreadHelper.lblAddLabel(this, label1, array[0].ToString());  //Question
                        ThreadHelper.btnAddTxtButton(this, bunifuFlatButton1, array[1].ToString());  //A
                        ThreadHelper.btnAddTxtButton(this, bunifuFlatButton2, array[2].ToString());  //B
                        ThreadHelper.btnAddTxtButton(this, bunifuFlatButton3, array[3].ToString());  //C
                        ThreadHelper.btnAddTxtButton(this, bunifuFlatButton4, array[4].ToString());  //D
                        correctanswer = array[5].ToString();  //CorrectAnswer
                        points = array[8].ToString();




                        ThreadHelper.imgbtnIN(this, bunifuImageButton1, false);
                        ThreadHelper.BunifuBoxHide(this, bunifuMetroTextbox1, false);
                        ThreadHelper.ControlHide(this, bunifuFlatButton5, false);
                        ThreadHelper.ControlHide(this, bunifuFlatButton6, false);

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


                        timer1.Start(); /// dae nagana after mag stop dae na na start to be fixed



                    }
                    else if (array[10].ToString() == "True/False")
                    {
                        ThreadHelper.PanelOut(this, panel2, false);
                        ThreadHelper.PanelOut(this, panel3, false);

                        ThreadHelper.lblAddLabel(this, label1, array[0].ToString());  //Question
                        correctanswer = array[5].ToString();  //CorrectAnswer
                        points = array[8].ToString();


                        ThreadHelper.imgbtnIN(this, bunifuImageButton1, false);
                        ThreadHelper.BunifuBoxHide(this, bunifuMetroTextbox1, false);
                        ThreadHelper.ControlHide(this, bunifuFlatButton1, false);
                        ThreadHelper.ControlHide(this, bunifuFlatButton2, false);
                        ThreadHelper.ControlHide(this, bunifuFlatButton3, false);
                        ThreadHelper.ControlHide(this, bunifuFlatButton4, false);
                        ThreadHelper.ControlHide(this, bunifuFlatButton5, true);
                        ThreadHelper.ControlHide(this, bunifuFlatButton6, true);




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



                        timer1.Start();
                    }

                    else
                    {

                        ThreadHelper.PanelOut(this, panel2, false);
                        ThreadHelper.PanelOut(this, panel3, false);

                        ThreadHelper.lblAddLabel(this, label1, array[0].ToString());  //Question
                        correctanswer = array[5].ToString(); ;  //CorrectAnswer
                        points = array[8].ToString();


                        ThreadHelper.imgbtnIN(this, bunifuImageButton1, true);
                        ThreadHelper.BunifuBoxHide(this, bunifuMetroTextbox1, true);
                        ThreadHelper.ControlHide(this, bunifuFlatButton1, false);
                        ThreadHelper.ControlHide(this, bunifuFlatButton2, false);
                        ThreadHelper.ControlHide(this, bunifuFlatButton3, false);
                        ThreadHelper.ControlHide(this, bunifuFlatButton4, false);
                        ThreadHelper.ControlHide(this, bunifuFlatButton5, false);
                        ThreadHelper.ControlHide(this, bunifuFlatButton6, false);

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




                        timer1.Start();


                    }



                    Receive();

                }
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

        private void GameParticipant_Load()
        {
            var gametype = FindViewById<TextView>(Resource.Id.textView1);
            var gamerules = FindViewById<TextView>(Resource.Id.textView2);
            string typegame = gametype.Text;
            string rules = gamerules.Text;
            if (GameType == "QB")
            {
                typegame = "Quiz Bee";
                rules = System.IO.File.ReadAllText(@"QuizBeeRules.txt");

            }

            else if (GameType == "PZ")
            {
                typegame = "Picture Puzzle";
                rules = System.IO.File.ReadAllText(@"PicturePuzzleRules.txt");
            }
        }

        private string validate(string answer)
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

        //NAPUNDO AKO SA MGA BUTTONS BE BACK LATER || MINUS MULT CHOICE BUTTON
        //AAYUSON ANG THREADHELPER SAKA SI PAG LAAG VARIABLE SA MGA OBJECTS

    */}
}