using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("INGRESO_ITEM")]
	public partial class IngresoItem:IHasId<System.Int32>{

		public IngresoItem(){}

		[Alias("ID")]
		[Sequence("INGRESO_ITEM_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_INGRESO")]
		public System.Int32 IdIngreso { get; set;} 

		[Alias("ID_PRODUCTO")]
		public System.Int32 IdProducto { get; set;} 

		[Alias("VALOR_UNITARIO")]
		[DecimalLength(15,2)]
		public System.Decimal ValorUnitario { get; set;} 

		[Alias("CANTIDAD")]
		[DecimalLength(5,2)]
		public System.Decimal Cantidad { get; set;} 

		[Alias("IVA_PORCENTAJE")]
		[DecimalLength(5,2)]
		public System.Decimal IvaPorcentaje { get; set;} 

		[Alias("IVA_VALOR")]
		[DecimalLength(15,2)]
		public System.Decimal IvaValor { get; set;} 

		[Alias("VALOR_TOTAL")]
		[DecimalLength(15,2)]
		public System.Decimal ValorTotal { get; set;} 

	}
}
