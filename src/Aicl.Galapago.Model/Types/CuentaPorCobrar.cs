using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("CUENTA_POR_COBRAR")]
	public partial class CuentaPorCobrar:IHasId<System.Int32>{

		public CuentaPorCobrar(){}

		[Alias("ID")]
		[Sequence("CUENTA_POR_COBRAR_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_INGRESO")]
		public System.Int32? IdIngreso { get; set;} 

		[Alias("ID_TERCERO")]
		public System.Int32 IdTercero { get; set;} 

		[Alias("NUMERO")]
		public System.Int32 Numero { get; set;} 

		[Alias("DESCRIPCION")]
		[StringLength(50)]
		public System.String Descripcion { get; set;} 

		[Alias("FECHA")]
		public System.DateTime Fecha { get; set;} 

		[Alias("PERIODO")]
		[Required]
		[StringLength(6)]
		public System.String Periodo { get; set;} 

		[Alias("VALOR_TOTAL")]
		[DecimalLength(15,2)]
		public System.Decimal ValorTotal { get; set;} 

		[Alias("VALOR_NETO")]
		[DecimalLength(15,2)]
		public System.Decimal ValorNeto { get; set;} 

		[Alias("SALDO")]
		[DecimalLength(15,2)]
		public System.Decimal Saldo { get; set;} 

		[Alias("FECHA_ASENTADO")]
		public System.DateTime? FechaAsentado { get; set;} 

		[Alias("ID_SUCURSAL")]
		public System.Int32 IdSucursal { get; set;} 

		[Alias("FECHA_ANULADO")]
		public System.DateTime? FechaAnulado { get; set;} 

		[Alias("EXTERNO")]
		public System.Int16? Externo { get; set;} 

	}
}
