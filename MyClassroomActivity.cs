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
    [Activity(Label = "My Classroom", Theme = "@style/Theme.DesignDemo")]
    public class MyClassroomActivity : Activity
    {
        SqlConnection con = new SqlConnection("Data Source='" + Program.source + "' ; Initial Catalog='" + Program.db + "'; User ID='" + Program.id + "';Password='" + Program.password + "'");
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            
            SetContentView(Resource.Layout.activity_myclassroom);
            load_data();
            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var enroll = FindViewById<Button>(Resource.Id.button1);
            enroll.Click += EnrollActivity;
        }

        public void EnrollActivity(object sender, EventArgs e)
        {
            var codeme = FindViewById<EditText>(Resource.Id.editText1);
            try
            {
                SqlDataAdapter ser = new SqlDataAdapter("Select count(cl.participant_id) from classlist cl , classrooms c where cl.class_id=c.class_id AND cl.participant_id='" + Program.par_id + "' AND class_code= '" + codeme.Text + "' ", con);
                DataTable der = new DataTable();
                ser.Fill(der);
                string idr = der.Rows[0][0].ToString();
                int count = Convert.ToInt32(idr);

                if (count >= 1)
                {
                    Toast.MakeText(this, "You are already Part of the Class!", ToastLength.Short).Show();
                    
                }

                else
                {

                    SqlDataAdapter sad = new SqlDataAdapter("Select count(class_id) from classrooms where class_code = '" + codeme.Text + "' ", con);
                    DataTable data = new DataTable();
                    sad.Fill(data);




                    if (data.Rows[0][0].ToString() == "1")
                    {
                        SqlDataAdapter s = new SqlDataAdapter("Select class_id from classrooms where class_code = '" + codeme.Text + "' ", con);
                        DataTable d = new DataTable();
                        s.Fill(d);
                        string ID = d.Rows[0][0].ToString();


                        SqlDataAdapter se = new SqlDataAdapter("Select facilitator_id from classrooms where class_id = '" + ID + "' ", con);
                        DataTable de = new DataTable();
                        se.Fill(de);
                        string id = de.Rows[0][0].ToString();



                        con.Open();
                        String query = "INSERT INTO classlist (participant_id,class_id,facilitator_id) VALUES ('" + Program.par_id + "','" + ID + "','" + id + "')";
                        SqlDataAdapter sda = new SqlDataAdapter(query, con);
                        int n = sda.SelectCommand.ExecuteNonQuery();
                        con.Close();


                        if (n > 0)
                        {
                            codeme.Text = "";
                            Toast.MakeText(this, "Enrolled!", ToastLength.Short).Show();

                        }
                        else
                        {
                            Toast.MakeText(this, "Enroll Failed!", ToastLength.Short).Show();

                        }

                    }

                    else
                    {
                        Toast.MakeText(this, "Class Code doesn't Exist", ToastLength.Short).Show();
                    }


                }

            }//end first IF

            catch (Exception ex)
            {

                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();

            }
        }

        void load_data()
        {

            try
            {
                Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);
              
                // comboBox1.Items.Clear(); comboBox1 = spinner

                SqlCommand cmd = new SqlCommand("SELECT class_name from classrooms c, classlist cl where c.class_id = cl.class_id AND cl.participant_id = '" + Program.par_id + "'", con);
                
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var classname = (dr["class_name"]);
                    String[] values = { classname.ToString() };
                    ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, values);
                    adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    RunOnUiThread(() => spinner.Adapter = adapter);
                }
                dr.Close();
                con.Close();
            }

            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            try
            {

                ListView list = FindViewById<ListView>(Resource.Id.listView1);

                SqlDataAdapter sda = new SqlDataAdapter("select fullname AS 'Name' from participant p,classlist cl where p.participant_id=cl.participant_id AND class_id=(select class_id from classrooms where class_name = '" + sender + "') ", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    var classname = (sda.Update(dt));
                    String[] values = { classname.ToString() };
                    ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, values);
                    RunOnUiThread(() => list.Adapter = adapter);
                    /*
                    BindingSource bs = new BindingSource();
                    bs.DataSource = dt;
                    bunifuCustomDataGrid1.DataSource = bs;
                    sda.Update(dt);
                    bunifuCustomLabel1.Visible = true;
                    */
                }


                else
                {
                    var classname = (sda.Update(dt));
                    String[] values = { classname.ToString() };
                    ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, values);
                    RunOnUiThread(() => list.Adapter = adapter);
                    /*
                    BindingSource bs = new BindingSource();
                    bs.DataSource = dt;
                    bunifuCustomDataGrid1.DataSource = bs;
                    sda.Update(dt);
                    bunifuCustomLabel1.Visible = false;
                    */
                }

            }

            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }


        }
    }
}