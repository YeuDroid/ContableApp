using Android.App;
using Android.Widget;
using LiteDB;
using Android.OS;
using System;
using Android.Content;

namespace GCuentas
{
    [Activity(Label = "ContableApp", MainLauncher = true, Icon = "@drawable/icon")]


    public class MainActivity : Activity
    {


        ///CONTROLS TO MANAGER
        Button bAGR_entrada, bAGR_gasto,bRegistro;
        TextView tvEntrada, tvSalidas, tvBalance;
        /// 
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
            //start db
            TransacPipe.startConection();

           // inital control config
            initialControls();

        }
        public override void OnWindowFocusChanged(bool haso)
        {
            getAndUpdateData();
        }
        
        void initialControls()
        {
            //get id
            this.bAGR_entrada = this.FindViewById<Button>(Resource.Id.IngEntrada);
            this.bAGR_gasto = this.FindViewById<Button>(Resource.Id.IngGastos);
            this.tvEntrada = this.FindViewById<TextView>(Resource.Id.tvIngresos_b);
            this.tvSalidas = this.FindViewById<TextView>(Resource.Id.tvSalida_b);
            this.tvBalance = this.FindViewById<TextView>(Resource.Id.tvBalance);
            this.bRegistro = this.FindViewById<Button>(Resource.Id.bVerRegistro);
            //setEvents
            this.bAGR_entrada.Click += clickAGR_entrada;
            this.bAGR_gasto.Click += clickAGR_gasto;
            this.bRegistro.Click += BRegistro_Click;
            //obtener valores de sumas
             getAndUpdateData();
        }

        private void BRegistro_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Lregisto));
        }

        public override void OnBackPressed()
        {
            System.Environment.Exit(1);
        }
        void getAndUpdateData()
        {
            double sumGast = 0.0;
            double sumIngr = 0.0;
            double rest = 0.0;
            try
            {
                sumGast = TransacPipe.getSumaGastos();
                sumIngr = TransacPipe.getSumaIngresos();
                rest = (sumIngr - sumGast);
            } catch (Exception) {; }
            this.tvSalidas.Text = "$" + sumGast.ToString() + " MXN";
            this.tvEntrada.Text = "$" + sumIngr.ToString() + " MXN";
            this.tvBalance.Text = "$" + rest.ToString() + " MXN";

            if(rest < 0 )
            {
                //pintar rojo
                this.tvBalance.SetBackgroundColor(Android.Graphics.Color.Red);

            }
            if (rest >= 0)
            {
                //pintar azul
                this.tvBalance.SetBackgroundColor(Android.Graphics.Color.Black);
            }
            


        }
        void clickAGR_entrada(object sender, EventArgs e)
        {

            StartActivity(typeof(Lingreso));
        }
        void clickAGR_gasto(object sender, EventArgs e)
        {
            //mostrar layout de ingresar gasto
            StartActivity(typeof(Lgasto));
            
        }
        void Msg(string msg)
        {
            Toast.MakeText(this, msg, ToastLength.Short).Show();
        }

    }
}

