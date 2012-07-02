using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("IVA")]
	public partial class Iva:IHasId<System.Int32>{

		public Iva(){}

		[Alias("ID")]
		[Sequence("IVA_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("CODIGO")]
		[Required]
		[StringLength(6)]
		public System.String Codigo { get; set;} 

		[Alias("VALOR")]
		[DecimalLength(5,2)]
		public System.Decimal? Valor { get; set;} 

	}
}
