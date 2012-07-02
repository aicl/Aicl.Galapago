using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("CIUDAD")]
	public partial class Ciudad:IHasId<System.Int32>{

		public Ciudad(){}

		[Alias("ID")]
		[Sequence("CIUDAD_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("CODIGO")]
		[Required]
		[StringLength(8)]
		public System.String Codigo { get; set;} 

		[Alias("NOMBRE")]
		[Required]
		[StringLength(30)]
		public System.String Nombre { get; set;} 

		[Alias("ID_DEPARTAMENTO")]
		public System.Int32 IdDepartamento { get; set;} 

	}
}
