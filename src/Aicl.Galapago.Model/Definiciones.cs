using System;
using System.Collections.Generic;
using System.Configuration;

namespace Aicl.Galapago.Model.Types
{
	public static class Definiciones
	{
        public static readonly string Egreso="EG";
		public static readonly string ComprobranteContable="CC";
		public static readonly string ComprobranteEgreso="CE";
              	
		public static readonly double LockSeconds=5;
		public static readonly double DiasEnCache=7;
				
		public static readonly string RegistroActivo="Activo";
		public static readonly string CuentaDetalleActiva="CuentaDetalleActiva";
        public static readonly string PrspItemActivo="PrspItemActivo";
		public static readonly string UsaTercero="UsaTercero";	

        public static readonly string CheckRequestBeforeUpdate="CheckRequestBeforeUpdate";
        public static readonly string CheckRequestBeforeAsentar="CheckRequestBeforeAsentar";
        public static readonly string CheckRequestBeforeReversar="CheckRequestBeforeReversar";
        public static readonly string CheckRequestBeforeAnular="CheckRequestBeforeAnular";

        public static readonly int IdCentroGeneral=1;
        public static readonly int IdPresupuestoGeneral=1;


        public static readonly string ProveedorFV="PRFV";
        public static readonly string ProveedorCC="PRCC";

        // los PrspItem >1 y >2 el IdCentro e IdPresupuesto=1....

        public static readonly string GrupoCajaBancos="9";
        public static readonly string GrupoRetenciones="6101";

		public static int   PrspNiveles{
			get{
				string pp= ConfigurationManager.AppSettings.Get("PresupuestoNiveles");
				int r;
				return int.TryParse(pp, out r)? r: 3;
			}
		}
		
		public static int   CntbNiveles{
			get{
				string pp= ConfigurationManager.AppSettings.Get("ContabilidadNiveles");
				int r;
				return int.TryParse(pp, out r)? r: 4;
			}
		}
		
		
		public static int   PrspPosicionPunto{
			get{
				return PrspNiveles*2-2;
			}
		}
		
		
		public static int   CntbPosicionPunto{
			get{
				return CntbNiveles*2-2;
			}
		}
		
	}
	
	
}


/*
        public static readonly string FacturaVenta="FV";
        public static readonly string CuentaVenta="CV";  // Cuenta de Cobro A favor..

        //public static readonly string CuentaDetalle="detalle";

        // El Documento Determina el codigo de la cuenta x pagar si es del caso....

        public static readonly string FacturaCompra="FC";
        public static readonly string CuentaDeCobro="CB";
        public static readonly string DocumentoNomina="DN";
        public static readonly string DocumentoSalud="DS";
        public static readonly string DocumentoPension="DP";
        public static readonly string DocumentoRiesgos="DR";
        public static readonly string DocumentoPF="DP";
        public static readonly string DocumentoLibranza="DL";
        public static readonly string DocumentoEmbargo="DE";
        public static readonly string DocumentoRetenfuente="DF";
        public static readonly string DocumentoRetIva="DI";
        public static readonly string DocumentoRetIca="DC";

        public static readonly string ReciboCaja="RC";
        //NotaDebito, NotaCredito

        //public static readonly string CuentaPorCobrar="CB";
        //public static readonly string CuentaPorPagar="PG";
*/      

/*
        public static readonly string CxPProveedores="5101.01";
        public static readonly string CxPRetenfuente="5102.01";
        public static readonly string CxPRetIva="5102.02";
        public static readonly string CxPRetIca="5102.03";
        public static readonly string CxPNomina="5103.01";
        public static readonly string CxPSalud="5104.01";
        public static readonly string CxPPension="5104.02";
        public static readonly string CxPRiesgos="5104.03";
        public static readonly string CxPPF="5105.01";
        public static readonly string CxPLibranzas="5106.01";
        public static readonly string CxPEmbargos="5106.02";
*/

        /*
        public static Dictionary<string, string> CodigosPresupuesto= new Dictionary<string, string>{

            {FacturaCompra, CxPProveedores},
            {CuentaDeCobro, CxPProveedores},
            {DocumentoNomina, CxPNomina},
            {DocumentoSalud, CxPSalud},
            {DocumentoPension,CxPPension},
            {DocumentoRiesgos,CxPRiesgos},
            {DocumentoPF, CxPPF},
            {DocumentoLibranza, CxPLibranzas},
            {DocumentoEmbargo, CxPEmbargos},
            {DocumentoRetenfuente, CxPRetenfuente},
            {DocumentoRetIva, CxPRetIva},
            {DocumentoRetIca, CxPRetIca}
        };
*/

