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

            
            var view = inflater.Inflate(Resource.Layout.fragment_rules, container, false);
            GameParticipant_Load();
            return base.OnCreateView(inflater, container, savedInstanceState);
            
        }

        public void GameParticipant_Load()
        {
            
            try
            {
                var rulestitle = View.FindViewById<TextView>(Resource.Id.textTitle).Text;
                var rulescontentv = View.FindViewById<TextView>(Resource.Id.textContent);
                string rulescontent = rulescontentv.Text;
                

                if (GameType == "QB")
                {
                    rulestitle = "Quiz Bee";
                    using (StreamReader sr = new StreamReader(Application.Context.Assets.Open("QuizBeeRules.txt")))
                    {
                        rulescontent = sr.ReadToEnd();
                    }
                }

                else if (GameType == "PZ")
                {
                    rulestitle = "Picture Puzzle";
                    using (StreamReader sr = new StreamReader(Application.Context.Assets.Open("PicturePuzzleRules.txt")))
                    {
                        rulescontent = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this.Activity, ex.ToString(), ToastLength.Long).Show();
            }


        }
    }
}