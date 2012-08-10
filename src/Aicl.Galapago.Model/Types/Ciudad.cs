using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("CIUDAD")]
	[JoinTo(typeof(Departamento),"IdDepartamento","Id")]
	public partial class Ciudad:IHasId<Int32>{

		public Ciudad(){}

		[Alias("ID")]
		[Sequence("CIUDAD_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public Int32 Id { get; set;} 

		[Alias("CODIGO")]
		[Required]
		[StringLength(8)]
		public String Codigo { get; set;} 

		[Alias("NOMBRE")]
		[Required]
		[StringLength(30)]
		public String Nombre { get; set;} 

		[Alias("ID_DEPARTAMENTO")]
		public Int32 IdDepartamento { get; set;} 

		[BelongsTo(typeof(Departamento),"Nombre")]
		public string NombreDepartamento{ get;set; }

	}
}
