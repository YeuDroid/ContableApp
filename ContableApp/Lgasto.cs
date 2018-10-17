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
    [Activity(Label = "Ingresar gasto", Icon = "@drawable/icon")]

    public class Lgasto : Activity
    {

        
        //
        Spinner spiner;
        EditText etConcepto, etFecha, etCantidad;
        Button bAgregar, bCancelar;
        
        public static OGasto DataNewGasto;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.LayGasto);
            DataNewGasto = new OGasto();
            initialControls();
        }

        void initialControls()
        {
            this.spiner = FindViewById<Spinner>(Resource.Id.spin_tpago_g);
            this.etCantidad = FindViewById<EditText>(Resource.Id.etCantidad_g);
           // this.etNota= FindViewById<EditText>(Resource.Id.etNota_g);
            this.etFecha= FindViewById<EditText>(Resource.Id.etFecha_g);
            this.etConcepto= FindViewById<EditText>(Resource.Id.etConcepto_g);
            this.bAgregar = FindViewById<Button>(Resource.Id.bAgregar_g);
            this.bCancelar = FindViewById<Button>(Resource.Id.bCancelar_g);
            //cargar spinner

            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.T_PAGOS_STR, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spiner.Adapter = adapter;

            //cargar fecha
            this.etFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");

            //cargar eventos
            this.bAgregar.Click += BAgregar_Click;
            this.bCancelar.Click += BCancelar_Click;
         
        }
        OGasto parseValues()
        {
            var ogast = new OGasto();

                if (etCantidad.Text == null || etCantidad.Text == "") etCantidad.Text = "0";
                ogast.CANTIDAD = Convert.ToDouble(etCantidad.Text.Trim());

                if (etConcepto.Text == null) etConcepto.Text = "";
                ogast.CONCEPTO = etConcepto.Text.TrimEnd().TrimStart();
                
                ogast.FECHA = etFecha.Text.Trim();
                ogast.T_PAGO = spiner.GetItemAtPosition(spiner.SelectedItemPosition).ToString();
 
                ogast.NOTA = "";

                

            return ogast;
        }
        private void BCancelar_Click(object sender, EventArgs e)
        {
            this.Finish();
            
        }

        private void BAgregar_Click(object sender, EventArgs e)
        {
            var pval = parseValues();
            TransacPipe.InsertGasto(pval);
            Msg("Se agrego una salida de $" + pval.CANTIDAD + " MXN, en concepto de: " + pval.CONCEPTO );
            this.Finish();
        }

        void Msg(string msg)
        {
            Toast.MakeText(this, msg, ToastLength.Short).Show();
        }
    }
}