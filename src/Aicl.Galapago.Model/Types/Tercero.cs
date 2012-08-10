using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("TERCERO")]
    //[JoinTo(typeof(Tercero),typeof(TipoDocumento),"IdTipoDocumento","Id", Order=0)] // asi produce stackOverflow!
    [JoinTo(typeof(TipoDocumento),"IdTipoDocumento","Id", Order=0)]
	public partial class Tercero:IHasId<System.Int32>{

		public Tercero(){}

		[Alias("ID")]
		[Sequence("TERCERO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public Int32 Id { get; set;} 

		[Alias("ID_TIPO_DOCUMENTO")]
		public Int32 IdTipoDocumento { get; set;} 

		[Alias("DOCUMENTO")]
		[Required]
		[StringLength(13)]
		public String Documento { get; set;} 

		[Alias("DIGITO_VERIFICACION")]
		[StringLength(1)]
		public String DigitoVerificacion { get; set;} 

		[Alias("NOMBRE")]
		[Required]
		[StringLength(120)]
		public String Nombre { get; set;} 

		[Alias("ID_CIUDAD")]
		public Int32 IdCiudad { get; set;} 

		[Alias("DIRECCION")]
		[StringLength(80)]
		public String Direccion { get; set;} 

		[Alias("TELEFONO")]
		[StringLength(15)]
		public String Telefono { get; set;} 

		[Alias("CELULAR")]
		[StringLength(15)]
		public String Celular { get; set;} 

		[Alias("ULTIMA_FACTURA")]
		public Int32 UltimaFactura { get; set;} 
		
		[Alias("ACTIVO")]
		public Boolean Activo { get; set;} 
		
		[Alias("ES_PROVEEDOR")]
		public Boolean EsProveedor { get; set;} 

		[Alias("ES_CLIENTE")]
		public Boolean EsCliente { get; set;} 

		[Alias("ES_AUTO_RETENEDOR")]
		public Boolean EsAutoRetenedor { get; set;} 

		[Alias("ES_EMPLEADO")]
		public Boolean EsEmpleado { get; set;} 

		[Alias("ES_EPS")]
		public Boolean EsEps { get; set;} 

		[Alias("ES_FP")]
		public Boolean EsFp { get; set;} 

		[Alias("ES_PARAFISCAL")]
		public Boolean EsParafiscal { get; set;} 

		[Alias("EMAIL")]
		[StringLength(80)]
		public String Mail { get; set;} 

        #region TipoDocumento
        [BelongsTo(typeof(TipoDocumento),"Nombre")]
        public string NombreDocumento {get;set;}
        #endregion TipoDocumento


	}
}
