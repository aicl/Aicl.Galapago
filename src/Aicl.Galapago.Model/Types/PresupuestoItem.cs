using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
    [Alias("PRESUPUESTO_ITEM")]
    public partial class PresupuestoItem:IHasId<System.Int32>{

        public PresupuestoItem(){}

        [Alias("ID")]
        [Sequence("PRESUPUESTO_ITEM_ID_GEN")]
        [PrimaryKey]
        [AutoIncrement]
        public System.Int32 Id { get; set;} 

        [Alias("ID_PRESUPUESTO")]
        public System.Int32 IdPresupuesto { get; set;} 

        [Alias("CODIGO")]
        [Required]
        [StringLength(16)]
        public System.String Codigo { get; set;} 

        [Alias("TIPO_ITEM")]
        public System.Int32 TipoItem { get; set;} 

        [Alias("NOMBRE")]
        [Required]
        [StringLength(50)]
        public System.String Nombre { get; set;} 

        [Alias("PRESUPUESTADO")]
        [DecimalLength(15,2)]
        public System.Decimal Presupuestado { get; set;} 

        [Alias("RESERVADO")]
        [DecimalLength(15,2)]
        public System.Decimal Reservado { get; set;} 

        [Alias("SALDO_ANTERIOR")]
        [DecimalLength(15,2)]
        public System.Decimal SaldoAnterior { get; set;} 

        [Alias("DEBITOS")]
        [DecimalLength(15,2)]
        public System.Decimal Debitos { get; set;} 

        [Alias("CREDITOS")]
        [DecimalLength(15,2)]
        public System.Decimal Creditos { get; set;} 

        [Alias("USA_TERCERO")]
        public System.Boolean UsaTercero { get; set;} 

        [Alias("ACTIVO")]
        public System.Boolean Activo { get; set;} 

        [Ignore]
        public System.Decimal Ejecutado { get{return Debitos-Creditos;} } 

        [Ignore]
        public System.Decimal SaldoActual { get{return SaldoAnterior+ Debitos-Creditos;} } 


        public void Update(decimal debitos, decimal creditos)
        {
            Debitos=Debitos+debitos;
            Creditos=Creditos+creditos;
        }

        public string GetParentCode(){
            if(Codigo.Contains("."))
                return Codigo.Substring(0,Codigo.IndexOf("."));
            if(Codigo.Length==1) return string.Empty;
            if(Codigo.Length==2) return Codigo.Substring(0,1);
            return Codigo.Substring(0,Codigo.Length-2);
            
        }

    }
}