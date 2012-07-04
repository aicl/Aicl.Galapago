Ext.define('App.store.RemoteTercero',{
	extend: 'Aicl.data.Store',
	model: 'App.model.Tercero',
	constructor: function(config){
		config=config||{};
		config.storeId='RemoteTercero';
		config.pageSize= 12;
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

/*
Ext.define('App.store.RemoteTercero', {
	extend: 'Ext.data.Store',
	model: 'App.model.Tercero',
    storeId: 'RemoteTercero',
    proxy: {
        type: 'ajax',
        url: Aicl.Util.getUrlApi()+'/Tercero/read',
        reader: {
            type: 'json',
            root: 'Data',
            totalProperty: 'TotalCount'
        }
    }
});
*/
