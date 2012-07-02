using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("SUCURSAL_CENTRO")]
	public partial class SucursalCentro:IHasId<System.Int32>{

		public SucursalCentro(){}

		[Alias("ID")]
		[Sequence("SUCURSAL_CENTRO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_SUCURSAL")]
		public System.Int32 IdSucursal { get; set;} 

		[Alias("ID_CENTRO")]
		public System.Int32 IdCentro { get; set;} 

		[Alias("ACTIVO")]
		public System.Int16? Activo { get; set;} 

	}
}
