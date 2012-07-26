using System;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
    [JoinTo(typeof(PresupuestoItem),"IdPresupuestoItem", "Id", Order=0)]
    [JoinTo(typeof(Centro),"IdCentro", "Id", Order=1)]
    [JoinTo(typeof(Tercero),"IdTercero", "Id", Order=2, JoinType=JoinType.Left)]
	[Alias("EGRESO_ITEM")]
	public partial class EgresoItem:IHasId<Int32>, IHasIdCentro{

		public EgresoItem(){}

		[Alias("ID")]
		[Sequence("EGRESO_ITEM_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public Int32 Id { get; set;} 

		[Alias("ID_EGRESO")]
		public Int32 IdEgreso { get; set;} 

		[Alias("ID_PRESUPUESTO_ITEM")]
		public Int32 IdPresupuestoItem { get; set;} 

		[Alias("TIPO_PARTIDA")]
		public Int16 TipoPartida { get; set;} 

		[Alias("VALOR")]
		[DecimalLength(15,2)]
		public Decimal Valor { get; set;} 

		[Alias("ID_CENTRO")]
		public Int32 IdCentro { get; set;} 

        [Alias("ID_TERCERO")]
        public Int32? IdTercero { get; set;} 

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

        #region Tercero
        [BelongsTo(typeof(Tercero),"Nombre")]
        public string NombreTercero {get; set;}

        [BelongsTo(typeof(Tercero),"Documento")]
        public string DocumentoTercero {get;set;}

        [BelongsTo(typeof(Tercero),"DigitoVerificacion")]
        public string DVTercero {get;set;}
        #endregion Tercero

	}

}
