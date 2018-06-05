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

                var classroom_button = FindViewById<ImageButton>(Resource.Id.imageButton2);
                var public_button = FindViewById<ImageButton>(Resource.Id.imageButton1);
                var group_button = FindViewById<ImageButton>(Resource.Id.imageButton3);

                classroom_button.Click += delegate
                {
                    Role = "Individual";
                    var serverconnection = new Intent(this, typeof(ServerConnectionActivity));
                    StartActivity(serverconnection);
                    
                };

                public_button.Click += delegate
                {
                    Role = "Public";
                    Intent intent = new Intent(this, typeof(HostIPActivity));
                    StartActivity(intent);
                };

                group_button.Click += delegate
                {
                    Role = "Group";
                    var serverconnection = new Intent(this, typeof(ServerConnectionActivity));
                    StartActivity(serverconnection);

                };
            }
        }
}

