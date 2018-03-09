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

namespace TooLearnAndroid
{
    [Activity(Label = "Sign Up", Theme = "@style/Theme.DesignDemo")]
    public class SignUpActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_signup);
            Button signup_button = FindViewById<Button>(Resource.Id.button1);

            signup_button.Click += delegate
            {
                StartActivity(typeof(SignInActivity));
            };
        }

        
    }
}