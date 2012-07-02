using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("PRESUPUESTO")]
	public partial class Presupuesto:IHasId<System.Int32>{

		public Presupuesto(){}

		[Alias("ID")]
		[Sequence("PRESUPUESTO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("NOMBRE")]
		[StringLength(40)]
		public System.String Nombre { get; set;} 

		[Alias("ID_SUCURSAL")]
		public System.Int32 IdSucursal { get; set;} 

		[Alias("ID_CENTRO")]
		public System.Int32 IdCentro { get; set;} 

        [Alias("ACTIVO")]
        public System.Boolean Activo { get; set;} 
	}
}
