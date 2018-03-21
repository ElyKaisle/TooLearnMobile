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
using Android.Text;

using System.Data;
using System.Data.SqlClient;

namespace TooLearnAndroid
{
    [Activity(Label = "Edit Profile", Theme = "@style/Theme.DesignDemo")]
    public class EditProfileActivity : Activity
    {

        SqlConnection con = new SqlConnection("Data Source='" + Program.source + "' ; Initial Catalog='" + Program.db + "'; User ID='" + Program.id + "';Password='" + Program.password + "'");
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.activity_editprofile);
            load_account();
            Button edit_button = FindViewById<Button>(Resource.Id.button1);
            edit_button.Click += EditActivity;
        }

        public void load_account()
        {
            var fullname = FindViewById<EditText>(Resource.Id.editText1);
            var username = FindViewById<EditText>(Resource.Id.editText2);
            var password = FindViewById<EditText>(Resource.Id.editText3);
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select fullname,p_username,p_password from participant Where participant_id='" + Program.par_id + "' ", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    
                    fullname.Text = dt.Rows[0][0].ToString();
                    username.Text = dt.Rows[0][1].ToString();
                    password.Text = dt.Rows[0][2].ToString();
                }
                else { }

            }

            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }



        private void EditActivity(object sender, EventArgs e)
        {
            var fullname = FindViewById<EditText>(Resource.Id.editText1);
            var username = FindViewById<EditText>(Resource.Id.editText2);
            var password = FindViewById<EditText>(Resource.Id.editText3);
            var MyAccountEdit = FindViewById<Button>(Resource.Id.button1);
            switch (MyAccountEdit.Text)
            {
                case "Edit":
                    {
                        
                        fullname.Enabled = true;
                        username.Enabled = true;
                        password.Enabled = true;
                        MyAccountEdit.Text = "Save";
                        password.InputType = InputTypes.TextVariationNormal;

                    }
                    break;

                case "Save":
                    {
                        
                        if (fullname.Text != "" && username.Text != "" && password.Text != "")
                        {
                            
                            AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);
                            alertDialog.SetTitle("Notice!");
                            alertDialog.SetMessage("Are you sure?");
                            alertDialog.SetPositiveButton("Ok", (senderAlert , args) =>
                            {
                               

                                    fullname.Enabled = false;
                                    username.Enabled = false;
                                    password.Enabled = false;
                                    MyAccountEdit.Text = "Edit";




                                    con.Open();
                                    String query = "UPDATE participant SET fullname= '" + fullname.Text + "', p_username='" + username.Text + "', p_password = '" + password.Text + "' WHERE participant_id= '" + Program.par_id + "' ";
                                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                                    int n = sda.SelectCommand.ExecuteNonQuery();

                                    con.Close();

                                    password.InputType = InputTypes.TextVariationPassword;

                                    load_account();


                               
                            });
                            alertDialog.SetNegativeButton("Cancel", delegate
                            {
                                alertDialog.Dispose();
                            });
                            Dialog dialog = alertDialog.Create();
                            dialog.Show();
                            
                        }

                        else
                        {
                            Toast.MakeText(this, "Please Fill all Fields!", ToastLength.Long).Show();

                        }
                        break;
                    }


            }

        }
    }
}