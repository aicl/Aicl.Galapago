Ext.define('App.controller.ComprobanteIngreso',{
	extend: 'Ext.app.Controller',
    stores: ['ComprobanteIngreso','ComprobanteIngresoItem','ComprobanteIngresoRetencion',
    'RemoteSaldoTercero','Ingreso'],  
    models: ['ComprobanteIngreso','ComprobanteIngresoItem','ComprobanteIngresoRetencion',
    'SaldoTercero','Ingreso'],
    views:  ['comprobanteingreso.Panel' ],
    refs:[
    	{ref: 'comprobanteIngresoList',    	 selector: 'comprobanteingresolist' },
    	{ref: 'comprobanteIngresoForm',    	 selector: 'comprobanteingresoform' },
    	
    	{ref: 'comprobanteIngresoDeleteButton', selector: 'comprobanteingresopanel button[action=delete]' },
    	{ref: 'comprobanteIngresoNewButton',    selector: 'comprobanteingresopanel button[action=new]' }, 
    	{ref: 'comprobanteIngresoSaveButton', 	 selector: 'comprobanteingresopanel button[action=save]' },
    	{ref: 'comprobanteIngresoAsentarButton',selector: 'comprobanteingresopanel button[action=asentar]' },
    	
    	{ref: 'buscarAnioText', 	 selector: 'comprobanteingresopanel textfield[name=buscarAnioText]' },
    	{ref: 'buscarMesText', 	 selector: 'comprobanteingresopanel textfield[name=buscarMesText]' },
    	{ref: 'buscarTerceroText', 	 selector: 'comprobanteingresopanel textfield[name=buscarTerceroText]' },
    	{ref: 'estadoAsentoCombo', 	 selector: 'comprobanteingresopanel estadoasentadocombo' },
    	
    	{ref: 'sucursalAutorizadaCombo', 	 selector: 'comprobanteingresopanel sucursalautorizadacombo' },
    	{ref: 'remoteSaldoTerceroCombo', 	 selector: 'comprobanteingresopanel remotesaldotercerocombo' },
    	
    	{ref: 'rubroCombo',    	 selector: 'comprobanteingresopanel rubrocombo' },
    	
    	{ref: 'itemToolbar',     selector: 'comprobanteingresopanel toolbar[name=itemToolbar]' },
    	{ref: 'itemList',    	 selector: 'comprobanteingresoitemlist' },
    	{ref: 'itemForm',    	 selector: 'comprobanteingresoitemform' },
    	
    	{ref: 'itemDeleteButton', selector: 'comprobanteingresopanel button[action=delete_item]' },
    	{ref: 'itemNewButton',    selector: 'comprobanteingresopanel button[action=new_item]' },
    	{ref: 'itemSaveButton',   selector: 'comprobanteingresopanel button[action=save_item]' },
    	{ref: 'ingresoCombo', 	  selector: 'comprobanteingresopanel ingresocombo' }
    	
    ],

    init: function(application) {
    	    	    	
        this.control({
            'comprobanteingresolist': { 
                selectionchange: function( sm,  selections,  eOpts){
                	this.getComprobanteIngresoItemStore().removeAll();   
                	this.getIngresoStore().removeAll();
                	
                	if (selections.length){
    					this.cargarItems(selections[0]);   		
        			}
        			else{
        				this.getItemToolbar().setDisabled(true);
        			}
                	
                	this.refreshButtons(selections);
                }
            },
            
            'comprobanteingresopanel button[action=delete]': {
                click: function(button, event, options){
                	var grid = this.getComprobanteIngresoList();
                	var record = grid.getSelectionModel().getSelection()[0];
        			this.getComprobanteIngresoStore().anular(record);
                }
            },
            
            'comprobanteingresopanel button[action=new]': {
            	click:function(button, event, options){
            		this.getComprobanteIngresoList().getSelectionModel().deselectAll();
            	}
            },
            
            'comprobanteingresopanel button[action=save]':{            	
            	click:function(button, event, options){
            		var model = this.getComprobanteIngresoStore();
            		var record = this.getComprobanteIngresoForm().getForm().getFieldValues(true);
            		this.getComprobanteIngresoStore().save(record);
            	}
            },
            
            'comprobanteingresopanel button[action=asentar]': {
                click: function(button, event, options){
                	var grid = this.getComprobanteIngresoList();
                	var record = grid.getSelectionModel().getSelection()[0];
        			if(record.get('FechaAsentado'))
        				this.getComprobanteIngresoStore().reversar(record);
        			else
        				this.getComprobanteIngresoStore().asentar(record);
                }
            },
            
            'comprobanteingresopanel button[action=buscarComprobantes]': {
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
                	
                	var store = this.getComprobanteIngresoStore();       	  	
                	store.getProxy().extraParams=request;
                	store.loadPage(1);   	
                }
            },
            'comprobanteingresopanel sucursalautorizadacombo':{
            	select:function(combo, value, options){
            		var rc = value[0];
            		
            		this.getRemoteSaldoTerceroStore().getProxy().
            			setExtraParam('IdSucursal', rc.getId());
            		var cuentas=getCuentasGiradoras(rc.getId());
            		this.getRubroCombo().getStore().loadRawData(cuentas);        		
            	}
            },
            
            'comprobanteingresopanel ingresocombo':{
            	select:function(combo, value, options){
            		var rc = value[0];
            		this.getItemForm().getForm().setValues({
            			Abono:rc.get('Saldo')
            		});
            	}
            },
            
            'comprobanteingresoitemlist': { 
                selectionchange: function( sm,  selections,  eOpts){
                	this.refreshButtonsItem(selections);
                }
            },
            
            'comprobanteingresopanel button[action=delete_item]': {
                click: function(button, event, options){
                	var grid = this.getItemList();
                	var record = grid.getSelectionModel().getSelection()[0];
        			this.getComprobanteIngresoItemStore().remove(record);
                }
            },
            
            'comprobanteingresopanel button[action=new_item]': {
            	click:function(button, event, options){
            		this.getItemList().getSelectionModel().deselectAll();
            	}
            },
            
            'comprobanteingresopanel button[action=save_item]':{            	
            	click:function(button, event, options){
            		var parent=this.getComprobanteIngresoList().getSelectionModel().getSelection()[0];
            		var idIngreso =this.getIngresoCombo().getValue();
            		this.getItemForm().getForm().setValues({
            			'IdComprobanteIngreso':parent.getId()
            			//'IdIngreso': idIngreso
            		});
            		var record = this.getItemForm().getForm().getFieldValues(true);
            		this.getComprobanteIngresoItemStore().save(record);
            	}
            }
            
        });
    },
    
    onLaunch: function(application){
    	
    	this.getComprobanteIngresoNewButton().setDisabled(!this.getComprobanteIngresoStore().canCreate());
    	this.getComprobanteIngresoSaveButton().setTooltip('Guardar');
        this.getComprobanteIngresoSaveButton().setDisabled(!this.getComprobanteIngresoStore().canUpdate());
    	
        this.getRemoteSaldoTerceroStore().getProxy().setExtraParam('Grupo', 'CxC');
        
    	this.getComprobanteIngresoStore().on('write', function(store, operation, eOpts ){
    		var record =  operation.getRecords()[0];
    		if (operation.action=='create') {
        		this.cargarItems(record);
        	}  
            if (operation.action != 'destroy') {
               this.getComprobanteIngresoList().getSelectionModel().select(record,true,true);
               this.refreshButtons([record]);
            }
    	}, this);
    	
    	this.getComprobanteIngresoStore().on('anulado', function(store, record, success){
    		if(success){
    			this.refreshButtons([record]);
    			this.getItemToolbar().setDisabled(true);
    		}
    	}, this);
    	
    	this.getComprobanteIngresoStore().on('asentado', function(store, record, success){
    		if(success){
    			this.refreshButtons([record]);
    			this.getItemToolbar().setDisabled(true);
    		}
    	}, this);
    	
    	this.getComprobanteIngresoStore().on('reversado', function(store, record, success){
    		if(success){
    			this.refreshButtons([record]);
    			this.getItemToolbar().setDisabled(false);
    		}
    	}, this);
    	
    	this.getComprobanteIngresoItemStore().on('write', function(store, operation, eOpts ){    		
    		var valor=0;
    		store.each(function(rec){
    			valor=valor+rec.get('Abono');
    		});
    		var retStore= this.getComprobanteIngresoRetencionStore();
    		retStore.each(function(rec){
    			valor=valor-rec.get('Valor');
    		});
    		var parent = this.getComprobanteIngresoList().getSelectionModel().getSelection()[0];
    		this.getComprobanteIngresoStore().updateLocal({Id: parent.getId(), Valor:valor});
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
			
			var canUpdate=true;
			if(record.get('FechaAsentado') ){
				this.getComprobanteIngresoAsentarButton().setDisabled(false || record.get('Externo'));
				this.getComprobanteIngresoDeleteButton().setDisabled(true);
				this.getComprobanteIngresoAsentarButton().setIconCls('desasentar');
				canUpdate=false;
			}
			else{
				if(record.get('FechaAnulado')){
					this.getComprobanteIngresoAsentarButton().setDisabled(true);
					this.getComprobanteIngresoDeleteButton().setDisabled(true);
					canUpdate=false;
				}
				else{
					this.getComprobanteIngresoAsentarButton().setDisabled(false);
					this.getComprobanteIngresoDeleteButton().setDisabled(false);
					this.getComprobanteIngresoAsentarButton().setIconCls('asentar');
				}
			}
				
        	this.getComprobanteIngresoForm().getForm().loadRecord(record);
            this.getComprobanteIngresoSaveButton().setTooltip('Actualizar comprobante');
            //this.getIngresoDeleteButton().setDisabled(!this.getIngresoStore().canDestroy());
            this.getComprobanteIngresoSaveButton().setDisabled(!(this.getComprobanteIngresoStore().canUpdate() && canUpdate));
                        
            var ingreso=this.getIngresoStore();
            ingreso.getProxy().setExtraParam('IdSucursal',record.get('IdSucursal'));
            ingreso.getProxy().setExtraParam('IdTercero', record.get('IdTercero'));
            ingreso.getProxy().setExtraParam('ConSaldo', true);
            ingreso.getProxy().setExtraParam('Asentado', true);
            ingreso.loadPage(1);
        }
        else{
			
			this.getRemoteSaldoTerceroCombo().setReadOnly(false);
			this.getSucursalAutorizadaCombo().setReadOnly(false);
			
        	this.getComprobanteIngresoForm().getForm().reset();   
        	
        	this.getComprobanteIngresoSaveButton().setTooltip('Guardar comprobante');
        	this.getComprobanteIngresoDeleteButton().setDisabled(true);
        	this.getComprobanteIngresoSaveButton().setDisabled(!this.getComprobanteIngresoStore().canCreate());
        	        	
        	var suc = this.getSucursalAutorizadaCombo().getStore().getAt(0);
        	if (suc) this.getSucursalAutorizadaCombo().setValue(suc.getId());
        	        	
        };
		
		// dispara evento para el combo de sucursales...
        this.getSucursalAutorizadaCombo().fireEvent('select', this.getSucursalAutorizadaCombo(),
				[this.getSucursalAutorizadaCombo().findRecordByValue(this.getSucursalAutorizadaCombo().getValue() )]);    
	},
	
	cargarItems:function(record){
	    
	    if(record.get('Valor')!=0){
	    	this.getComprobanteIngresoItemStore().load({params:{IdComprobanteIngreso: record.getId()}});
	    	this.getItemList().determineScrollbars();
	    }
	
	    if(record.get('FechaAnulado') || record.get('FechaAsentado'))
	    	this.getItemToolbar().setDisabled(true);
	    else
	        this.getItemToolbar().setDisabled(false);
	        
	    this.refreshButtonsItem();
	},
	
	refreshButtonsItem: function(selections){	

		this.getItemNewButton().setDisabled(!this.getComprobanteIngresoItemStore().canCreate());
		this.getItemSaveButton().setDisabled(!this.getComprobanteIngresoItemStore().canUpdate());
		
		selections=selections||[];	
		
		if (selections.length){
			var record= selections[0];
			
			var storeIngreso =this.getIngresoStore();
			if(!storeIngreso.getById(record.get('IdIngreso'))){
				storeIngreso.addLocal({
				Id:record.get('IdIngreso'),
				Documento:record.get('Documento'),
				Saldo:record.get('Saldo'),
				Valor:record.get('Valor')
				})
			};
			
        	this.getItemForm().getForm().loadRecord(record);
            this.getItemSaveButton().setTooltip('Actualizar');
            this.getItemDeleteButton().setDisabled(!this.getComprobanteIngresoItemStore().canDestroy());
            this.getIngresoCombo().setReadOnly(true);
        }
        else{
        	this.getItemForm().getForm().reset();            
        	this.getItemSaveButton().setTooltip('Guardar');
        	this.getItemDeleteButton().setDisabled(true);
        	this.getIngresoCombo().setReadOnly(false);
        };           
	}	
});
