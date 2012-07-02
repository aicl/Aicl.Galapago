using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("MENSUALIDAD_PRODUCTO")]
	public partial class MensualidadProducto:IHasId<System.Int32>{

		public MensualidadProducto(){}

		[Alias("ID")]
		[Sequence("MENSUALIDAD_PRODUCTO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_MATRICULA")]
		public System.Int32 IdMatricula { get; set;} 

		[Alias("ID_PRODUCTO")]
		public System.Int32 IdProducto { get; set;} 

		[Alias("VALOR")]
		[DecimalLength(15,2)]
		public System.Decimal? Valor { get; set;} 

	}
}
