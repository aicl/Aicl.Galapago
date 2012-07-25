Ext.define('App.model.SaldoTercero',{
	extend: 'Ext.data.Model',
	idProperty: 'Id',
	fields:[{
			name: 'Id',
			type: 'int'
		},{
			name: 'IdPresupuestoItem',
			type: 'int'
		},{
			name: 'IdSucursal',
			type: 'int'
		},{
			name: 'IdTercero',
			type: 'int'
		},{
			name: 'SaldoInicial',
			type: 'number'
		},{
			name: 'Debitos',
			type: 'number'
		},{
			name: 'Creditos',
			type: 'number'
		},{
			name: 'NombreSucursal',
			type: 'string'
		},{
			name: 'Documento',
			type: 'string'
		},{
			name: 'DigitoVerificacion',
			type: 'string'
		},{
			name: 'Nombre',
			type: 'string'
		},{
			name: 'NombreDocumento',
			type: 'string'
		},{
			name: 'CodigoItem',
			type: 'string'
		},{
			name: 'NombreItem',
			type: 'string'
		}]
});