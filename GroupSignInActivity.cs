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
    [Activity(Label = "Group - Sign In", Theme = "@style/Theme.DesignDemo")]
    public class GroupSignInActivity : Activity
    {
        SqlConnection con = new SqlConnection("Data Source='" + Program.source + "' ; Initial Catalog='" + Program.db + "'; User ID='" + Program.id + "';Password='" + Program.password + "'");
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_groupsigninactivity);
            
            Button signin_button = FindViewById<Button>(Resource.Id.button1);

            signin_button.Click += SignInActivity;
            
        }

        public void SignInActivity(object sender, EventArgs e)
        {
            
            var username = FindViewById<EditText>(Resource.Id.editText1).Text;
            var password = FindViewById<EditText>(Resource.Id.editText2).Text;

            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) From groups Where g_username='" + username + "' COLLATE SQL_Latin1_General_CP1_CS_AS and g_password= '" + password + "' COLLATE SQL_Latin1_General_CP1_CS_AS", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                SqlCommand cmd = new SqlCommand("Select group_id from groups where g_username='" + username + "' COLLATE SQL_Latin1_General_CP1_CS_AS and g_password= '" + password + "' COLLATE SQL_Latin1_General_CP1_CS_AS", con);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Program.group_id = Convert.ToInt32(dr["group_id"]);

                }
                dr.Close();
                con.Close();

                StartActivity(typeof(GroupGamePinActivity));
            }
            else
            {
                Toast.MakeText(this, "Login Failed! Please Check your Username and Password!", ToastLength.Short).Show();
                
            }
            
        }
    }
}