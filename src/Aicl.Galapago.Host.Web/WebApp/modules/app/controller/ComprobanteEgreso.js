Ext.define('App.controller.ComprobanteEgreso',{
	extend: 'Ext.app.Controller',
    stores: ['ComprobanteEgreso','ComprobanteEgresoItem','ComprobanteEgresoRetencion',
    'RemoteSaldoTercero','RemoteTercero','Egreso'],  
    models: ['ComprobanteEgreso','ComprobanteEgresoItem','ComprobanteEgresoRetencion',
    'SaldoTercero','Tercero','Egreso'],
    views:  ['comprobanteegreso.Panel' ],
    refs:[
    	{ref: 'comprobanteEgresoList',    	 selector: 'comprobanteegresolist' },
    	{ref: 'comprobanteEgresoForm',    	 selector: 'comprobanteegresoform' },
    	
    	{ref: 'comprobanteEgresoDeleteButton', selector: 'comprobanteegresopanel button[action=delete]' },
    	{ref: 'comprobanteEgresoNewButton',    selector: 'comprobanteegresopanel button[action=new]' }, 
    	{ref: 'comprobanteEgresoSaveButton', 	 selector: 'comprobanteegresopanel button[action=save]' },
    	{ref: 'comprobanteEgresoAsentarButton',selector: 'comprobanteegresopanel button[action=asentar]' },
    	
    	{ref: 'buscarAnioText', 	 selector: 'comprobanteegresopanel textfield[name=buscarAnioText]' },
    	{ref: 'buscarMesText', 	 selector: 'comprobanteegresopanel textfield[name=buscarMesText]' },
    	{ref: 'buscarTerceroText', 	 selector: 'comprobanteegresopanel textfield[name=buscarTerceroText]' },
    	{ref: 'estadoAsentoCombo', 	 selector: 'comprobanteegresopanel estadoasentadocombo' },
    	
    	{ref: 'sucursalAutorizadaCombo', 	 selector: 'comprobanteegresopanel sucursalautorizadacombo' },
    	{ref: 'remoteSaldoTerceroCombo', 	 selector: 'comprobanteegresopanel remotesaldotercerocombo' },
    	
    	{ref: 'rubroCombo',    	 selector: 'comprobanteegresopanel rubrocombo' },
    	
    	{ref: 'itemToolbar',     selector: 'comprobanteegresopanel toolbar[name=itemToolbar]' },
    	{ref: 'itemList',    	 selector: 'comprobanteegresoitemlist' },
    	{ref: 'itemForm',    	 selector: 'comprobanteegresoitemform' },
    	
    	{ref: 'itemDeleteButton', selector: 'comprobanteegresopanel button[action=delete_item]' },
    	{ref: 'itemNewButton',    selector: 'comprobanteegresopanel button[action=new_item]' },
    	{ref: 'itemSaveButton',   selector: 'comprobanteegresopanel button[action=save_item]' },
    	{ref: 'egresoCombo', 	  selector: 'comprobanteegresopanel egresocombo' }
    	
    ],

    init: function(application) {
    	    	    	
        this.control({
            'comprobanteegresolist': { 
                selectionchange: function( sm,  selections,  eOpts){
                	this.getComprobanteEgresoItemStore().removeAll();   
                	this.getEgresoStore().removeAll();
                	
                	if (selections.length){
    					this.cargarItems(selections[0]);   		
        			}
        			else{
        				this.getItemToolbar().setDisabled(true);
        			}
                	
                	this.refreshButtons(selections);
                }
            },
            
            'comprobanteegresopanel button[action=delete]': {
                click: function(button, event, options){
                	var grid = this.getComprobanteEgresoList();
                	var record = grid.getSelectionModel().getSelection()[0];
        			this.getComprobanteEgresoStore().anular(record);
                }
            },
            
            'comprobanteegresopanel button[action=new]': {
            	click:function(button, event, options){
            		this.getComprobanteEgresoList().getSelectionModel().deselectAll();
            	}
            },
            
            'comprobanteegresopanel button[action=save]':{            	
            	click:function(button, event, options){
            		var model = this.getComprobanteEgresoStore();
            		var record = this.getComprobanteEgresoForm().getForm().getFieldValues(true);
            		this.getComprobanteEgresoStore().save(record);
            	}
            },
            
            'comprobanteegresopanel button[action=asentar]': {
                click: function(button, event, options){
                	var grid = this.getComprobanteEgresoList();
                	var record = grid.getSelectionModel().getSelection()[0];
        			if(record.get('FechaAsentado'))
        				this.getComprobanteEgresoStore().reversar(record);
        			else
        				this.getComprobanteEgresoStore().asentar(record);
                }
            },
            
            'comprobanteegresopanel button[action=buscarComprobantes]': {
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
                	
                	var store = this.getComprobanteEgresoStore();       	  	
                	store.getProxy().extraParams=request;
                	store.loadPage(1);   	
                }
            },
            'comprobanteegresopanel sucursalautorizadacombo':{
            	select:function(combo, value, options){
            		var rc = value[0];
            		
            		this.getRemoteSaldoTerceroStore().getProxy().
            			setExtraParam('IdSucursal', rc.getId());
            		var cuentas=getCuentasGiradoras(rc.getId());
            		this.getRubroCombo().getStore().loadRawData(cuentas);        		
            	}
            },
            
            'comprobanteegresopanel egresocombo':{
            	select:function(combo, value, options){
            		console.log(combo);
            		var rc = value[0];
            		this.getItemForm().getForm().setValues({
            			Abono:rc.get('Saldo')
            			
            		});
            	}
            },
            
            'comprobanteegresoitemlist': { 
                selectionchange: function( sm,  selections,  eOpts){
                	this.refreshButtonsItem(selections);
                }
            },
            
            'comprobanteegresopanel button[action=delete_item]': {
                click: function(button, event, options){
                	var grid = this.getItemList();
                	var record = grid.getSelectionModel().getSelection()[0];
        			this.getComprobanteEgresoItemStore().remove(record);
                }
            },
            
            'comprobanteegresopanel button[action=new_item]': {
            	click:function(button, event, options){
            		this.getItemList().getSelectionModel().deselectAll();
            	}
            },
            
            'comprobanteegresopanel button[action=save_item]':{            	
            	click:function(button, event, options){
            		var parent=this.getComprobanteEgresoList().getSelectionModel().getSelection()[0];
            		var idEgreso =this.getEgresoCombo().getValue();
            		this.getItemForm().getForm().setValues({
            			'IdComprobanteEgreso':parent.getId()
            			//'IdEgreso': idEgreso
            		});
            		var record = this.getItemForm().getForm().getFieldValues(true);
            		console.log('parent.getId, record', parent.getId(), record);
            		this.getComprobanteEgresoItemStore().save(record);
            	}
            }
            
        });
    },
    
    onLaunch: function(application){
    	
    	this.getComprobanteEgresoNewButton().setDisabled(!this.getComprobanteEgresoStore().canCreate());
    	this.getComprobanteEgresoSaveButton().setTooltip('Guardar');
        this.getComprobanteEgresoSaveButton().setDisabled(!this.getComprobanteEgresoStore().canUpdate());
    	
    	this.getComprobanteEgresoStore().on('write', function(store, operation, eOpts ){
    		var record =  operation.getRecords()[0];
    		if (operation.action=='create') {
        		this.cargarItems(record);
        	}  
            if (operation.action != 'destroy') {
               this.getComprobanteEgresoList().getSelectionModel().select(record,true,true);
               this.refreshButtons([record]);
            }
    	}, this);
    	
    	this.getComprobanteEgresoStore().on('anulado', function(store, record, success){
    		if(success){
    			this.refreshButtons([record]);
    			this.getItemToolbar().setDisabled(true);
    		}
    	}, this);
    	
    	this.getComprobanteEgresoStore().on('asentado', function(store, record, success){
    		if(success){
    			this.refreshButtons([record]);
    			this.getItemToolbar().setDisabled(true);
    		}
    	}, this);
    	
    	this.getComprobanteEgresoStore().on('reversado', function(store, record, success){
    		if(success){
    			this.refreshButtons([record]);
    			this.getItemToolbar().setDisabled(false);
    		}
    	}, this);
    	
    	this.getComprobanteEgresoItemStore().on('write', function(store, operation, eOpts ){    		
    		var valor=0;
    		store.each(function(rec){
    			valor=valor+rec.get('Abono');
    		});
    		var retStore= this.getComprobanteEgresoRetencionStore();
    		retStore.each(function(rec){
    			valor=valor-rec.get('Valor');
    		});
    		var parent = this.getComprobanteEgresoList().getSelectionModel().getSelection()[0];
    		this.getComprobanteEgresoStore().updateLocal({Id: parent.getId(), Valor:valor});
    	}, this);
    	
    	this.refreshButtons();
    },
        	
	refreshButtons: function(selections){
		
		selections=selections||[];
		if (selections.length){
			var record= selections[0];
			
			this.getSucursalAutorizadaCombo().setReadOnly(true);
			this.getRemoteSaldoTerceroCombo().setReadOnly(true);
						
			var rt= this.getRemoteSaldoTerceroStore();
			var index = rt.findExact('IdTercero', record.get('IdTercero'));
			if(index<0){
				rt.addLocal({
				IdTercero:record.get('IdTercero'),
				Nombre:record.get('NombreTercero'),
				Documento:record.get('DocumentoTercero'),
				DigitoVerificacion:record.get('DVTercero'),
				NombreDocumento:record.get('NombreDocumentoTercero')})
			};
			
			var rr =this.getRemoteTerceroStore();
			if(!rr.getById(record.get('IdTerceroReceptor'))){
				rr.addLocal({Id:record.get('IdTerceroReceptor'),
				Nombre:record.get('NombreReceptor'),
				Documento:record.get('DocumentoReceptor'),
				DigitoVerificacion:record.get('DVReceptor'),
				NombreDocumento:record.get('NombreDocumentoReceptor')})
			};
			
			var canUpdate=true;
			if(record.get('FechaAsentado') ){
				this.getComprobanteEgresoAsentarButton().setDisabled(false || record.get('Externo'));
				this.getComprobanteEgresoDeleteButton().setDisabled(true);
				this.getComprobanteEgresoAsentarButton().setIconCls('desasentar');
				canUpdate=false;
			}
			else{
				if(record.get('FechaAnulado')){
					this.getComprobanteEgresoAsentarButton().setDisabled(true);
					this.getComprobanteEgresoDeleteButton().setDisabled(true);
					canUpdate=false;
				}
				else{
					this.getComprobanteEgresoAsentarButton().setDisabled(false);
					this.getComprobanteEgresoDeleteButton().setDisabled(false);
					this.getComprobanteEgresoAsentarButton().setIconCls('asentar');
				}
			}
				
        	this.getComprobanteEgresoForm().getForm().loadRecord(record);
            this.getComprobanteEgresoSaveButton().setTooltip('Actualizar comprobante');
            //this.getEgresoDeleteButton().setDisabled(!this.getEgresoStore().canDestroy());
            this.getComprobanteEgresoSaveButton().setDisabled(!(this.getComprobanteEgresoStore().canUpdate() && canUpdate));
                        
            var egreso=this.getEgresoStore();
            egreso.getProxy().setExtraParam('IdSucursal',record.get('IdSucursal'));
            egreso.getProxy().setExtraParam('IdTercero', record.get('IdTercero'));
            egreso.getProxy().setExtraParam('ConSaldo', true);
            egreso.getProxy().setExtraParam('Asentado', true);
            egreso.loadPage(1);
        }
        else{
			
			this.getRemoteSaldoTerceroCombo().setReadOnly(false);
			this.getSucursalAutorizadaCombo().setReadOnly(false);
			
        	this.getComprobanteEgresoForm().getForm().reset();   
        	
        	this.getComprobanteEgresoSaveButton().setTooltip('Guardar comprobante');
        	this.getComprobanteEgresoDeleteButton().setDisabled(true);
        	this.getComprobanteEgresoSaveButton().setDisabled(!this.getComprobanteEgresoStore().canCreate());
        	        	
        	var suc = this.getSucursalAutorizadaCombo().getStore().getAt(0);
        	if (suc) this.getSucursalAutorizadaCombo().setValue(suc.getId());
        	        	
        };
		
		// dispara evento para el combo de sucursales...
        this.getSucursalAutorizadaCombo().fireEvent('select', this.getSucursalAutorizadaCombo(),
				[this.getSucursalAutorizadaCombo().findRecordByValue(this.getSucursalAutorizadaCombo().getValue() )]);    
	},
	
	cargarItems:function(record){
	    
	    if(record.get('Valor')!=0){
	    	this.getComprobanteEgresoItemStore().load({params:{IdComprobanteEgreso: record.getId()}});
	    	this.getItemList().determineScrollbars();
	    }
	
	    if(record.get('FechaAnulado') || record.get('FechaAsentado'))
	    	this.getItemToolbar().setDisabled(true);
	    else
	        this.getItemToolbar().setDisabled(false);
	        
	    this.refreshButtonsItem();
	},
	
	refreshButtonsItem: function(selections){	

		this.getItemNewButton().setDisabled(!this.getComprobanteEgresoItemStore().canCreate());
		this.getItemSaveButton().setDisabled(!this.getComprobanteEgresoItemStore().canUpdate());
		
		selections=selections||[];	
		
		if (selections.length){
			var record= selections[0];
			
			var storeEgreso =this.getEgresoStore();
			if(!storeEgreso.getById(record.get('IdEgreso'))){
				storeEgreso.addLocal({
				Id:record.get('IdEgreso'),
				Documento:record.get('Documento'),
				Saldo:record.get('Saldo'),
				Valor:record.get('Valor')
				})
			};
			
        	this.getItemForm().getForm().loadRecord(record);
            this.getItemSaveButton().setTooltip('Actualizar');
            this.getItemDeleteButton().setDisabled(!this.getComprobanteEgresoItemStore().canDestroy());
            this.getEgresoCombo().setReadOnly(true);
        }
        else{
        	this.getItemForm().getForm().reset();            
        	this.getItemSaveButton().setTooltip('Guardar');
        	this.getItemDeleteButton().setDisabled(true);
        	this.getEgresoCombo().setReadOnly(false);
        };
                
	}
		
});
