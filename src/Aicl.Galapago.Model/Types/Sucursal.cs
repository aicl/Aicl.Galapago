using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("SUCURSAL")]
	public partial class Sucursal:IHasId<System.Int32>{

		public Sucursal(){}

		[Alias("ID")]
		[Sequence("SUCURSAL_ID_GEN")]
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

		[Alias("ACTIVO")]
		public System.Boolean Activo { get; set;} 

		[Alias("ID_CUENTA_POR_COBRAR")]
		public System.Int32 IdCuentaPorCobrar { get; set;} 

		[Alias("ID_CUENTA_POR_PAGAR")]
		public System.Int32 IdCuentaPorPagar { get; set;} 

	}
}
