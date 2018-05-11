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

namespace TooLearnAndroid
{
    public class MyAccountFragment : Fragment
    {
        //Button scorerecord, myclassroom, editprofile;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.fragment_myaccount, container, false);
            var scorerecord = view.FindViewById<ImageButton>(Resource.Id.imageButton3);
            var myclassroom = view.FindViewById<ImageButton>(Resource.Id.imageButton2);
            var editprofile = view.FindViewById<ImageButton>(Resource.Id.imageButton1);   
            scorerecord.Click += StartScoreRecordActivity;
            myclassroom.Click += StartMyClassroomActivity;
            editprofile.Click += StartEditProfileActivity;
            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
        void StartScoreRecordActivity(object sender, EventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(ScoreRecordActivity));
            StartActivity(intent);
        }

        void StartMyClassroomActivity(object sender, EventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(MyClassroomActivity));
            StartActivity(intent);
        }

        void StartEditProfileActivity(object sender, EventArgs e)
        {
            Intent intent = new Intent(this.Activity, typeof(EditProfileActivity));
            StartActivity(intent);
        }
    }
}