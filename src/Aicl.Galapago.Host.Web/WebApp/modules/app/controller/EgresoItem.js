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
    	{ref: 'egresoItemSaveButton', 	 selector: 'egresoitemform button[action=save]' }
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
            }
        });
    },
    
    onLaunch: function(application){
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
		if (selections.length){
			this.getEgresoItemNewButton().setDisabled(!this.getEgresoItemStore().canCreate());
        	this.getEgresoItemForm().getForm().loadRecord(selections[0]);
            this.getEgresoItemSaveButton().setText('Update');
            this.getEgresoItemDeleteButton().setDisabled(!this.getEgresoItemStore().canDestroy());
            this.getEgresoItemSaveButton().setDisabled(!this.getEgresoItemStore().canUpdate());
        }
        else{
        	this.getEgresoItemForm().getForm().reset();            
        	this.getEgresoItemSaveButton().setText('Add');
        	this.getEgresoItemDeleteButton().setDisabled(true);
        	this.getEgresoItemNewButton().setDisabled(true);
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
	}
	
});
