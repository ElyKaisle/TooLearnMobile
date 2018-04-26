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
    [Activity(Label = "Nickname", Theme = "@style/Theme.DesignDemo")]
    public class NicknameActivity : Activity
    {
        public static string NameFREE;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_nickname);
            Button join_button = FindViewById<Button>(Resource.Id.button1);
            join_button.Click += JoinLobbyActivity;
        }

        public void JoinLobbyActivity(object sender, EventArgs e)
        {
            var nickname = FindViewById<EditText>(Resource.Id.editText1).Text;
            if (nickname == null || nickname == "")
            {
                Toast.MakeText(this, "Provide a Screen Name!", ToastLength.Long).Show();

            }

            else
            {
                NameFREE = nickname;
                StartActivity(typeof(LobbyActivity));
            }
        }
    }
}