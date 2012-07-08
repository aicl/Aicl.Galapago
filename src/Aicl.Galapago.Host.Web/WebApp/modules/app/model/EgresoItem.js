Ext.define('App.model.EgresoItem',{
	extend: 'Ext.data.Model',
	idProperty: 'Id',
	fields: 
	[
		{
			name: 'Id',
			type: 'int'
		},
		{
			name: 'IdEgreso',
			type: 'int'
		},
		{
			name: 'IdPresupuestoItem',
			type: 'int'
		},
		{
			name: 'TipoPartida',
			type: 'int'
		},
		{
			name: 'Valor',
			type: 'number'
		},
		{
			name: 'IdCentro',
			type: 'int'
		},
		{
			name: 'IdTercero',
			type: 'int'
		},
		{
			name: 'CodigoItem',
			type: 'string'
		},
		{
			name: 'NombreItem',
			type: 'string'
		},
		{
			name: 'NombreCentro',
			type: 'string'
		},
		{
			name: 'NombreTercero',
			type: 'string'
		},
		{
			name: 'DocumentoTercero',
			type: 'string'
		},
		{
			name: 'DVTercero',
			type: 'string'
		}
	]
});