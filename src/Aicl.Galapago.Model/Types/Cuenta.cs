using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("CUENTA")]
	public partial class Cuenta:IHasId<System.Int32>{

		public Cuenta(){}

		[Alias("ID")]
		[Sequence("CUENTA_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("CODIGO")]
		[Required]
		[StringLength(16)]
		public System.String Codigo { get; set;} 

		[Alias("NOMBRE")]
		[Required]
		[StringLength(40)]
		public System.String Nombre { get; set;} 

		[Alias("USATERCERO")]
		public System.Boolean UsaTercero { get; set;} 

		[Alias("ID_PRESUPUESTO")]
		public System.Int32? IdPresupuesto { get; set;} 

		[Alias("ACTIVA")]
		public System.Boolean Activa { get; set;} 

		[Alias("SALDO_INICIAL")]
		[DecimalLength(15,2)]
		public System.Decimal SaldoInicial { get; set;} 

		[Alias("DEBITOS")]
		[DecimalLength(15,2)]
		public System.Decimal Debitos { get; set;} 

		[Alias("CREDITOS")]
		[DecimalLength(15,2)]
		public System.Decimal Creditos { get; set;} 

		[Alias("SALDO_ACTUAL")]
		[DecimalLength(15,2)]
		public System.Decimal SaldoActual { get; set;} 
		
		
		public string GetParentCode(){
			if(Codigo.Contains("."))
				return Codigo.Substring(0,Codigo.IndexOf("."));
			if(Codigo.Length==1) return string.Empty;
			if(Codigo.Length==2) return Codigo.Substring(0,1);
			return Codigo.Substring(0,Codigo.Length-2);
			
		}

	}
}
