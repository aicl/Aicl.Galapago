using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("ASIENTO")]
	public partial class Asiento:IHasId<System.Int32>,IHasIdSucursal,IHasPeriodo,IHasFechaAsentado,IHasFechaAnulado{

		public Asiento(){}

		[Alias("ID")]
		[Sequence("ASIENTO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("NUMERO")]
		public System.Int32 Numero { get; set;} 

		[Alias("FECHA")]
		public System.DateTime Fecha { get; set;} 

		[Alias("FECHA_ASENTADO")]
		public System.DateTime? FechaAsentado { get; set;} 

		[Alias("DEBITOS")]
		[DecimalLength(15,2)]
		public System.Decimal Debitos { get; set;} 

		[Alias("CREDITOS")]
		[DecimalLength(15,2)]
		public System.Decimal Creditos { get; set;} 

		[Alias("PERIODO")]
		[Required]
		[StringLength(6)]
		public System.String Periodo { get; set;} 

		[Alias("DESCRIPCION")]
		[StringLength(40)]
		public System.String Descripcion { get; set;} 

		[Alias("ID_SUCURSAL")]
		public System.Int32 IdSucursal { get; set;} 

		[Alias("FECHA_ANULADO")]
		public System.DateTime? FechaAnulado { get; set;} 

		[Alias("EXTERNO")]
		public System.Boolean Externo { get; set;} 
		
		[Alias("CODIGO_DOCUMENTO")]
		[Required]
		[StringLength(2)]
		public System.String CodigoDocumento { get; set;} 
		
		[Alias("DOCUMENTO")]
		[Required]
		[StringLength(12)]
		public System.String Documento { get; set;} 

	}
}
