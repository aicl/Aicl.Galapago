using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("CUENTA_DINERO")]
	public partial class CuentaDinero:IHasId<System.Int32>{

		public CuentaDinero(){}

		[Alias("ID")]
		[Sequence("CUENTA_DINERO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

	}
}
