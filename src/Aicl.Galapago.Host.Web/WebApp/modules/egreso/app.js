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
    	item.getCentroAutorizadoCombo().getStore().removeAll();
    	item.getEgresoItemStore().removeAll();
    		
    	if (selections.length){
    		this.cargarItems(selections[0],item);   		
        }
        else{
        	item.setIdEgreso(0);
        	item.disableAll();
        }
    }, this);
    
    controller.onwrite(function(store, operation, eOpts ){
    	var item = this.getController('EgresoItem');
    	var record =  operation.getRecords()[0];                                    
        if (operation.action=='create') {
        	this.cargarItems(record,item);
        }    
    }, this);
    
    controller.onanulado(function(store, record, success ){
    	var item = this.getController('EgresoItem');
    	if(success) item.disableAll();    
    }, this);
    
    
},
    
controllers: ['Egreso','EgresoItem'],

cargarItems:function(record, item){

	var codigoEgreso= this.getController('Egreso').getCodigoEgresoCombo().getStore().getById(record.get('CodigoDocumento'));
    		    		    		
    item.getCentroAutorizadoCombo().getStore().loadRawData(getCentrosData(record.get('IdSucursal')));
    		
    item.setCodigoEgreso(codigoEgreso);
    item.setIdEgreso(record.getId());
    
    if(record.get('Valor')!=0){
    	item.getEgresoItemStore().load({params:{IdEgreso: record.getId()}});
    	item.getEgresoItemList().determineScrollbars();
    }

    if(record.get('FechaAnulado') || record.get('FechaAsentado')){
    	item.disableAll();
    	return ;
    }
    
    item.refreshButtons();
	
}
    
});