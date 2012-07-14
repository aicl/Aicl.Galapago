using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("COMPROBANTE_EGRESO")]
    [JoinTo(typeof(Sucursal),"IdSucursal", "Id", Order=0)]
    [JoinTo(typeof(Tercero),"IdTercero","Id", Order=1)]
    [JoinTo(typeof(Tercero),typeof(TipoDocumento),"IdTipoDocumento","Id", Order=2)]
    [JoinTo(typeof(Tercero),"IdTerceroReceptor","Id", ChildAlias="Receptor", Order=3, JoinType=JoinType.Left)]
    [JoinTo(typeof(Tercero),typeof(TipoDocumento),"IdTipoDocumento","Id", ParentAlias="Receptor", ChildAlias="DocRec", Order=4, JoinType=JoinType.Left)]
    [JoinTo(typeof(PresupuestoItem),"IdCuentaGiradora","Id", Order=5)]
	public partial class ComprobanteEgreso:IHasId<System.Int32>,IHasPeriodo,IHasIdSucursal,IHasIdTercero
    {
		public ComprobanteEgreso(){}

		[Alias("ID")]
		[Sequence("COMPROBANTE_EGRESO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("FECHA")]
		public System.DateTime Fecha { get; set;} 

        [Alias("PERIODO")]
        [Required]
        [StringLength(6)]
        public System.String Periodo { get; set;} 

		[Alias("NUMERO")]
		public System.Int32 Numero { get; set;} 

		[Alias("DESCRIPCION")]
		[Required]
		[StringLength(50)]
		public System.String Descripcion { get; set;} 

		[Alias("VALOR")]
		[DecimalLength(15,2)]
		public System.Decimal Valor { get; set;} 

		[Alias("ID_CUENTA_GIRADORA")]
		public System.Int32 IdCuentaGiradora { get; set;} 

		[Alias("ID_TERCERO")]
		public System.Int32 IdTercero { get; set;} 

		[Alias("ID_SUCURSAL")]
		public System.Int32 IdSucursal { get; set;} 

		[Alias("FECHA_ASENTADO")]
		public System.DateTime? FechaAsentado { get; set;} 

		[Alias("FECHA_ANULADO")]
		public System.DateTime? FechaAnulado { get; set;} 

		[Alias("EXTERNO")]
		public System.Boolean? Externo { get; set;} 

		[Alias("ID_TERCERO_RECEPTOR")]
		public System.Int32 IdTerceroReceptor { get; set;} 

        [Alias("ID_TERCERO_GIRADORA")]
        public System.Int32? IdTerceroGiradora { get; set;} 


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

        #region TerceroReceptor
        [BelongsTo(typeof(Tercero),"Documento", ParentAlias="Receptor" )]
        public string DocumentoReceptor {get;set;}

        [BelongsTo(typeof(Tercero),"DigitoVerificacion", ParentAlias="Receptor")]
        public string DVReceptor {get;set;}

        [BelongsTo(typeof(Tercero), "Nombre", ParentAlias="Receptor")]
        public string NombreReceptor {get;set;}

        #endregion TerceroReceptor

        #region TipoDocumento TerceroReceptor
        [BelongsTo(typeof(TipoDocumento),"Nombre", ParentAlias="DocRec")]
        public string NombreDocumentoReceptor {get;set;}
        #endregion TipoDocumento TerceroReceptor

        #region CuentaGiradora

        [BelongsTo(typeof(PresupuestoItem),"Codigo")]
        public System.String CodigoItem { get; set;} 

        [BelongsTo(typeof(PresupuestoItem),"Nombre")]
        public System.String NombreItem { get; set;} 

        #endregion CuentaGiradora

	}
}
