Ext.define('App.controller.Ingreso',{
	extend: 'Ext.app.Controller',
    stores: ['Ingreso','IngresoItem','RemoteTercero'],  
    models: ['Ingreso','IngresoItem','Tercero'],
    views:  ['ingreso.Panel'],
    refs:[
    	{ref: 'ingresoList',    	 selector: 'ingresolist' },
    	{ref: 'ingresoForm',    	 selector: 'ingresoform' },
    	
    	{ref: 'ingresoDeleteButton', selector: 'ingresopanel button[action=delete]' },
    	{ref: 'ingresoNewButton',    selector: 'ingresopanel button[action=new]' },
    	{ref: 'ingresoAsentarButton',selector: 'ingresopanel button[action=asentar]' },
    	{ref: 'ingresoSaveButton', 	 selector: 'ingresopanel button[action=save]' },
    	 		
    	{ref: 'buscarAnioText', 	 selector: 'ingresopanel textfield[name=buscarAnioText]' },
    	{ref: 'buscarMesText', 	 selector: 'ingresopanel textfield[name=buscarMesText]' },
    	{ref: 'buscarTerceroText', 	 selector: 'ingresopanel textfield[name=buscarTerceroText]' },
    	{ref: 'estadoAsentoCombo', 	 selector: 'ingresopanel estadoasentadocombo' },
    	{ref: 'sucursalAutorizadaCombo', 	 selector: 'ingresopanel sucursalautorizadacombo' },
    	{ref: 'codigoIngresoCombo', 	 selector: 'ingresopanel codigoingresocombo' },
    	
    	{ref: 'ingresoItemList',    	 selector: 'ingresoitemlist' },
    	{ref: 'ingresoItemForm',    	 selector: 'ingresoitemform' },
    	
    	{ref: 'ingresoItemToolbar',    	 selector: 'ingresopanel toolbar[name=ingresoItemToolbar]' },
    	{ref: 'ingresoItemDeleteButton', selector: 'ingresopanel button[action=delete_item]' },
    	{ref: 'ingresoItemNewButton',    selector: 'ingresopanel button[action=new_item]' },
    	{ref: 'ingresoItemSaveButton', 	 selector: 'ingresopanel button[action=save_item]' },
    	
    	{ref: 'centroAutorizadoCombo',    	 selector: 'ingresopanel centroautorizadocombo'},
    	{ref: 'rubroCombo',    	 selector: 'ingresopanel rubrocombo' },
    	{ref: 'tipoIngresoItemCombo',    	 selector: 'ingresopanel tipoingresoitemcombo' }
    	
    ],

    init: function(application) {
    	
        this.control({
            'ingresolist': { 
                selectionchange: function( sm,  selections,  eOpts){
                	
                	this.getCentroAutorizadoCombo().getStore().removeAll();
                	this.getIngresoItemStore().removeAll();
    		
    				if (selections.length){
    					this.cargarItems(selections[0]);   		
        			}
        			else{
        				this.getIngresoItemToolbar().setDisabled(true);
        			}

                	this.refreshButtons(selections);
                }
            },
            
            'ingresopanel button[action=delete]': {
                click: function(button, event, options){
                	var grid = this.getIngresoList();
                	var record = grid.getSelectionModel().getSelection()[0];
        			this.getIngresoStore().anular(record);
                }
            },
            
            'ingresopanel button[action=asentar]': {
                click: function(button, event, options){
                	var grid = this.getIngresoList();
                	var record = grid.getSelectionModel().getSelection()[0];
        			if(record.get('FechaAsentado'))
        				this.getIngresoStore().reversar(record);
        			else
        				this.getIngresoStore().asentar(record);
                }
            },
            
            'ingresopanel button[action=new]': {
            	click:function(button, event, options){
            		this.getIngresoList().getSelectionModel().deselectAll();
            	}
            },
            
            'ingresopanel button[action=save]':{            	
            	click:function(button, event, options){
            		var model = this.getIngresoStore();
            		var record = this.getIngresoForm().getForm().getFieldValues(true);
            		this.getIngresoStore().save(record);
            	}
            },
            
            'ingresopanel button[action=buscarIngresos]': {
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
                	
                	var store = this.getIngresoStore();       	  	
                	store.getProxy().extraParams=request;
                	store.loadPage(1);
                	
                }
            },
            
            'ingresopanel codigoingresocombo':{
            	select:function(combo, value, options){
            		//console.log('TODO : ingresoform codigoingresocombo select', combo, value, options);
            	}
            },
            
            // item
            
            'ingresoitemlist': { 
                selectionchange: function( sm,  selections,  eOpts){
                	this.refreshButtonsItem(selections);
                }
            },
            
            'ingresopanel button[action=delete_item]': {
                click: function(button, event, options){
                	var grid = this.getIngresoItemList();
                	var record = grid.getSelectionModel().getSelection()[0];
        			this.getIngresoItemStore().remove(record);
                }
            },
            
            'ingresopanel button[action=new_item]': {
            	click:function(button, event, options){
            		this.getIngresoItemList().getSelectionModel().deselectAll();
            	}
            },
            
            'ingresopanel button[action=save_item]':{            	
            	click:function(button, event, options){
            		var parent=this.getIngresoList().getSelectionModel().getSelection()[0];
            		this.getIngresoItemForm().getForm().setValues({'IdIngreso':parent.getId()});
            		var record = this.getIngresoItemForm().getForm().getFieldValues(true);
            		this.getIngresoItemStore().save(record);
            	}
            },
            
            'ingresopanel tipoingresoitemcombo':{
            	select:function(combo, value, options){
            		var data = value[0].data;
            		this.getCentroAutorizadoCombo().getStore().clearFilter(true);
            		if(data.Id==1){
            			this.getIngresoItemForm().getForm().setValues({'TipoPartida':2});
            			this.getCentroAutorizadoCombo().getStore().
            				filter([{filterFn: function(item) { return item.getId() > 1; }}])
            		}
            		else{
            			this.getIngresoItemForm().getForm().setValues({'TipoPartida':1});
            			this.getCentroAutorizadoCombo().getStore().
            				filter([{filterFn: function(item) { return item.getId() == 1; }}])
            		}
            		
            		var record= this.getCentroAutorizadoCombo().getStore().getAt(0);
            		if(record) this.getCentroAutorizadoCombo().setValue(record.get('Id'));
            		else this.getCentroAutorizadoCombo().setValue(0); 
            		this.getCentroAutorizadoCombo().fireEvent('select', this.getCentroAutorizadoCombo(), [record]);
            	}
            },
            
            'ingresopanel centroautorizadocombo':{
            	select:function(combo, value, options){
            		var rc = value[0];
            		var permitidos=(this.getTipoIngresoItemCombo().getValue()==1)?'CreditosPermitidos':'DebitosPermitidos';
            		var parent=this.getIngresoList().getSelectionModel().getSelection()[0];
            		var codigoIngreso= this.getCodigoIngresoCombo().getStore().getById(parent.get('CodigoDocumento'));
            		var codigos=codigoIngreso.get(permitidos);
            		var cd=[];
            		if(this.getTipoIngresoItemCombo().getValue()==1) cd=codigos;
            		else{
            			if(this.getTipoIngresoItemCombo().getValue()==2 ){
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
            
            'ingresopanel rubrocombo':{
            	select:function(combo, value, options){
            		
            	}
            }
            
        });
    },
    
    onLaunch: function(application){
    	
    	this.getIngresoNewButton().setDisabled(!this.getIngresoStore().canCreate());
    	this.getIngresoSaveButton().setTooltip('Guardar');
        this.getIngresoSaveButton().setDisabled(!this.getIngresoStore().canUpdate());
    	
    	this.getIngresoStore().on('write', function(store, operation, eOpts ){
    		var record =  operation.getRecords()[0];
    		
    		if (operation.action=='create') {
        		this.cargarItems(record);
        	}    
    		
            if (operation.action != 'destroy') {
               this.getIngresoList().getSelectionModel().select(record,true,true);
               this.refreshButtons([record]);
            }            
    	}, this);
    	  		
    	this.getIngresoStore().on('anulado', function(store, record, success){
    		if(success){
    			this.refreshButtons([record]);
    			this.getIngresoItemToolbar().setDisabled(true);
    		}
    	}, this);
    	
    	this.getIngresoStore().on('asentado', function(store, record, success){
    		if(success){
    			this.refreshButtons([record]);
    			this.getIngresoItemToolbar().setDisabled(true);
    		}
    	}, this);
    	
    	this.getIngresoStore().on('reversado', function(store, record, success){
    		if(success){
    			this.refreshButtons([record]);
    			this.getIngresoItemToolbar().setDisabled(false);
    		}
    	}, this);
    	
    	this.getIngresoItemStore().on('write', function(store, operation, eOpts ){    		
    		var totalD=0, totalC=0;
    		store.each(function(rec){
    			if(rec.get('TipoPartida')==1) 
    				totalD=totalD+rec.get('Valor');
    			else
    				totalC=totalC+rec.get('Valor');
    		});
    		var parent = this.getIngresoList().getSelectionModel().getSelection()[0];
    		this.getIngresoStore().updateLocal({Id: parent.getId(), Valor:totalC, Saldo:totalC-totalD});
    	}, this);

    	
    	this.refreshButtons();    	
    },
        	
	refreshButtons: function(selections){
		
		selections=selections||[];
		if (selections.length){
			this.getSucursalAutorizadaCombo().setReadOnly(true);
			this.getCodigoIngresoCombo().setReadOnly(true);
			
			var record= selections[0];
			var rt= this.getRemoteTerceroStore();
			if(!rt.getById(record.get('IdTercero'))){
				rt.addLocal({Id:record.get('IdTercero'),
				Nombre:record.get('NombreTercero'),
				Documento:record.get('DocumentoTercero'),
				DigitoVerificacion:record.get('DVTercero'),
				NombreDocumento:record.get('NombreDocumentoTercero')})
			};
						
			var canUpdate=true;
			if(record.get('FechaAsentado') ){
				this.getIngresoAsentarButton().setDisabled(false || record.get('Externo'));
				this.getIngresoDeleteButton().setDisabled(true);
				this.getIngresoAsentarButton().setIconCls('desasentar');
				canUpdate=false;
			}
			else{
				if(record.get('FechaAnulado')){
					this.getIngresoAsentarButton().setDisabled(true);
					this.getIngresoDeleteButton().setDisabled(true);
					canUpdate=false;
				}
				else{
					this.getIngresoAsentarButton().setDisabled(false);
					this.getIngresoDeleteButton().setDisabled(false);
					this.getIngresoAsentarButton().setIconCls('asentar');
				}
			}
				
        	this.getIngresoForm().getForm().loadRecord(record);
            this.getIngresoSaveButton().setTooltip('Actualizar Ingreso');
            //this.getIngresoDeleteButton().setDisabled(!this.getIngresoStore().canDestroy());
            this.getIngresoSaveButton().setDisabled(!(this.getIngresoStore().canUpdate() && canUpdate));
        }
        else{
			
			this.getCodigoIngresoCombo().setReadOnly(false);
			this.getSucursalAutorizadaCombo().setReadOnly(false);
			
        	this.getIngresoForm().getForm().reset();   
        	
        	this.getIngresoSaveButton().setTooltip('Guardar Ingreso');
        	this.getIngresoDeleteButton().setDisabled(true);
        	this.getIngresoSaveButton().setDisabled(!this.getIngresoStore().canCreate());
        	        	
        	var suc = this.getSucursalAutorizadaCombo().getStore().getAt(0);
        	if (suc) this.getSucursalAutorizadaCombo().setValue(suc.getId());
        	this.getIngresoForm().setFocus();
        };
	},
		
	onselectionchange:function(fn, scope){
		this.getIngresoList().on('selectionchange', fn, scope);
	},
	
	onwrite:function(fn, scope){
		this.getIngresoStore().on('write', fn, scope);
	},
	
	cargarItems:function(record){

		var codigoIngreso= this.getCodigoIngresoCombo().getStore().getById(record.get('CodigoDocumento'));
	    		    		    		
	    this.getCentroAutorizadoCombo().getStore().loadRawData(getCentrosData(record.get('IdSucursal')));
	    
	    if(record.get('Valor')!=0){
	    	this.getIngresoItemStore().load({params:{IdIngreso: record.getId()}});
	    	this.getIngresoItemList().determineScrollbars();
	    }
	
	    if(record.get('FechaAnulado') || record.get('FechaAsentado'))
	    	this.getIngresoItemToolbar().setDisabled(true);
	    else
	        this.getIngresoItemToolbar().setDisabled(false);
	        
	    this.refreshButtonsItem();
	},
	
	refreshButtonsItem: function(selections){	
		
		this.getIngresoItemNewButton().setDisabled(!this.getIngresoItemStore().canCreate());
		this.getIngresoItemSaveButton().setDisabled(!this.getIngresoItemStore().canUpdate());
		
		selections=selections||[];
		
		
		if (selections.length){
			var record= selections[0];
						
			var teiValue;
			if(record.get('TipoPartida')==2){
				teiValue=1;
			}
			else{
				if(record.get('CodigoItem').substring(0,1)=='9') teiValue=3;
				else teiValue=2; 
			}
			
			this.getTipoIngresoItemCombo().setValue(teiValue);
			
			this.getTipoIngresoItemCombo().fireEvent('select', this.getTipoIngresoItemCombo(),
				[this.getTipoIngresoItemCombo().findRecordByValue(teiValue)]);
			
        	this.getIngresoItemForm().getForm().loadRecord(record);
            this.getIngresoItemSaveButton().setTooltip('Actualizar');
            this.getIngresoItemDeleteButton().setDisabled(!this.getIngresoItemStore().canDestroy());
                        
                   
        }
        else{
        	this.getIngresoItemForm().getForm().reset();            
        	this.getIngresoItemSaveButton().setTooltip('Guardar');
        	this.getIngresoItemDeleteButton().setDisabled(true);
        	this.getIngresoItemForm().setFocus();
        	
        };
                
	}
	
});
