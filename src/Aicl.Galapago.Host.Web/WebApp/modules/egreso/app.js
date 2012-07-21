Ext.Loader.setConfig({enabled: true});
Ext.Loader.setPath('App', '../app');
    
Ext.application({
	name: 'App',
	appFolder: '../app',
	launch: function(){
	    Ext.create('Ext.container.Viewport',{
	    id:'panelModule',
	    renderTo: 'module',
	    layout: 'fit',
	    items:[{
	    	xtype:'egresopanel'
	    }]
	})},
	
	controllers: ['Egreso']
});