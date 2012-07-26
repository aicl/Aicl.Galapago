using System;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("COMPROBANTE_EGRESO_ITEM")]
	[JoinTo(typeof(Egreso),"IdEgreso", "Id", Order=0)]
	public partial class ComprobanteEgresoItem:IHasId<Int32>{

		public ComprobanteEgresoItem(){}

		[Alias("ID")]
		[Sequence("COMPROBANTE_EGRESO_ITEM_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public Int32 Id { get; set;} 

		[Alias("ID_COMPROBANTE_EGRESO")]
		public Int32 IdComprobanteEgreso { get; set;} 

		[Alias("ID_EGRESO")]
		public Int32 IdEgreso { get; set;} 

		[Alias("ABONO")]
		[DecimalLength(15,2)]
		public Decimal Abono { get; set;} 

		#region Egreso
		[BelongsTo(typeof(Egreso))]
		public Int32 Numero { get; set;} 

		[BelongsTo(typeof(Egreso))]
		public Decimal Valor { get; set;} 

		[BelongsTo(typeof(Egreso))]
		public Decimal Saldo { get; set;} 

		[BelongsTo(typeof(Egreso))]
		public Int16 DiasCredito { get; set;} 

		[BelongsTo(typeof(Egreso))]
		public Int32 IdSucursal { get; set;} 

		[BelongsTo(typeof(Egreso))]
		public Int32 IdTercero { get; set;} 

		[BelongsTo(typeof(Egreso))]
		public DateTime Fecha { get; set;} 

		[BelongsTo(typeof(Egreso))]
		public String Documento { get; set;} 

		[BelongsTo(typeof(Egreso))]
		public String Descripcion { get; set;} 
		#endregion Egreso

	}
}
