using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("EGRESO_ITEM")]
	public partial class EgresoItem:IHasId<System.Int32>, IHasIdCentro{

		public EgresoItem(){}

		[Alias("ID")]
		[Sequence("EGRESO_ITEM_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_EGRESO")]
		public System.Int32 IdEgreso { get; set;} 

		[Alias("ID_PRESUPUESTO_ITEM")]
		public System.Int32 IdPresupuestoItem { get; set;} 

		[Alias("TIPO_PARTIDA")]
		public System.Int16 TipoPartida { get; set;} 

		[Alias("VALOR")]
		[DecimalLength(15,2)]
		public System.Decimal Valor { get; set;} 

		[Alias("ID_CENTRO")]
		public System.Int32 IdCentro { get; set;} 

        [Alias("ID_TERCERO")]
        public System.Int32? IdTercero { get; set;} 

	}
}
