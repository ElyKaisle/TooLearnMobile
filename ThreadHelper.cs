using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Widget;
using Android.App;

namespace TooLearnOfficial
{
    public static class ThreadHelper
    {
        /*
        delegate void AddItemCallback(Activity frm, EditText lsb, string text);
        delegate void AddLabelCallback(Activity frm, TextView lbl, string text);
        delegate void FormCloseCallback(Activity frm);


        public static void Hide(Form frm)
        {
            if (frm.InvokeRequired)
            {
                FormCloseCallback d = new FormCloseCallback(Hide);
                frm.Invoke(d, new object[] { frm });
            }
            else
            {
                frm.Hide();
            }
        }



        public static void lsbAddItem(Form frm, EditText lsb, string text)
        {
            if (lsb.InvokeRequired)
            {
                AddItemCallback d = new AddItemCallback(lsbAddItem);
                frm.Invoke(d, new object[] { frm, lsb, text });
            }
            else
            {
                lsb.Items.Add(text);
            }
        }

        public static void lblAddLabel(Form frm, TextView lbl, string text)
        {
            if (lbl.InvokeRequired)
            {
                AddLabelCallback d = new AddLabelCallback(lblAddLabel);
                frm.Invoke(d, new object[] { frm, lbl, text });
            }
            else
            {
                lbl.Text = text;
            }
        }
    */
    }
}
