using System;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
    [Alias("COMPROBANTE_INGRESO_RT")]
    public partial class ComprobanteIngresoRetencion:IHasId<int>
    {
        public ComprobanteIngresoRetencion ()
        {
        }

        [Alias("ID")]
        [Sequence("COMPROBANTE_INGRESO_RT_ID_GEN")]
        [PrimaryKey]
        [AutoIncrement]
        public Int32 Id { get; set;} 

        [Alias("ID_COMPROBANTE_INGRESO_ITEM")]
        public Int32 IdComprobanteIngresoItem { get; set;} 

        [Alias("ID_COMPROBANTE_INGRESO")]
        public Int32 IdComprobanteIngreso { get; set;} 

        [Alias("ID_PRESUPUESTO_ITEM")]
        public Int32 IdPresupuestoItem { get; set;} 

        [Alias("VALOR")]
        [DecimalLength(15,2)]
        public Decimal Valor { get; set;} 

    }
}

