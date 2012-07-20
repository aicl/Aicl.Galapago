Ext.define('App.store.ComprobanteEgresoItem',{
	extend: 'Aicl.data.Store',
	model: 'App.model.ComprobanteEgresoItem',
	constructor: function(config){config=config||{};config.storeId=config.storeId||'ComprobanteEgresoItem';if(arguments.length==0) this.callParent([config]);else this.callParent(arguments);}
});