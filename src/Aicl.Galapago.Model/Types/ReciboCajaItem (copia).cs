using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("RECIBO_CAJA_ITEM")]
	public partial class ReciboCajaItem:IHasId<System.Int32>{

		public ReciboCajaItem(){}

		[Alias("ID")]
		[Sequence("RECIBO_CAJA_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_RECIBO_CAJA")]
		public System.Int32 IdReciboCaja { get; set;} 

		[Alias("ID_EGRESO")]
		public System.Int32 IdEgreso { get; set;} 

		[Alias("VALOR")]
		[DecimalLength(15,2)]
		public System.Decimal Valor { get; set;} 

	}
}
