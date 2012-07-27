using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("COMPROBANTE_INGRESO")]
    [JoinTo(typeof(Sucursal),"IdSucursal", "Id", Order=0)]
    [JoinTo(typeof(Tercero),"IdTercero","Id", Order=1)]
    [JoinTo(typeof(Tercero),typeof(TipoDocumento),"IdTipoDocumento","Id", Order=2)]
    [JoinTo(typeof(PresupuestoItem),"IdCuentaReceptora","Id", Order=3)]
	public partial class ComprobanteIngreso:IHasId<Int32>,IHasPeriodo,IHasIdSucursal,IHasIdTercero
    {
		public ComprobanteIngreso(){}

		[Alias("ID")]
		[Sequence("COMPROBANTE_INGRESO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public Int32 Id { get; set;} 

		[Alias("FECHA")]
		public DateTime Fecha { get; set;} 

        [Alias("PERIODO")]
        [Required]
        [StringLength(6)]
        public String Periodo { get; set;} 

		[Alias("NUMERO")]
		public Int32 Numero { get; set;} 

		[Alias("DESCRIPCION")]
		[Required]
		[StringLength(50)]
		public String Descripcion { get; set;} 

		[Alias("VALOR")]
		[DecimalLength(15,2)]
		public Decimal Valor { get; set;} 

		[Alias("ID_CUENTA_RECEPTORA")]
		public Int32 IdCuentaReceptora { get; set;} 

		[Alias("ID_TERCERO")]
		public Int32 IdTercero { get; set;} 

		[Alias("ID_SUCURSAL")]
		public Int32 IdSucursal { get; set;} 

		[Alias("FECHA_ASENTADO")]
		public DateTime? FechaAsentado { get; set;} 

		[Alias("FECHA_ANULADO")]
		public DateTime? FechaAnulado { get; set;} 

		[Alias("EXTERNO")]
		public Boolean? Externo { get; set;} 

        [Alias("ID_TERCERO_RECEPTORA")]
        public Int32? IdTerceroReceptora { get; set;} 


        #region Sucursal
        [BelongsTo(typeof(Sucursal),"Nombre")]
        public string NombreSucursal{ get;set;}
        #endregion Sucursal

        #region Tercero
        [BelongsTo(typeof(Tercero),"Documento")]
        public string DocumentoTercero {get;set;}

        [BelongsTo(typeof(Tercero),"DigitoVerificacion")]
        public string DVTercero {get;set;}

        [BelongsTo(typeof(Tercero),"Nombre")]
        public string NombreTercero {get;set;}

        #endregion Tercero

        #region TipoDocumento Tercero
        [BelongsTo(typeof(TipoDocumento),"Nombre")]
        public string NombreDocumentoTercero {get;set;}
        #endregion TipoDocumento Tercero


        #region CuentaReceptora
        [BelongsTo(typeof(PresupuestoItem),"Codigo")]
        public String CodigoItem { get; set;} 

        [BelongsTo(typeof(PresupuestoItem),"Nombre")]
        public String NombreItem { get; set;} 
        #endregion CuentaReceptora

	}
}
