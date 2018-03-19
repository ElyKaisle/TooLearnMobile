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
            var rulescontent8 = View.FindViewById<TextView>(Resource.Id.textEleven).Text;
            var rulescontent7 = View.FindViewById<TextView>(Resource.Id.textTen).Text;
            var rulestitle = View.FindViewById<TextView>(Resource.Id.textView2).Text;
            var rulescontent = View.FindViewById<TextView>(Resource.Id.textView3).Text;
            var rulescontent1 = View.FindViewById<TextView>(Resource.Id.textView4).Text;
            var rulescontent2 = View.FindViewById<TextView>(Resource.Id.textView5).Text;
            var rulescontent3 = View.FindViewById<TextView>(Resource.Id.textView6).Text;
            var rulescontent4 = View.FindViewById<TextView>(Resource.Id.textView7).Text;
            var rulescontent5 = View.FindViewById<TextView>(Resource.Id.textView8).Text;
            var rulescontent6 = View.FindViewById<TextView>(Resource.Id.textView9).Text;


            if (GameType == "QB")   
            {
                rulestitle = "Quiz Bee";
                rulescontent = Context.Resources.GetString(Resource.String.qbrules1);
                rulescontent1 = Context.Resources.GetString(Resource.String.qbrules2);
                rulescontent2 = Context.Resources.GetString(Resource.String.qbrules3);
                rulescontent3 = Context.Resources.GetString(Resource.String.qbrules4);
                rulescontent4 = Context.Resources.GetString(Resource.String.qbrules5);
                rulescontent5 = Context.Resources.GetString(Resource.String.qbrules6);

            }

            else if (GameType == "PZ")
            {
                rulestitle = "Picture Puzzle";
                rulescontent = Context.Resources.GetString(Resource.String.pprules1);
                rulescontent1 = Context.Resources.GetString(Resource.String.pprules2);
                rulescontent2 = Context.Resources.GetString(Resource.String.pprules3);
                rulescontent3 = Context.Resources.GetString(Resource.String.pprules4);
                rulescontent4 = Context.Resources.GetString(Resource.String.pprules5);
                rulescontent5 = Context.Resources.GetString(Resource.String.pprules6);
                rulescontent6 = Context.Resources.GetString(Resource.String.pprules7);
                rulescontent7 = Context.Resources.GetString(Resource.String.pprules8);
                rulescontent8 = Context.Resources.GetString(Resource.String.pprules9);
            }



        }
    }
}