Ext.define('App.model.Tercero',{
	extend: 'Ext.data.Model',
	idProperty: 'Id',
	fields:[{
			name: 'Id',
			type: 'int'
		},{
			name: 'IdTipoDocumento',
			type: 'int'
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
			name: 'IdCiudad',
			type: 'int'
		},{
			name: 'Direccion',
			type: 'string'
		},{
			name: 'Telefono',
			type: 'string'
		},{
			name: 'Celular',
			type: 'string'
		},{
			name: 'UltimaFactura',
			type: 'int'
		},{
			name: 'Activo',
			type: 'boolean'
		},{
			name: 'EsProveedor',
			type: 'boolean'
		},{
			name: 'EsCliente',
			type: 'boolean'
		},{
			name: 'EsAutoRetenedor',
			type: 'boolean'
		},{
			name: 'EsEmpleado',
			type: 'boolean'
		},{
			name: 'EsEps',
			type: 'boolean'
		},{
			name: 'EsFp',
			type: 'boolean'
		},{
			name: 'EsParafiscal',
			type: 'boolean'
		},{
			name: 'NombreDocumento',
			type: 'string'
		},{
			name: 'Mail',
			type: 'string'
		
		}]
});