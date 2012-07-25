using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
    [Alias("COMPROBANTE_EGRESO_RT")]
    public partial class ComprobanteEgresoRetencion:IHasId<int>
    {
        public ComprobanteEgresoRetencion ()
        {
        }

        [Alias("ID")]
        [Sequence("COMPROBANTE_EGRESO_RT_ID_GEN")]
        [PrimaryKey]
        [AutoIncrement]
        public System.Int32 Id { get; set;} 

        [Alias("ID_COMPROBANTE_EGRESO_ITEM")]
        public System.Int32 IdComprobanteEgresoItem { get; set;} 

        [Alias("ID_COMPROBANTE_EGRESO")]
        public System.Int32 IdComprobanteEgreso { get; set;} 

        [Alias("ID_PRESUPUESTO_ITEM")]
        public System.Int32 IdPresupuestoItem { get; set;} 

        [Alias("VALOR")]
        [DecimalLength(15,2)]
        public System.Decimal Valor { get; set;} 

    }
}

