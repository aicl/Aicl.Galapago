using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
    [JoinTo(typeof(Tercero),"IdTercero","Id")]
    [Alias("INFANTE_ACUDIENTE")]
    public partial class InfanteAcudiente:IHasId<Int32>{

        public InfanteAcudiente(){}

        [Alias("ID")]
        [Sequence("INFANTE_ACUDIENTE_ID_GEN")]
        [PrimaryKey]
        [AutoIncrement]
        public Int32 Id { get; set;} 

        [Alias("ID_INFANTE")]
        public Int32 IdInfante { get; set;} 

        [Alias("ID_TERCERO")]
        public Int32 IdTercero { get; set;} 

        #region Tercero
        [BelongsTo(typeof(Tercero),"Documento")]
        public string DocumentoTercero {get;set;}

        [BelongsTo(typeof(Tercero),"DigitoVerificacion")]
        public string DVTercero {get;set;}

        [BelongsTo(typeof(Tercero),"Nombre")]
        public string NombreTercero {get;set;}
        #endregion Tercero

    }
}