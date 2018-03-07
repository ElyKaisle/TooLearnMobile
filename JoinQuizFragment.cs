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
    public class JoinQuizFragment : Fragment
    {
        Button join;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.fragment_joinquiz, container, false);
            join = view.FindViewById<Button>(Resource.Id.button1);
            join.Click += StartLobbyActivity;
            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
        void StartLobbyActivity(object sender, EventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(LobbyActivity));
            StartActivity(intent);
        }
    }
}