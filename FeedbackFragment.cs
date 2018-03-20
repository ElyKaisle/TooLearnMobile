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
    public class FeedbackFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.fragment_feedback, container, false);
            ComputeActivity();
            return view;
        }

        public void ComputeActivity()
        {
            var scorepts = View.FindViewById<TextView>(Resource.Id.textView4).Text;
            var totalscores = View.FindViewById<TextView>(Resource.Id.textView2).Text;
            var totalitems = View.FindViewById<TextView>(Resource.Id.totalItems).Text;
            var comment = View.FindViewById<TextView>(Resource.Id.textView5).Text;
            int rawscore = Convert.ToInt32(scorepts);
            totalscores = rawscore.ToString();
            totalitems = GameActivity.Total;
            float compute = (rawscore / Convert.ToInt32(totalscores)) * 100;
            if(compute < Convert.ToInt32("60"))
            {
                comment = compute + "% You Need Improvement, Study and Play!";
            }
                    else if (compute == Convert.ToInt32("100"))
            {
                comment = compute + "% Excellent!";
            }
            else
            {
                comment = compute + "% Not Bad!, aim Perfect Next Time ";
            }
        }
    }
}