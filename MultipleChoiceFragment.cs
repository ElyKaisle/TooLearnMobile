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

namespace TooLearnAndroid
{
    public class MultipleChoiceFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.fragment_multiplechoice, container, false);
            AddTextButtonActivity();
            var choice1 = view.FindViewById(Resource.Id.button1);
            var choice2 = view.FindViewById(Resource.Id.button2);
            var choice3 = view.FindViewById(Resource.Id.button3);
            var choice4 = view.FindViewById(Resource.Id.button4);
            choice1.Click += ChoiceOneActivity;
            return view;
        }

        public void AddTextButtonActivity()
        {
            var question = View.FindViewById<EditText>(Resource.Id.textView1).Text;
            question = Array[0].ToString();
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
        }

        private void ChoiceOneActivity(object sender, EventArgs e)
        {/*
            string feed = validate("A");
            int score;

            if (feed == "Correct")
            {

                score = Convert.ToInt32(bunifuCustomLabel5.Text);
                score = score + Convert.ToInt32(points);
                bunifuCustomLabel5.Text = score.ToString();
                panel3.Visible = true;
                panel2.Visible = false;
                label5.Text = "Correct!";
            }

            else
            {
                panel2.Visible = true;
                panel3.Visible = false;
                label4.Text = "Wrong! The Right Answer is " + correctanswer.ToUpper();
           */ }
        }
    }
}