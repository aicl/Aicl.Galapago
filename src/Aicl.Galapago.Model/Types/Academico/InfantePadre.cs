using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[JoinTo(typeof(Tercero),"IdTercero","Id")]
	[Alias("INFANTE_PADRE")]
	public partial class InfantePadre:IHasId<Int32>{

		public InfantePadre(){}

		[Alias("ID")]
		[Sequence("INFANTE_PADRE_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public Int32 Id { get; set;} 

		[Alias("ID_INFANTE")]
		public Int32 IdInfante { get; set;} 

		[Alias("ID_TERCERO")]
		public Int32 IdTercero { get; set;} 

		[Alias("PARENTESCO")]
		[Required]
		[StringLength(30)]
		public String Parentesco { get; set;} 

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
