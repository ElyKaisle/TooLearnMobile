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
            var choice1 = view.FindViewById<Button>(Resource.Id.button1);
            var choice2 = view.FindViewById<Button>(Resource.Id.button2);
            var choice3 = view.FindViewById<Button>(Resource.Id.button3);
            var choice4 = view.FindViewById<Button>(Resource.Id.button4);
            choice1.Click += ChoiceOneActivity;
            choice2.Click += ChoiceTwoActivity;
            choice3.Click += ChoiceThreeActivity;
            choice4.Click += ChoiceFourActivity;
            return view;
        }

        public void AddTextButtonActivity()
        {
            var question = View.FindViewById<EditText>(Resource.Id.textView1).Text;
            question = GameActivity.array[0].ToString();
            var choice1 = View.FindViewById<Button>(Resource.Id.button1).Text;
            var choice2 = View.FindViewById<Button>(Resource.Id.button2).Text;
            var choice3 = View.FindViewById<Button>(Resource.Id.button3).Text;
            var choice4 = View.FindViewById<Button>(Resource.Id.button4).Text;
            choice1 = GameActivity.array[1].ToString();
            choice2 = GameActivity.array[2].ToString();
            choice3 = GameActivity.array[3].ToString();
            choice4 = GameActivity.array[4].ToString();

            GameActivity.correctanswer = GameActivity.array[5].ToString();  //CorrectAnswer
            GameActivity.points = GameActivity.array[8].ToString();

            GameActivity.Total = GameActivity.array[10].ToString();

            string str = GameActivity.array[7].ToString();
            int index = str.IndexOf('(');

            if (index >= 0)
            {
                GameActivity.time = str.Substring(0, index);



            }
            else
            {

                GameActivity.time = str;
            }


            GameActivity.convertedtime = Convert.ToInt32(GameActivity.time);//timer


            string cut = GameActivity.array[7].ToString();
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
                GameActivity.convertedtime = GameActivity.convertedtime * 60;
            }
        }

        private void ChoiceOneActivity(object sender, EventArgs e)
        {
            string feed = GameActivity.validate("A");
            int score;
            var scorepts = View.FindViewById<TextView>(Resource.Id.textView4).Text;

            if (feed == "Correct")
            {

                score = Convert.ToInt32(scorepts);
                score = score + Convert.ToInt32(GameActivity.points);
                scorepts = score.ToString();
                RightAnswerFragment fragment5 = new RightAnswerFragment();
                FragmentTransaction fragmentTx5 = this.FragmentManager.BeginTransaction();
                fragmentTx5.Replace(Resource.Id.fragment_container, fragment5);
                fragmentTx5.Commit();

                GameActivity.SendScore(scorepts.ToString());
            }

            else
            {
                WrongAnswerFragment fragment6 = new WrongAnswerFragment();
                FragmentTransaction fragmentTx6 = this.FragmentManager.BeginTransaction();
                fragmentTx6.Replace(Resource.Id.fragment_container, fragment6);
                fragmentTx6.Commit();
                
            }
        }

        private void ChoiceTwoActivity(object sender, EventArgs e)
        {
            string feed = GameActivity.validate("B");
            int score;
            var scorepts = View.FindViewById<TextView>(Resource.Id.textView4).Text;

            if (feed == "Correct")
            {

                score = Convert.ToInt32(scorepts);
                score = score + Convert.ToInt32(GameActivity.points);
                scorepts = score.ToString();
                RightAnswerFragment fragment5 = new RightAnswerFragment();
                FragmentTransaction fragmentTx5 = this.FragmentManager.BeginTransaction();
                fragmentTx5.Replace(Resource.Id.fragment_container, fragment5);
                fragmentTx5.Commit();

                GameActivity.SendScore(scorepts.ToString());
            }

            else
            {
                WrongAnswerFragment fragment6 = new WrongAnswerFragment();
                FragmentTransaction fragmentTx6 = this.FragmentManager.BeginTransaction();
                fragmentTx6.Replace(Resource.Id.fragment_container, fragment6);
                fragmentTx6.Commit();
            }
        }

        private void ChoiceThreeActivity(object sender, EventArgs e)
        {
            string feed = GameActivity.validate("C");
            int score;
            var scorepts = View.FindViewById<TextView>(Resource.Id.textView4).Text;

            if (feed == "Correct")
            {

                score = Convert.ToInt32(scorepts);
                score = score + Convert.ToInt32(GameActivity.points);
                scorepts = score.ToString();
                RightAnswerFragment fragment5 = new RightAnswerFragment();
                FragmentTransaction fragmentTx5 = this.FragmentManager.BeginTransaction();
                fragmentTx5.Replace(Resource.Id.fragment_container, fragment5);
                fragmentTx5.Commit();

                GameActivity.SendScore(scorepts.ToString());
            }

            else
            {
                WrongAnswerFragment fragment6 = new WrongAnswerFragment();
                FragmentTransaction fragmentTx6 = this.FragmentManager.BeginTransaction();
                fragmentTx6.Replace(Resource.Id.fragment_container, fragment6);
                fragmentTx6.Commit();
            }
        }

        private void ChoiceFourActivity(object sender, EventArgs e)
        {
            string feed = GameActivity.validate("D");
            int score;
            var scorepts = View.FindViewById<TextView>(Resource.Id.textView4).Text;

            if (feed == "Correct")
            {

                score = Convert.ToInt32(scorepts);
                score = score + Convert.ToInt32(GameActivity.points);
                scorepts = score.ToString();
                RightAnswerFragment fragment5 = new RightAnswerFragment();
                FragmentTransaction fragmentTx5 = this.FragmentManager.BeginTransaction();
                fragmentTx5.Replace(Resource.Id.fragment_container, fragment5);
                fragmentTx5.Commit();

                GameActivity.SendScore(scorepts.ToString());
            }

            else
            {
                WrongAnswerFragment fragment6 = new WrongAnswerFragment();
                FragmentTransaction fragmentTx6 = this.FragmentManager.BeginTransaction();
                fragmentTx6.Replace(Resource.Id.fragment_container, fragment6);
                fragmentTx6.Commit();
            }
        }
    }
}
