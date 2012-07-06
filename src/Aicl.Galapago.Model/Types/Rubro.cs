using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
    [SelectFrom(typeof(Presupuesto))]
    [JoinTo(typeof(PresupuestoItem),"Id", "IdPresupuesto")]
    public class Rubro
    {
        public Rubro (){}

        [BelongsTo(typeof(PresupuestoItem))]
        public System.Int32 Id { get; set;} 

        [BelongsTo(typeof(PresupuestoItem))]
        public System.Int32 IdPresupuesto { get; set;} 

        [BelongsTo(typeof(PresupuestoItem))]
        public System.String Codigo { get; set;} 

        [BelongsTo(typeof(PresupuestoItem))]
        public System.Int32 TipoItem { get; set;} 

        [BelongsTo(typeof(PresupuestoItem))]
        public System.String Nombre { get; set;} 

        [BelongsTo(typeof(PresupuestoItem))]
        public System.Decimal Presupuestado { get; set;} 

        [BelongsTo(typeof(PresupuestoItem))]
        public System.Decimal Reservado { get; set;} 

        [BelongsTo(typeof(PresupuestoItem))]
        public System.Decimal SaldoAnterior { get; set;} 

        [BelongsTo(typeof(PresupuestoItem))]
        public System.Decimal Debitos { get; set;} 

        [BelongsTo(typeof(PresupuestoItem))]
        public System.Decimal Creditos { get; set;} 

        [BelongsTo(typeof(PresupuestoItem))]
        public System.Boolean UsaTercero { get; set;} 

        [BelongsTo(typeof(PresupuestoItem))]
        public System.Boolean Activo { get; set;} 

        #region Presupuesto
        [BelongsTo(typeof(Presupuesto))]
        public System.Int32 IdSucursal { get; set;} 

        [BelongsTo(typeof(Presupuesto))]
        public System.Int32 IdCentro { get; set;} 

        [BelongsTo(typeof(Presupuesto),"Activo")]
        public bool PresupuestoActivo { get; set;} 

        #endregion Presupuesto

    }
}


/*

 // From "Person" "Person" 
    [SelectFrom(typeof(Person))]
    // join "City" "City" on "Person"."BirthCityId"= "City"."Id"
    [JoinTo(typeof(City),"BirthCityId","Id")]  
    public class Join1{

        public Join1(){}

        //SELECT "Person"."PersonName" as "Name"  //all info is taken from property Name of SelectFrom Person
        public  string Name {get; set;}

        //"City.People" as Population
        [BelongsTo(typeof(City))]       //all info is taken from property Population of City 
        public int Population { get; set;}

    }

*/