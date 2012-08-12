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
		
		if(arguments.length==0) this.callParent([config]);else this.callParent(arguments);
	},
	canCreate:function(){
		 return Aicl.Util.hasPermission(Ext.String.format('{0}.create', 'Tercero'));
	},
    canRead:function(){
		 return Aicl.Util.hasPermission(Ext.String.format('{0}.read', 'Tercero'));
	},
	canUpdate:function(){
		 return Aicl.Util.hasPermission(Ext.String.format('{0}.update', 'Tercero'));
	},
	canDestroy:function(){
		 return Aicl.Util.hasPermission(Ext.String.format('{0}.destroy', 'Tercero'));
	},
	canExecute:function(operation){
		 return Aicl.Util.hasPermission(Ext.String.format('{0}.{1}', 'Tercero',operation));
	}
});