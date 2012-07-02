using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("MATRICULA_PRODUCTO")]
	public partial class MatriculaProducto:IHasId<System.Int32>{

		public MatriculaProducto(){}

		[Alias("ID")]
		[Sequence("MATRICULA_PRODUCTO_ID_GEN")]
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
