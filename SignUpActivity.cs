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
    [Activity(Label = "Sign Up", Theme = "@style/Theme.DesignDemo")]
    public class SignUpActivity : Activity
    {
        SqlConnection con = new SqlConnection("Data Source='" + Program.source + "' ; Initial Catalog='" + Program.db + "'; User ID='" + Program.id + "';Password='" + Program.password + "'");
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_signup);
            Button signup_button = FindViewById<Button>(Resource.Id.button1);
            signup_button.Click += CreateAccountActivity;
          
        }
        public void CreateAccountActivity(object sender, EventArgs e)
        {
            var fname = FindViewById<EditText>(Resource.Id.editText1);
            var mname = FindViewById<EditText>(Resource.Id.editText2);
            var lname = FindViewById<EditText>(Resource.Id.editText3);
            var username = FindViewById<EditText>(Resource.Id.editText4);
            var password = FindViewById<EditText>(Resource.Id.editText5);
            var repassword = FindViewById<EditText>(Resource.Id.editText6);
            string first = fname.Text;
            string middle = mname.Text;
            string last = lname.Text;
            string user = username.Text;
            string pw = password.Text;
            string repw = repassword.Text;

            if (first == "" || middle == "" || last == "" || user == "" || pw == "" || repw == "")
            {

                Toast.MakeText(this, "Fill All Fields!", ToastLength.Short).Show();
            }

            else
            {

                if (labelAvailableUsername.ForeColor == System.Drawing.Color.Green)
                {
                    if (pw == repw)
                    {
                        con.Open();


                        SqlCommand cmd = new SqlCommand("Insert into participant(fullname,F_name, M_name, L_name, p_username, p_password) Values('" + TextboxName.Text + " " + TextboxMName.Text + " " + TextboxLName.Text + "','" + TextboxName.Text + "','" + TextboxMName.Text + "','" + TextboxLName.Text + "','" + TextboxUsername.Text + "','" + TextboxPassword.Text + "')", con);
                        cmd.ExecuteNonQuery();
                        Toast.MakeText(this, "Successfully Inserted", ToastLength.Short).Show();
                        con.Close();


                        StartActivity(typeof(SignInActivity));

                    }
                    else
                    {
                        Toast.MakeText(this, "Your Password does not Match", ToastLength.Short).Show();

                    }
                }
                else
                {
                    Toast.MakeText(this, "Please use Available Username", ToastLength.Short).Show();

                }


            }
        }


    }
}