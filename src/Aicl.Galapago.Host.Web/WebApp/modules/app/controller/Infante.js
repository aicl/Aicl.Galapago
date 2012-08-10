Ext.define('App.controller.Infante',{
	extend: 'Ext.app.Controller',
    stores: ['Infante','RemoteTercero'],  
    models: ['Infante','Tercero'],
    views:  ['infante.Panel' ],
    refs:[
    	{ref: 'mainPanel', selector: 'infantepanel' },
    	{ref: 'infanteNewButton', selector: 'infantepanel button[action=new]' },
    	{ref: 'infanteSaveButton', selector: 'infanteform button[action=save]' },
    	{ref: 'infanteDeleteButton', selector: 'infanteform button[action=delete]' },
    	{ref: 'buscarInfanteText', 	 selector: 'infantepanel textfield[name=buscarInfanteText]'},
    	
    	{ref: 'infanteList',    	 selector: 'infantelist' },
    	{ref: 'infanteForm',    	 selector: 'infanteform' },
    	{ref: 'selectInfanteButton', selector: 'infantelist button[action=selectInfante]'},
    	
    	{ref: 'terceroForm', 	 selector: 'infanteterceroform' },
    	{ref: 'terceroSaveButton', 	 selector: 'infanteterceroform button[action=save]' }
    ],

    init: function(application) {
    	    	
        this.control({
        	
        	'infantelist': { 
                selectionchange: function( sm,  selections,  eOpts){
                	var disableSIB=true;
                	if (selections.length){
    					disableSIB=false;   		
        			}
        			this.getSelectInfanteButton().setDisabled(disableSIB);
                }
            },
        	
        	'infantepanel button[action=buscarInfante]': {
                click: function(button, event, options){
                	var searchText= this.getBuscarInfanteText().getValue();
                	var request={
                		Documento: '',
                		Nombres:searchText,
                		Apellidos:'',
						format:'json'
                	};
                	
                	var store = this.getInfanteStore();       	  	
                	store.getProxy().extraParams=request;
                	store.loadPage(1);
                }
            },
            
            'infantelist button[action=selectInfante]': {
                click: function(button, event, options){
                	this.getMainPanel().hideSearchWindow();
                	var record= this.getInfanteList().getSelectionModel().getSelection()[0];
                	this.infanteLoadRecord(record);
                }
            },
            
            'infantepanel button[action=new]':{
            	click:function(button, event, options){
                	this.getInfanteForm().getForm().reset();
                }
            },
            
            'infanteform button[action=save]':{
            	click: function(button, event, options){
            		var record = this.getInfanteForm().getForm().getFieldValues(true);
            		this.getInfanteStore().getProxy().extraParams={format:'json'};
            		this.getInfanteStore().save(record);
            	}
            },
            
            'infanteform button[action=newTercero]': {
                click: function(button, event, options){
                	this.getMainPanel().showCreateTerceroWindow();
                }
            },
            
            'infanteform infantetercerocombo':{
            	select:function(combo, value, options){
            		var data = value[0].data;
            		this.getInfanteForm().getForm().setValues({
            			'DocumentoTercero':data.Documento,
            			'DVTercero':data.DigitoVerificacion
            		});
            	}
            },         
            
            'infanteterceroform button[action=save]':{
            	click: function(button, event, options){
            		var record = this.getTerceroForm().getForm().getFieldValues(true);
            		this.getRemoteTerceroStore().save(record);
            	}
            }
        })
    },
    
    onLaunch: function(application){
    	
    	this.getInfanteStore().on('load', function(store , records, success, eOpts){
    		if(!success){
    			Ext.Msg.alert('Error', eOpts);
    			return;
    		}
    		
    		if(records.length==0){
    			Aicl.Util.msg('Aviso', 'Sin informacion');
    			return;
    		}
    		
    		if(records.length==1){
    			var record = records[0];
    			this.infanteLoadRecord(record);
    			return;
    		}
    		
    		this.getMainPanel().showSearchWindow();
            
    	}, this);
    	
    	this.getInfanteStore().on('write', function(store, operation, eOpts ){
    		var record =  operation.getRecords()[0];
            
            if (operation.action != 'destroy') {
            	this.infanteLoadRecord(record);
            }            
    	}, this);
    	
    	this.getTerceroSaveButton().setDisabled(!(this.getRemoteTerceroStore().canUpdate()));
    	this.getInfanteNewButton().setDisabled(!this.getInfanteStore().canCreate());
		this.getInfanteSaveButton().setDisabled(!this.getInfanteStore().canUpdate());
    },
    
    infanteLoadRecord:function(record){
    	var rt= this.getRemoteTerceroStore();
		if(!rt.getById(record.get('IdTerceroFactura'))){
			rt.addLocal({
				Id:record.get('IdTercero'),
				Nombre:record.get('NombreTercero'),
				Documento:record.get('DocumentoTercero'),
				DigitoVerificacion:record.get('DVTercero')
			})
		};
                    	
        this.getInfanteForm().getForm().loadRecord(record);
    }
 	
	
});