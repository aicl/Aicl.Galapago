using System;

namespace Aicl.Galapago.Model.Types
{
	public class CentroAutorizado
	{
		public CentroAutorizado ()
		{
		}
		public int Id { get; set;}
		public string Nombre { get; set;}
		public string Codigo { get; set;}
		public int IdSucursal { get; set;}
	}
}

/*
 SELECT a.ID as "Id",  a.ID_SUCURSAL as "IdSucursal", a.ID_CENTRO as "IdCentro",
S.CODIGO AS "CodigoSucursal", S.NOMBRE as "NombreSucursal",
C.CODIGO as "CodigoCentro", C.NOMBRE as "NombreCentro"
FROM USUARIO_SUCURSAL_CENTRO a
JOIN SUCURSAL S ON S.ID= A.ID_SUCURSAL
JOIN CENTRO C ON C.ID= A.ID_CENTRO
WHERE A.ID_USUARIO=2
 
 */


