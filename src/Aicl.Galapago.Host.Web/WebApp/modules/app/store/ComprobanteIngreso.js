Ext.define('App.store.ComprobanteIngreso',{
	extend: 'Aicl.data.Store',
	model: 'App.model.ComprobanteIngreso',
	constructor: function(config){config=config||{};config.storeId=config.storeId||'ComprobanteIngreso';if(arguments.length==0) this.callParent([config]);else this.callParent(arguments);}
});