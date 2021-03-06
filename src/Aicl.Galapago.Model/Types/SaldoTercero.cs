using System;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
    [Alias("SALDO_TERCERO")]
	[JoinTo(typeof(Sucursal),"IdSucursal", "Id", Order=0)]
    [JoinTo(typeof(Tercero),"IdTercero","Id", Order=1)]
    [JoinTo(typeof(Tercero),typeof(TipoDocumento),"IdTipoDocumento","Id", Order=2)]
    [JoinTo(typeof(PresupuestoItem),"IdPresupuestoItem","Id", Order=3)]
    public partial class SaldoTercero:IHasId<Int32>{

        public SaldoTercero(){}

        [Alias("ID")]
        [Sequence("SALDO_TERCERO_ID_GEN")]
        [PrimaryKey]
        [AutoIncrement]
        public Int32 Id { get; set;} 

        [Alias("ID_PRESUPUESTO_ITEM")]
        public Int32 IdPresupuestoItem { get; set;} 

        [Alias("ID_SUCURSAL")]
        public Int32 IdSucursal { get; set;} 

        [Alias("ID_TERCERO")]
        public Int32 IdTercero { get; set;} 

		[Alias("SALDO_INICIAL")]
        [DecimalLength(15,2)]
        public Decimal SaldoInicial { get; set;} 
        
        [Alias("DEBITOS")]
        [DecimalLength(15,2)]
        public Decimal Debitos { get; set;} 

        [Alias("CREDITOS")]
        [DecimalLength(15,2)]
        public Decimal Creditos { get; set;} 

		#region Sucursal
        [BelongsTo(typeof(Sucursal),"Nombre")]
        public string NombreSucursal{ get;set;}
        #endregion Sucursal

        #region Tercero
        [BelongsTo(typeof(Tercero))]
        public string Documento {get;set;}

        [BelongsTo(typeof(Tercero))]
        public string DigitoVerificacion {get;set;}

        [BelongsTo(typeof(Tercero))]
        public string Nombre {get;set;}
        #endregion Tercero

        #region TipoDocumento Tercero
        [BelongsTo(typeof(TipoDocumento),"Nombre")]
        public string NombreDocumento {get;set;}
        #endregion TipoDocumento Tercero

        #region PresupuestoItem
        [BelongsTo(typeof(PresupuestoItem),"Codigo")]
        public String CodigoItem { get; set;} 

        [BelongsTo(typeof(PresupuestoItem),"Nombre")]
        public String NombreItem { get; set;} 
        #endregion PresupuestoItem

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
