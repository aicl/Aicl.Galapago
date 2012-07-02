using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("CUENTA_PRESUPUESTO")]
	public partial class CuentaPresupuesto:IHasId<System.Int32>{

		public CuentaPresupuesto(){}

		[Alias("ID")]
		[Sequence("CUENTA_PRESUPUESTO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_CUENTA")]
		public System.Int32 IdCuenta { get; set;} 

		[Alias("ID_PRESUPUESTO")]
		public System.Int32 IdPresupuesto { get; set;} 

		[Alias("ID_PRESUPUESTO_ITEM")]
		public System.Int32 IdPresupuestoItem { get; set;} 

	}
}
