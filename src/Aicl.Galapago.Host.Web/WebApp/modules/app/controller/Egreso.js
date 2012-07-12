Ext.define('App.controller.Egreso',{
	extend: 'Ext.app.Controller',
    stores: ['Egreso','RemoteTercero'],  
    models: ['Egreso','Tercero'],
    views:  ['egreso.List','egreso.Form' ],
    refs:[
    	{ref: 'egresoList',    	 selector: 'egresolist' },
    	{ref: 'egresoDeleteButton', selector: 'egresolist button[action=delete]' },
    	{ref: 'egresoNewButton',    selector: 'egresolist button[action=new]' },
    	{ref: 'egresoAsentarButton',selector: 'egresolist button[action=asentar]' },
    	{ref: 'egresoForm',    	 selector: 'egresoform' }, 
    	{ref: 'egresoSaveButton', 	 selector: 'egresoform button[action=save]' },
    	
    	
    	{ref: 'textBuscarAnio', 	 selector: 'egresolist textfield[name=textBuscarAnio]' },
    	{ref: 'textBuscarMes', 	 selector: 'egresolist textfield[name=textBuscarMes]' },
    	{ref: 'textBuscarTercero', 	 selector: 'egresolist textfield[name=textBuscarTercero]' },
    	{ref: 'estadoAsentoCombo', 	 selector: 'egresolist estadoasentado' },
    	{ref: 'sucursalAutorizadaCombo', 	 selector: 'egresoform sucursalautorizadacombo' },
    	{ref: 'codigoEgresoCombo', 	 selector: 'egresoform codigoegresocombo' }
    	
    	
    ],

    init: function(application) {
    	    	
    	Ext.create('App.store.RemoteTercero',{storeId:'RemoteReceptor'});
    	
        this.control({
            'egresolist': { 
                selectionchange: function( sm,  selections,  eOpts){
                	console.log('egresolist selectionchange sm', sm);
                	this.refreshButtons(selections);
                }
            },
            
            'egresolist button[action=delete]': {
                click: function(button, event, options){
                	var grid = this.getEgresoList();
                	var record = grid.getSelectionModel().getSelection()[0];
        			//this.getEgresoStore().remove(record);
                	alert ('anular registro');
                }
            },
            
            'egresolist button[action=new]': {
            	click:function(button, event, options){
            		this.getEgresoList().getSelectionModel().deselectAll();
            	}
            },
            
            'egresoform button[action=save]':{            	
            	click:function(button, event, options){
            		var model = this.getEgresoStore();
            		var record = this.getEgresoForm().getForm().getFieldValues(true);
            		this.getEgresoStore().save(record);
            	}
            },
            'egresolist button[action=buscarEgresos]': {
                click: function(button, event, options){
                	
                	var anio = this.getTextBuscarAnio().getValue();
                	if(!anio){
                		Ext.Msg.alert('Debe indicar el periodo');
            			return;
                	}
                	var mes = this.getTextBuscarMes().getValue();
                	                	
                	var estado= this.getEstadoAsentoCombo().getValue(); 
                	
                	var request={
                		Activo:true,
                		Periodo: anio+ (mes? Ext.String.leftPad(mes, 2, '0'):''),
                		NombreTercero: this.getTextBuscarTercero().getValue(),
                		Asentado: (estado=='1')?null: (estado==2)?true:false
                	};
                	
                	var store = this.getEgresoStore();       	  	
                	store.getProxy().extraParams=request;
                	store.loadPage(1);
                	
                }
            },
            'egresoform codigoegresocombo':{
            	select:function(combo, value, options){
            		//console.log('TODO : egresoform codigoegresocombo select', combo, value, options);
            	}
            }
        });
    },
    
    onLaunch: function(application){
    	this.getEgresoStore().on('write', function(store, operation, eOpts ){
    		var record =  operation.getRecords()[0];                                    
            if (operation.action != 'destroy') {
               this.getEgresoList().getSelectionModel().select(record,true,true);
               this.refreshButtons([record]);
            }
    	}, this);
    },
        	
	refreshButtons: function(selections){
		this.getEgresoNewButton().setDisabled(!this.getEgresoStore().canCreate());
		selections=selections||[];
		if (selections.length){
			this.getSucursalAutorizadaCombo().setReadOnly(true);
			this.getCodigoEgresoCombo().setReadOnly(true);
			
			var record= selections[0];
			var rt= this.getRemoteTerceroStore();
			if(!rt.getById(record.get('IdTercero'))){
				rt.addLocal({Id:record.get('IdTercero'),
				Nombre:record.get('NombreTercero'),
				Documento:record.get('DocumentoTercero'),
				DigitoVerificacion:record.get('DVTercero'),
				NombreDocumento:record.get('NombreDocumentoTercero')})
			};
			
			var rr =this.getStore('RemoteReceptor');
			if(!rr.getById(record.get('IdTerceroReceptor'))){
				rr.addLocal({Id:record.get('IdTerceroReceptor'),
				Nombre:record.get('NombreReceptor'),
				Documento:record.get('DocumentoReceptor'),
				DigitoVerificacion:record.get('DVReceptor'),
				NombreDocumento:record.get('NombreDocumentoReceptor')})
			};
			
			if(record.get('FechaAsentado') ){
				this.getEgresoAsentarButton().setDisabled(false);
				this.getEgresoDeleteButton().setDisabled(true);
				this.getEgresoAsentarButton().setIconCls('desasentar');
			}
			else{
				if(record.get('FechaAnulado')){
					this.getEgresoAsentarButton().setDisabled(true);
					this.getEgresoDeleteButton().setDisabled(true);
				}
				else{
					this.getEgresoAsentarButton().setDisabled(false);
					this.getEgresoDeleteButton().setDisabled(false);
					this.getEgresoAsentarButton().setIconCls('asentar');
				}
			}
				
				
        	this.getEgresoForm().getForm().loadRecord(record);
            this.getEgresoSaveButton().setText('Update');
            //this.getEgresoDeleteButton().setDisabled(!this.getEgresoStore().canDestroy());
            this.getEgresoSaveButton().setDisabled(!this.getEgresoStore().canUpdate());
        }
        else{
			
			this.getCodigoEgresoCombo().setReadOnly(false);
			this.getSucursalAutorizadaCombo().setReadOnly(false);
			
        	this.getEgresoForm().getForm().reset();   
        	
        	this.getEgresoSaveButton().setText('Add');
        	this.getEgresoDeleteButton().setDisabled(true);
        	this.getEgresoSaveButton().setDisabled(!this.getEgresoStore().canCreate());
        	        	
        	var suc = this.getSucursalAutorizadaCombo().getStore().getAt(0);
        	if (suc) this.getSucursalAutorizadaCombo().setValue(suc.getId());
        	this.getEgresoForm().setFocus();
        };
        this.enableAll();
	},
	
	disableForm:function(){
		this.getEgresoForm().setDisabled(true);
	},
	
	enableForm:function(){
		this.getEgresoForm().setDisabled(false);	
	},

	disableList:function(){
		this.getEgresoList().setDisabled(true);
	},
	
	enableList:function(){
		this.getEgresoList().setDisabled(false);
	},
	
	disableAll: function(){
		this.getEgresoList().setDisabled(true);
		this.getEgresoForm().setDisabled(true);
	},
	
	enableAll: function(){
		this.getEgresoList().setDisabled(false);
		this.getEgresoForm().setDisabled(false);
	},
	
	onselectionchange:function(fn, scope){
		this.getEgresoList().on('selectionchange', fn, scope);
	},
	
	onwrite:function(fn, scope){
		this.getEgresoStore().on('write', fn, scope);
	}
	
});
