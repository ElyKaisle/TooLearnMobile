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
    public class ShortAnswerFragment : Fragment
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
            var view = inflater.Inflate(Resource.Layout.fragment_shortanswer, container, false);
            var enter = view.FindViewById<Button>(Resource.Id.button1);
            enter.Click += EnterAnswerActivity;
            AddGameActivity();
            return view;
        }

        public void AddGameActivity()
        {
            var question = View.FindViewById<TextView>(Resource.Id.textView1).Text;
            var input = View.FindViewById<EditText>(Resource.Id.editText1).Text;
            var enter = View.FindViewById<Button>(Resource.Id.button1).Text;

            question = GameActivity.array[0].ToString();
            GameActivity.correctanswer = GameActivity.array[5].ToString();
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

        private void EnterAnswerActivity(object sender, EventArgs e)
        {
            var enter = View.FindViewById<Button>(Resource.Id.button1).Text;
            string feed = GameActivity.validateSA(enter);
            int score;
            var scorepts = View.FindViewById<TextView>(Resource.Id.textView4).Text;

            if (feed == "Correct")
            {

                score = Convert.ToInt32(scorepts);
                score = score + Convert.ToInt32(GameActivity.points);
                scorepts = score.ToString();

                RightAnswerFragment fragment = new RightAnswerFragment();
                FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
                fragmentTx.Replace(Resource.Id.fragment_container, fragment);
                fragmentTx.Commit();

                GameActivity.SendScore(scorepts.ToString());
            }
        }
    }
}