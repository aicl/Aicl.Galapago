using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("CLASE")]
	public partial class Clase:IHasId<System.Int32>{

		public Clase(){}

		[Alias("ID")]
		[Sequence("CLASE_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("DESCRIPCION")]
		[Required]
		[StringLength(30)]
		public System.String Descripcion { get; set;} 

		[Alias("ACTIVO")]
		public System.Int16? Activo { get; set;} 

	}
}
