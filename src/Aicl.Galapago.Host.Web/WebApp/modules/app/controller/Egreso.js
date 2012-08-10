Ext.define('App.controller.Egreso',{
	extend: 'Ext.app.Controller',
    stores: ['Egreso','EgresoItem','RemoteTercero'],  
    models: ['Egreso','EgresoItem','Tercero'],
    views:  ['egreso.Panel'],
    refs:[
    	{ref: 'egresoList',    	 selector: 'egresolist' },
    	{ref: 'egresoForm',    	 selector: 'egresoform' },
    	
    	{ref: 'egresoDeleteButton', selector: 'egresopanel button[action=delete]' },
    	{ref: 'egresoNewButton',    selector: 'egresopanel button[action=new]' },
    	{ref: 'egresoAsentarButton',selector: 'egresopanel button[action=asentar]' },
    	{ref: 'egresoSaveButton', 	 selector: 'egresopanel button[action=save]' },
    	 		
    	{ref: 'buscarAnioText', 	 selector: 'egresopanel textfield[name=buscarAnioText]' },
    	{ref: 'buscarMesText', 	 selector: 'egresopanel textfield[name=buscarMesText]' },
    	{ref: 'buscarTerceroText', 	 selector: 'egresopanel textfield[name=buscarTerceroText]' },
    	{ref: 'estadoAsentoCombo', 	 selector: 'egresopanel estadoasentadocombo' },
    	{ref: 'sucursalAutorizadaCombo', 	 selector: 'egresopanel sucursalautorizadacombo' },
    	{ref: 'codigoEgresoCombo', 	 selector: 'egresopanel codigoegresocombo' },
    	
    	{ref: 'egresoItemList',    	 selector: 'egresoitemlist' },
    	{ref: 'egresoItemForm',    	 selector: 'egresoitemform' },
    	
    	{ref: 'egresoItemToolbar',    	 selector: 'egresopanel toolbar[name=egresoItemToolbar]' },
    	{ref: 'egresoItemDeleteButton', selector: 'egresopanel button[action=delete_item]' },
    	{ref: 'egresoItemNewButton',    selector: 'egresopanel button[action=new_item]' },
    	{ref: 'egresoItemSaveButton', 	 selector: 'egresopanel button[action=save_item]' },
    	
    	{ref: 'centroAutorizadoCombo',    	 selector: 'egresopanel centroautorizadocombo'},
    	{ref: 'rubroCombo',    	 selector: 'egresopanel rubrocombo' },
    	{ref: 'tipoEgresoItemCombo',    	 selector: 'egresopanel tipoegresoitemcombo' },	
    	{ref: 'terceroCombo',    	 selector: 'egresopanel remoteterceroitemcombo' }
    	
    ],

    init: function(application) {
    	    	
    	Ext.create('App.store.RemoteTercero',{storeId:'RemoteReceptor'});
    	Ext.create('App.store.RemoteTercero',{storeId:'RemoteTerceroItem'});
    	
        this.control({
            'egresolist': { 
                selectionchange: function( sm,  selections,  eOpts){
                	
                	this.getCentroAutorizadoCombo().getStore().removeAll();
                	this.getEgresoItemStore().removeAll();
    		
    				if (selections.length){
    					this.cargarItems(selections[0]);   		
        			}
        			else{
        				this.getEgresoItemToolbar().setDisabled(true);
        			}

                	this.refreshButtons(selections);
                }
            },
            
            'egresopanel button[action=delete]': {
                click: function(button, event, options){
                	var grid = this.getEgresoList();
                	var record = grid.getSelectionModel().getSelection()[0];
        			this.getEgresoStore().anular(record);
                }
            },
            
            'egresopanel button[action=asentar]': {
                click: function(button, event, options){
                	var grid = this.getEgresoList();
                	var record = grid.getSelectionModel().getSelection()[0];
        			if(record.get('FechaAsentado'))
        				this.getEgresoStore().reversar(record);
        			else
        				this.getEgresoStore().asentar(record);
                }
            },
            
            'egresopanel button[action=new]': {
            	click:function(button, event, options){
            		this.getEgresoList().getSelectionModel().deselectAll();
            	}
            },
            
            'egresopanel button[action=save]':{            	
            	click:function(button, event, options){
            		var record = this.getEgresoForm().getForm().getFieldValues(true);
            		this.getEgresoStore().save(record);
            	}
            },
            
            'egresopanel button[action=buscarEgresos]': {
                click: function(button, event, options){
                	
                	var anio = this.getBuscarAnioText().getValue();
                	if(!anio){
                		Ext.Msg.alert('Debe indicar el periodo');
            			return;
                	}
                	var mes = this.getBuscarMesText().getValue();
                	                	
                	var estado= this.getEstadoAsentoCombo().getValue(); 
                	
                	var request={
                		Activo:true,
                		Periodo: anio+ (mes? Ext.String.leftPad(mes, 2, '0'):''),
                		NombreTercero: this.getBuscarTerceroText().getValue(),
                		Asentado: (estado=='1')?null: (estado==2)?true:false,
						format:'json'
                	};
                	
                	var store = this.getEgresoStore();       	  	
                	store.getProxy().extraParams=request;
                	store.loadPage(1);
                	
                }
            },
            
            'egresopanel codigoegresocombo':{
            	select:function(combo, value, options){
            		//console.log('TODO : egresoform codigoegresocombo select', combo, value, options);
            	}
            },
            
            // item
            
            'egresoitemlist': { 
                selectionchange: function( sm,  selections,  eOpts){
                	this.refreshButtonsItem(selections);
                }
            },
            
            'egresopanel button[action=delete_item]': {
                click: function(button, event, options){
                	var grid = this.getEgresoItemList();
                	var record = grid.getSelectionModel().getSelection()[0];
        			this.getEgresoItemStore().remove(record);
                }
            },
            
            'egresopanel button[action=new_item]': {
            	click:function(button, event, options){
            		this.getEgresoItemList().getSelectionModel().deselectAll();
            	}
            },
            
            'egresopanel button[action=save_item]':{            	
            	click:function(button, event, options){
            		var parent=this.getEgresoList().getSelectionModel().getSelection()[0];
            		this.getEgresoItemForm().getForm().setValues({'IdEgreso':parent.getId()});
            		var record = this.getEgresoItemForm().getForm().getFieldValues(true);
            		this.getEgresoItemStore().save(record);
            	}
            },
            
            'egresopanel tipoegresoitemcombo':{
            	select:function(combo, value, options){
            		var data = value[0].data;
            		this.getCentroAutorizadoCombo().getStore().clearFilter(true);
            		if(data.Id==1){
            			this.getEgresoItemForm().getForm().setValues({'TipoPartida':1});
            			this.getCentroAutorizadoCombo().getStore().
            				filter([{filterFn: function(item) { return item.getId() > 1; }}])
            		}
            		else{
            			this.getEgresoItemForm().getForm().setValues({'TipoPartida':2});
            			this.getCentroAutorizadoCombo().getStore().
            				filter([{filterFn: function(item) { return item.getId() == 1; }}])
            		}
            		
            		var record= this.getCentroAutorizadoCombo().getStore().getAt(0);
            		if(record) this.getCentroAutorizadoCombo().setValue(record.get('Id'));
            		else this.getCentroAutorizadoCombo().setValue(0); 
            		this.getCentroAutorizadoCombo().fireEvent('select', this.getCentroAutorizadoCombo(), [record]);
            	}
            },
            
            'egresopanel centroautorizadocombo':{
            	select:function(combo, value, options){
            		var rc = value[0];
            		var permitidos=(this.getTipoEgresoItemCombo().getValue()==1)?'DebitosPermitidos':'CreditosPermitidos';
            		var parent=this.getEgresoList().getSelectionModel().getSelection()[0];
            		var codigoEgreso= this.getCodigoEgresoCombo().getStore().getById(parent.get('CodigoDocumento'));
            		var codigos=codigoEgreso.get(permitidos);
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
            },
            
            'egresopanel rubrocombo':{
            	select:function(combo, value, options){
            		console.log('egresoitemform rubrocombo select', value[0])
            		this.getTerceroCombo().setDisabled(!value[0].get('UsaTercero'));
            	}
            }
            
        });
    },
    
    onLaunch: function(application){
    	
    	this.getEgresoNewButton().setDisabled(!this.getEgresoStore().canCreate());
    	this.getEgresoSaveButton().setTooltip('Guardar');
        this.getEgresoSaveButton().setDisabled(!this.getEgresoStore().canUpdate());
    	
    	this.getEgresoStore().on('write', function(store, operation, eOpts ){
    		var record =  operation.getRecords()[0];
    		
    		if (operation.action=='create') {
        		this.cargarItems(record);
        	}    
    		
            if (operation.action != 'destroy') {
               this.getEgresoList().getSelectionModel().select(record,true,true);
               this.refreshButtons([record]);
            }            
    	}, this);
    	  		
    	this.getEgresoStore().on('anulado', function(store, record, success){
    		if(success){
    			this.refreshButtons([record]);
    			this.getEgresoItemToolbar().setDisabled(true);
    		}
    	}, this);
    	
    	this.getEgresoStore().on('asentado', function(store, record, success){
    		if(success){
    			this.refreshButtons([record]);
    			this.getEgresoItemToolbar().setDisabled(true);
    		}
    	}, this);
    	
    	this.getEgresoStore().on('reversado', function(store, record, success){
    		if(success){
    			this.refreshButtons([record]);
    			this.getEgresoItemToolbar().setDisabled(false);
    		}
    	}, this);
    	
    	this.getEgresoItemStore().on('write', function(store, operation, eOpts ){    		
    		var totalD=0, totalC=0;
    		store.each(function(rec){
    			if(rec.get('TipoPartida')==1) 
    				totalD=totalD+rec.get('Valor');
    			else
    				totalC=totalC+rec.get('Valor');
    		});
    		var parent = this.getEgresoList().getSelectionModel().getSelection()[0];
    		this.getEgresoStore().updateLocal({Id: parent.getId(), Valor:totalD, Saldo:totalD-totalC});
    	}, this);

    	
    	this.refreshButtons();    	
    },
        	
	refreshButtons: function(selections){
		
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
			
			var canUpdate=true;
			if(record.get('FechaAsentado') ){
				this.getEgresoAsentarButton().setDisabled(false || record.get('Externo'));
				this.getEgresoDeleteButton().setDisabled(true);
				this.getEgresoAsentarButton().setIconCls('desasentar');
				canUpdate=false;
			}
			else{
				if(record.get('FechaAnulado')){
					this.getEgresoAsentarButton().setDisabled(true);
					this.getEgresoDeleteButton().setDisabled(true);
					canUpdate=false;
				}
				else{
					this.getEgresoAsentarButton().setDisabled(false);
					this.getEgresoDeleteButton().setDisabled(false);
					this.getEgresoAsentarButton().setIconCls('asentar');
				}
			}
				
        	this.getEgresoForm().getForm().loadRecord(record);
            this.getEgresoSaveButton().setTooltip('Actualizar Egreso');
            //this.getEgresoDeleteButton().setDisabled(!this.getEgresoStore().canDestroy());
            this.getEgresoSaveButton().setDisabled(!(this.getEgresoStore().canUpdate() && canUpdate));
        }
        else{
			
			this.getCodigoEgresoCombo().setReadOnly(false);
			this.getSucursalAutorizadaCombo().setReadOnly(false);
			
        	this.getEgresoForm().getForm().reset();   
        	
        	this.getEgresoSaveButton().setTooltip('Guardar Egreso');
        	this.getEgresoDeleteButton().setDisabled(true);
        	this.getEgresoSaveButton().setDisabled(!this.getEgresoStore().canCreate());
        	        	
        	var suc = this.getSucursalAutorizadaCombo().getStore().getAt(0);
        	if (suc) this.getSucursalAutorizadaCombo().setValue(suc.getId());
        	this.getEgresoForm().setFocus();
        };
	},
		
	onselectionchange:function(fn, scope){
		this.getEgresoList().on('selectionchange', fn, scope);
	},
	
	onwrite:function(fn, scope){
		this.getEgresoStore().on('write', fn, scope);
	},
	
	cargarItems:function(record){

		var codigoEgreso= this.getCodigoEgresoCombo().getStore().getById(record.get('CodigoDocumento'));
	    		    		    		
	    this.getCentroAutorizadoCombo().getStore().loadRawData(getCentrosData(record.get('IdSucursal')));
	    
	    if(record.get('Valor')!=0){
	    	this.getEgresoItemStore().load({params:{IdEgreso: record.getId()}});
	    	this.getEgresoItemList().determineScrollbars();
	    }
	
	    if(record.get('FechaAnulado') || record.get('FechaAsentado'))
	    	this.getEgresoItemToolbar().setDisabled(true);
	    else
	        this.getEgresoItemToolbar().setDisabled(false);
	        
	    this.refreshButtonsItem();
	},
	
	refreshButtonsItem: function(selections){	
		
		this.getEgresoItemNewButton().setDisabled(!this.getEgresoItemStore().canCreate());
		this.getEgresoItemSaveButton().setDisabled(!this.getEgresoItemStore().canUpdate());
		
		selections=selections||[];
		
		var habilitarTerceroCombo;
		
		if (selections.length){
			var record= selections[0];
			
			var rt= this.getStore('RemoteTerceroItem');
			if(!rt.getById(record.get('IdTercero'))){
				rt.addLocal({Id:record.get('IdTercero'),
				Nombre:record.get('NombreTercero'),
				Documento:record.get('DocumentoTercero'),
				DigitoVerificacion:record.get('DVTercero')})
			};
			
			var teiValue;
			if(record.get('TipoPartida')==1){
				teiValue=1;
			}
			else{
				if(record.get('CodigoItem').substring(0,1)=='9') teiValue=3;
				else teiValue=2; 
			}
			
			this.getTipoEgresoItemCombo().setValue(teiValue);
			
			this.getTipoEgresoItemCombo().fireEvent('select', this.getTipoEgresoItemCombo(),
				[this.getTipoEgresoItemCombo().findRecordByValue(teiValue)]);
			
        	this.getEgresoItemForm().getForm().loadRecord(record);
            this.getEgresoItemSaveButton().setTooltip('Actualizar');
            this.getEgresoItemDeleteButton().setDisabled(!this.getEgresoItemStore().canDestroy());
                        
            var rubro =this.getRubroCombo().getStore().getById(this.getRubroCombo().getValue());
            if(rubro.get('UsaTercero'))
            	habilitarTerceroCombo=true;
            else
            	habilitarTerceroCombo=false;       
        }
        else{
        	this.getEgresoItemForm().getForm().reset();            
        	this.getEgresoItemSaveButton().setTooltip('Guardar');
        	this.getEgresoItemDeleteButton().setDisabled(true);
        	this.getEgresoItemForm().setFocus();
        	habilitarTerceroCombo=false;
        };
        this.getTerceroCombo().setDisabled(!habilitarTerceroCombo);        
	}
	
});
