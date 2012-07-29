using System;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("COMPROBANTE_INGRESO_ITEM")]
	[JoinTo(typeof(Ingreso),"IdIngreso", "Id", Order=0)]
	public partial class ComprobanteIngresoItem:IHasId<Int32>,IHasIdSucursal{

		public ComprobanteIngresoItem(){}

		[Alias("ID")]
		[Sequence("COMPROBANTE_INGRESO_ITEM_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public Int32 Id { get; set;} 

		[Alias("ID_COMPROBANTE_INGRESO")]
		public Int32 IdComprobanteIngreso { get; set;} 

		[Alias("ID_INGRESO")]
		public Int32 IdIngreso { get; set;} 

		[Alias("ABONO")]
		[DecimalLength(15,2)]
		public Decimal Abono { get; set;} 

		#region Ingreso
		[BelongsTo(typeof(Ingreso))]
		public Int32 Numero { get; set;} 

		[BelongsTo(typeof(Ingreso))]
		public Decimal Valor { get; set;} 

		[BelongsTo(typeof(Ingreso))]
		public Decimal Saldo { get; set;} 

		[BelongsTo(typeof(Ingreso))]
		public Int16 DiasCredito { get; set;} 

		[BelongsTo(typeof(Ingreso))]
		public Int32 IdSucursal { get; set;} 

		[BelongsTo(typeof(Ingreso))]
		public Int32 IdTercero { get; set;} 

		[BelongsTo(typeof(Ingreso))]
		public DateTime Fecha { get; set;}

		[BelongsTo(typeof(Ingreso))]
		public String Documento { get; set;} 

		[BelongsTo(typeof(Ingreso))]
		public String Descripcion { get; set;} 
		#endregion Ingreso

	}
}
