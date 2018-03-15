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

            //return base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.fragment_rules, container, false);
            GameParticipant_Load();
            return view;
        }

        private void GameParticipant_Load()
        {

            var rulestitle = View.FindViewById<TextView>(Resource.Id.textView2).Text;
            var rulescontent = View.FindViewById<TextView>(Resource.Id.textView3).Text;

            if (GameType == "QB")
            {
                rulestitle = "Quiz Bee";
                rulescontent = System.IO.File.ReadAllText("drawable/QuizBeeRules.txt");

            }

            else if (GameType == "PZ")
            {
                rulestitle = "Picture Puzzle";
                rulescontent = System.IO.File.ReadAllText("drawable/PicturePuzzleRules.txt");
            }



        }
    }
}