using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("PRODUCTO")]
	public partial class Producto:IHasId<System.Int32>{

		public Producto(){}

		[Alias("ID")]
		[Sequence("PRODUCTO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("CODIGO")]
		[Required]
		[StringLength(15)]
		public System.String Codigo { get; set;} 

		[Alias("NOMBRE")]
		[Required]
		[StringLength(40)]
		public System.String Nombre { get; set;} 

		[Alias("ID_GRUPO_PRODUCTO")]
		public System.Int32 IdGrupoProducto { get; set;} 

		[Alias("ID_IVA")]
		public System.Int32 IdIva { get; set;} 

		[Alias("VALOR")]
		[DecimalLength(15,2)]
		public System.Decimal? Valor { get; set;} 

		[Alias("ID_CUENTA_INGRESO")]
		public System.Int32? IdCuentaIngreso { get; set;} 

		[Alias("ID_CUENTA_EGRESO")]
		public System.Int32? IdCuentaEgreso { get; set;} 

		[Alias("INCLUIR_EN_MATRICULA")]
		public System.Int16? IncluirEnMatricula { get; set;} 

		[Alias("INCLUIR_EN_MENSUALIDAD")]
		public System.Int16? IncluirEnMensualidad { get; set;} 

	}
}
