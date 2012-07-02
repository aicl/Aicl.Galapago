using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("INFANTE")]
	public partial class Infante:IHasId<System.Int32>{

		public Infante(){}

		[Alias("ID")]
		[Sequence("INFANTE_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_TIPO_DOCUMENTO")]
		public System.Int32 IdTipoDocumento { get; set;} 

		[Alias("DOCUMENTO")]
		[Required]
		[StringLength(15)]
		public System.String Documento { get; set;} 

		[Alias("NOMBRE")]
		[Required]
		[StringLength(120)]
		public System.String Nombre { get; set;} 

		[Alias("ID_TERCERO_FACTURA")]
		public System.Int32? IdTerceroFactura { get; set;} 

		[Alias("FECHA_NACIMIENTO")]
		public System.DateTime FechaNacimiento { get; set;} 

		[Alias("SEXO")]
		[StringLength(1)]
		public System.String Sexo { get; set;} 

	}
}
