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

using System.Data;
using System.Data.SqlClient;


namespace TooLearnAndroid
{
    [Activity(Label = "Group - Game Pin", Theme = "@style/Theme.DesignDemo")]
    public class GroupGamePinActivity : Activity
    {
        SqlConnection con = new SqlConnection("Data Source='" + Program.source + "' ; Initial Catalog='" + Program.db + "'; User ID='" + Program.id + "';Password='" + Program.password + "'");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_groupgamepin);
            // Create your application here
            var join = FindViewById<Button>(Resource.Id.button1);
            join.Click += JoinGameActivity;
        }

        public void JoinGameActivity(object sender, EventArgs e)
        {
            var gamepin = FindViewById<EditText>(Resource.Id.editText1);
            SqlDataAdapter sda = new SqlDataAdapter("Select Game_Pin,Mode From Pincode where Mode='GP' ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                Toast.MakeText(this, "No Host Server Detected for Groups! Please Wait for the Host to Start.", ToastLength.Long).Show();
                
            }

            else
            {

                string code = dt.Rows[0][0].ToString();


                if (code == gamepin.Text)
                {

                    StartActivity(typeof(LobbyActivity));
                }

                else if (gamepin.Text == null || gamepin.Text == "")
                {
                    Toast.MakeText(this, "* Please Enter Code", ToastLength.Long).Show();

                }
                else
                {
                    Toast.MakeText(this, "* Code is Invalid", ToastLength.Long).Show();

                }


            }
        }
    }
}