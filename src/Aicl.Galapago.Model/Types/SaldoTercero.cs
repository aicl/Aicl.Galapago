
using System;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.Common.Utils;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
    [Alias("SALDO_TERCERO")]
    public partial class SaldoTercero:IHasId<System.Int32>{

        public SaldoTercero(){}

        [Alias("ID")]
        [Sequence("SALDO_TERCERO_ID_GEN")]
        [PrimaryKey]
        [AutoIncrement]
        public System.Int32 Id { get; set;} 

        [Alias("ID_PRESUPUESTO_ITEM")]
        public System.Int32 IdPresupuestoItem { get; set;} 

        [Alias("ID_SUCURSAL")]
        public System.Int32 IdSucursal { get; set;} 

        [Alias("ID_TERCERO")]
        public System.Int32 IdTercero { get; set;} 

		[Alias("SALDO_INICIAL")]
        [DecimalLength(15,2)]
        public System.Decimal SaldoInicial { get; set;} 
        
        [Alias("DEBITOS")]
        [DecimalLength(15,2)]
        public System.Decimal Debitos { get; set;} 

        [Alias("CREDITOS")]
        [DecimalLength(15,2)]
        public System.Decimal Creditos { get; set;} 
		        

        public void UpdateDbCr( decimal debitos, decimal creditos)
        {
            Debitos=Debitos+debitos;
            Creditos=Creditos+creditos;
        }

        public static string GetLockKey(int idPresupuestoItem, int idTercero)
        {
            return string.Format("urn:lock:SaldoTercero:IdPresupuestoItem:{0}:IdTercero:{1}",
                                     idPresupuestoItem, idTercero);            
        }
    }
}
