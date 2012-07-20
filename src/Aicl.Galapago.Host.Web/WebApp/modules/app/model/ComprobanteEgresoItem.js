Ext.define('App.model.ComprobanteEgresoItem',{
	extend: 'Ext.data.Model',
	idProperty: 'Id',
	fields: 
	[
		{
			name: 'Id',
			type: 'int'
		},
		{
			name: 'IdComprobanteEgreso',
			type: 'int'
		},
		{
			name: 'IdEgreso',
			type: 'int'
		},
		{
			name: 'Valor',
			type: 'number'
		}
	]
});