using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;
namespace Aicl.Galapago.Model.Types
{
    [Alias("USUARIO_GIRADORA")]
    public partial class UsuarioGiradora
    {
        public UsuarioGiradora ()
        {
        }

        [Alias("ID")]
        [Sequence("EGRESO_ID_GEN")]
        [PrimaryKey]
        [AutoIncrement]
        public int Id {get; set;}

        [Alias("ID_USUARIO")]
        public int IdUsuario {get; set;}

        [Alias("ID_PRESUPUESTO_ITEM")]
        public int IdPresupuestoItem {get; set;}

        [Alias("ID_TERCERO")]
        public int? IdTercero { get; set;}

        [Alias("DESCRIPCION")]
        [Required]
        [StringLength(50)]
        public string Descripcion {get; set;}


    }
}

