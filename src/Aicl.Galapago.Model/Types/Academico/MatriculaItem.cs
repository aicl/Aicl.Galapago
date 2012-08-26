using System;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;
using System.ComponentModel.DataAnnotations;

namespace Aicl.Galapago.Model.Types
{
	[Alias("MATRICULA_ITEM")]
	public partial class MatriculaItem:IHasId<Int32>{

		public MatriculaItem(){}

		[Alias("ID")]
		[Sequence("MATRICULA_ITEM_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public Int32 Id { get; set;} 

		[Alias("ID_MATRICULA")]
		public Int32 IdMatricula { get; set;} 

		[Alias("ID_TARIFA")]
		public Int32 IdTarifa { get; set;} 

		[Alias("VALOR")]
		[DecimalLength(15,2)]
		public Decimal Valor { get; set;} 
	}
}
