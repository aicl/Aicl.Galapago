using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("RECIBO_CAJA")]
	public partial class ReciboCaja:IHasId<System.Int32>{

		public ReciboCaja(){}

		[Alias("ID")]
		[Sequence("RECIBO_CAJA_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("FECHA")]
		public System.DateTime Fecha { get; set;} 

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

	}
}
