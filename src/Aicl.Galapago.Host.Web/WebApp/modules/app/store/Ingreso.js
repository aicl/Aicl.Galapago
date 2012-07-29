Ext.define('App.store.Ingreso',{
	extend: 'Aicl.data.Store',
	model: 'App.model.Ingreso',
	constructor: function(config){
		config=config||{};
		config.storeId=config.storeId||'Ingreso';
		config.pageSize= 12;
    	
    	config.remoteSort=true;
    	config.proxy= Aicl.Util.createRestProxy({
    		url: config.url||(Aicl.Util.getUrlApi()+'/Ingreso'),
    		totalProperty: 'TotalCount',
    		storeId:config.storeId,
        	pageParam:'page',
        	limitParam:'limit',
        	startParam:'start'
    	});
			
		if(arguments.length==0) this.callParent([config]);else this.callParent(arguments);}
});