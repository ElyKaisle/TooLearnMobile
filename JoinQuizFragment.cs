using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Data;
using System.Data.SqlClient;

namespace TooLearnAndroid
{
    [Activity(Label = "Join Quiz", Theme = "@style/Theme.DesignDemo", NoHistory = true)]
    public class JoinQuizFragment : Fragment
    {
        Button join;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.fragment_joinquiz, container, false);
            join = view.FindViewById<Button>(Resource.Id.button1);
            join.Click += StartLobbyActivity;
            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
        public void StartLobbyActivity(object sender, EventArgs e)
        {
            
            try {
                //Alternative
                SqlConnection con = new SqlConnection();
                con.ConnectionString = Helper.ConnectionString;

                SqlDataAdapter sda = new SqlDataAdapter("Select Game_Pin From Pincode where Mode='IP' ", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    Toast.MakeText(this.Activity, "No Host Server Detected! Please Wait for the Host to Start", ToastLength.Short).Show();

                }

                else
                {

                    string code = dt.Rows[0][0].ToString();

                    var text = View.FindViewById<EditText>(Resource.Id.editText1).Text;
                    if (code == text)
                    {
                        Intent intent = new Intent(this.Activity, typeof(LobbyActivity));
                        StartActivity(intent);
                    }

                    else if (text == null || text == "")
                    {
                        Toast.MakeText(this.Activity, "* Please Enter Code", ToastLength.Short).Show();

                    }
                    else
                    {
                        Toast.MakeText(this.Activity, "* Code is Invalid!", ToastLength.Short).Show();

                    }
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this.Activity, ex.ToString(), ToastLength.Short).Show();
            }
            
        }

        static class Helper
        {
            public static string ConnectionString
            {
                get
                {
                    string str = "Data Source='" + Program.source + "' ; Initial Catalog='" + Program.db + "'; User ID='" + Program.id + "';Password='" + Program.password + "'";
                    return str;
                }
            }
        }

    }
}