using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("ASIENTO_ITEM")]
	public partial class AsientoItem:IHasId<System.Int32>, IHasIdCentro{

		public AsientoItem(){}

		[Alias("ID")]
		[Sequence("ASIENTO_ITEM_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_ASIENTO")]
		public System.Int32 IdAsiento { get; set;} 

		[Alias("ID_CUENTA")]
		public System.Int32 IdCuenta { get; set;} 

		[Alias("VALOR")]
		[DecimalLength(15,2)]
		public System.Decimal Valor { get; set;} 

		[Alias("TIPO_PARTIDA")]
		public System.Int16 TipoPartida { get; set;} 

		[Alias("ID_CENTRO")]
		public System.Int32 IdCentro { get; set;} 
		
		
		[Alias("ID_TERCERO")]
		public System.Int32? IdTercero { get; set;} 

	}
}
