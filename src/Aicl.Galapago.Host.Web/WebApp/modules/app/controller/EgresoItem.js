Ext.define('App.controller.EgresoItem',{
	extend: 'Ext.app.Controller',
    stores: ['EgresoItem'],  
    models: ['EgresoItem'],
    views:  ['egresoitem.List','egresoitem.Form' ],
    refs:[
    	{ref: 'egresoItemList',    	 selector: 'egresoitemlist' },
    	{ref: 'egresoItemDeleteButton', selector: 'egresoitemlist button[action=delete]' },
    	{ref: 'egresoItemNewButton',    selector: 'egresoitemlist button[action=new]' },
    	{ref: 'egresoItemForm',    	 selector: 'egresoitemform' }, 
    	{ref: 'egresoItemSaveButton', 	 selector: 'egresoitemform button[action=save]' },
    	{ref: 'centroAutorizadoCombo',    	 selector: 'egresoitemform centroautorizadocombo'},
    	{ref: 'rubroCombo',    	 selector: 'egresoitemform rubrocombo' },
    	{ref: 'tipoEgresoItemCombo',    	 selector: 'egresoitemform tipoegresoitemcombo' }
    	
    	
    ],

    init: function(application) {
    	    	
        this.control({
            'egresoitemlist': { 
                selectionchange: function( sm,  selections,  eOpts){
                	this.refreshButtons(selections);
                }
            },
            
            'egresoitemlist button[action=delete]': {
                click: function(button, event, options){
                	var grid = this.getEgresoItemList();
                	var record = grid.getSelectionModel().getSelection()[0];
        			this.getEgresoItemStore().remove(record);
                }
            },
            
            'egresoitemlist button[action=new]': {
            	click:function(button, event, options){
            		this.getEgresoItemList().getSelectionModel().deselectAll();
            	}
            },
            
            'egresoitemform button[action=save]':{            	
            	click:function(button, event, options){
            		var model = this.getEgresoItemStore();
            		var record = this.getEgresoItemForm().getForm().getFieldValues(true);
            		this.getEgresoItemStore().save(record);
            	}
            },
            'egresoitemform tipoegresoitemcombo':{
            	select:function(combo, value, options){
            		var data = value[0].data;
            		this.getCentroAutorizadoCombo().getStore().clearFilter(true);
            		if(data.Id==1)
            			this.getCentroAutorizadoCombo().getStore().
            				filter([{filterFn: function(item) { return item.getId() > 1; }}])
            		else
            			this.getCentroAutorizadoCombo().getStore().
            				filter([{filterFn: function(item) { return item.getId() == 1; }}])
            		
            		var record= this.getCentroAutorizadoCombo().getStore().getAt(0);
            		if(record) this.getCentroAutorizadoCombo().setValue(record.get('Id'));
            		else this.getCentroAutorizadoCombo().setValue(0); 
            		this.getCentroAutorizadoCombo().fireEvent('select', combo, [record]);
            	}
            },
            'egresoitemform centroautorizadocombo':{
            	select:function(combo, value, options){
            		console.log('egresoitemform centroautorizadocombo select....',value, this.codigoEgreso);
            		var rc = value[0];
            		var permitidos=(this.getTipoEgresoItemCombo().getValue()==1)?'DebitosPermitidos':'CreditosPermitidos';
            		var codigos=this.codigoEgreso.get(permitidos);
            		var cd=[];
            		if(this.getTipoEgresoItemCombo().getValue()==1) cd=codigos;
            		else{
            			if(this.getTipoEgresoItemCombo().getValue()==2 ){
            				for(var i in codigos)
            					if(codigos[i].substring(0,1)!='9') cd.push(codigos[i]);
            			}
            			else{
            				for(var i in codigos)
            					if(codigos[i].substring(0,1)=='9') cd.push(codigos[i]);
            			}
            		}            		
            		
            		var rubroData=getRubrosData(rc.get('IdSucursal'), rc.get('Id'), cd);
            		this.getRubroCombo().getStore().loadRawData(rubroData);
            	}
            }
        });
    },
    
    onLaunch: function(application){
    	
    	this.codigoEgreso= {};
    	
    	this.getEgresoItemStore().on('write', function(store, operation, eOpts ){
    		var record =  operation.getRecords()[0];                                    
            if (operation.action != 'destroy') {
               this.getEgresoItemList().getSelectionModel().select(record,true,true);
               this.refreshButtons([record]);
            }
    	}, this);
    },
        	
	refreshButtons: function(selections){	
		selections=selections||[];
		this.getEgresoItemNewButton().setDisabled(!this.getEgresoItemStore().canCreate());
		if (selections.length){
        	this.getEgresoItemForm().getForm().loadRecord(selections[0]);
            this.getEgresoItemSaveButton().setText('Update');
            this.getEgresoItemDeleteButton().setDisabled(!this.getEgresoItemStore().canDestroy());
            this.getEgresoItemSaveButton().setDisabled(!this.getEgresoItemStore().canUpdate());
        }
        else{
        	this.getEgresoItemForm().getForm().reset();            
        	this.getEgresoItemSaveButton().setText('Add');
        	this.getEgresoItemDeleteButton().setDisabled(true);
        	this.getEgresoItemSaveButton().setDisabled(!this.getEgresoItemStore().canCreate());
        	this.getEgresoItemForm().setFocus();
        };
        this.enableAll();
	},
	
	disableForm:function(){
		this.getEgresoItemForm().setDisabled(true);
	},
	
	enableForm:function(){
		this.getEgresoItemForm().setDisabled(false);	
	},

	disableList:function(){
		this.getEgresoItemList().setDisabled(true);
	},
	
	enableList:function(){
		this.getEgresoItemList().setDisabled(false);
	},
	
	disableAll: function(){
		this.getEgresoItemList().setDisabled(true);
		this.getEgresoItemForm().setDisabled(true);
	},
	
	enableAll: function(){
		this.getEgresoItemList().setDisabled(false);
		this.getEgresoItemForm().setDisabled(false);
	},
	
	onselectionchange:function(fn, scope){
		this.getEgresoItemList().on('selectionchange', fn, scope);
	},
	
	onwrite:function(fn, scope){
		this.getEgresoItemStore().on('write', fn, scope);
	},
	
	// ce del modelo CodigoEI
	setCodigoEgreso:function(ce){
		this.codigoEgreso=ce;
	}
	
});
