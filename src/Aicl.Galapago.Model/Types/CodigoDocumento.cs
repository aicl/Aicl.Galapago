using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("CODIGO_DOCUMENTO")]
	public partial class CodigoDocumento
	{
		public CodigoDocumento ()
        {
            CreditosPermitidos=new List<string>();
            DebitosPermitidos= new List<string>();
        }
		
		[Alias("ID")]
		[Sequence("CODIGO_DOCUMENTO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("CODIGO")]
		[Required]
		[StringLength(4)]
		public string Codigo {get;set;}
		
		[Alias("NOMBRE")]
		[Required]
		[StringLength(40)]
		public string Nombre {get;set;}
		
        [Alias("CODIGO_PRESUPUESTO")]
        [StringLength(7)]
        public string CodigoPresupuesto {get;set;}

        [Alias("DEBITOS_PERMITIDOS")]
        public List<string> DebitosPermitidos {get;set;}

        [Alias("CREDITOS_PERMITIDOS")]
        public List<string> CreditosPermitidos {get;set;}

        [Alias("TIPO")]
        [StringLength(1)]
        public string Tipo {get;set;}

        [Alias("ACTIVO")]
        public bool Activo {get;set;}

		public static string GetCacheKey(string codigo){
			return  UrnId.Create(typeof(CodigoDocumento),"Codigo",codigo);
		}
		
		public string GetCacheKey(){
			return  UrnId.Create(typeof(CodigoDocumento),"Codigo",Codigo);
		}
		
		
	}
}

//var x = UrnId.Create(;