using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("EGRESO")]
	public partial class Egreso:IHasId<System.Int32>, IHasIdSucursal,IHasPeriodo,IHasIdTercero, IHasCodigoDocumento{

		public Egreso(){}

		[Alias("ID")]
		[Sequence("EGRESO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_TERCERO")]
		public System.Int32 IdTercero { get; set;} 

		[Alias("NUMERO")]
		public System.Int32 Numero { get; set;} 

		[Alias("DESCRIPCION")]
		[StringLength(50)]
		public System.String Descripcion { get; set;} 

		[Alias("FECHA")]
		public System.DateTime Fecha { get; set;} 

		[Alias("PERIODO")]
		[Required]
		[StringLength(6)]
		public System.String Periodo { get; set;} 

		[Alias("VALOR")]
		[DecimalLength(15,2)]
		public System.Decimal Valor { get; set;} 

		[Alias("SALDO")]
		[DecimalLength(15,2)]
		public System.Decimal Saldo { get; set;} 

		[Alias("DIAS_CREDITO")]
		public System.Int16 DiasCredito { get; set;} 

		[Alias("FECHA_ASENTADO")]
		public System.DateTime? FechaAsentado { get; set;} 

		[Alias("ID_SUCURSAL")]
		public System.Int32 IdSucursal { get; set;} 

		[Alias("FECHA_ANULADO")]
		public System.DateTime? FechaAnulado { get; set;} 

		[Alias("EXTERNO")]
		public System.Boolean Externo { get; set;} 

		[Alias("CODIGO_DOCUMENTO")]
		[Required]
		[StringLength(4)]
		public System.String CodigoDocumento { get; set;} 

		[Alias("DOCUMENTO")]
		[Required]
		[StringLength(12)]
		public System.String Documento { get; set;} 

        [Alias("ID_TERCERO_RECEPTOR")]
        public System.Int32? IdTerceroReceptor  { get; set;} 
	}
}
