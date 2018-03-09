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

using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Data.SqlClient;

namespace TooLearnAndroid
{
    [Activity(Label = "Join Quiz", Theme = "@style/Theme.DesignDemo")]
    public class PublicJoinQuizActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_publicjoinquiz);
            Button join_button = FindViewById<Button>(Resource.Id.button1);

            join_button.Click += delegate
            {
                StartActivity(typeof(NicknameActivity));
            };
        }

        

    }
}