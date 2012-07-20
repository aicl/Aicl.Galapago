Ext.define('App.model.ComprobanteEgresoRetencion',{
	extend: 'Ext.data.Model',
	idProperty: 'Id',
	fields: 
	[
		{
			name: 'Id',
			type: 'int'
		},
		{
			name: 'IdComprobanteEgresoItem',
			type: 'int'
		},
		{
			name: 'IdComprobanteEgreso',
			type: 'int'
		},
		{
			name: 'IdPresupuestoItem',
			type: 'int'
		},
		{
			name: 'Valor',
			type: 'number'
		}
	]
});