using System;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.Common.Utils;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
    [Alias("MAYOR_PRESUPUESTO_{0}")]
    public partial class MayorPresupuesto:IHasId<System.Int32>{

        public MayorPresupuesto(){}

        [Alias("ID")]
        [Sequence("MAYOR_PRESUPUESTO_ID_GEN")]
        [PrimaryKey]
        [AutoIncrement]
        public System.Int32 Id { get; set;} 

        [Alias("ID_PRESUPUESTO_ITEM")]
        public System.Int32 IdPresupuestoItem { get; set;} 

        [Alias("ID_SUCURSAL")]
        public System.Int32 IdSucursal { get; set;} 

        [Alias("ID_CENTRO")]
        public System.Int32 IdCentro { get; set;} 

        [Alias("ID_TERCERO")]
        public System.Int32? IdTercero { get; set;} 

        [Alias("PRESUPUESTADO_00")]
        [DecimalLength(15,2)]
        public System.Decimal Presupuestado00 { get; set;} 

        [Alias("RESERVADO_00")]
        [DecimalLength(15,2)]
        public System.Decimal Reservado00 { get; set;} 

        [Alias("SALDO_ANTERIOR_00")]
        [DecimalLength(15,2)]
        public System.Decimal SaldoAnterior00 { get; set;} 

        [Alias("DEBITOS_00")]
        [DecimalLength(15,2)]
        public System.Decimal Debitos00 { get; set;} 

        [Alias("CREDITOS_00")]
        [DecimalLength(15,2)]
        public System.Decimal Creditos00 { get; set;} 

        [Alias("PRESUPUESTADO_01")]
        [DecimalLength(15,2)]
        public System.Decimal ValorPresupuestado01 { get; set;} 

        [Alias("RESERVADO_01")]
        [DecimalLength(15,2)]
        public System.Decimal ValorReservado01 { get; set;} 

        [Alias("SALDO_ANTERIOR_01")]
        [DecimalLength(15,2)]
        public System.Decimal SaldoAnterior01 { get; set;} 

        [Alias("DEBITOS_01")]
        [DecimalLength(15,2)]
        public System.Decimal Debitos01 { get; set;} 

        [Alias("CREDITOS_01")]
        [DecimalLength(15,2)]
        public System.Decimal Creditos01 { get; set;} 

        [Alias("PRESUPUESTADO_02")]
        [DecimalLength(15,2)]
        public System.Decimal ValorPresupuestado02 { get; set;} 

        [Alias("RESERVADO_02")]
        [DecimalLength(15,2)]
        public System.Decimal ValorReservado02 { get; set;} 

        [Alias("SALDO_ANTERIOR_02")]
        [DecimalLength(15,2)]
        public System.Decimal SaldoAnterior02 { get; set;} 

        [Alias("DEBITOS_02")]
        [DecimalLength(15,2)]
        public System.Decimal Debitos02 { get; set;} 

        [Alias("CREDITOS_02")]
        [DecimalLength(15,2)]
        public System.Decimal Creditos02 { get; set;} 

        [Alias("PRESUPUESTADO_03")]
        [DecimalLength(15,2)]
        public System.Decimal ValorPresupuestado03 { get; set;} 

        [Alias("RESERVADO_03")]
        [DecimalLength(15,2)]
        public System.Decimal ValorReservado03 { get; set;} 

        [Alias("SALDO_ANTERIOR_03")]
        [DecimalLength(15,2)]
        public System.Decimal SaldoAnterior03 { get; set;} 

        [Alias("DEBITOS_03")]
        [DecimalLength(15,2)]
        public System.Decimal Debitos03 { get; set;} 

        [Alias("CREDITOS_03")]
        [DecimalLength(15,2)]
        public System.Decimal Creditos03 { get; set;} 

        [Alias("PRESUPUESTADO_04")]
        [DecimalLength(15,2)]
        public System.Decimal ValorPresupuestado04 { get; set;} 

        [Alias("RESERVADO_04")]
        [DecimalLength(15,2)]
        public System.Decimal ValorReservado04 { get; set;} 

        [Alias("SALDO_ANTERIOR_04")]
        [DecimalLength(15,2)]
        public System.Decimal SaldoAnterior04 { get; set;} 

        [Alias("DEBITOS_04")]
        [DecimalLength(15,2)]
        public System.Decimal Debitos04 { get; set;} 

        [Alias("CREDITOS_04")]
        [DecimalLength(15,2)]
        public System.Decimal Creditos04 { get; set;} 

        [Alias("PRESUPUESTADO_05")]
        [DecimalLength(15,2)]
        public System.Decimal ValorPresupuestado05 { get; set;} 

        [Alias("RESERVADO_05")]
        [DecimalLength(15,2)]
        public System.Decimal ValorReservado05 { get; set;} 

        [Alias("SALDO_ANTERIOR_05")]
        [DecimalLength(15,2)]
        public System.Decimal SaldoAnterior05 { get; set;} 

        [Alias("DEBITOS_05")]
        [DecimalLength(15,2)]
        public System.Decimal Debitos05 { get; set;} 

        [Alias("CREDITOS_05")]
        [DecimalLength(15,2)]
        public System.Decimal Creditos05 { get; set;} 

        [Alias("PRESUPUESTADO_06")]
        [DecimalLength(15,2)]
        public System.Decimal ValorPresupuestado06 { get; set;} 

        [Alias("RESERVADO_06")]
        [DecimalLength(15,2)]
        public System.Decimal ValorReservado06 { get; set;} 

        [Alias("SALDO_ANTERIOR_06")]
        [DecimalLength(15,2)]
        public System.Decimal SaldoAnterior06 { get; set;} 

        [Alias("DEBITOS_06")]
        [DecimalLength(15,2)]
        public System.Decimal Debitos06 { get; set;} 

        [Alias("CREDITOS_06")]
        [DecimalLength(15,2)]
        public System.Decimal Creditos06 { get; set;} 

        [Alias("PRESUPUESTADO_07")]
        [DecimalLength(15,2)]
        public System.Decimal ValorPresupuestado07 { get; set;} 

        [Alias("RESERVADO_07")]
        [DecimalLength(15,2)]
        public System.Decimal ValorReservado07 { get; set;} 

        [Alias("SALDO_ANTERIOR_07")]
        [DecimalLength(15,2)]
        public System.Decimal SaldoAnterior07 { get; set;} 

        [Alias("DEBITOS_07")]
        [DecimalLength(15,2)]
        public System.Decimal Debitos07 { get; set;} 

        [Alias("CREDITOS_07")]
        [DecimalLength(15,2)]
        public System.Decimal Creditos07 { get; set;} 

        [Alias("PRESUPUESTADO_08")]
        [DecimalLength(15,2)]
        public System.Decimal ValorPresupuestado08 { get; set;} 

        [Alias("RESERVADO_08")]
        [DecimalLength(15,2)]
        public System.Decimal ValorReservado08 { get; set;} 

        [Alias("SALDO_ANTERIOR_08")]
        [DecimalLength(15,2)]
        public System.Decimal SaldoAnterior08 { get; set;} 

        [Alias("DEBITOS_08")]
        [DecimalLength(15,2)]
        public System.Decimal Debitos08 { get; set;} 

        [Alias("CREDITOS_08")]
        [DecimalLength(15,2)]
        public System.Decimal Creditos08 { get; set;} 


        [Alias("PRESUPUESTADO_09")]
        [DecimalLength(15,2)]
        public System.Decimal ValorPresupuestado09 { get; set;} 

        [Alias("RESERVADO_09")]
        [DecimalLength(15,2)]
        public System.Decimal ValorReservado09 { get; set;} 

        [Alias("SALDO_ANTERIOR_09")]
        [DecimalLength(15,2)]
        public System.Decimal SaldoAnterior09 { get; set;} 

        [Alias("DEBITOS_09")]
        [DecimalLength(15,2)]
        public System.Decimal Debitos09 { get; set;} 

        [Alias("CREDITOS_09")]
        [DecimalLength(15,2)]
        public System.Decimal Creditos09 { get; set;} 

        [Alias("PRESUPUESTADO_10")]
        [DecimalLength(15,2)]
        public System.Decimal ValorPresupuestado10 { get; set;} 

        [Alias("RESERVADO_10")]
        [DecimalLength(15,2)]
        public System.Decimal ValorReservado10 { get; set;} 

        [Alias("SALDO_ANTERIOR_10")]
        [DecimalLength(15,2)]
        public System.Decimal SaldoAnterior10 { get; set;} 

        [Alias("DEBITOS_10")]
        [DecimalLength(15,2)]
        public System.Decimal Debitos10 { get; set;} 

        [Alias("CREDITOS_10")]
        [DecimalLength(15,2)]
        public System.Decimal Creditos10 { get; set;} 

        [Alias("PRESUPUESTADO_11")]
        [DecimalLength(15,2)]
        public System.Decimal ValorPresupuestado11 { get; set;} 

        [Alias("RESERVADO_11")]
        [DecimalLength(15,2)]
        public System.Decimal ValorReservado11 { get; set;} 

        [Alias("SALDO_ANTERIOR_11")]
        [DecimalLength(15,2)]
        public System.Decimal SaldoAnterior11 { get; set;} 

        [Alias("DEBITOS_11")]
        [DecimalLength(15,2)]
        public System.Decimal Debitos11 { get; set;} 

        [Alias("CREDITOS_11")]
        [DecimalLength(15,2)]
        public System.Decimal Creditos11 { get; set;} 

        [Alias("PRESUPUESTADO_12")]
        [DecimalLength(15,2)]
        public System.Decimal ValorPresupuestado12 { get; set;} 

        [Alias("RESERVADO_12")]
        [DecimalLength(15,2)]
        public System.Decimal ValorReservado12 { get; set;} 

        [Alias("SALDO_ANTERIOR_12")]
        [DecimalLength(15,2)]
        public System.Decimal SaldoAnterior12 { get; set;} 

        [Alias("DEBITOS_12")]
        [DecimalLength(15,2)]
        public System.Decimal Debitos12 { get; set;} 

        [Alias("CREDITOS_12")]
        [DecimalLength(15,2)]
        public System.Decimal Creditos12 { get; set;} 

        [Alias("PRESUPUESTADO_13")]
        [DecimalLength(15,2)]
        public System.Decimal ValorPresupuestado13 { get; set;} 

        [Alias("RESERVADO_13")]
        [DecimalLength(15,2)]
        public System.Decimal ValorReservado13 { get; set;} 

        [Alias("SALDO_ANTERIOR_13")]
        [DecimalLength(15,2)]
        public System.Decimal SaldoAnterior13 { get; set;} 

        [Alias("DEBITOS_13")]
        [DecimalLength(15,2)]
        public System.Decimal Debitos13 { get; set;} 

        [Alias("CREDITOS_13")]
        [DecimalLength(15,2)]
        public System.Decimal Creditos13 { get; set;} 

        [Ignore]
        public System.Decimal Ejecutado00 { get{return Debitos00-Creditos00;} } 
    
        [Ignore]
        public System.Decimal Ejecutado01 { get{return Debitos01-Creditos01;} } 

        [Ignore]
        public System.Decimal Ejecutado02 { get{return Debitos02-Creditos02;} } 

        [Ignore]
        public System.Decimal Ejecutado03 { get{return Debitos03-Creditos03;} } 

        [Ignore]
        public System.Decimal Ejecutado04 { get{return Debitos04-Creditos04;} } 

        [Ignore]
        public System.Decimal Ejecutado05 { get{return Debitos05-Creditos05;} } 

        [Ignore]
        public System.Decimal Ejecutado06 { get{return Debitos06-Creditos06;} } 

        [Ignore]
        public System.Decimal Ejecutado07 { get{return Debitos07-Creditos07;} } 

        [Ignore]
        public System.Decimal Ejecutado08 { get{return Debitos08-Creditos08;} } 

        [Ignore]
        public System.Decimal Ejecutado09 { get{return Debitos09-Creditos09;} } 

        [Ignore]
        public System.Decimal Ejecutado10 { get{return Debitos10-Creditos10;} } 

        [Ignore]
        public System.Decimal Ejecutado11 { get{return Debitos11-Creditos11;} } 

        [Ignore]
        public System.Decimal Ejecutado12 { get{return Debitos12-Creditos12;} } 

        [Ignore]
        public System.Decimal Ejecutado13 { get{return Debitos13-Creditos13;} } 

        [Ignore]
        public System.Decimal SaldoActual { get{return SaldoAnterior13+ Debitos13-Creditos13;} } 


        public void UpdateSaldos(string periodo, decimal debitos, decimal creditos)
        {
            string month = periodo.Length==6? periodo.Substring(4,2): periodo.Substring(0,2);

            if(debitos!=0)
            {
                PropertyInfo pi= ReflectionUtils.GetPropertyInfo(GetType(), "Debitos"+month);
                var oldDebitos= Convert.ToDecimal( pi.GetValue(this, new object[]{}) );
                ReflectionUtils.SetProperty(this, pi, oldDebitos+debitos );  
            }

            if(creditos!=0)
            {
                PropertyInfo pi= ReflectionUtils.GetPropertyInfo(GetType(), "Creditos"+month);
                var oldCreditos= Convert.ToDecimal( pi.GetValue(this, new object[]{}) );
                ReflectionUtils.SetProperty(this, pi, oldCreditos+creditos );  
            }


            if(debitos!=0 || creditos!=0)
            {
                int m= int.Parse(month)+1;
                for( int i=m; i<=13; i++)
                {
                    PropertyInfo pi= ReflectionUtils.
                        GetPropertyInfo(GetType(),"SaldoAnterior"+ i.ToString().PadLeft(2,'0') );
                    var oldSaldo= Convert.ToDecimal( pi.GetValue(this, new object[]{}) );
                    ReflectionUtils.SetProperty(this, pi, oldSaldo+debitos-creditos );
                }
            }


        }


        public static string GetLockKey(int idPresupuestoItem, int? idTercero)
        {
            if(idTercero.HasValue)
                return string.Format("urn:lock:PresupuestoMayor:IdPresupuestoItem:{0}:IdTercero:{1}",
                                     idPresupuestoItem, idTercero.Value);
            else
                return string.Format("urn:lock:PresupuestoMayor:IdPresupuestoItem:{0}",
                                     idPresupuestoItem);
        }
    }
}