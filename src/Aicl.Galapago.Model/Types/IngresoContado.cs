using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("INGRESO_CONTADO")]
	public partial class IngresoContado:IHasId<System.Int32>{

		public IngresoContado(){}

		[Alias("ID")]
		[Sequence("INGRESO_CONTADO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_INGRESO")]
		public System.Int32 IdIngreso { get; set;} 

		[Alias("ID_CUENTA_DINERO")]
		public System.Int32 IdCuentaDinero { get; set;} 

		[Alias("VALOR")]
		[DecimalLength(15,2)]
		public System.Decimal Valor { get; set;} 

	}
}
