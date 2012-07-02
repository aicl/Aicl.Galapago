using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("RECIBO_CAJA_ACT")]
	public partial class ComprobanteEgresoAct:IHasId<System.Int32>{

		public ComprobanteEgresoAct(){}

		[Alias("ID")]
		[Sequence("RECIBO_CAJA_ACT_ID")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_RECIBO_CAJA")]
		public System.Int32 IdReciboCaja { get; set;} 

		[Alias("ID_PRESUPUESTO_ITEM")]
		public System.Int32 IdPresupuestoItem { get; set;} 

		[Alias("TIPO_PARTIDA")]
		public System.Int16 TipoPartida { get; set;} 

		[Alias("VALOR")]
		[DecimalLength(15,2)]
		public System.Decimal Valor { get; set;} 

		[Alias("ID_CENTRO")]
		public System.Int32 IdCentro { get; set;} 

	}
}
