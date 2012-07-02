using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("TIPO_DOCUMENTO")]
	public partial class TipoDocumento:IHasId<System.Int32>{

		public TipoDocumento(){}

		[Alias("ID")]
		[Sequence("TIPO_DOCUMENTO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("NOMBRE")]
		[Required]
		[StringLength(30)]
		public System.String Nombre { get; set;} 

		[Alias("REQUIRE_DV")]
		public System.Int16? RequireDv { get; set;} 

	}
}
