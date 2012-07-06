Ext.define('App.store.RemoteTercero',{
	extend: 'Aicl.data.Store',
	model: 'App.model.Tercero',
	constructor: function(config){
		config=config||{};
		config.storeId=config.storeId||'RemoteTercero';
		config.pageSize= config.pageSize|| 12;
    	config.remoteSort=true;
    	config.proxy= Aicl.Util.createRestProxy({
    		url: config.url||(Aicl.Util.getUrlApi()+'/Tercero'),
    		totalProperty: 'TotalCount',
    		storeId:config.storeId,
        	pageParam:'page',
        	limitParam:'limit',
        	startParam:'start'
    	});
		
		if(arguments.length==0) this.callParent([config]);else this.callParent(arguments);}
});