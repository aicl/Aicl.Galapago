Ext.define('App.model.IngresoItem',{
	extend: 'Ext.data.Model',
	idProperty: 'Id',
	fields:[{
			name: 'Id',
			type: 'int'
		},{
			name: 'IdIngreso',
			type: 'int'
		},{
			name: 'IdPresupuestoItem',
			type: 'int'
		},{
			name: 'TipoPartida',
			type: 'int'
		},{
			name: 'Valor',
			type: 'number'
		},{
			name: 'IdCentro',
			type: 'int'
		},{
			name: 'CodigoItem',
			type: 'string'
		},{
			name: 'NombreItem',
			type: 'string'
		},{
			name: 'NombreCentro',
			type: 'string'
		}]
});