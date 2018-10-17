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
    [Activity(Label = "Registros de entradas y salidas", Icon = "@drawable/icon")]

    

    public class Lregisto : Activity
    {
        public enum OpcionesVer
        { DatosBasicos,VerTodosLosDatos, SoloIngresos, SoloGastos, SoloCantYConcepto, SoloCantYFecha, SoloCantYFPago, SoloConTDC ,Error};
        Button borrarReg, bSalir;
        Spinner spinOpciones;
        TextView tvRegistro;

        //
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.LayRegistro);
            initialControls();
        }
        void initialControls()
        {
            this.bSalir = FindViewById<Button>(Resource.Id.bSalir);
            this.borrarReg = FindViewById<Button>(Resource.Id.borrarReg);

            this.tvRegistro = FindViewById<TextView>(Resource.Id.tvLogRegistro);
            this.spinOpciones = FindViewById<Spinner>(Resource.Id.spinerOpciones);

            this.borrarReg.Click += BorrarReg_Click;
            this.bSalir.Click += BSalir_Click;


            //llenar logs
            //


            //iniciar spiner

            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.MENU_VER, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinOpciones.Adapter = adapter;
            //

            this.spinOpciones.ItemSelected += SpinOpciones_ItemSelected; ;


        }
        private void SpinOpciones_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var estado  = GetOpcionFromSpiner();

            if (estado == OpcionesVer.Error) Msg("ERROR DE ESPINER, NOTIFIQUE AL DESARROLLADOR DE LA APP. a EL TEL 8353370143 CUANTO ANTES.");

            //
            if (estado == OpcionesVer.DatosBasicos) this.tvRegistro.Text = ParseOption.GetDatosBasicos();
            if (estado == OpcionesVer.VerTodosLosDatos) this.tvRegistro.Text = ParseOption.GetTodosLosDatos();
            if (estado == OpcionesVer.SoloIngresos) this.tvRegistro.Text = ParseOption.GetSoloIngresos();
            if (estado == OpcionesVer.SoloGastos) this.tvRegistro.Text = ParseOption.GetSoloGastos();
            if (estado == OpcionesVer.SoloCantYConcepto) this.tvRegistro.Text = ParseOption.GetSoloCantYConcepto();
            if (estado == OpcionesVer.SoloCantYFecha) this.tvRegistro.Text = ParseOption.GetSoloCantidadYFecha();
            if (estado == OpcionesVer.SoloCantYFPago) this.tvRegistro.Text = ParseOption.GetSoloCantYFPago();
            if (estado == OpcionesVer.SoloConTDC) this.tvRegistro.Text = ParseOption.GetSoloGastosConTCredito();


        }
        private void BSalir_Click(object sender, EventArgs e)
        {
            this.Finish();
        }
        private void BorrarReg_Click(object sender, EventArgs e)
        {

            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Aviso");
            alert.SetMessage("¿Realmente desea borrar todos los datos?");
            alert.SetPositiveButton("No", (senderAlert, args) => {
                //
            });

            alert.SetNegativeButton("Si, borrar", (senderAlert, args) => {
                TransacPipe.deleteAllGastosYIngresos();
                Msg("Datos borrados...");
                this.Finish();
            });

            Dialog dialog = alert.Create();
            dialog.Show();



        }
        void Msg(string msg)
        {
            Toast.MakeText(this, msg, ToastLength.Short).Show();
        }
        public OpcionesVer GetOpcionFromSpiner()
        {
            string value = spinOpciones.GetItemAtPosition(spinOpciones.SelectedItemPosition).ToString();
            //7
            if (value == "Datos basicos") return OpcionesVer.DatosBasicos;
            if (value == "Todos los datos") return OpcionesVer.VerTodosLosDatos;
            if (value == "Solo Cant. y Concepto") return OpcionesVer.SoloCantYConcepto;
            if (value == "Solo gastos con TDC") return OpcionesVer.SoloConTDC;
            if (value == "Solo Cant. y F.Pago") return OpcionesVer.SoloCantYFPago;
            if (value == "Solo ingreso de dinero") return OpcionesVer.SoloIngresos;
            if (value == "Solo salidas de dinero") return OpcionesVer.SoloGastos;
            if (value == "Solo Cant. y Fecha") return OpcionesVer.SoloCantYFecha;
          
            return OpcionesVer.Error;
        }
        public class ParseOption
        {
            public static string GetDatosBasicos()
            {
                var r = "";
                var collGastos = TransacPipe.database.GetCollection<GCuentas.OGasto>("GASTOS");
                var collIngresos = TransacPipe.database.GetCollection<GCuentas.OIngreso>("INGRESOS");

                foreach (var gasto in collGastos.FindAll())
                {
                    r += "-Gasto de $" + gasto.CANTIDAD + " En " + gasto.CONCEPTO + " FECHA: " + gasto.FECHA + System.Environment.NewLine;
                }

                r += System.Environment.NewLine;

                foreach (var ingreso in collIngresos.FindAll())
                {
                    r += "-Entrada de $" + ingreso.CANTIDAD + " En " + ingreso.CONCEPTO + " FECHA: " + ingreso.FECHA + System.Environment.NewLine;
                }
                return r;
            }
            public static string GetTodosLosDatos()
            {
                var r = "";
                var collGastos = TransacPipe.database.GetCollection<GCuentas.OGasto>("GASTOS");
                var collIngresos = TransacPipe.database.GetCollection<GCuentas.OIngreso>("INGRESOS");

                foreach (var gasto in collGastos.FindAll())
                {
                    r += "-Gasto de $" + gasto.CANTIDAD + " En " + gasto.CONCEPTO + " T.Pago: " + gasto.T_PAGO+ " FECHA: " + gasto.FECHA + System.Environment.NewLine;
                }

                r += System.Environment.NewLine;

                foreach (var ingreso in collIngresos.FindAll())
                {
                    r += "-Entrada de $" + ingreso.CANTIDAD + " En " + ingreso.CONCEPTO + " T.Pago: "+ ingreso.T_PAGO + " FECHA: " + ingreso.FECHA + System.Environment.NewLine;
                }
                return r;
            }
            public static string GetSoloIngresos()
            {
                var r = "";
                var collIngresos = TransacPipe.database.GetCollection<GCuentas.OIngreso>("INGRESOS");

                foreach (var ingreso in collIngresos.FindAll())
                {
                    r += "-Entrada de $" + ingreso.CANTIDAD + " En " + ingreso.CONCEPTO + " FECHA: " + ingreso.FECHA + System.Environment.NewLine;
                }
                return r;
            }
            public static string GetSoloGastos()
            {
                var r = "";
                var collGastos = TransacPipe.database.GetCollection<GCuentas.OGasto>("GASTOS");

                foreach (var gasto in collGastos.FindAll())
                {
                    r += "-Gasto de $" + gasto.CANTIDAD + " En " + gasto.CONCEPTO + " FECHA: " + gasto.FECHA + System.Environment.NewLine;
                }

                return r;
            }
            public static string GetSoloCantYConcepto()
            {
                var r = "";
                var collGastos = TransacPipe.database.GetCollection<GCuentas.OGasto>("GASTOS");
                var collIngresos = TransacPipe.database.GetCollection<GCuentas.OIngreso>("INGRESOS");

                foreach (var gasto in collGastos.FindAll())
                {
                    r += "-Gasto de $" + gasto.CANTIDAD + " En " + gasto.CONCEPTO + System.Environment.NewLine;
                }

                r += System.Environment.NewLine;

                foreach (var ingreso in collIngresos.FindAll())
                {
                    r += "-Entrada de $" + ingreso.CANTIDAD + " En " + ingreso.CONCEPTO + System.Environment.NewLine;
                }
                return r;
            }
            public static string GetSoloCantidadYFecha()
            {
                var r = "";
                var collGastos = TransacPipe.database.GetCollection<GCuentas.OGasto>("GASTOS");
                var collIngresos = TransacPipe.database.GetCollection<GCuentas.OIngreso>("INGRESOS");

                foreach (var gasto in collGastos.FindAll())
                {
                    r += "-Gasto de $" + gasto.CANTIDAD + " FECHA: " + gasto.FECHA + System.Environment.NewLine;
                }

                r += System.Environment.NewLine;

                foreach (var ingreso in collIngresos.FindAll())
                {
                    r += "-Entrada de $" + ingreso.CANTIDAD + ingreso.FECHA + System.Environment.NewLine;
                }
                return r;
            }
            public static string GetSoloCantYFPago()
            {
                    var r = "";
                    var collGastos = TransacPipe.database.GetCollection<GCuentas.OGasto>("GASTOS");
                    var collIngresos = TransacPipe.database.GetCollection<GCuentas.OIngreso>("INGRESOS");

                    foreach (var gasto in collGastos.FindAll())
                    {
                        r += "-Gasto de $" + gasto.CANTIDAD + " T.Pago: " + gasto.T_PAGO+ System.Environment.NewLine;
                    }

                    r += System.Environment.NewLine;

                    foreach (var ingreso in collIngresos.FindAll())
                    {
                        r += "-Entrada de $" + ingreso.CANTIDAD + " T.pago " +ingreso.T_PAGO + System.Environment.NewLine;
                    }
                    return r;
             }
            public static string GetSoloGastosConTCredito()
            {

                var r = "";
                var collGastos = TransacPipe.database.GetCollection<GCuentas.OGasto>("GASTOS");

                foreach (var gasto in collGastos.FindAll())
                {
                    if (gasto.T_PAGO == "Targeta de credito")
                    {
                        r += "-Gasto de $" + gasto.CANTIDAD + " En " + gasto.CONCEPTO + " Con TDC" + " FECHA: " + gasto.FECHA + System.Environment.NewLine;
                    } 
                }

                return r;
            }
            
        }
    }
}