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

        public void GameParticipant_Load()
        {
            
            try
            {
                var rulescontent8 = View.FindViewById<TextView>(Resource.Id.textEleven).Text;
                var rulescontentv8 = View.FindViewById<TextView>(Resource.Id.textEleven);
                var rulescontent7 = View.FindViewById<TextView>(Resource.Id.textTen).Text;
                var rulescontentv7 = View.FindViewById<TextView>(Resource.Id.textTen);
                var rulestitle = View.FindViewById<TextView>(Resource.Id.textView2).Text;
                var rulescontent = View.FindViewById<TextView>(Resource.Id.textView3).Text;
                var rulescontentv = View.FindViewById<TextView>(Resource.Id.textView3);
                var rulescontent1 = View.FindViewById<TextView>(Resource.Id.textView4).Text;
                var rulescontentv1 = View.FindViewById<TextView>(Resource.Id.textView4);
                var rulescontent2 = View.FindViewById<TextView>(Resource.Id.textView5).Text;
                var rulescontentv2 = View.FindViewById<TextView>(Resource.Id.textView5);
                var rulescontent3 = View.FindViewById<TextView>(Resource.Id.textView6).Text;
                var rulescontentv3 = View.FindViewById<TextView>(Resource.Id.textView6);
                var rulescontent4 = View.FindViewById<TextView>(Resource.Id.textView7).Text;
                var rulescontentv4 = View.FindViewById<TextView>(Resource.Id.textView7);
                var rulescontent5 = View.FindViewById<TextView>(Resource.Id.textView8).Text;
                var rulescontentv5 = View.FindViewById<TextView>(Resource.Id.textView8);
                var rulescontent6 = View.FindViewById<TextView>(Resource.Id.textView9).Text;
                var rulescontentv6 = View.FindViewById<TextView>(Resource.Id.textView9);
                if (GameType == "QB")
                {
                    rulestitle = "Quiz Bee";
                    rulescontentv.Visibility = ViewStates.Visible;
                    rulescontent = Context.Resources.GetString(Resource.String.qbrules1);
                    rulescontentv1.Visibility = ViewStates.Visible;
                    rulescontent1 = Context.Resources.GetString(Resource.String.qbrules2);
                    rulescontentv2.Visibility = ViewStates.Visible;
                    rulescontent2 = Context.Resources.GetString(Resource.String.qbrules3);
                    rulescontentv3.Visibility = ViewStates.Visible;
                    rulescontent3 = Context.Resources.GetString(Resource.String.qbrules4);
                    rulescontentv4.Visibility = ViewStates.Visible;
                    rulescontent4 = Context.Resources.GetString(Resource.String.qbrules5);
                    rulescontentv5.Visibility = ViewStates.Visible;
                    rulescontent5 = Context.Resources.GetString(Resource.String.qbrules6);
                    rulescontentv6.Visibility = ViewStates.Visible;

                }

                else if (GameType == "PZ")
                {
                    rulestitle = "Picture Puzzle";
                    rulescontentv.Visibility = ViewStates.Visible;
                    rulescontent = Context.Resources.GetString(Resource.String.pprules1);
                    rulescontentv1.Visibility = ViewStates.Visible;
                    rulescontent1 = Context.Resources.GetString(Resource.String.pprules2);
                    rulescontentv2.Visibility = ViewStates.Visible;
                    rulescontent2 = Context.Resources.GetString(Resource.String.pprules3);
                    rulescontentv3.Visibility = ViewStates.Visible;
                    rulescontent3 = Context.Resources.GetString(Resource.String.pprules4);
                    rulescontentv4.Visibility = ViewStates.Visible;
                    rulescontent4 = Context.Resources.GetString(Resource.String.pprules5);
                    rulescontentv5.Visibility = ViewStates.Visible;
                    rulescontent5 = Context.Resources.GetString(Resource.String.pprules6);
                    rulescontentv6.Visibility = ViewStates.Visible;
                    rulescontent6 = Context.Resources.GetString(Resource.String.pprules7);
                    rulescontentv7.Visibility = ViewStates.Visible;
                    rulescontent7 = Context.Resources.GetString(Resource.String.pprules8);
                    rulescontentv8.Visibility = ViewStates.Visible;
                    rulescontent8 = Context.Resources.GetString(Resource.String.pprules9);
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this.Activity, ex.ToString(), ToastLength.Long).Show();
            }


        }
    }
}