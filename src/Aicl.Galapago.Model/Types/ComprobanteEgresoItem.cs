using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("COMPROBANTE_EGRESO_ITEM")]
	[JoinTo(typeof(Egreso),"IdEgreso", "Id", Order=0)]
	public partial class ComprobanteEgresoItem:IHasId<System.Int32>{

		public ComprobanteEgresoItem(){}

		[Alias("ID")]
		[Sequence("COMPROBANTE_EGRESO_ITEM_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_COMPROBANTE_EGRESO")]
		public System.Int32 IdComprobanteEgreso { get; set;} 

		[Alias("ID_EGRESO")]
		public System.Int32 IdEgreso { get; set;} 

		[Alias("ABONO")]
		[DecimalLength(15,2)]
		public System.Decimal Abono { get; set;} 

		#region Egreso
		[BelongsTo(typeof(Egreso))]
		public System.Int32 Numero { get; set;} 

		[BelongsTo(typeof(Egreso))]
		public System.Decimal Valor { get; set;} 

		[BelongsTo(typeof(Egreso))]
		public System.Decimal Saldo { get; set;} 

		[BelongsTo(typeof(Egreso))]
		public System.Int16 DiasCredito { get; set;} 

		[BelongsTo(typeof(Egreso))]
		public System.Int32 IdSucursal { get; set;} 

		[BelongsTo(typeof(Egreso))]
		public System.Int32 IdTercero { get; set;} 

		[BelongsTo(typeof(Egreso))]
		public System.DateTime Fecha { get; set;} 

		[BelongsTo(typeof(Egreso))]
		public System.String Documento { get; set;} 
		#endregion Egreso
	}
}
