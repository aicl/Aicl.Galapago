using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("PERIODO_SUCURSAL")]
	public partial class PeriodoSucursal:IHasId<System.Int32>{

		public PeriodoSucursal(){}

		[Alias("ID")]
		[Sequence("PERIODO_SUCURSAL_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_PERIODO")]
		public System.Int32 IdPeriodo { get; set;} 
		
		[Alias("ID_SUCURSAL")]
		public System.Int32 IdSucursal { get; set;} 
	

		[Alias("BLOQUEADO")]
		public System.Boolean Bloqueado { get; set;} 

	}
}
