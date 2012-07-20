Ext.define('App.store.ComprobanteEgreso',{
	extend: 'Aicl.data.Store',
	model: 'App.model.ComprobanteEgreso',
	constructor: function(config){config=config||{};config.storeId=config.storeId||'ComprobanteEgreso';if(arguments.length==0) this.callParent([config]);else this.callParent(arguments);}
});