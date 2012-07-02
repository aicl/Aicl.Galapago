using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("GRUPO_PRODUCTO")]
	public partial class GrupoProducto:IHasId<System.Int32>{

		public GrupoProducto(){}

		[Alias("ID")]
		[Sequence("GRUPO_PRODUCTO_ID_GEN")]
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

		[Alias("INVENTARIO")]
		public System.Int16? Inventario { get; set;} 

		[Alias("PARAVENDER")]
		public System.Int16? Paravender { get; set;} 

	}
}
