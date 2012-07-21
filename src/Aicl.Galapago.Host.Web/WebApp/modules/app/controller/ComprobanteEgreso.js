Ext.define('App.controller.ComprobanteEgreso',{
	extend: 'Ext.app.Controller',
    stores: ['ComprobanteEgreso','ComprobanteEgresoItem','ComprobanteEgresoRetencion'],  
    models: ['ComprobanteEgreso','ComprobanteEgresoItem','ComprobanteEgresoRetencion'],
    views:  ['comprobanteegreso.Panel' ],
    refs:[
    	{ref: 'comprobanteEgresoList',    	 selector: 'comprobanteegresolist' },
    	{ref: 'comprobanteEgresoForm',    	 selector: 'comprobanteegresoform' },
    	
    	{ref: 'comprobanteEgresoDeleteButton', selector: 'comprobanteegresopanel button[action=delete]' },
    	{ref: 'comprobanteEgresoNewButton',    selector: 'comprobanteegresopanel button[action=new]' }, 
    	{ref: 'comprobanteEgresoSaveButton', 	 selector: 'comprobanteegresopanel button[action=save]' }
    ],

    init: function(application) {
    	    	
        this.control({
            'comprobanteegresolist': { 
                selectionchange: function( sm,  selections,  eOpts){
                	this.refreshButtons(selections);
                }
            },
            
            'comprobanteegresolist button[action=delete]': {
                click: function(button, event, options){
                	var grid = this.getComprobanteEgresoList();
                	var record = grid.getSelectionModel().getSelection()[0];
        			this.getComprobanteEgresoStore().remove(record);
                }
            },
            
            'comprobanteegresolist button[action=new]': {
            	click:function(button, event, options){
            		this.getComprobanteEgresoList().getSelectionModel().deselectAll();
            	}
            },
            
            'comprobanteegresoform button[action=save]':{            	
            	click:function(button, event, options){
            		var model = this.getComprobanteEgresoStore();
            		var record = this.getComprobanteEgresoForm().getForm().getFieldValues(true);
            		this.getComprobanteEgresoStore().save(record);
            	}
            }
        });
    },
    
    onLaunch: function(application){
    	this.getComprobanteEgresoStore().on('write', function(store, operation, eOpts ){
    		var record =  operation.getRecords()[0];                                    
            if (operation.action != 'destroy') {
               this.getComprobanteEgresoList().getSelectionModel().select(record,true,true);
               this.refreshButtons([record]);
            }
    	}, this);
    	var store= this.getComprobanteEgresoStore();
    	console.log(store);
    	this.refreshButtons();
    },
        	
	refreshButtons: function(selections){
		this.getComprobanteEgresoNewButton().setDisabled(!this.getComprobanteEgresoStore().canCreate());
		selections=selections||[];
		if (selections.length){
        	this.getComprobanteEgresoForm().getForm().loadRecord(selections[0]);
            this.getComprobanteEgresoSaveButton().setText('Guardar cambios');
            this.getComprobanteEgresoDeleteButton().setDisabled(!this.getComprobanteEgresoStore().canDestroy());
            this.getComprobanteEgresoSaveButton().setDisabled(!this.getComprobanteEgresoStore().canUpdate());
        }
        else{
        	this.getComprobanteEgresoForm().getForm().reset();            
        	this.getComprobanteEgresoSaveButton().setTooltip('Guardar nuevo');
        	this.getComprobanteEgresoDeleteButton().setDisabled(true);
        	this.getComprobanteEgresoNewButton().setDisabled(true);
        	this.getComprobanteEgresoSaveButton().setDisabled(!this.getComprobanteEgresoStore().canCreate());
        	this.getComprobanteEgresoForm().setFocus();
        };
        this.enableAll();
	},
	
	disableForm:function(){
		this.getComprobanteEgresoForm().setDisabled(true);
	},
	
	enableForm:function(){
		this.getComprobanteEgresoForm().setDisabled(false);	
	},

	disableList:function(){
		this.getComprobanteEgresoList().setDisabled(true);
	},
	
	enableList:function(){
		this.getComprobanteEgresoList().setDisabled(false);
	},
	
	disableAll: function(){
		this.getComprobanteEgresoList().setDisabled(true);
		this.getComprobanteEgresoForm().setDisabled(true);
	},
	
	enableAll: function(){
		this.getComprobanteEgresoList().setDisabled(false);
		this.getComprobanteEgresoForm().setDisabled(false);
	},
	
	onselectionchange:function(fn, scope){
		this.getComprobanteEgresoList().on('selectionchange', fn, scope);
	},
	
	onwrite:function(fn, scope){
		this.getComprobanteEgresoStore().on('write', fn, scope);
	}
	
});
