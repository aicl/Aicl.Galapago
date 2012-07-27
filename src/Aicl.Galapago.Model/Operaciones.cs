using System;
namespace Aicl.Galapago.Model.Types
{
	public static class Operaciones
	{
		public static readonly string Create="create";
		public static readonly string Read="read";
		public static readonly string Update="update";
		public static readonly string Destroy="destroy";
		
		public static readonly string Asentar="asentar";
		public static readonly string Reversar="reversar";
        public static readonly string Anular="anular";

        public static readonly string InsertarEgresoEnCE= "insertaregresoence";
        public static readonly string ActualizarEgresoEnCE= "actualizaregresoence";
        public static readonly string BorrarEgresoEnCE= "borraregresoence";

        public static readonly string ActualizarValorEgresoAlAsentarCE= "actualizarvaloregresoalasentarce";

        public static readonly string InsertarRetencionEnCE= "insertarretencionence";
        public static readonly string BorrarRetencionEnCE= "borrarretencionence";

		public static readonly string InsertarIngresoEnCI= "insertaringresoenci";
        public static readonly string ActualizarIngresoEnCI= "actualizaringresoenci";
        public static readonly string BorrarIngresoEnCI= "borraringresoenci";

        public static readonly string ActualizarValorIngresoAlAsentarCI= "actualizarvaloringresoalasentarci";

        public static readonly string InsertarRetencionEnCI= "insertarretencionenci";
        public static readonly string BorrarRetencionEnCI= "borrarretencionenci";

	}
}

