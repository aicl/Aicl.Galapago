using System;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;
using System.ComponentModel.DataAnnotations;

namespace Aicl.Galapago.Model.Types
{
	[Alias("TARIFA")]
	[JoinTo(typeof(PresupuestoItem),"IdPresupuestoItem","Id", Order=0)]
	[JoinTo(typeof(PresupuestoItem),typeof(Presupuesto),"IdPresupuesto","Id",Order=1)]
	public partial class Tarifa:IHasId<Int32>{

		public Tarifa(){}

		[Alias("ID")]
		[Sequence("TARIFA_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public Int32 Id { get; set;} 

		[Alias("NOMBRE")]
		[StringLength(40)]
		[Required]
		public string Nombre { get; set;}

		[Alias("VALOR")]
		[DecimalLength(15,2)]
		public Decimal Valor { get; set;} 

		[Alias("ID_PRESUPUESTO_ITEM")]
		public Int32 IdPresupuestoItem { get; set;} 

		[Alias("INCLUIR_EN_MATRICULA")]
		public bool IncluirEnMatricula { get; set;}

		[Alias("INCLUIR_EN_MENSUALIDAD")]
		public bool IncluirEnPension { get; set;}

		[Alias("ACTIVO")]
		public bool Activo { get; set;}

		#region Presupuesto
		[BelongsTo(typeof(Presupuesto))]
		public Int32 IdSucursal { get; set;} 

		[BelongsTo(typeof(Presupuesto))]
		public Int32 IdCentro { get; set;} 

		[BelongsTo(typeof(Presupuesto),"Activo")]
		public bool PresupuestoActivo {get; set;}
		#endregion Presupuesto

	}
}
