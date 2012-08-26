using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("PRESUPUESTO")]
	public partial class Presupuesto:IHasId<Int32>{

		public Presupuesto(){}

		[Alias("ID")]
		[Sequence("PRESUPUESTO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public Int32 Id { get; set;} 

		[Alias("NOMBRE")]
		[StringLength(40)]
		public String Nombre { get; set;} 

		[Alias("ID_SUCURSAL")]
		public Int32 IdSucursal { get; set;} 

		[Alias("ID_CENTRO")]
		public Int32 IdCentro { get; set;} 

        [Alias("ACTIVO")]
        public Boolean Activo { get; set;} 
	}
}
