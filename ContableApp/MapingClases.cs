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

namespace GCuentas
{
    public class OIngreso
    {
        public string T_PAGO { get; set; }
        public double CANTIDAD { get; set; }
        public string NOTA { get; set; }
        public string CONCEPTO { get; set; }
        public string FECHA { get; set; }
        public int Id { get; set; }
    }
    public class OGasto
    {
        public string T_PAGO { get; set; }
        public double CANTIDAD { get; set; }
        public string NOTA { get; set; }
        public string CONCEPTO { get; set; }
        public string FECHA { get; set; }
        public int Id { get; set; }
    }
    public class OConfig
    {

    }
    class OCortes
    {
        public string T_PAGO { get; set; }
        public double CANTIDAD { get; set; }
        public string NOTA { get; set; }
        public string FECHA { get; set; }
        public int Id { get; set; }
    }
}