using System;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
    [JoinTo(typeof(PresupuestoItem),"IdPresupuestoItem", "Id", Order=0)]
    [JoinTo(typeof(Centro),"IdCentro", "Id", Order=1)]
	[Alias("INGRESO_ITEM")]
	public partial class IngresoItem:IHasId<Int32>, IHasIdCentro{

		public IngresoItem(){}

		[Alias("ID")]
		[Sequence("INGRESO_ITEM_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public Int32 Id { get; set;} 

		[Alias("ID_INGRESO")]
		public Int32 IdIngreso { get; set;} 

		[Alias("ID_PRESUPUESTO_ITEM")]
		public Int32 IdPresupuestoItem { get; set;} 

		[Alias("TIPO_PARTIDA")]
		public Int16 TipoPartida { get; set;} 

		[Alias("VALOR")]
		[DecimalLength(15,2)]
		public Decimal Valor { get; set;} 

		[Alias("ID_CENTRO")]
		public Int32 IdCentro { get; set;} 
        
        #region PresupuestoItem
        [BelongsTo(typeof(PresupuestoItem),"Codigo")]
        public string CodigoItem {get; set;}

        [BelongsTo(typeof(PresupuestoItem),"Nombre")]
        public string NombreItem {get; set;}
        #endregion PresupuestoItem

        #region Centro
        [BelongsTo(typeof(Centro),"Nombre")]
        public string NombreCentro {get; set;}
        #endregion Centro


	}

}
