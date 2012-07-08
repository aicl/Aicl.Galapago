Ext.Loader.setConfig({enabled: true});
Ext.Loader.setPath('App', '../app');
    
Ext.application({
name: 'App',
appFolder: '../app',
launch: function(){
    Ext.create('Ext.form.Panel',{
  	width:990,
    id:'panelModule',
    frame: true,
    renderTo: 'module',
    layout: {
        type: 'table',
        columns: 2
    },
    items:[
     	{xtype:'egresolist'},
       	{
			xtype:'panel',
			height:352,
			width:440,
			baseCls:'x-plain',
			layout: {
    		type: 'vbox'       
    		},
			items:[
				{ xtype:'egresoform'}
			]	
		},
		{xtype:'egresoitemlist'},
        {
			xtype:'panel',
			height:352,
			width:440,
			baseCls:'x-plain',
			layout: {
       			type: 'vbox'       
    		},
			items:[
				{ xtype:'egresoitemform'}
			]	
		}
    ]
    });
    
    var controller =this.getController('Egreso');
    controller.refreshButtons();
    
    this.getController('EgresoItem').disableAll();
        
    controller.onselectionchange(
    function( sm,  selections,  eOpts){
    	var item = this.getController('EgresoItem');
    	console.log('controler proyecto,selections:->', selections);
    	if (selections.length){
    		var record= selections[0];
        	item.getEgresoItemStore().load({params:{IdEgreso: record.getId()}});
        	item.getEgresoItemList().determineScrollbars();
        	item.refreshButtons();
        	//item.getEgresoItemIdProyectoCombo().setValue( record.getId() );
        }
        else{
        	item.getEgresoItemStore().removeAll();
        	item.disableAll();
        }
    }, this);
    
    controller.onwrite(function(store, operation, eOpts ){
    	var item = this.getController('EgresoItem');
    	var record =  operation.getRecords()[0];                                    
        if (operation.action=='create') {
         	item.refreshButtons();
        	//item.getEgresoItemIdProyectoCombo().setValue( record.getId() );poner el id en el hijo?
        }    
    }, this);
    
},
    
controllers: ['Egreso','EgresoItem']
    
});