Ext.define('App.store.ComprobanteIngresoRetencion',{
	extend: 'Aicl.data.Store',
	model: 'App.model.ComprobanteIngresoRetencion',
	constructor: function(config){config=config||{};config.storeId=config.storeId||'ComprobanteIngresoRetencion';if(arguments.length==0) this.callParent([config]);else this.callParent(arguments);}
});