using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("MATRICULA_PAGO")]
	public partial class MatriculaPago:IHasId<System.Int32>{

		public MatriculaPago(){}

		[Alias("ID")]
		[Sequence("MATRICULA_PAGO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_MATRICULA")]
		public System.Int32 IdMatricula { get; set;} 

		[Alias("ID_CUENTA_DINERO")]
		public System.Int32 IdCuentaDinero { get; set;} 

		[Alias("VALOR")]
		[DecimalLength(15,2)]
		public System.Decimal Valor { get; set;} 

	}
}
