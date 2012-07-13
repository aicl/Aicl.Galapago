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
    var itemController= this.getController('EgresoItem');
    
    controller.refreshButtons();
    itemController.disableAll();
        
    controller.onselectionchange(
    function( sm,  selections,  eOpts){
    	itemController.getCentroAutorizadoCombo().getStore().removeAll();
    	itemController.getEgresoItemStore().removeAll();
    		
    	if (selections.length){
    		this.cargarItems(selections[0],itemController);   		
        }
        else{
        	itemController.setIdEgreso(0);
        	itemController.disableAll();
        }
    }, this);
    
    controller.onwrite(function(store, operation, eOpts){
    	var record =  operation.getRecords()[0];                                    
        if (operation.action=='create') {
        	this.cargarItems(record,itemController);
        }    
    }, this);
    
    controller.onanulado(function(store, record, success ){
    	if(success) itemController.disableAllToolbars();    
    }, this);
    
    controller.onasentado(function(store, record, success ){
    	if(success) itemController.disableAllToolbars();    
    }, this);
    
    controller.onreversado(function(store, record, success ){
    	if(success) itemController.enableAllToolbars();    
    }, this);
    
    itemController.onwrite(function(store, operation, eOpts){
    	
    	var totalD=0, totalC=0;
    	store.each(function(rec){
    		if(rec.get('TipoPartida')==1) 
    			totalD=totalD+rec.get('Valor');
    		else
    			totalC=totalC+rec.get('Valor');
    	});
    	var record = controller.getEgresoList().getSelectionModel().getSelection()[0];
    	controller.getEgresoStore().updateLocal({Id: record.getId(), Valor:totalD, Saldo:totalD-totalC});
    
    }, this);
    
},
    
controllers: ['Egreso','EgresoItem'],

cargarItems:function(record, itemController){

	var codigoEgreso= this.getController('Egreso').getCodigoEgresoCombo().getStore().getById(record.get('CodigoDocumento'));
    		    		    		
    itemController.getCentroAutorizadoCombo().getStore().loadRawData(getCentrosData(record.get('IdSucursal')));
    		
    itemController.setCodigoEgreso(codigoEgreso);
    itemController.setIdEgreso(record.getId());
    
    if(record.get('Valor')!=0){
    	itemController.getEgresoItemStore().load({params:{IdEgreso: record.getId()}});
    	itemController.getEgresoItemList().determineScrollbars();
    }

    if(record.get('FechaAnulado') || record.get('FechaAsentado'))
    	itemController.disableAllToolbars();
    else
        itemController.enableAllToolbars();
        
    itemController.refreshButtons();
	
}
    
});