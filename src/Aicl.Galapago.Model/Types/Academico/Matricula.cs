using System;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;
using System.ComponentModel.DataAnnotations;

namespace Aicl.Galapago.Model.Types
{
	[Alias("MATRICULA")]
	[JoinTo(typeof(Curso),"IdCurso","Id", Order=0)]
	[JoinTo(typeof(Clase),"IdClase","Id",JoinType=JoinType.Left,Order=1)]
	[JoinTo(typeof(Ingreso),"IdIngreso","Id",JoinType=JoinType.Left,Order=2)]
	public partial class Matricula:IHasId<Int32>{

		public Matricula(){}

		[Alias("ID")]
		[Sequence("MATRICULA_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public Int32 Id { get; set;} 

		[Alias("ID_INFANTE")]
		public Int32 IdInfante { get; set;} 

		[Alias("ID_CURSO")]
		public Int32 IdCurso { get; set;} 

		[Alias("ID_INGRESO")]
		public Int32? IdIngreso { get; set;} 

		[Alias("ID_CLASE")]
		public Int32? IdClase { get; set;} 

		[Alias("ACTIVO")]
		public bool Activo { get; set;} 

		#region Curso
		[BelongsTo(typeof(Curso))]
		public String Descripcion { get; set;} 

		[BelongsTo(typeof(Curso))]
		public String Calendario { get; set;} 

		[BelongsTo(typeof(Curso))]
		public DateTime FechaInicio { get; set;} 

		[BelongsTo(typeof(Curso))]
		public DateTime FechaTerminacion { get; set;} 
		#endregion Curso

		#region Clase
		[BelongsTo(typeof(Clase))]
		public string Nombre {get; set;}
		#endregion Clase

		#region Ingreso
		[BelongsTo(typeof(Ingreso),"IdTercero")]
		public int? IdTercero { get; set;}

		[BelongsTo(typeof(Ingreso),"Valor")]
		[DecimalLength(15,2)]
		public Decimal? Valor { get; set;} 

		[BelongsTo(typeof(Ingreso),"Saldo")]
		[DecimalLength(15,2)]
		public Decimal? Saldo { get; set;} 

		[BelongsTo(typeof(Ingreso),"CodigoDocumento")]
		[StringLength(4)]
		public String CodigoDocumento { get; set;} 

		[BelongsTo(typeof(Ingreso),"Documento")]
		[StringLength(12)]
		public String Documento { get; set;} 
		#endregion Ingreso

	}
}
