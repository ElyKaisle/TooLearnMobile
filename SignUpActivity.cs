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
using Android.Graphics;
using Android.Text;
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
            var username = FindViewById<EditText>(Resource.Id.editText4);
            username.TextChanged += TextChangedActivity;
            signup_button.Click += CreateAccountActivity;

        }
        public void TextChangedActivity(object sender, TextChangedEventArgs e)
        {
            try {
                var username = FindViewById<EditText>(Resource.Id.editText4);
                var availableuser = FindViewById<TextView>(Resource.Id.textView1);
                var error = FindViewById<ImageView>(Resource.Id.imageView2);
                var check = FindViewById<ImageView>(Resource.Id.imageView3);
                string user = username.Text;
                string availuser = availableuser.Text;
                SqlDataAdapter sda = new SqlDataAdapter($"Select count(*) From participant Where p_username={user}", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);


                if (String.IsNullOrWhiteSpace(user))
                {
                    availuser = null;
                    error.Visibility = ViewStates.Invisible;
                    check.Visibility = ViewStates.Invisible;
                }


                else if (int.Parse(dt.Rows[0][0].ToString()) == 0)
                {
                    availuser = $"{user} is Available";
                    availableuser.SetTextColor(Color.Green);
                    error.Visibility = ViewStates.Invisible;
                    check.Visibility = ViewStates.Visible;

                }




                else if (int.Parse(dt.Rows[0][0].ToString()) > 0)
                {
                    availuser = $"{user} is Not Available";
                    availableuser.SetTextColor(Color.Red);
                    check.Visibility = ViewStates.Invisible;
                    error.Visibility = ViewStates.Visible;
                }


                else { }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
            
        }


        public void CreateAccountActivity(object sender, EventArgs e)
        {
            var fname = FindViewById<EditText>(Resource.Id.editText1);
            var mname = FindViewById<EditText>(Resource.Id.editText2);
            var lname = FindViewById<EditText>(Resource.Id.editText3);
            var username = FindViewById<EditText>(Resource.Id.editText4);
            var password = FindViewById<EditText>(Resource.Id.editText5);
            var repassword = FindViewById<EditText>(Resource.Id.editText6);
            var availableuser = FindViewById<TextView>(Resource.Id.textView1);
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

                if (availableuser.CurrentTextColor == Color.Green)
                {
                    if (pw == repw)
                    {
                        con.Open();
                        
                        SqlCommand cmd = new SqlCommand($"Insert into participant(fullname,F_name, M_name, L_name, p_username, p_password) Values({(first + " " + middle + " " + last)},{first},{middle},{last},{user},{pw}", con);
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