using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("CONSECUTIVO")]
	public partial class Consecutivo:IHasId<System.Int32>{

		public Consecutivo(){}

		[Alias("ID")]
		[Sequence("CONSECUTIVO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_SUCURSAL")]
		public System.Int32 IdSucursal { get; set;} 

		[Alias("DOCUMENTO")]
		[Required]
		[StringLength(2)]
		public System.String Documento { get; set;} 

		[Alias("NUMERO")]
		public System.Int32 Numero { get; set;} 

	}
}
