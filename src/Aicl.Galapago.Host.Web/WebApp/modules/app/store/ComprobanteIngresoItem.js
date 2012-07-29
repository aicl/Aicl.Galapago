Ext.define('App.store.ComprobanteIngresoItem',{
	extend: 'Aicl.data.Store',
	model: 'App.model.ComprobanteIngresoItem',
	constructor: function(config){config=config||{};config.storeId=config.storeId||'ComprobanteIngresoItem';if(arguments.length==0) this.callParent([config]);else this.callParent(arguments);}
});