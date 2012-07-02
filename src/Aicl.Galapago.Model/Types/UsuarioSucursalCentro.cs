using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("USUARIO_SUCURSAL_CENTRO")]
	public partial class UsuarioSucursalCentro:IHasId<System.Int32>,IHasIdUsuario{

		public UsuarioSucursalCentro(){}

		[Alias("ID")]
		[Sequence("USUARIO_SUCURSAL_CENTRO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_USUARIO")]
		public System.Int32 IdUsuario { get; set;} 

		[Alias("ID_SUCURSAL")]
		public System.Int32 IdSucursal { get; set;} 

		[Alias("ID_CENTRO")]
		public System.Int32 IdCentro { get; set;} 

	}
}
