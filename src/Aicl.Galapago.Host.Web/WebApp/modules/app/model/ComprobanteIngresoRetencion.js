Ext.define('App.model.ComprobanteIngresoRetencion',{
	extend: 'Ext.data.Model',
	idProperty: 'Id',
	fields: 
	[
		{
			name: 'Id',
			type: 'int'
		},
		{
			name: 'IdComprobanteIngresoItem',
			type: 'int'
		},
		{
			name: 'IdComprobanteIngreso',
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