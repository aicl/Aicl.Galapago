using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("INGRESO")]
    [JoinTo(typeof(Sucursal),"IdSucursal", "Id", Order=0)]
    [JoinTo(typeof(Tercero),"IdTercero","Id", Order=1)]
    [JoinTo(typeof(Tercero),typeof(TipoDocumento),"IdTipoDocumento","Id", Order=2)]
	public partial class Ingreso:IHasId<Int32>, IHasIdSucursal,IHasPeriodo,IHasIdTercero, IHasCodigoDocumento{

		public Ingreso(){}

		[Alias("ID")]
		[Sequence("INGRESO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public Int32 Id { get; set;} 

		[Alias("ID_TERCERO")]
		public Int32 IdTercero { get; set;} 

		[Alias("NUMERO")]
		public Int32 Numero { get; set;} 

		[Alias("DESCRIPCION")]
		[StringLength(50)]
		public String Descripcion { get; set;} 

		[Alias("FECHA")]
		public DateTime Fecha { get; set;} 

		[Alias("PERIODO")]
		[Required]
		[StringLength(6)]
		public String Periodo { get; set;} 

		[Alias("VALOR")]
		[DecimalLength(15,2)]
		public Decimal Valor { get; set;} 

		[Alias("SALDO")]
		[DecimalLength(15,2)]
		public Decimal Saldo { get; set;} 

		[Alias("DIAS_CREDITO")]
		public Int16 DiasCredito { get; set;} 

		[Alias("FECHA_ASENTADO")]
		public DateTime? FechaAsentado { get; set;} 

		[Alias("ID_SUCURSAL")]
		public Int32 IdSucursal { get; set;} 

		[Alias("FECHA_ANULADO")]
		public DateTime? FechaAnulado { get; set;} 

		[Alias("EXTERNO")]
		public Boolean Externo { get; set;} 

		[Alias("CODIGO_DOCUMENTO")]
		[Required]
		[StringLength(4)]
		public String CodigoDocumento { get; set;} 

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


	}
}
