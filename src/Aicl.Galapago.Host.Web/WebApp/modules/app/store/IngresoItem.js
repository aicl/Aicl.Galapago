Ext.define('App.store.IngresoItem',{
	extend: 'Aicl.data.Store',
	model: 'App.model.IngresoItem',
	constructor: function(config){
		config=config||{};
		config.storeId=config.storeId||'IngresoItem';
		if(arguments.length==0) 
			this.callParent([config]);
		else 
			this.callParent(arguments);}
});