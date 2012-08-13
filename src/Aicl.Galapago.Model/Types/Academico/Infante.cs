using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[JoinTo(typeof(Tercero),"IdTerceroFactura","Id",JoinType=JoinType.Left)]
	[Alias("INFANTE")]
	public partial class Infante:IHasId<Int32>{

		public Infante(){}

		[Alias("ID")]
		[Sequence("INFANTE_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public Int32 Id { get; set;} 

		[Alias("DOCUMENTO")]
		[Required]
		[StringLength(15)]
		public String Documento { get; set;} 

		[Alias("NOMBRES")]
		[Required]
		[StringLength(60)]
		public String Nombres { get; set;} 

		[Alias("APELLIDOS")]
		[Required]
		[StringLength(60)]
		public String Apellidos { get; set;} 

		[Alias("ID_TERCERO_FACTURA")]
		public Int32? IdTerceroFactura { get; set;} 

		[Alias("FECHA_NACIMIENTO")]
		public DateTime FechaNacimiento { get; set;} 

		[Alias("SEXO")]
		[StringLength(1)]
		public String Sexo { get; set;} 

		[Alias("DIRECCION")]
		[StringLength(80)]
		public String Direccion { get; set;} 

		[Alias("TELEFONO")]
		[StringLength(15)]
		public String Telefono { get; set;} 

		[Alias("CELULAR")]
		[StringLength(15)]
		public String Celular { get; set;} 

		[Alias("EMAIL")]
		[StringLength(80)]
		public String Mail { get; set;} 

		[Alias("COMENTARIO")]
		[StringLength(120)]
		public String Comentario { get; set;} 

		#region Tercero
        [BelongsTo(typeof(Tercero),"Documento")]
        public string DocumentoTercero {get;set;}

        [BelongsTo(typeof(Tercero),"DigitoVerificacion")]
        public string DVTercero {get;set;}

        [BelongsTo(typeof(Tercero),"Nombre")]
        public string NombreTercero {get;set;}

		[BelongsTo(typeof(Tercero),"Celular")]
        public string CelularTercero {get;set;}

		[BelongsTo(typeof(Tercero),"Telefono")]
        public string TelefonoTercero {get;set;}

		[BelongsTo(typeof(Tercero),"Mail")]
        public string MailTercero {get;set;}
        #endregion Tercero

	}
}
