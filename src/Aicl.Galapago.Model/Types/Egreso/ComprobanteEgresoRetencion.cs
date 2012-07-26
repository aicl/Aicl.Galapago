using System;
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
        public Int32 Id { get; set;} 

        [Alias("ID_COMPROBANTE_EGRESO_ITEM")]
        public Int32 IdComprobanteEgresoItem { get; set;} 

        [Alias("ID_COMPROBANTE_EGRESO")]
        public Int32 IdComprobanteEgreso { get; set;} 

        [Alias("ID_PRESUPUESTO_ITEM")]
        public Int32 IdPresupuestoItem { get; set;} 

        [Alias("VALOR")]
        [DecimalLength(15,2)]
        public Decimal Valor { get; set;} 

    }
}

