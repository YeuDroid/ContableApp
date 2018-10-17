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
    public class TransacPipe
    {
        public static LiteDatabase database;
        static LiteCollection<OIngreso> collIngresos;
        static LiteCollection<OGasto> collGastos;
        //LiteCollection<OCortes>

        public static void deleteAllGastosYIngresos()
        {
            database.DropCollection("GASTOS");
            database.DropCollection("INGRESOS");
        }
       
        
        public static void InsertGasto(OGasto gasto)
        {
            collGastos.Insert(gasto);
        }
        public static void InsertIngreso(OIngreso ingreso)
        {   
            collIngresos.Insert(ingreso);
        }
        public static void startConection()
        {
            database = new LiteDatabase(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + System.IO.Path.DirectorySeparatorChar.ToString() + "database.db");
            collGastos = database.GetCollection<OGasto>("GASTOS");
            collIngresos = database.GetCollection<OIngreso>("INGRESOS");
        }
        public static double getSumaGastos()
        {
            double sum = 0;
            foreach(var gast in collGastos.FindAll())
            {
                sum += gast.CANTIDAD;
            }

            return sum;
        }
        public static double getSumaIngresos()
        {
            double sum = 0;
            foreach (var ingr in collIngresos.FindAll())
            {
                sum += ingr.CANTIDAD;
            }

            return sum;
        }
    }
}