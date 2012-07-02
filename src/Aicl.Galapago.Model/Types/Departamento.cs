using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("DEPARTAMENTO")]
	public partial class Departamento:IHasId<System.Int32>{

		public Departamento(){}

		[Alias("ID")]
		[Sequence("DEPARTAMENTO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("CODIGO")]
		[Required]
		[StringLength(2)]
		public System.String Codigo { get; set;} 

		[Alias("NOMBRE")]
		[Required]
		[StringLength(30)]
		public System.String Nombre { get; set;} 

	}
}
