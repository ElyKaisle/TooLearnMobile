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
    [Activity(Label = "Sign In", Theme = "@style/Theme.DesignDemo")]
    public class SignInActivity : Activity
    {
        SqlConnection con = new SqlConnection("Data Source='" + Program.source + "' ; Initial Catalog='" + Program.db + "'; User ID='" + Program.id + "';Password='" + Program.password + "'");
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_signin);
            var username = FindViewById<EditText>(Resource.Id.editText1);
            var password = FindViewById<EditText>(Resource.Id.editText2);
            Button signup_button = FindViewById<Button>(Resource.Id.button2);
            Button signin_button = FindViewById<Button>(Resource.Id.button1);
            signin_button.Click += LoginActivity;
            
            signup_button.Click += delegate
            {
                StartActivity(typeof(SignUpActivity));
            };
        }

        public void LoginActivity(object sender, EventArgs e)
        {
            var username = FindViewById<EditText>(Resource.Id.editText1);
            var password = FindViewById<EditText>(Resource.Id.editText2);
            string user = username.Text;
            string pw = password.Text;

            SqlDataAdapter sda = new SqlDataAdapter($"Select count(*) From participant Where p_username={user} COLLATE SQL_Latin1_General_CP1_CS_AS and p_password={pw} COLLATE SQL_Latin1_General_CP1_CS_AS", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                SqlCommand cmd = new SqlCommand($"Select participant_id from participant where p_username={user} COLLATE SQL_Latin1_General_CP1_CS_AS and p_password={pw} COLLATE SQL_Latin1_General_CP1_CS_AS", con);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Program.par_id = Convert.ToInt32(dr["participant_id"]);

                }
                dr.Close();
                con.Close();

                Program.PSession_id = user; //For Session
                                            //  Dialogue.Show("Login Successful!", "", "Ok", "Cancel");
                StartActivity(typeof(MainmenuActivity));
            }
            else
            {
                Toast.MakeText(this, "Login Failed!", ToastLength.Short).Show();
                Toast.MakeText(this, "Please Check your Username and Password!!", ToastLength.Short).Show();
                
            }
        } 
    }
}