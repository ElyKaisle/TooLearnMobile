using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;


namespace TooLearnAndroid
{
        [Activity(Label = "TooLearn", Theme = "@style/Theme.DesignDemo")]
        public class MainActivity : Activity
        {
        public static string Role;
        protected override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);

                // Set our view from the "main" layout resource
                SetContentView(Resource.Layout.Main);

                Button classroom_button = FindViewById<Button>(Resource.Id.button1);
                Button public_button = FindViewById<Button>(Resource.Id.button2);
                Button group_button = FindViewById<Button>(Resource.Id.button3);

                classroom_button.Click += delegate
                {
                    Role = "Individual";
                    var serverconnection = new Intent(this, typeof(ServerConnectionActivity));
                    StartActivity(serverconnection);
                    
                };

                public_button.Click += delegate
                {
                    StartActivity(typeof(PublicJoinQuizActivity));
                };

                group_button.Click += delegate
                {
                    Role = "Group";
                    var serverconnection = new Intent(this, typeof(ServerConnectionActivity));
                    serverconnection.PutExtra("Group", Program.Role);
                    StartActivity(serverconnection);

                };
            }
        }
}

