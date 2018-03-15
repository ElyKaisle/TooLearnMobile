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
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_game);
            var scorepts = FindViewById<TextView>(Resource.Id.textView4);
            var timersec = FindViewById<TextView>(Resource.Id.textView5);
        }
    }
}