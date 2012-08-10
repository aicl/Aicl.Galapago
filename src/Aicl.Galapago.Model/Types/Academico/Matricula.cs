using System;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("MATRICULA")]
	[JoinTo(typeof(Curso),"IdCurso","Id")]
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
		public String Nombre { get; set;} 

		[BelongsTo(typeof(Curso))]
		public DateTime FechaInicio { get; set;} 

		[BelongsTo(typeof(Curso))]
		public DateTime FechaTerminacion { get; set;} 
		#endregion Curso

	}
}
