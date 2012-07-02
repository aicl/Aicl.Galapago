using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("TERCERO")]
	public partial class Tercero:IHasId<System.Int32>{

		public Tercero(){}

		[Alias("ID")]
		[Sequence("TERCERO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_TIPO_DOCUMENTO")]
		public System.Int32 IdTipoDocumento { get; set;} 

		[Alias("DOCUMENTO")]
		[Required]
		[StringLength(13)]
		public System.String Documento { get; set;} 

		[Alias("DIGITO_VERIFICACION")]
		[StringLength(1)]
		public System.String DigitoVerificacion { get; set;} 

		[Alias("NOMBRE")]
		[Required]
		[StringLength(120)]
		public System.String Nombre { get; set;} 

		[Alias("ID_CIUDAD")]
		public System.Int32 IdCiudad { get; set;} 

		[Alias("DIRECCION")]
		[StringLength(80)]
		public System.String Direccion { get; set;} 

		[Alias("TELEFONO")]
		[StringLength(15)]
		public System.String Telefono { get; set;} 

		[Alias("CELULAR")]
		[StringLength(15)]
		public System.String Celular { get; set;} 

		[Alias("ULTIMA_FACTURA")]
		public System.Int32 UltimaFactura { get; set;} 
		
		[Alias("ACTIVO")]
		public System.Boolean Activo { get; set;} 
		
		[Alias("ES_PROVEEDOR")]
		public System.Boolean EsProveedor { get; set;} 

		[Alias("ES_CLIENTE")]
		public System.Boolean EsCliente { get; set;} 

		[Alias("ES_AUTO_RETENEDOR")]
		public System.Boolean EsAutoRetenedor { get; set;} 

		[Alias("ES_EMPLEADO")]
		public System.Boolean EsEmpleado { get; set;} 

		[Alias("ES_EPS")]
		public System.Boolean EsEps { get; set;} 

		[Alias("ES_FP")]
		public System.Boolean EsFp { get; set;} 

		[Alias("ES_PARAFISCAL")]
		public System.Boolean EsParafiscal { get; set;} 

	}
}
