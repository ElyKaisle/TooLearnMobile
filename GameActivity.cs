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

namespace TooLearnAndroid 
{
    [Activity(Label = "GameActivity")]
    public class GameActivity : Activity
    {
        private TcpClient _client = new TcpClient();
        private const int _buffer_size = 2048;
        private byte[] _buffer = new byte[_buffer_size];
        private string _IPAddress = Program.serverIP;
        private const int _PORT = 1433;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_game);
            var timersec = FindViewById<TextView>(Resource.Id.textView5);
            //TypeOfGames();
        }

        /*
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

        private void BeginWriteCallback(IAsyncResult ar)
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
                    RulesFragment fragment = new RulesFragment();
                    FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
                    fragmentTx.Replace(Resource.Id.fragment_container, fragment);
                    fragmentTx.Commit();
                    Receive();

                }

                else if (message.Contains("C1o2m3pute"))
                {
                    int rawscore = Convert.ToInt32(scorepts);
                    //label6.Text = rawscore.ToString();
                    ThreadHelper.SetText(this, label6, rawscore.ToString());
                    //label10.Text = Total.ToString();
                    ThreadHelper.SetText(this, label10, Total);
                    ThreadHelper.PanelOut(this, panel4, true);
                    //panel4.Visible = true;
                    float compute = (rawscore / Convert.ToInt32(Total)) * 100;//bawal zero

                    if (compute < Convert.ToInt32("60"))
                    {
                        // label9.Text = compute + " You Needs Improvement, Study and Play!";

                        ThreadHelper.SetText(this, label9, compute + " You Needs Improvement, Study and Play!");
                    }
                    else if (compute == Convert.ToInt32("100"))
                    {
                        //label9.Text = compute + " Excellent!";
                        ThreadHelper.SetText(this, label9, compute + " Excellent!");
                    }

                    else
                    {
                        //label9.Text = compute + " Not Bad!, aim 100 Next Time :)";
                        ThreadHelper.SetText(this, label9, compute + " Not Bad!, aim Perfect Next Time ");
                    }


                }

                else if (message.Contains("CloseThis"))
                {
                    Send("DISCONNECT");
                    ThreadHelper.Hide(this);
                }

                else
                {


                    var array = message.Split('\n');






                    if (array[11].ToString() == "Multiple Choice")//Item Format
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

                        Total = array[10].ToString();


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






                        this.Invoke(new ThreadStart(delegate () { timer1.Enabled = true; timer1.Start(); }));



                    }
                    else if (array[11].ToString() == "True/False")
                    {
                        ThreadHelper.PanelOut(this, panel2, false);
                        ThreadHelper.PanelOut(this, panel3, false);

                        ThreadHelper.lblAddLabel(this, label1, array[0].ToString());  //Question
                        correctanswer = array[5].ToString();  //CorrectAnswer
                        points = array[8].ToString();
                        Total = array[10].ToString();

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




                        this.Invoke(new ThreadStart(delegate () { timer1.Enabled = true; timer1.Start(); }));
                    }

                    else
                    {

                        ThreadHelper.PanelOut(this, panel2, false);
                        ThreadHelper.PanelOut(this, panel3, false);

                        ThreadHelper.lblAddLabel(this, label1, array[0].ToString());  //Question
                        correctanswer = array[5].ToString(); ;  //CorrectAnswer
                        points = array[8].ToString();
                        Total = array[10].ToString();

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


                        this.Invoke(new ThreadStart(delegate () { timer1.Enabled = true; timer1.Start(); }));





                    }



                    Receive();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void TypeOfGames()
        {

        }
        */
    }
}