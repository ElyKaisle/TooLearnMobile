using System;
using Android.App;
using Android.Widget;
using Android.OS;

namespace TooLearnAndroid
{
        [Activity(Label = "TooLearn", Theme = "@style/Theme.DesignDemo")]
        public class MainActivity : Activity
        {
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
                    StartActivity(typeof(SignInActivity));
                };

                public_button.Click += delegate
                {
                    StartActivity(typeof(PublicJoinQuizActivity));
                };

                group_button.Click += delegate
                {
                    StartActivity(typeof(SignInActivity));
                };
            }
        }
}

