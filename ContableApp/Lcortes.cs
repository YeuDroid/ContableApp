using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteDB;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace GCuentas
{
    [Activity(Label = "Manejo de cortes de E/I", Icon = "@drawable/icon")]

    
    public class Lcortes : Activity
    {
        
        
        //
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.LayCorte);
            initialControls();
        }

        void initialControls()
        {
        
        }

        void Msg(string msg)
        {
            Toast.MakeText(this, msg, ToastLength.Short).Show();
        }
    }
}