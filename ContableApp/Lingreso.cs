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
    [Activity(Label = "Ingresar entrada", Icon = "@drawable/icon")]

    public class Lingreso : Activity
    {

        
        //
        Spinner spiner;
        EditText etConcepto, etFecha, etCantidad;
        Button bAgregar, bCancelar;
        
        public static OIngreso DataNewIngreso;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.LayIngreso);
            DataNewIngreso = new OIngreso();
            initialControls();
        }

        void initialControls()
        {
            this.spiner = FindViewById<Spinner>(Resource.Id.spin_tpago);
            this.etCantidad = FindViewById<EditText>(Resource.Id.etCantidad);
            // this.etNota= FindViewById<EditText>(Resource.Id.etNota);
            this.etFecha= FindViewById<EditText>(Resource.Id.etFecha);
            this.etConcepto= FindViewById<EditText>(Resource.Id.etConcepto);
            this.bAgregar = FindViewById<Button>(Resource.Id.bAgregar);
            this.bCancelar = FindViewById<Button>(Resource.Id.bCancelar);
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
        OIngreso parseValues()
        {
            var oingr = new OIngreso();

                if (etCantidad.Text == null || etCantidad.Text == "") etCantidad.Text = "0";
                oingr.CANTIDAD = Convert.ToDouble(etCantidad.Text.Trim());

                if (etConcepto.Text == null) etConcepto.Text = "";
                oingr.CONCEPTO = etConcepto.Text.TrimEnd().TrimStart();
                
                oingr.FECHA = etFecha.Text.Trim();
                oingr.T_PAGO = spiner.GetItemAtPosition(spiner.SelectedItemPosition).ToString();

                
                oingr.NOTA = "";

              

            return oingr;
        }
        private void BCancelar_Click(object sender, EventArgs e)
        {
            this.Finish();  
        }

        private void BAgregar_Click(object sender, EventArgs e)
        {
            var pval = parseValues();
            TransacPipe.InsertIngreso(pval);
            Msg("Se agrego un ingreso de $" + pval.CANTIDAD + " MXN, en concepto de: " + pval.CONCEPTO);
            
            this.Finish();
        }

        void Msg(string msg)
        {
            Toast.MakeText(this, msg, ToastLength.Short).Show();
        }
    }
}