Ext.define('App.store.ComprobanteEgresoRetencion',{
	extend: 'Aicl.data.Store',
	model: 'App.model.ComprobanteEgresoRetencion',
	constructor: function(config){config=config||{};config.storeId=config.storeId||'ComprobanteEgresoRetencion';if(arguments.length==0) this.callParent([config]);else this.callParent(arguments);}
});