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
    [Activity(Label = "Score Record", Theme = "@style/Theme.DesignDemo")]
    public class ScoreRecordActivity : Activity
    {
        string Class = "";
        private List<string> progress;
        private List<string> grpprogress;
        SqlConnection con = new SqlConnection("Data Source='" + Program.source + "' ; Initial Catalog='" + Program.db + "'; User ID='" + Program.id + "';Password='" + Program.password + "'");
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.activity_scorerecord);
            load_class();
            var spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var indi_button = FindViewById<Button>(Resource.Id.button1);
            var group_button = FindViewById<Button>(Resource.Id.button2);
            indi_button.Click += button1_Click;
            //trigger_combo();

        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            var indi_button = FindViewById<Button>(Resource.Id.button1);
            var group_button = FindViewById<Button>(Resource.Id.button2);
            var list = FindViewById<ListView>(Resource.Id.listView1);
            try
            {
                if (button1WasClicked)
                {
                    SqlDataAdapter sed = new SqlDataAdapter("select facilitator_id from facilitator where name='" + spinner.GetItemAtPosition(e.Position) + "'  ", con);
                    DataTable data = new DataTable();
                    sed.Fill(data);
                    string ID = data.Rows[0][0].ToString();




                    SqlDataAdapter sda = new SqlDataAdapter("select c.class_name,sum(s.quiz_score)/sum(q.total_score)*100 AS 'Percentage'  from quizzes q, participant p, scoreRecords s, classrooms c where p.participant_id = s.participant_id AND c.class_id=s.class_id AND s.quiz_id = q.quiz_id AND c.facilitator_id = '" + ID + "' AND s.participant_id= '" + Program.par_id + "' AND group_id is null group by s.group_id, s.quiz_score, q.total_score, c.class_name ", con);

                    progress = new List<string>();

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {

                    }

                    else
                    {

                    }
                    int Count = dt.Rows.Count;
                    int R = 0;
                    int C = 1;
                    while (C <= Count)
                    {
                        progress.Add(dt.Rows[R][0].ToString());
                        progress.Add(remarks(Convert.ToDouble(dt.Rows[R][1])));
                        //progress.Add(generate_Progress(Convert.ToDouble(dt.Rows[R][1])));



                        R++;
                        C++;
                    }
                    ArrayAdapter ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, progress);
                    RunOnUiThread(() => list.Adapter = ListAdapter);
                    list.ItemClick += Indi_ItemClick;


                }//end IF

                else
                {
                    SqlDataAdapter sed = new SqlDataAdapter("select group_id from groups where class_id=(select class_id from classlist where facilitator_id=(select facilitator_id from facilitator where name ='" + spinner.GetItemAtPosition(e.Position) + "')AND participant_id='" + Program.par_id + "'AND class_id IN(SELECT class_id from groups)) ", con);
                    DataTable data = new DataTable();
                    sed.Fill(data);
                    string ID = data.Rows[0][0].ToString();


                    //  select group_id from groups where class_id = (select class_id from classlist where facilitator_id = (select facilitator_id from facilitator where name = 'Aileen Rillon')AND participant_id = '57' AND class_id IN(SELECT class_id from groups))

                    SqlDataAdapter sda = new SqlDataAdapter("select g.group_name, sum(sc.quiz_score)/sum(q.total_score)*100 AS 'Percentage' from groups g, scoreRecords sc, quizzes q, participant p where g.group_id = sc.group_id AND sc.group_id = '" + ID + "' AND q.quiz_id = sc.quiz_id AND p.participant_id=sc.participant_id AND sc.participant_id='" + Program.user_id + "' group by g.group_name, sc.quiz_score, q.total_score  ", con);
                    grpprogress = new List<string>();

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                    }

                    else
                    {
                    }

                    int Count = dt.Rows.Count;
                    int R = 0;
                    int C = 1;
                    while (C <= Count)
                    {


                        grpprogress.Add(dt.Rows[R][0].ToString());
                        grpprogress.Add(remarks(Convert.ToDouble(dt.Rows[R][1])));
                        //grpprogress.Add(generate_Progress(Convert.ToDouble(dt.Rows[R][1])));


                        R++;
                        C++;
                    }
                    ArrayAdapter List2Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, grpprogress);
                    RunOnUiThread(() => list.Adapter = List2Adapter);
                    list.ItemClick += Grp_ItemClick;
                }//end Else

            }

            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        public void load_class()
        {
            var spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            try
            {
                //Facilitator Name in Combo Box
                SqlCommand cmd = new SqlCommand("select DISTINCT(f.name) from facilitator f, classlist cl where f.facilitator_id=cl.facilitator_id and cl.participant_id= '" + Program.par_id + "' ", con);
                //Select class_name from classrooms WHERE facilitator_id= '" + Program.user_id + "' 
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                List<string> facilitator = new List<string>();
                while (dr.Read())
                {
                    facilitator.Add(dr["name"].ToString());
                }
                ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, facilitator);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                RunOnUiThread(() => spinner.Adapter = adapter);

                dr.Close();
                con.Close();
            }

            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        private bool button1WasClicked = false;

        public void button1_Click(object sender, EventArgs e)
        {
            button1WasClicked = true;
        }

        /*
        public void trigger_combo()
        {
            var indi_button = FindViewById<Button>(Resource.Id.button1);
            var group_button = FindViewById<Button>(Resource.Id.button2);
            var list = FindViewById<ListView>(Resource.Id.listView1);
            try
            {
                if (button1WasClicked)
                {
                    SqlDataAdapter sed = new SqlDataAdapter("select facilitator_id from facilitator where name='" + Class + "'  ", con);
                    DataTable data = new DataTable();
                    sed.Fill(data);
                    string ID = data.Rows[0][0].ToString();




                    SqlDataAdapter sda = new SqlDataAdapter("select c.class_name,sum(s.quiz_score)/sum(q.total_score)*100 AS 'Percentage'  from quizzes q, participant p, scoreRecords s, classrooms c where p.participant_id = s.participant_id AND c.class_id=s.class_id AND s.quiz_id = q.quiz_id AND c.facilitator_id = '" + ID + "' AND s.participant_id= '" + Program.par_id + "' AND group_id is null group by s.group_id, s.quiz_score, q.total_score, c.class_name ", con);

                    progress = new List<string>();

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        
                    }

                    else
                    {

                    }
                    int Count = dt.Rows.Count;
                    int R = 0;
                    int C = 1;
                    while (C <= Count)
                    {
                        progress.Add(dt.Rows[R][0].ToString());
                        progress.Add(remarks(Convert.ToDouble(dt.Rows[R][1])));
                        //progress.Add(generate_Progress(Convert.ToDouble(dt.Rows[R][1])));



                        R++;
                        C++;
                    }
                    ArrayAdapter ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, progress);
                    RunOnUiThread(() => list.Adapter = ListAdapter);
                    list.ItemClick += Indi_ItemClick;


                }//end IF

                else
                {
                    SqlDataAdapter sed = new SqlDataAdapter("select group_id from groups where class_id=(select class_id from classlist where facilitator_id=(select facilitator_id from facilitator where name ='" + Class + "')AND participant_id='" + Program.par_id + "'AND class_id IN(SELECT class_id from groups)) ", con);
                    DataTable data = new DataTable();
                    sed.Fill(data);
                    string ID = data.Rows[0][0].ToString();


                    //  select group_id from groups where class_id = (select class_id from classlist where facilitator_id = (select facilitator_id from facilitator where name = 'Aileen Rillon')AND participant_id = '57' AND class_id IN(SELECT class_id from groups))

                    SqlDataAdapter sda = new SqlDataAdapter("select g.group_name, sum(sc.quiz_score)/sum(q.total_score)*100 AS 'Percentage' from groups g, scoreRecords sc, quizzes q, participant p where g.group_id = sc.group_id AND sc.group_id = '" + ID + "' AND q.quiz_id = sc.quiz_id AND p.participant_id=sc.participant_id AND sc.participant_id='" + Program.user_id + "' group by g.group_name, sc.quiz_score, q.total_score  ", con);
                    grpprogress = new List<string>();

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                    }

                    else
                    {
                    }

                    int Count = dt.Rows.Count;
                    int R = 0;
                    int C = 1;
                    while (C <= Count)
                    {


                        grpprogress.Add(dt.Rows[R][0].ToString());
                        grpprogress.Add(remarks(Convert.ToDouble(dt.Rows[R][1])));
                        //grpprogress.Add(generate_Progress(Convert.ToDouble(dt.Rows[R][1])));


                        R++;
                        C++;
                    }
                    ArrayAdapter List2Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, grpprogress);
                    RunOnUiThread(() => list.Adapter = List2Adapter);
                    list.ItemClick += Grp_ItemClick;
                }//end Else

            }

            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }
        */
        private void Grp_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, grpprogress[e.Position], ToastLength.Long).Show();
        }

        private void Indi_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, progress[e.Position], ToastLength.Long).Show();
        }




        private string remarks(double grade)
        {
            string remarks;

            if (grade <= 74)
            {
                remarks = "Failed: Need to Work Hard!";
            }

            else
            {
                remarks = "Passed: Keep It Up!";
            }

            return remarks;
        }

        public string PN;
        public string CR;
        public string Data
        {
            get { return PN; }
            set { PN = value; }
        }

        public string Data1
        {
            get { return CR; }
            set { CR = value; }
        }

    }
}