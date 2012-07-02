using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("MAYOR_CONTABILIDAD_2012")]
	public partial class MayorContabilidad2012:IHasId<System.Int32>{

		public MayorContabilidad2012(){}

		[Alias("ID")]
		[Sequence("MAYOR_CONTABILIDAD_2012_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_CUENTA")]
		public System.Int32 IdCuenta { get; set;} 

		[Alias("ID_SUCURSAL")]
		public System.Int32 IdSucursal { get; set;} 

		[Alias("ID_CENTRO")]
		public System.Int32 IdCentro { get; set;} 

		[Alias("ID_TERCERO")]
		public System.Int32? IdTercero { get; set;} 

		[Alias("INICIAL_00")]
		[DecimalLength(15,2)]
		public System.Decimal Inicial00 { get; set;} 

		[Alias("DEBITOS_00")]
		[DecimalLength(15,2)]
		public System.Decimal Debitos00 { get; set;} 

		[Alias("CREDITOS_00")]
		[DecimalLength(15,2)]
		public System.Decimal Creditos00 { get; set;} 

		[Alias("INICIAL_01")]
		[DecimalLength(15,2)]
		public System.Decimal Inicial01 { get; set;} 

		[Alias("DEBITOS_01")]
		[DecimalLength(15,2)]
		public System.Decimal Debitos01 { get; set;} 

		[Alias("CREDITOS01")]
		[DecimalLength(15,2)]
		public System.Decimal Creditos01 { get; set;} 

		[Alias("INICIAL_02")]
		[DecimalLength(15,2)]
		public System.Decimal Inicial02 { get; set;} 

		[Alias("DEBITOS_02")]
		[DecimalLength(15,2)]
		public System.Decimal Debitos02 { get; set;} 

		[Alias("CREDITOS_02")]
		[DecimalLength(15,2)]
		public System.Decimal Creditos02 { get; set;} 

		[Alias("INICIAL_03")]
		[DecimalLength(15,2)]
		public System.Decimal Inicial03 { get; set;} 

		[Alias("DEBITOS_03")]
		[DecimalLength(15,2)]
		public System.Decimal Debitos03 { get; set;} 

		[Alias("CREDITOS_03")]
		[DecimalLength(15,2)]
		public System.Decimal Creditos03 { get; set;} 

		[Alias("INICIAL_04")]
		[DecimalLength(15,2)]
		public System.Decimal Inicial04 { get; set;} 

		[Alias("DEBITOS_04")]
		[DecimalLength(15,2)]
		public System.Decimal Debitos04 { get; set;} 

		[Alias("CREDITOS_04")]
		[DecimalLength(15,2)]
		public System.Decimal Creditos04 { get; set;} 

		[Alias("INICIAL_05")]
		[DecimalLength(15,2)]
		public System.Decimal Inicial05 { get; set;} 

		[Alias("DEBITOS_05")]
		[DecimalLength(15,2)]
		public System.Decimal Debitos05 { get; set;} 

		[Alias("CREDITOS_05")]
		[DecimalLength(15,2)]
		public System.Decimal Creditos05 { get; set;} 

		[Alias("INICIAL_06")]
		[DecimalLength(15,2)]
		public System.Decimal Inicial06 { get; set;} 

		[Alias("DEBITOS_06")]
		[DecimalLength(15,2)]
		public System.Decimal Debitos06 { get; set;} 

		[Alias("CREDITOS_06")]
		[DecimalLength(15,2)]
		public System.Decimal Creditos06 { get; set;} 

		[Alias("INICIAL_07")]
		[DecimalLength(15,2)]
		public System.Decimal Inicial07 { get; set;} 

		[Alias("DEBITOS_07")]
		[DecimalLength(15,2)]
		public System.Decimal Debitos07 { get; set;} 

		[Alias("CREDITOS_07")]
		[DecimalLength(15,2)]
		public System.Decimal Creditos07 { get; set;} 

		[Alias("INICIAL_08")]
		[DecimalLength(15,2)]
		public System.Decimal Inicial08 { get; set;} 

		[Alias("DEBITOS_08")]
		[DecimalLength(15,2)]
		public System.Decimal Debitos08 { get; set;} 

		[Alias("CREDITOS_08")]
		[DecimalLength(15,2)]
		public System.Decimal Creditos08 { get; set;} 

		[Alias("INICIAL_09")]
		[DecimalLength(15,2)]
		public System.Decimal Inicial09 { get; set;} 

		[Alias("DEBITOS_09")]
		[DecimalLength(15,2)]
		public System.Decimal Debitos09 { get; set;} 

		[Alias("CREDITOS_09")]
		[DecimalLength(15,2)]
		public System.Decimal Creditos09 { get; set;} 

		[Alias("INICIAL_10")]
		[DecimalLength(15,2)]
		public System.Decimal Inicial10 { get; set;} 

		[Alias("DEBITOS_10")]
		[DecimalLength(15,2)]
		public System.Decimal Debitos10 { get; set;} 

		[Alias("CREDITOS_10")]
		[DecimalLength(15,2)]
		public System.Decimal Creditos10 { get; set;} 

		[Alias("INICIAL_11")]
		[DecimalLength(15,2)]
		public System.Decimal Inicial11 { get; set;} 

		[Alias("DEBITOS_11")]
		[DecimalLength(15,2)]
		public System.Decimal Debitos11 { get; set;} 

		[Alias("CREDITOS_11")]
		[DecimalLength(15,2)]
		public System.Decimal Creditos11 { get; set;} 

		[Alias("INICIAL_12")]
		[DecimalLength(15,2)]
		public System.Decimal Inicial12 { get; set;} 

		[Alias("DEBITOS_12")]
		[DecimalLength(15,2)]
		public System.Decimal Debitos12 { get; set;} 

		[Alias("CREDITOS_12")]
		[DecimalLength(15,2)]
		public System.Decimal Creditos12 { get; set;} 

		[Alias("INICIAL_13")]
		[DecimalLength(15,2)]
		public System.Decimal Inicial13 { get; set;} 

		[Alias("DEBITOS_13")]
		[DecimalLength(15,2)]
		public System.Decimal Debitos13 { get; set;} 

		[Alias("CREDITOS_13")]
		[DecimalLength(15,2)]
		public System.Decimal Creditos13 { get; set;} 

	}
}
