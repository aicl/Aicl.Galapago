Ext.define('App.store.EgresoItem',{
	extend: 'Aicl.data.Store',
	model: 'App.model.EgresoItem',
	constructor: function(config){config=config||{};config.storeId=config.storeId||'EgresoItem';if(arguments.length==0) this.callParent([config]);else this.callParent(arguments);}
});