Ext.define('App.controller.Infante',{
	extend: 'Ext.app.Controller',
    stores: ['Infante','RemoteTercero','InfanteAcudiente',
    'InfantePadre','Matricula','Curso','Clase','Tarifa','MatriculaItem'],  
    models: ['Infante','Tercero'],
    views:  ['infante.Panel' ],
    refs:[
    	{ref: 'mainPanel', selector: 'infantepanel' },
    	{ref: 'infanteNewButton',   selector: 'toolbar[name=mainToolbar] button[action=new]' },
    	{ref: 'infanteSaveButton', selector: 'infanteform button[action=save]' },
    	{ref: 'infanteDeleteButton', selector: 'infanteform button[action=delete]' },
    	{ref: 'buscarInfanteText', 	 selector: 'infantepanel textfield[name=buscarInfanteText]'},
    	
    	{ref: 'infanteList',    	 selector: 'infantelist' },
    	{ref: 'infanteForm',    	 selector: 'infanteform' },
    	{ref: 'infanteSelectButton', selector: 'infantelist button[action=select]'},
    	
    	{ref: 'terceroForm', 	 selector: 'infanteterceroform' },
    	{ref: 'terceroSaveButton', 	 selector: 'infanteterceroform button[action=save]' },
    	
    	{ref: 'padreList', 	 selector: 'infantepadrelist' },
    	{ref: 'padreForm', 	 selector: 'infantepadreform' },
    	{ref: 'padreNewButton', selector: 'infantepadreform button[action=new]' },
    	{ref: 'padreSaveButton', selector: 'infantepadreform button[action=save]' },
    	{ref: 'padreDeleteButton', selector: 'infantepadreform button[action=delete]' },
    	
    	{ref: 'acudienteList', 	 selector: 'infanteacudientelist' },
    	{ref: 'acudienteForm', 	 selector: 'infanteacudienteform' },
    	{ref: 'acudienteNewButton', selector: 'infanteacudienteform button[action=new]' },
    	{ref: 'acudienteSaveButton', selector: 'infanteacudienteform button[action=save]' },
    	{ref: 'acudienteDeleteButton', selector: 'infanteacudienteform button[action=delete]' },
    	
    	{ref: 'matriculaForm', 	 selector: 'infantematriculaform' },
    	{ref: 'sucursalAutorizadaCombo', 	 selector: 'infantematriculaform sucursalautorizadacombo' },
    	{ref: 'centroAutorizadoCombo', 	 selector: 'infantematriculaform centroautorizadocombo' },
    	{ref: 'matriculaNewButton', selector: 'infantematriculaform button[action=new]' },
    	{ref: 'matriculaSaveButton', selector: 'infantematriculaform button[action=save]' },
    	{ref: 'matriculaAsentarButton', selector: 'infantematriculaform button[action=asentar]' },
    	{ref: 'matriculaDeleteButton', selector: 'infantematriculaform button[action=delete]' }
    	
    ],

    init: function(application) {
    	    	
        this.control({
        	
        	'infantelist': { 
                selectionchange: function( sm,  selections,  eOpts){
                	var disableSIB=true;
                	if (selections.length){
    					disableSIB=false;   		
        			}
        			this.getInfanteSelectButton().setDisabled(disableSIB);
                }
            },
        	
            'infantepadrelist': { 
                selectionchange: function( sm,  selections,  eOpts){
                	var disable=true;
                	if (selections.length){
    					disable= !this.getInfanteStore().canUpdate();
    					var record=selections[0];
    					this.terceroAddLocal(record, 'IdTercero');
    					this.getPadreForm().getForm().loadRecord(record);
        			}
        			else{
        				this.getPadreForm().getForm().reset();
        			}
        			this.getPadreDeleteButton().setDisabled(disable);
                }
            },
            
            'infanteacudientelist': { 
                selectionchange: function( sm,  selections,  eOpts){
                	var disable=true;
                	if (selections.length){
    					disable= !this.getInfanteStore().canUpdate();
    					var record=selections[0];
    					this.terceroAddLocal(record, 'IdTercero');
    					this.getAcudienteForm().getForm().loadRecord(record);
        			}
        			else{
        				this.getAcudienteForm().getForm().reset();
        			}
        			this.getAcudienteDeleteButton().setDisabled(disable);
                }
            },
            
            'infantematriculalist': { 
                selectionchange: function( sm,  selections,  eOpts){
                	var disable=true;
                	
                	if (selections.length){
    					disable= !this.getInfanteStore().canUpdate();
    					var record=selections[0]; 
    					this.getMatriculaForm().getForm().loadRecord(record);
    					if(record.get('IdIngreso')){
    						this.getMatriculaAsentarButton().setIconCls('desasentar');
    						this.getMatriculaAsentarButton().setTooltip('Reversar Matricula');
    					}
    					else{
    						this.getMatriculaAsentarButton().setIconCls('asentar');
    						this.getMatriculaAsentarButton().setTooltip('Matricular');
    					}
    					
        			}
        			
        			this.getMatriculaDeleteButton().setDisabled(disable);
        			this.getMatriculaAsentarButton().setDisabled(disable);
                }
            },
           
            
        	'infantepanel button[action=buscarInfante]': {
                click: function(button, event, options){
                	var searchText= this.getBuscarInfanteText().getValue();
                	var documento,nombres,apellidos;
                	
                	documento=parseInt(searchText);
                	if(isNaN(documento)){
                		documento=''
                		if(searchText.indexOf(',')>=0){
                			var v= searchText.split(',');
                			nombres=v[0].trim();
                			apellidos=v[1].trim();
                		}
                		else{
                			nombres=searchText
                		}
                	}	
                	var request={
                		Documento: documento,
                		Nombres:nombres,
                		Apellidos:apellidos,
						format:'json'
                	};
                	
                	var store = this.getInfanteStore();       	  	
                	store.getProxy().extraParams=request;
                	store.loadPage(1);
                }
            },
            
            'infantelist button[action=select]': {
                click: function(button, event, options){
                	this.getMainPanel().hideSearchWindow();
                	var record= this.getInfanteList().getSelectionModel().getSelection()[0];
                	this.infanteLoadRecord(record);
                }
            },
            
            'toolbar[name=mainToolbar] button[action=new]':{
            	click:function(button, event, options){
                	this.getInfanteForm().getForm().reset();
                	
                	this.getStore('InfantePadre').removeAll();
                	this.getPadreSaveButton().setDisabled(true);
                	this.getPadreNewButton().setDisabled(true);
                	
                	this.getStore('InfanteAcudiente').removeAll();
                	this.getAcudienteSaveButton().setDisabled(true);
                	this.getAcudienteNewButton().setDisabled(true);
                	
                	this.getMatriculaStore().removeAll();
                	this.getMatriculaNewButton().setDisabled(true);
    				this.getMatriculaSaveButton().setDisabled(true);			
                }
            },
            
            'infanteform button[action=save]':{
            	click: function(button, event, options){
            		var record = this.getInfanteForm().getForm().getFieldValues(true);
            		this.getInfanteStore().getProxy().extraParams={format:'json'};
            		this.getInfanteStore().save(record);
            	}
            },
            
            //padre
            'infantepadreform button[action=new]':{
            	click:function(button, event, options){
            		this.getPadreList().getSelectionModel().deselectAll();
                }
            },
            
            'infantepadreform button[action=save]':{
            	click: function(button, event, options){
            		var record = this.getPadreForm().getForm().getFieldValues(true);
            		this.getStore('InfantePadre').getProxy().extraParams={format:'json'};
            		this.getStore('InfantePadre').save(record);
            	}
            },
            
            //acudiente
            'infanteacudienteform button[action=new]':{
            	click:function(button, event, options){
            		this.getAcudienteList().getSelectionModel().deselectAll();
                }
            },
            
            'infanteacudienteform button[action=save]':{
            	click: function(button, event, options){
            		var record = this.getAcudienteForm().getForm().getFieldValues(true);
            		this.getStore('InfanteAcudiente').getProxy().extraParams={format:'json'};
            		this.getStore('InfanteAcudiente').save(record);
            	}
            },
            
            //tercero 
            'infanteform button[action=new]': {
                click: function(button, event, options){
                	this.getMainPanel().showCreateTerceroWindow();
                }
            },
            
            'infanteform infantetercerocombo':{
            	select:function(combo, value, options){
            		var data = value[0].data;
            		this.getInfanteForm().getForm().setValues({
            			'DocumentoTercero':data.Documento,
            			'DVTercero':data.DigitoVerificacion,
            			'TelefonoTercero':data.Telefono,
            			'CelularTercero':data.Celular,
            			'MailTercero':data.Mail
            		});
            	}
            },         
            
            'infanteterceroform button[action=save]':{
            	click: function(button, event, options){
            		var record = this.getTerceroForm().getForm().getFieldValues(true);
            		this.getRemoteTerceroStore().save(record);
            	}
            },
            
            //matricula
            'infantematriculaform button[action=new]':{
            	click:function(button, event, options){
            		
            	}
            },
            
            'infantematriculaform button[action=save]':{
            	click:function(button, event, options){
            		var record = this.getMatriculaForm().getForm().getFieldValues(true);
            		if(!record.Activo) record.Activo=false;
            		var maStore= this.getMatriculaStore();
            		this.getMatriculaStore().getProxy().extraParams={
            			format:'json',
            			CrearItems:true
            		};
            		this.getMatriculaStore().save(record);            		
            		
            	}
            },
            
            'infantematriculaform sucursalautorizadacombo':{
            	select:function(combo, value, options){
            		var sucursal=value[0];
            		this.getCentroAutorizadoCombo().getStore().removeAll();
            		this.getCentroAutorizadoCombo().getStore().loadRawData(getCentrosData(sucursal.getId()));
            		this.getCentroAutorizadoCombo().getStore().clearFilter(true);
            		this.getCentroAutorizadoCombo().getStore().
            				filter([{filterFn: function(item) { return item.getId() > 1; }}])
            		
            		var centro = this.getCentroAutorizadoCombo().getStore().getAt(0);
			        if (centro){
			        	this.getCentroAutorizadoCombo().setValue(centro.getId());
			      
			        }
            	}
            }
            
        })
    },
    
    onLaunch: function(application){
    	var me=this;
    	Aicl.Util.executeRestRequest({
			url : Aicl.Util.getUrlApi()+'/Infante/Aux',
			params:{Fecha:Ext.Date.format(new Date(),'d.m.Y'), Activo:true},
			method : 'get',
			callback : function(result,success) {
				console.log('InfanteAux result', arguments);
				if(success){
					me.getCursoStore().loadRawData(result.CursoList);
					me.getClaseStore().loadRawData(result.ClaseList);
				}
			}
		});
    	    	
    	
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
            if (operation.action == 'create') {
            	this.infanteLoadRecord(record);
            }            
    	}, this);
    	
    	this.getRemoteTerceroStore().on('write', function(store, operation, eOpts ){
    		this.getMainPanel().hideCreateTerceroWindow();
            this.getTerceroForm().getForm().setValues({
            	Documento:'',
            	DigitoVerificacion:'',
            	Nombre:'',
            	Celular:'',
            	Mail:''
            });
            this.getTerceroForm().getForm().clearInvalid();
    	}, this);
    	
    	this.getMatriculaStore().on('write', function(store, operation, eOpts ){
    		var result = Ext.decode(operation.response.responseText);
    		console.log('matriculaStore on write result', result);
    		this.getMatriculaItemStore().removeAll();
    		this.getMatriculaItemStore().loadRawData(result.MatriculaItemList);
    		this.getTarifaStore().removeAll();
    		this.getTarifaStore().loadRawData(result.TarifaList);
    	}, this);
    	
    	
    	this.getTerceroSaveButton().setDisabled(!(this.getRemoteTerceroStore().canUpdate()));
    	this.getInfanteNewButton().setDisabled(!this.getInfanteStore().canCreate());
		this.getInfanteSaveButton().setDisabled(!this.getInfanteStore().canUpdate());
		
		var suc = this.getSucursalAutorizadaCombo().getStore().getAt(0);
        if (suc){
        	this.getSucursalAutorizadaCombo().setValue(suc.getId());
        	this.getSucursalAutorizadaCombo().fireEvent('select', 
        	this.getSucursalAutorizadaCombo(), [suc]);
        }
				
    },
    
    infanteLoadRecord:function(record){
    	var me=this;
    	var ipStore= me.getStore('InfantePadre');
    	var iaStore= me.getStore('InfanteAcudiente');
    	var maStore = me.getMatriculaStore()
    	
    	this.getInfanteDeleteButton().setDisabled(!this.getInfanteStore().canDestroy());
    	
    	this.getPadreNewButton().setDisabled(!this.getInfanteStore().canUpdate());
    	this.getPadreSaveButton().setDisabled(!this.getInfanteStore().canUpdate());
    	
    	this.getAcudienteNewButton().setDisabled(!this.getInfanteStore().canUpdate());
    	this.getAcudienteSaveButton().setDisabled(!this.getInfanteStore().canUpdate());
    	
    	this.getMatriculaNewButton().setDisabled(!this.getInfanteStore().canUpdate());
    	this.getMatriculaSaveButton().setDisabled(!this.getInfanteStore().canUpdate());
    	
    	this.terceroAddLocal(record, 'IdTerceroFactura');
    	    	                    	
        this.getInfanteForm().getForm().loadRecord(record);
        this.getMatriculaForm().getForm().setValues({IdInfante:record.getId()});
        
        ipStore.removeAll();
        iaStore.removeAll();
        maStore.removeAll();
        
        Aicl.Util.executeRestRequest({
			url : Aicl.Util.getUrlApi()+'/Infante/Info/'+record.getId(),
			method : 'get',
			callback : function(result,success) {
				console.log('InfanteInfo result', arguments);
				if(success){
					ipStore.loadRawData(result.InfantePadreList);
					iaStore.loadRawData(result.InfanteAcudienteList);	
					maStore.loadRawData(result.MatriculaList);
				}
			}
		});
    },
    
    terceroAddLocal:function(record, id){
    	var rt= this.getRemoteTerceroStore();
		if(!rt.getById(record.get(id))){
			rt.addLocal({
				Id:record.get(id),
				Nombre:record.get('NombreTercero'),
				Documento:record.get('DocumentoTercero'),
				DigitoVerificacion:record.get('DVTercero'),
				Telefono:record.get('TelefonoTercero'),
				Celular:record.get('CelularTercero'),
				Mail:record.get('MailTercero')
			})
		};
    }
    
 	
});