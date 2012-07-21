(function(){
	Ext.ns('Aicl.Util');
	Aicl.Util = {}; 
	
	var Util = Aicl.Util, 
		_msgCt, 
		_createBox= function (title, content){
       		return '<div class="msg"><h3>' + title + '</h3><p>' + content + '</p></div>';
    	};
    	
    _registerSession=function(result){
		sessionStorage["authenticated"]= true;
		sessionStorage["roles"]=Ext.encode(result.Roles||[]);
		sessionStorage["permissions"]=Ext.encode(result.Permissions||[]);
		sessionStorage["sucursales"]=Ext.encode(result.Sucursales||[]);
		sessionStorage["centros"]=Ext.encode(result.Centros||[]);
		sessionStorage["codigosEgreso"]=Ext.encode(result.CodigosEgreso||[]);
		sessionStorage["codigosIngreso"]=Ext.encode(result.CodigosIngreso||[]);
		sessionStorage["rubros"]=Ext.encode(result.Rubros||[]);
		sessionStorage["displayName"]=result.DisplayName|| 'anonimo';				
	};

	_clearSession=function(result){
		sessionStorage["authenticated"]= false;
		sessionStorage.removeItem("roles");
		sessionStorage.removeItem("permissions");
		sessionStorage.removeItem("sucursales");
		sessionStorage.removeItem("centros");
		sessionStorage.removeItem("codigosEgreso");
		sessionStorage.removeItem("codigosIngreso");
		sessionStorage.removeItem("rubros");
		sessionStorage.removeItem("displayName");
	};
	        
    Ext.apply(Util,{
    	
    	convertToDate: function (v){
			if (!v) return null;
			return (typeof v == 'string')
			?new Date(parseFloat(/Date\(([^)]+)\)/.exec(v)[1])) // thanks demis bellot!
			:v ;   
		},

		convertToUTC: function (date){
			return Ext.Date.format(date,'MS');
		},
		
		formatInt: function (value, format){
			return this.formatNumber(value, ',0');		
		},

		formatNumber: function (value, format){
			format= format|| ',0.00'; 
			return ( typeof(value) =='number')?
				Ext.util.Format.number(value,format):
				Ext.util.Format.currency( this.unFormatValue(value),format );
		},

		formatCurrency: function (value){
			return typeof(value=='number')?
				Ext.util.Format.currency(value):
				Ext.util.Format.currency( this.unFormatValue(value) );
			  
		},

		unFormatValue: function (value){
			return  value.replace(/[^0-9\.]/g, '');
		},

		isToday: function(date){
			var d ;
			if( typeof date=='object') d= Ext.Date.format(date,'d.m.Y');
			else d= Ext.util.Format.substr(date,0,10);
	
			var today = Ext.Date.format(new Date() ,'d.m.Y');
			return today==d;
		},
		
		// ajax request
		
		executeAjaxRequest: function (config){
			Ext.MessageBox.show({
				msg: config.msg || 'Please wait...',
				progressText: config.progessText || 'Executing...',
		        width: config.width || 300,
				wait:true,
		        waitConfig: {interval:200}
		   		//icon:'ext-mb-download' //custom class in msg-box.html
			});
		
			Ext.Ajax.request({
				url: config.url + ( Ext.util.Format.uppercase(config.method)=='DELETE'
									? Aicl.Util.paramsToUrl(config.params) 
									:config.format==undefined?'': Ext.String.format('?format={0}', config.format)),
				method: config.method, 
			    success: function(response, options) {
		            var result = Ext.decode(response.responseText);
					if(result.ResponseStatus.ErrorCode){
						Aicl.Util.msg('Ajax request error in ResponseStatus', result.ResponseStatus.Message + ' -( '); 
						return ;
					}
					if(config.showReady) Aicl.Util.msg('Ready', result.ResponseStatus.Message);
					if( config.success ) config.success(result);
					
			    },
			    failure: function(response, options) {
			    	var result={};
			    	if(response.responseText){
			    		 result= Ext.decode(response.responseText);
			    	}
			    	else{
			    		result=response;
			    	}
		            
					Aicl.Util.msg('Ajax request failure', 'Status: ' + response.status +'<br/>Message: '+
					 ((result.ResponseStatus)? result.ResponseStatus.Message: response.statusText) +' -( ');
					if( config.failure ) config.failure(result);
				},
				callback: function(options, success, response) {
					Ext.MessageBox.hide();	
					var result={};
					if(response.responseText){
						result = Ext.decode(response.responseText);
					}
					else{
						result=response;
					}
					if( config.callback ) config.callback(result, success);
				},
				params:config.params
			});	
		},

		executeRestRequest: function (config){
			config.format=  config.format|| 'json';
			this.executeAjaxRequest(config)
		},

		paramsToUrl:function(params){
			var s='';
			for( p in params){
				s= Ext.String.urlAppend(s, Ext.String.format('{0}={1}', p, params[p]));
			};
			return s;
			
		},
		
		// proxies
			
		createRestProxy:function (config){
			
			config.format=config.format|| 'json';
			config.type= config.type || 'rest';
			
			config.api=config.api||{};
			config.url= config.url || (Aicl.Util.getUrlApi()+'/' + config.storeId);
			config.api.create=config.api.create|| (config.url+'/create');
			config.api.read=config.api.read|| (config.url+'/read');
			config.api.update=config.api.update|| (config.url+'/update');
			config.api.destroy=config.api.destroy|| (config.url+'/destroy');
			config.api.patch=config.api.patch|| (config.url+'/patch');
			
			return this.createProxy(config);
			
		},
		
		createAjaxProxy:function (config){
			config.type=config.type||'ajax';
			config.url= config.url || (Aicl.Util.getHttpUrlApi()+'/' + config.storeId);
			var proxy= this.createProxy(config);
			proxy.actionMethods= {create: "POST", read: "GET", update: "PUT", destroy: "DELETE"};
			return proxy;
		},
		
		createProxy:function (config){
				
			if(config.format){
				config.extraParams=config.extraParams||{};
				config.extraParams['format']=config.format
			}
			
			config.api=config.api||{};
			config.api.create=config.api.create;
			config.api.read=config.api.read;
			config.api.update=config.api.update;
			config.api.destroy=config.api.destroy;
			
			config.reader= config.reader||{
				type: 'json',
		        root: 'Data', 
				totalProperty : config.totalProperty? config.totalProperty:undefined,
		    	successProperty	: config.successProperty? config.successProperty: undefined,
				messageProperty : config.messageProperty? config.messageProperty: undefined
			};
			
			config.writer= config.writer || {
				type: 'json',
				getRecordData: function(record) { 
					console.log('Proxy writer getRecordData', record);
					return record.data; 
				}
			};
			
			var proxy={
				type: config.type,
				url : config.url,
				api : config.api,
		    	reader: config.reader,
				writer: config.writer,
				pageParam: config.pageParam? config.pageParam: undefined,
				limitParam: config.limitParam? config.limitParam:undefined,
				startParam: config.startParam? config.startParam:undefined,
				extraParams: config.extraParams?config.extraParams:undefined,
				storeId: config.storeId,
				listeners:config.listeners ||
				{
		        	exception:function(proxy, response,  operation,  options) {
		        		console.log('Proxy exception store: '+ this.storeId, arguments)
		            	var result={};
			    		if(response.responseText){
			    	 		result= Ext.decode(response.responseText);
			    		}
			    		else{
			    			result=response;
			    		}
						Aicl.Util.msg('Proxy exception store:' + this.storeId ,
						'Status: ' + response.status +'<br/>Message: '+
							((result.ResponseStatus)? result.ResponseStatus.Message: response.statusText) +' -( ');
					 	
					 	if(this.storeId){
					 		var store= Ext.getStore(this.storeId);
					 		if(store) store.rejectChanges();
					 	}
		        	}
		        	
		    	}
		    };
		    return proxy;
		},
				
		isAuth: function (){
			var v = sessionStorage["authenticated"]
			return v==undefined? false: Ext.decode(v);
		},
				
		login: function(config){
			this.executeRestRequest({
				url : Aicl.Util.getUrlLogin(),
				method : 'get',
				success : function(result) {
					_registerSession(result);
					if(config.success) config.success(result);
				},
				failure : config.failure,
				callback: config.callback,
				params : config.params
			});
		},
				
		logout: function(config){
			config=config||{};
			this.executeRestRequest({
				url : Aicl.Util.getUrlLogout()+'?format=json',
				method : 'delete',
				callback : function(result, success) {
					_clearSession();
					if(config.callback) config.callback(result,success);
				},
				failure : config.failure,
				success : config.success
			});
		},
				
		getRoles:function(){
			return sessionStorage.roles? Ext.decode(sessionStorage.roles): [];		
		},
		
		getSucursales:function(){
			return sessionStorage.sucursales? Ext.decode(sessionStorage.sucursales): [];		
		},
		
		getCentros:function(){
			return sessionStorage.centros? Ext.decode(sessionStorage.centros): [];		
		},
		
		getCodigosEgreso:function(){
			return sessionStorage.codigosEgreso? Ext.decode(sessionStorage.codigosEgreso): [];		
		},
		
		getCodigosIngreso:function(){
			return sessionStorage.codigosIngreso? Ext.decode(sessionStorage.codigosIngreso): [];		
		},
		
		getRubros:function(){
			return sessionStorage.rubros? Ext.decode(sessionStorage.rubros): [];		
		},
		
		setUrlModules: function (urlModules){
			sessionStorage["urlModules"]=urlModules;
		},
		
		getUrlModules:function (){
			return sessionStorage["urlModules"];
		},
		
		
		setUrlLogin: function (urlLogin){
			sessionStorage["urlLogin"]=urlLogin;
		},
		
		getUrlLogin:function (){
			return sessionStorage["urlLogin"];
		},
		
		setUrlLogout: function (urlLogout){
			sessionStorage["urlLogout"]=urlLogout;
		},
		
		getUrlLogout:function (){
			return sessionStorage["urlLogout"];
		},
				
		setUrlApi: function (urlApi){
			sessionStorage["urlApi"]=urlApi;
		},
		
		getUrlApi:function (){
			return sessionStorage["urlApi"];
		},
		
		getHttpUrlApi:function (){
			return sessionStorage["httpUrlApi"];
		},
		
		setHttpUrlApi:function(httpUrlApi){
			sessionStorage["httpUrlApi"]=httpUrlApi;
		},
		
		setPhotoDir: function(photoDir){
			sessionStorage["photoDir"]=photoDir;
		},
		
		getPhotoDir: function(){
			return sessionStorage["photoDir"];
		},
		
		setEmptyImgUrl: function( url){
			sessionStorage["emptyImgUrl"]= url; 
		},
		
		getEmpytImgUrl: function(){
			return sessionStorage["emptyImgUrl"];
		},
		
		hasRole:function (role){
			var roles= this.getRoles();
			for(var r in roles){
 			  if (roles[r].Name==role) return true;
			};
			return false;
		},
		
		hasPermission:function (permission){
			var a= sessionStorage.permissions? Ext.decode(sessionStorage.permissions): [];
			return a.indexOf(permission)>=0;
		},
		
		
		//helpers
		isValidEmail:function (email) {
			var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
			return filter.test(email)
		},
		
		msg: function(title, format){
            if(!_msgCt){
                _msgCt = Ext.core.DomHelper.insertFirst(document.body, {id:'msg-div'}, true);
            }
            var s = Ext.String.format.apply(String, Array.prototype.slice.call(arguments, 1));
            var m = Ext.core.DomHelper.append(_msgCt, _createBox(title, s), true);
            m.hide();
            m.slideIn('t').ghost("t", { delay: 1000, remove: true});
		}   	
    	
    });	
    
})();
    	

Ext.define('Aicl.data.Store',{
	extend: 'Ext.data.Store',
	 initComponent:function() {
        // call parent init component
        Aicl.data.Store.superclass.initComponent.apply(this, arguments);
         // add custom events
        this.addEvents('asentado', 'reversado','anulado');
    },
	
    constructor: function(config){    	    	
    	
    	config.proxy= config.proxy || Aicl.Util.createRestProxy( {
    		storeId: config.storeId,
			url: config.proxy && config.proxy.url?
				config.proxy.url:
				Aicl.Util.getUrlApi()+'/' + config.storeId
    	});
    	
    	config.autoLoad= config.autoLoad==undefined? false: config.autoLoad;
    	config.autoSync= config.autoSync==undefined? true: config.autoSync;
    	config.listeners= config.listeners ||
    	{
            write: (config.listeners && config.listeners.write)?
            config.listeners.write(store, operation, options):
            function(store, operation, options){
            	//console.log('store'+ this.storeId+ '  write arguments: ', arguments); 
                var record =  operation.getRecords()[0],
                    name = Ext.String.capitalize(operation.action),
                    verb;                                
                if (name == 'Destroy') {
                	record =operation.records[0];
                    verb = 'Destroyed';
                } else {
                    verb = name + 'd';
                }
                //console.log('store'+ this.storeId +' write record: ', record);
                Aicl.Util.msg(name, Ext.String.format("{0} {1}: {2}", verb, this.storeId , record.getId()));
            }
        };
        this.callParent(arguments);
    }
});  


Ext.data.Store.implement({
    //record={somefield:'value', othefield:'value'}
    save:function(record){
		if (record.Id){
			var keys = Ext.create( this.model.getName(),{}).fields.keys;
			var sr = this.getById(parseInt( record.Id) );
			sr.beginEdit();
			for( var r in record){
				if( keys.indexOf(r)>0 )
					sr.set(r, record[r])
			}
			sr.endEdit(); 
		}
		else{
			var nr = Ext.create( this.model.getName(),record );
			this.add(nr);
		}			
	},
	
	//record={somefield:'value', othefield:'value'}
	addLocal:function(record){
		var nr = Ext.create( this.model.getName(),record );
		this.suspendAutoSync();
		this.add(nr);
		this.resumeAutoSync();
	},
	
	/**
     * asienta el registro
     * @param record={Id:idValue, somefield:'value', othefield:'value'}
     * @return model 
     */
	updateLocal:function(record){
			var keys = Ext.create( this.model.getName(),{}).fields.keys;
			var sr = this.getById(parseInt( record.Id) );
			this.suspendAutoSync();
			sr.beginEdit();
			for( var r in record){
				if( keys.indexOf(r)>0 )
					sr.set(r, record[r])
			}
			sr.endEdit();
			this.commitChanges();
			this.resumeAutoSync();
			return sr;
	},
	
	/**
     * patch field
     * @param  field (model), action(string), config:{success, failure, callback}
     */
	
	patch:function(field, action, config){
		config=config||{};
		Aicl.Util.executeRestRequest({
				url : this.getProxy().api.patch+'/'+field.getId()+'/'+action,
				method : 'PATCH',
				success : config.success,
				failure : config.failure,
				callback: config.callback
			});
	},
	
	/**
     * asienta el registro
     * @param {Ext.data.Model} field
     * @return {void} 
     */
	asentar:function(field){
		
		var me= this;
		this.patch(field, 'asentar',
		{
			callback:function(result, success){
				var record;
				if (success) 
					record =me.updateLocal(result.Data[0]);
				else
					record= Ext.create( me.model.getName(),{});
				me.fireEvent('asentado', me, record, success);
			}
		});
	},
	
	/**
     * reversa el registro
     * @param {Ext.data.Model} field
     */
	reversar:function(field){
		var me= this;
		this.patch(field, 'reversar',
		{
			callback:function(result, success){
				var record;
				if (success){
					var data= result.Data[0];
					data.FechaAsentado=null;
					record= me.updateLocal(data);
				}
				else
					record= Ext.create( me.model.getName(),{});
				me.fireEvent('reversado', me, record, success);
			}
		});
	},
	
	/**
     * anula el registro. Desata el evento 'anulado'
     * @param {Ext.data.Model} field
     */
	
	anular:function(field){
		
		var me= this;
		this.patch(field, 'anular',
		{
			callback:function(result, success){
				var record;
				if (success) 
					record =me.updateLocal(result.Data[0]);
				else
					record= Ext.create( me.model.getName(),{});
				me.fireEvent('anulado', me, record, success);
			}
		});
	},
	
	canCreate:function(){
		 return Aicl.Util.hasPermission(Ext.String.format('{0}.create', this.storeId));
	},
    canRead:function(){
		 return Aicl.Util.hasPermission(Ext.String.format('{0}.read', this.storeId));
	},
	canUpdate:function(){
		 return Aicl.Util.hasPermission(Ext.String.format('{0}.update', this.storeId));
	},
	canDestroy:function(){
		 return Aicl.Util.hasPermission(Ext.String.format('{0}.destroy', this.storeId));
	},
	canExecute:function(operation){
		 return Aicl.Util.hasPermission(Ext.String.format('{0}.{1}', this.storeId,operation));
	}
    
});

Ext.form.Panel.implement({
    setFocus:function(item){
    	var ff = item==undefined?this.items.items[1].name:Ext.isNumber(item)?this.items.items[item].name:item;
    	this.getForm().findField(ff).focus(false,10);
    }
});

Ext.define('EstadoAsentado', {
    extend: 'Ext.data.Model',
    idProperty: 'Id',
    fields: [{type: 'int', name: 'Id'},   {type: 'string', name: 'Estado'}]
});

var estadoAsentadoData = [{Id:1,Estado:'Todos'},{Id:2,Estado:'Asentados'},{Id:3,Estado:'Pendientes'}];

function createEstadoAsentadoStore() {
    return Ext.create('Ext.data.Store', {
        autoDestroy: true,
        model: 'EstadoAsentado',
        data: estadoAsentadoData
    });
}

Ext.define('estadoasento.ComboBox', {
	extend:'Ext.form.field.ComboBox',
	alias : 'widget.estadoasentadocombo',
    displayField: 'Estado',
	valueField: 'Id',
    store: createEstadoAsentadoStore(),
    queryMode: 'local',
    typeAhead: true,
    forceSelection:true,
    listeners   : {  
     	beforerender: function(combo){
       		combo.setValue(1);  
       	}
    }
});


Ext.define('SucursalAutorizadaModel',{
	extend: 'Ext.data.Model',
	idProperty: 'Id',
	fields:[{name: 'Id',type: 'int'},{name: 'Nombre',type: 'string'},{name: 'Codigo',type: 'string'}]
});


function createSucursalAutorizadaStore() {
    return Ext.create('Ext.data.Store', {
        autoDestroy: true,
        model: 'SucursalAutorizadaModel',
        data: Aicl.Util.getSucursales()
    });
}

Ext.define('sucursalautorizada.ComboBox',{
	extend:'Ext.form.field.ComboBox',
	alias : 'widget.sucursalautorizadacombo',
    displayField: 'Nombre',
	valueField: 'Id',
    store: createSucursalAutorizadaStore(),
    queryMode: 'local',
    typeAhead: true,
    forceSelection:true,
    name:'IdSucursal'
});


Ext.define('CentroAutorizadoModel',{
	extend: 'Ext.data.Model',
	idProperty: 'Id',
	fields:[{name: 'Id',type: 'int'},{name: 'Nombre',type: 'string'},
		{name: 'Codigo',type: 'string'},{name: 'IdSucursal',type: 'int'}]
});


function getCentrosData(idSucursal, idCentro) {
	var data=[];
	var centros= Aicl.Util.getCentros();
	for(var i in centros){
		if(centros[i].IdSucursal==idSucursal &&
		centros[i].IdCentro==(idCentro?idCentro:centros[i].IdCentro))
			data.push(centros[i]) 
	}
    return data;
};

Ext.define('centroautorizado.ComboBox',{
	extend:'Ext.form.field.ComboBox',
	alias : 'widget.centroautorizadocombo',
    displayField: 'Nombre',
	valueField: 'Id',
    store: Ext.create('Ext.data.Store',{autoDestroy: true,  model: 'CentroAutorizadoModel',data:[]}),
    queryMode: 'local',
    typeAhead: true,
    forceSelection:true,
    name:'IdCentro'
});


Ext.define('CodigoEI',{
	extend: 'Ext.data.Model',
	idProperty: 'Codigo',
	fields:[{name: 'Id',	type: 'int'},{name: 'Nombre',	type: 'string'},
			{name: 'Tipo',	type: 'string'},{name:'Activo',	type:'boolean'},
			{name: 'Codigo',	type: 'string'},{name: 'CodigoPresupuesto',	type: 'string'},
			{name: 'CreditosPermitidos',	type: 'auto'},{	name: 'DebitosPermitidos',	type: 'auto'}]
});

function createCodigosEgresoStore(){
    return Ext.create('Ext.data.Store', {
        autoDestroy: true,
        model: 'CodigoEI',
        data: Aicl.Util.getCodigosEgreso()
    });
}

function createCodigosIngresoStore(){
    return Ext.create('Ext.data.Store', {
        autoDestroy: true,
        model: 'CodigoEI',
        data: Aicl.Util.getCodigosIngreso()
    });
}

Ext.define('codigoegreso.ComboBox',{
	extend:'Ext.form.field.ComboBox',
	alias : 'widget.codigoegresocombo',
    displayField: 'Nombre',
	valueField: 'Codigo',
    store: createCodigosEgresoStore(),
    queryMode: 'local',
    typeAhead: true,
    forceSelection:true,
    name:'CodigoDocumento'
});


Ext.define('codigoingreso.ComboBox',{
	extend:'Ext.form.field.ComboBox',
	alias : 'widget.codigoingresocombo',
    displayField: 'Nombre',
	valueField: 'Codigo',
    store: createCodigosIngresoStore(),
    queryMode: 'local',
    typeAhead: true,
    forceSelection:true,
    name:'CodigoDocumento'
});


Ext.define('Rubro',{
	extend: 'Ext.data.Model',
	idProperty: 'Id',
	fields: [{name: 'Id',type: 'int'},{name: 'IdPresupuesto',type: 'int'},
		{name: 'Codigo',type: 'string'},{name: 'TipoItem', type: 'int'},
		{name: 'Nombre',type: 'string'},{name: 'Presupuestado',	type: 'number'},
		{name: 'Reservado', type: 'number'},{name: 'SaldoAnterior', type: 'number'},
		{name: 'Debitos', type: 'number'},{name: 'Creditos', type: 'number'},
		{name: 'UsaTercero',	type: 'boolean'},{name: 'Ejecutado', type: 'number'},
		{name: 'SaldoActual', type: 'number'},{name: 'IdSucursal',	type: 'int'},
		{name: 'IdCentro',	type: 'int'}]
});

// codigos : array CreditosPermitidos/Debitos permitidos que esta en CodigosEgreso/CodigosIngreso 
//function createRubrosStore(idSucursal, idCentro, codigos){
function getRubrosData(idSucursal, idCentro, codigos){
	codigos=codigos||[];
	var data=[];
	var rubros= Aicl.Util.getRubros();
	for(var i in rubros){
		if(rubros[i].IdSucursal==idSucursal && rubros[i].IdCentro==idCentro 
		&& codigos.indexOf(rubros[i].Codigo)>=0)
			data.push(rubros[i]) 
	}
	return data;
	/*
	return Ext.create('Ext.data.Store', {
        autoDestroy: true,
        model: 'Rubro',
        data: data
    });
    */
};

Ext.define('rubros.ComboBox',{
	extend:'Ext.form.field.ComboBox',
	alias : 'widget.rubrocombo',
    displayField: 'Nombre',
	valueField: 'Id',
    store: Ext.create('Ext.data.Store', {
        autoDestroy: true,
        model: 'Rubro',
        data: []
    }),
    queryMode: 'local',
    typeAhead: true,
    forceSelection:true,
    name:'IdPresupuestoItem'
});


// custom combobox 

/**
 * BoxSelect for ExtJS 4.1, a combo box improved for multiple value querying, selection and management.
 *
 * A friendlier combo box for multiple selections that creates easily individually
 * removable labels for each selection, as seen on facebook and other sites. Querying
 * and type-ahead support are also improved for multiple selections.
 *
 * Options and usage mostly remain consistent with the standard
 * [ComboBox](http://docs.sencha.com/ext-js/4-1/#!/api/Ext.form.field.ComboBox) control.
 * Some default configuration options have changed, but most should still work properly
 * if overridden unless otherwise noted.
 *
 * Please note, this component does not support versions of ExtJS earlier than 4.1.
 *
 * Inspired by the [SuperBoxSelect component for ExtJS 3](http://technomedia.co.uk/SuperBoxSelect/examples3.html),
 * which in turn was inspired by the [BoxSelect component for ExtJS 2](http://efattal.fr/en/extjs/extuxboxselect/).
 *
 * Various contributions and suggestions made by many members of the ExtJS community which can be seen
 * in the [official user extension forum post](http://www.sencha.com/forum/showthread.php?134751-Ext.ux.form.field.BoxSelect).
 *
 * Many thanks go out to all of those who have contributed, this extension would not be
 * possible without your help.
 *
 * See [AUTHORS.txt](../AUTHORS.TXT) for a list of major contributors
 *
 * @author kvee_iv http://www.sencha.com/forum/member.php?29437-kveeiv
 * @version 2.0.1
 * @requires BoxSelect.css
 * @xtype boxselect
 *
 */
Ext.define('Ext.ux.form.field.BoxSelect', {
    extend:'Ext.form.field.ComboBox',
    alias: ['widget.comboboxselect', 'widget.boxselect'],
    requires: ['Ext.selection.Model', 'Ext.data.Store'],

    //
    // Begin configuration options related to selected values
    //

    /**
     * @cfg {Boolean}
     * If set to `true`, allows the combo field to hold more than one value at a time, and allows selecting multiple
     * items from the dropdown list. The combo's text field will show all selected values using the template
     * defined by {@link #labelTpl}.
     *

     */
    multiSelect: true,

    /**
     * @cfg {String/Ext.XTemplate} labelTpl
     * The [XTemplate](http://docs.sencha.com/ext-js/4-1/#!/api/Ext.XTemplate) to use for the inner
     * markup of the labelled items. Defaults to the configured {@link #displayField}
     */

    /**
	 * @cfg
     * @inheritdoc
     *
     * When {@link #forceSelection} is `false`, new records can be created by the user as they
     * are typed. These records are **not** added to the combo's store. This creation
     * is triggered by typing the configured 'delimiter', and can be further configured using the
     * {@link #createNewOnEnter} and {@link #createNewOnBlur} configuration options.
     *
     * This functionality is primarily useful with BoxSelect components for things
     * such as an email address.
     */
    forceSelection: true,

    /**
	 * @cfg {Boolean}
     * Has no effect if {@link #forceSelection} is `true`.
     *
	 * With {@link #createNewOnEnter} set to `true`, the creation described in
     * {@link #forceSelection} will also be triggered by the 'enter' key.
	 */
    createNewOnEnter: false,

    /**
	 * @cfg {Boolean}
     * Has no effect if {@link #forceSelection} is `true`.
     *
     * With {@link #createNewOnBlur} set to `true`, the creation described in
     * {@link #forceSelection} will also be triggered when the field loses focus.
     *
     * Please note that this behavior is also affected by the configuration options
     * {@link #autoSelect} and {@link #selectOnTab}. If those are true and an existing
     * item would have been selected as a result, the partial text the user has entered will
	 * be discarded and the existing item will be added to the selection.
	 */
    createNewOnBlur: false,

    /**
     * @cfg {Boolean}
     * Has no effect if {@link #multiSelect} is `false`.
     *
     * Controls the formatting of the form submit value of the field as returned by {@link #getSubmitValue}
     *
     * - `true` for the field value to submit as a json encoded array in a single GET/POST variable
     * - `false` for the field to submit as an array of GET/POST variables
     */
    encodeSubmitValue: false,

    //
    // End of configuration options related to selected values
    //



    //
    // Configuration options related to pick list behavior
    //

    /**
     * @cfg {Boolean}
     * `true` to activate the trigger when clicking in empty space in the field. Note that the
     * subsequent behavior of this is controlled by the field's {@link #triggerAction}.
     * This behavior is similar to that of a basic ComboBox with {@link #editable} `false`.
     */
    triggerOnClick: true,

    /**
	 * @cfg {Boolean}
     * - `true` to have each selected value fill to the width of the form field
     * - `false to have each selected value size to its displayed contents
	 */
    stacked: false,

    /**
	 * @cfg {Boolean}
     * Has no effect if {@link #multiSelect} is `false`
     *
     * `true` to keep the pick list expanded after each selection from the pick list
     * `false` to automatically collapse the pick list after a selection is made
	 */
    pinList: true,

    /**
     * @cfg {Boolean}
     * True to hide the currently selected values from the drop down list. These items are hidden via
     * css to maintain simplicity in store and filter management.
     *
     * - `true` to hide currently selected values from the drop down pick list
     * - `false` to keep the item in the pick list as a selected item
     */
    filterPickList: false,

    //
    // End of configuration options related to pick list behavior
    //



    //
    // Configuration options related to text field behavior
    //

    /**
     * @cfg
     * @inheritdoc
     */
    selectOnFocus: true,

    /**
     * @cfg {Boolean}
     *
     * `true` if this field should automatically grow and shrink vertically to its content.
     * Note that this overrides the natural trigger grow functionality, which is used to size
     * the field horizontally.
     */
    grow: true,

    /**
     * @cfg {Number/Boolean}
     * Has no effect if {@link #grow} is `false`
     *
     * The minimum height to allow when {@link #grow} is `true`, or `false` to allow for
     * natural vertical growth based on the current selected values. See also {@link #growMax}.
     */
    growMin: false,

    /**
     * @cfg {Number/Boolean}
     * Has no effect if {@link #grow} is `false`
     *
     * The maximum height to allow when {@link #grow} is `true`, or `false to allow for
     * natural vertical growth based on the current selected values. See also {@link #growMin}.
     */
    growMax: false,

    /**
     * @cfg growAppend
     * @hide
     * Currently unsupported by BoxSelect since this is used for horizontal growth and
     * BoxSelect only supports vertical growth.
     */
    /**
     * @cfg growToLongestValue
     * @hide
     * Currently unsupported by BoxSelect since this is used for horizontal growth and
     * BoxSelect only supports vertical growth.
     */

    //
    // End of configuration options related to text field behavior
    //


    //
    // Event signatures
    //

    /**
     * @event autosize
     * Fires when the **{@link #autoSize}** function is triggered and the field is resized according to the
     * {@link #grow}/{@link #growMin}/{@link #growMax} configs as a result. This event provides a hook for the
     * developer to apply additional logic at runtime to resize the field if needed.
     * @param {Ext.ux.form.field.BoxSelect} this This BoxSelect field
     * @param {Number} height The new field height
     */

    //
    // End of event signatures
    //



    //
    // Configuration options that will break things if messed with
    //

    /**
     * @private
     */
    fieldSubTpl: [
        '<div id="{cmpId}-listWrapper" class="x-boxselect {fieldCls} {typeCls}">',
        '<ul id="{cmpId}-itemList" class="x-boxselect-list">',
        '<li id="{cmpId}-inputElCt" class="x-boxselect-input">',
        '<input id="{cmpId}-inputEl" type="{type}" ',
        '<tpl if="name">name="{name}" </tpl>',
        '<tpl if="value"> value="{[Ext.util.Format.htmlEncode(values.value)]}"</tpl>',
        '<tpl if="size">size="{size}" </tpl>',
        '<tpl if="tabIdx">tabIndex="{tabIdx}" </tpl>',
        'class="x-boxselect-input-field {inputElCls}" autocomplete="off">',
        '</li>',
        '</ul>',
        '</div>',
        {
            compiled: true,
            disableFormats: true
        }
    ],

    /**
     * @private
     */
    childEls: [ 'listWrapper', 'itemList', 'inputEl', 'inputElCt' ],

    /**
     * @private
     */
    componentLayout: 'boxselectfield',

    /**
     * @inheritdoc
     *
     * Initialize additional settings and enable simultaneous typeAhead and multiSelect support
     * @protected
	 */
    initComponent: function() {
        var me = this,
        typeAhead = me.typeAhead;

        if (typeAhead && !me.editable) {
            Ext.Error.raise('If typeAhead is enabled the combo must be editable: true -- please change one of those settings.');
        }

        Ext.apply(me, {
            typeAhead: false
        });

        me.callParent();

        me.typeAhead = typeAhead;

        me.selectionModel = new Ext.selection.Model({
            store: me.valueStore,
            mode: 'MULTI',
            onSelectChange: function(record, isSelected, suppressEvent, commitFn) {
                commitFn();
            }
        });

        if (!Ext.isEmpty(me.delimiter) && me.multiSelect) {
            me.delimiterRegexp = new RegExp(String(me.delimiter).replace(/[$%()*+.?\[\\\]{|}]/g, "\\$&"));
        }
    },

    /**
	 * Register events for management controls of labelled items
     * @protected
	 */
    initEvents: function() {
        var me = this;

        me.callParent(arguments);

        if (!me.enableKeyEvents) {
            me.mon(me.inputEl, 'keydown', me.onKeyDown, me);
        }
        me.mon(me.inputEl, 'paste', me.onPaste, me);
        me.mon(me.listWrapper, 'click', me.onItemListClick, me);
        me.mon(me.selectionModel, 'selectionchange', me.applyMultiselectItemMarkup, me);
    },

    /**
     * @inheritdoc
     *
	 * Create a store for the records of our current value based on the main store's model
     * @protected
	 */
    onBindStore: function(store, initial) {
        var me = this;

        if (store) {
            me.valueStore = new Ext.data.Store({
                model: store.model,
                proxy: {
                    type: 'memory'
                }
            });
            me.mon(me.valueStore, 'datachanged', me.applyMultiselectItemMarkup, me);
            if (me.selectionModel) {
                me.selectionModel.bindStore(me.valueStore);
            }
        }
    },

    /**
     * @inheritdoc
     *
     * Remove the selected value store and associated listeners
     * @protected
     */
    onUnbindStore: function(store) {
        var me = this,
        valueStore = me.valueStore;

        if (valueStore) {
            if (me.selectionModel) {
                me.selectionModel.deselectAll();
                me.selectionModel.bindStore(null);
            }
            me.mun(valueStore, 'datachanged', me.applyMultiselectItemMarkup, me);
            valueStore.destroy();
            me.valueStore = null;
        }

        me.callParent(arguments);
    },

    /**
     * @inheritdoc
     *
	 * Add refresh tracking to the picker for selection management
     * @protected
	 */
    createPicker: function() {
        var me = this,
        picker = me.callParent(arguments);

        me.mon(picker, {
            'beforerefresh': me.onBeforeListRefresh,
            scope: me
        });

        if (me.filterPickList) {
            picker.addCls('x-boxselect-hideselections');
        }

        return picker;
    },

    /**
     * @inheritdoc
     *
	 * Clean up selected values management controls
     * @protected
	 */
    onDestroy: function() {
        var me = this;

        Ext.destroyMembers(me, 'selectionModel', 'valueStore');

        me.callParent(arguments);
    },

    /**
     * Add empty text support to initial render.
     * @protected
     */
    getSubTplData: function() {
        var me = this,
            data = me.callParent(),
            isEmpty = me.emptyText && data.value.length < 1;

        if (isEmpty) {
            data.value = me.emptyText;
        } else {
            data.value = '';
        }
        data.inputElCls = data.fieldCls.match(me.emptyCls) ? me.emptyCls : '';

        return data;
    },

    /**
     * @inheritdoc
     *
	 * Overridden to avoid use of placeholder, as our main input field is often empty
     * @protected
	 */
    afterRender: function() {
        var me = this;

        if (Ext.supports.Placeholder && me.inputEl && me.emptyText) {
            delete me.inputEl.dom.placeholder;
        }

        me.bodyEl.applyStyles('vertical-align:top');

        if (me.grow) {
            if (Ext.isNumber(me.growMin) && (me.growMin > 0)) {
                me.listWrapper.applyStyles('min-height:'+me.growMin+'px');
            }
            if (Ext.isNumber(me.growMax) && (me.growMax > 0)) {
                me.listWrapper.applyStyles('max-height:'+me.growMax+'px');
            }
        }

        if (me.stacked === true) {
            me.itemList.addCls('x-boxselect-stacked');
        }

        if (!me.multiSelect) {
            me.itemList.addCls('x-boxselect-singleselect');
        }

        me.applyMultiselectItemMarkup();

        me.callParent(arguments);
    },

    /**
	 * Overridden to search entire unfiltered store since already selected values
     * can span across multiple store page loads and other filtering. Overlaps
     * some with {@link #isFilteredRecord}, but findRecord is used by the base component
     * for various logic so this logic is applied here as well.
     * @protected
	 */
    findRecord: function(field, value) {
        var ds = this.store,
        matches;

        if (!ds) {
            return false;
        }

        matches = ds.queryBy(function(rec, id) {
            return rec.isEqual(rec.get(field), value);
        });

        return (matches.getCount() > 0) ? matches.first() : false;
    },

    /**
	 * Overridden to map previously selected records to the "new" versions of the records
	 * based on value field, if they are part of the new store load
     * @protected
	 */
    onLoad: function() {
        var me = this,
        valueField = me.valueField,
        valueStore = me.valueStore,
        changed = false;

        if (valueStore) {
            if (!Ext.isEmpty(me.value) && (valueStore.getCount() == 0)) {
                me.setValue(me.value, false, true);
            }

            valueStore.suspendEvents();
            valueStore.each(function(rec) {
                var r = me.findRecord(valueField, rec.get(valueField)),
                i = r ? valueStore.indexOf(rec) : -1;
                if (i >= 0) {
                    valueStore.removeAt(i);
                    valueStore.insert(i, r);
                    changed = true;
                }
            });
            valueStore.resumeEvents();
            if (changed) {
                valueStore.fireEvent('datachanged', valueStore);
            }
        }

        me.callParent(arguments);
    },

    /**
	 * Used to determine if a record is filtered out of the current store's data set,
     * for determining if a currently selected value should be retained.
     *
     * Slightly complicated logic. A record is considered filtered and should be retained if:
     *
     * - It is not in the combo store and the store has no filter or it is in the filtered data set
     *   (Happens when our selected value is just part of a different load, page or query)
     * - It is not in the combo store and forceSelection is false and it is in the value store
     *   (Happens when our selected value was created manually)
     *
	 * @private
	 */
    isFilteredRecord: function(record) {
        var me = this,
        store = me.store,
        valueField = me.valueField,
        storeRecord,
        filtered = false;

        storeRecord = store.findExact(valueField, record.get(valueField));

        filtered = ((storeRecord === -1) && (!store.snapshot || (me.findRecord(valueField, record.get(valueField)) !== false)));

        filtered = filtered || (!filtered && (storeRecord === -1) && (me.forceSelection !== true) &&
            (me.valueStore.findExact(valueField, record.get(valueField)) >= 0));

        return filtered;
    },

    /**
     * @inheritdoc
     *
	 * Overridden to allow for continued querying with multiSelect selections already made
     * @protected
	 */
    doRawQuery: function() {
        var me = this,
        rawValue = me.inputEl.dom.value;

        if (me.multiSelect) {
            rawValue = rawValue.split(me.delimiter).pop();
        }

        this.doQuery(rawValue, false, true);
    },

    /**
	 * When the picker is refreshing, we should ignore selection changes. Otherwise
	 * the value of our field will be changing just because our view of the choices is.
     * @protected
	 */
    onBeforeListRefresh: function() {
        this.ignoreSelection++;
    },

    /**
	 * When the picker is refreshing, we should ignore selection changes. Otherwise
	 * the value of our field will be changing just because our view of the choices is.
     * @protected
	 */
    onListRefresh: function() {
        this.callParent(arguments);
        if (this.ignoreSelection > 0) {
            --this.ignoreSelection;
        }
    },

    /**
	 * Overridden to preserve current labelled items when list is filtered/paged/loaded
	 * and does not include our current value. See {@link #isFilteredRecord}
     * @private
	 */
    onListSelectionChange: function(list, selectedRecords) {
        var me = this,
        valueStore = me.valueStore,
        mergedRecords = [],
        i;

        // Only react to selection if it is not called from setValue, and if our list is
        // expanded (ignores changes to the selection model triggered elsewhere)
        if ((me.ignoreSelection <= 0) && me.isExpanded) {
            // Pull forward records that were already selected or are now filtered out of the store
            valueStore.each(function(rec) {
                if (Ext.Array.contains(selectedRecords, rec) || me.isFilteredRecord(rec)) {
                    mergedRecords.push(rec);
                }
            });
            mergedRecords = Ext.Array.merge(mergedRecords, selectedRecords);

            i = Ext.Array.intersect(mergedRecords, valueStore.getRange()).length;
            if ((i != mergedRecords.length) || (i != me.valueStore.getCount())) {
                me.setValue(mergedRecords, false);
                if (!me.multiSelect || !me.pinList) {
                    Ext.defer(me.collapse, 1, me);
                }
                if (valueStore.getCount() > 0) {
                    me.fireEvent('select', me, valueStore.getRange());
                }
            }
            me.inputEl.focus();
            if (!me.pinList) {
                me.inputEl.dom.value = '';
            }
            if (me.selectOnFocus) {
                me.inputEl.dom.select();
            }
        }
    },

    /**
     * Overridden to use valueStore instead of valueModels, for inclusion of
     * filtered records. See {@link #isFilteredRecord}
     * @private
     */
    syncSelection: function() {
        var me = this,
        picker = me.picker,
        valueField = me.valueField,
        pickStore, selection, selModel;

        if (picker) {
            pickStore = picker.store;

            // From the value, find the Models that are in the store's current data
            selection = [];
            if (me.valueStore) {
                me.valueStore.each(function(rec) {
                    var i = pickStore.findExact(valueField, rec.get(valueField));
                    if (i >= 0) {
                        selection.push(pickStore.getAt(i));
                    }
                });
            }

            // Update the selection to match
            me.ignoreSelection++;
            selModel = picker.getSelectionModel();
            selModel.deselectAll();
            if (selection.length > 0) {
                selModel.select(selection);
            }
            if (me.ignoreSelection > 0) {
                --me.ignoreSelection;
            }
        }
    },

    /**
	 * Overridden to align to itemList size instead of inputEl
     */
    doAlign: function(){
        var me = this,
            picker = me.picker,
            aboveSfx = '-above',
            isAbove;

        me.picker.alignTo(me.listWrapper, me.pickerAlign, me.pickerOffset);
        // add the {openCls}-above class if the picker was aligned above
        // the field due to hitting the bottom of the viewport
        isAbove = picker.el.getY() < me.inputEl.getY();
        me.bodyEl[isAbove ? 'addCls' : 'removeCls'](me.openCls + aboveSfx);
        picker[isAbove ? 'addCls' : 'removeCls'](picker.baseCls + aboveSfx);
    },

    /**
     * Overridden to preserve scroll position of pick list when list is realigned
     */
    alignPicker: function() {
        var me = this,
            picker = me.picker,
            pickerScrollPos = picker.getTargetEl().dom.scrollTop;

        me.callParent(arguments);

        if (me.isExpanded) {
            if (me.matchFieldWidth) {
                // Auto the height (it will be constrained by min and max width) unless there are no records to display.
                picker.setWidth(me.listWrapper.getWidth());
            }

            picker.getTargetEl().dom.scrollTop = pickerScrollPos;
        }
    },

    /**
	 * Get the current cursor position in the input field, for key-based navigation
	 * @private
	 */
    getCursorPosition: function() {
        var cursorPos;
        if (Ext.isIE) {
            cursorPos = document.selection.createRange();
            cursorPos.collapse(true);
            cursorPos.moveStart("character", -this.inputEl.dom.value.length);
            cursorPos = cursorPos.text.length;
        } else {
            cursorPos = this.inputEl.dom.selectionStart;
        }
        return cursorPos;
    },

    /**
	 * Check to see if the input field has selected text, for key-based navigation
	 * @private
	 */
    hasSelectedText: function() {
        var sel, range;
        if (Ext.isIE) {
            sel = document.selection;
            range = sel.createRange();
            return (range.parentElement() == this.inputEl.dom);
        } else {
            return this.inputEl.dom.selectionStart != this.inputEl.dom.selectionEnd;
        }
    },

    /**
	 * Handles keyDown processing of key-based selection of labelled items.
     * Supported keyboard controls:
     *
     * - If pick list is expanded
     *
     *     - `CTRL-A` will select all the items in the pick list
     *
     * - If the cursor is at the beginning of the input field and there are values present
     *
     *     - `CTRL-A` will highlight all the currently selected values
     *     - `BACKSPACE` and `DELETE` will remove any currently highlighted selected values
     *     - `RIGHT` and `LEFT` will move the current highlight in the appropriate direction
     *     - `SHIFT-RIGHT` and `SHIFT-LEFT` will add to the current highlight in the appropriate direction
     *
     * @protected
	 */
    onKeyDown: function(e, t) {
        var me = this,
        key = e.getKey(),
        rawValue = me.inputEl.dom.value,
        valueStore = me.valueStore,
        selModel = me.selectionModel,
        stopEvent = false,
        rec, i;

        if (me.readOnly || me.disabled || !me.editable) {
            return;
        }

        // Handle keyboard based navigation of selected labelled items
        if (me.isExpanded && (key == e.A && e.ctrlKey)) {
            me.select(me.getStore().getRange());
            selModel.deselectAll(true);
            me.collapse();
            me.inputEl.focus();
            stopEvent = true;
        } else if ((valueStore.getCount() > 0) &&
                ((rawValue == '') || ((me.getCursorPosition() === 0) && !me.hasSelectedText()))) {
            if ((key == e.BACKSPACE) || (key == e.DELETE)) {
                if (selModel.getCount() > 0) {
                    me.valueStore.remove(selModel.getSelection());
                } else {
                    me.valueStore.remove(me.valueStore.last());
                }
                me.setValue(me.valueStore.getRange());
                selModel.deselectAll();
                stopEvent = true;
            } else if ((key == e.RIGHT) || (key == e.LEFT)) {
                if ((selModel.getCount() === 0) && (key == e.LEFT)) {
                    selModel.select(valueStore.last());
                    stopEvent = true;
                } else if (selModel.getCount() > 0) {
                    rec = selModel.getLastFocused() || selModel.getLastSelected();
                    if (rec) {
                        i = valueStore.indexOf(rec);
                        if (key == e.RIGHT) {
                            if (i < (valueStore.getCount() - 1)) {
                                selModel.select(i + 1, e.shiftKey);
                                stopEvent = true;
                            } else if (!e.shiftKey) {
                                selModel.deselect(rec);
                                stopEvent = true;
                            }
                        } else if ((key == e.LEFT) && (i > 0)) {
                            selModel.select(i - 1, e.shiftKey);
                            stopEvent = true;
                        }
                    }
                }
            } else if (key == e.A && e.ctrlKey) {
                selModel.selectAll();
                stopEvent = e.A;
            }
            me.inputEl.focus();
        }

        if (stopEvent) {
            me.preventKeyUpEvent = stopEvent;
            e.stopEvent();
            return;
        }

        // Prevent key up processing for enter if it is being handled by the picker
        if (me.isExpanded && (key == e.ENTER) && me.picker.highlightedItem) {
            me.preventKeyUpEvent = true;
        }

        if (me.enableKeyEvents) {
            me.callParent(arguments);
        }

        if (!e.isSpecialKey() && !e.hasModifier()) {
            me.selectionModel.deselectAll();
            me.inputEl.focus();
        }
    },

    /**
	 * Handles auto-selection and creation of labelled items based on this field's
     * delimiter, as well as the keyUp processing of key-based selection of labelled items.
     * @protected
	 */
    onKeyUp: function(e, t) {
        var me = this,
        rawValue = me.inputEl.dom.value;

        if (me.preventKeyUpEvent) {
            e.stopEvent();
            if ((me.preventKeyUpEvent === true) || (e.getKey() === me.preventKeyUpEvent)) {
                delete me.preventKeyUpEvent;
            }
            return;
        }

        if (me.multiSelect && (me.delimiterRegexp && me.delimiterRegexp.test(rawValue)) ||
                ((me.createNewOnEnter === true) && e.getKey() == e.ENTER)) {
            rawValue = Ext.Array.clean(rawValue.split(me.delimiterRegexp));
            me.inputEl.dom.value = '';
            me.setValue(me.valueStore.getRange().concat(rawValue));
            me.inputEl.focus();
        }

        me.callParent([e,t]);
    },

    /**
     * Handles auto-selection of labelled items based on this field's delimiter when pasting
     * a list of values in to the field (e.g., for email addresses)
     * @protected
     */
    onPaste: function(e, t) {
        var me = this,
            rawValue = me.inputEl.dom.value,
            clipboard = (e && e.browserEvent && e.browserEvent.clipboardData) ? e.browserEvent.clipboardData : false;

        if (me.multiSelect && (me.delimiterRegexp && me.delimiterRegexp.test(rawValue))) {
            if (clipboard && clipboard.getData) {
                if (/text\/plain/.test(clipboard.types)) {
                    rawValue = clipboard.getData('text/plain');
                } else if (/text\/html/.test(clipboard.types)) {
                    rawValue = clipboard.getData('text/html');
                }
            }

            rawValue = Ext.Array.clean(rawValue.split(me.delimiterRegexp));
            me.inputEl.dom.value = '';
            me.setValue(me.valueStore.getRange().concat(rawValue));
            me.inputEl.focus();
        }
    },

    /**
     * Overridden to handle key navigation of pick list when list is filtered. Because we
     * want to avoid complexity that could be introduced by modifying the store's contents,
     * (e.g., always having to search back through and remove values when they might
     * be re-sent by the server, adding the values back in their previous position when
     * they are removed from the current selection, etc.), we handle this filtering
     * via a simple css rule. However, for the moment since those DOM nodes still exist
     * in the list we have to hijack the highlighting methods for the picker's BoundListKeyNav
     * to appropriately skip over these hidden nodes. This is a less than ideal solution,
     * but it centralizes all of the complexity of this problem in to this one method.
     * @protected
     */
    onExpand: function() {
        var me = this,
            keyNav = me.listKeyNav;

        me.callParent(arguments);

        if (keyNav || !me.filterPickList) {
            return;
        }
        keyNav = me.listKeyNav;
        keyNav.highlightAt = function(index) {
            var boundList = this.boundList,
                item = boundList.all.item(index),
                len = boundList.all.getCount(),
                direction;

            if (item && item.hasCls('x-boundlist-selected')) {
                if ((index == 0) || !boundList.highlightedItem || (boundList.indexOf(boundList.highlightedItem) < index)) {
                    direction = 1;
                } else {
                    direction = -1;
                }
                do {
                    index = index + direction;
                    item = boundList.all.item(index);
                } while ((index > 0) && (index < len) && item.hasCls('x-boundlist-selected'));

                if (item.hasCls('x-boundlist-selected')) {
                    return;
                }
            }

            if (item) {
                item = item.dom;
                boundList.highlightItem(item);
                boundList.getTargetEl().scrollChildIntoView(item, false);
            }
        };
    },

    /**
	 * Overridden to get and set the DOM value directly for type-ahead suggestion (bypassing get/setRawValue)
     * @protected
	 */
    onTypeAhead: function() {
        var me = this,
        displayField = me.displayField,
        inputElDom = me.inputEl.dom,
        valueStore = me.valueStore,
        boundList = me.getPicker(),
        record, newValue, len, selStart;

        if (me.filterPickList) {
            var fn = this.createFilterFn(displayField, inputElDom.value);
            record = me.store.findBy(function(rec) {
                return ((valueStore.indexOfId(rec.getId()) === -1) && fn(rec));
            });
            record = (record === -1) ? false : me.store.getAt(record);
        } else {
            record = me.store.findRecord(displayField, inputElDom.value);
        }

        if (record) {
            newValue = record.get(displayField);
            len = newValue.length;
            selStart = inputElDom.value.length;
            boundList.highlightItem(boundList.getNode(record));
            if (selStart !== 0 && selStart !== len) {
                inputElDom.value = newValue;
                me.selectText(selStart, newValue.length);
            }
        }
    },

    /**
	 * Delegation control for selecting and removing labelled items or triggering list collapse/expansion
     * @protected
	 */
    onItemListClick: function(evt, el, o) {
        var me = this,
        itemEl = evt.getTarget('.x-boxselect-item'),
        closeEl = itemEl ? evt.getTarget('.x-boxselect-item-close') : false;

        if (me.readOnly || me.disabled) {
            return;
        }

        evt.stopPropagation();

        if (itemEl) {
            if (closeEl) {
                me.removeByListItemNode(itemEl);
            } else {
                me.toggleSelectionByListItemNode(itemEl, evt.shiftKey);
            }
            me.inputEl.focus();
        } else if (me.triggerOnClick) {
            me.onTriggerClick();
        }
    },

    /**
	 * Build the markup for the labelled items. Template must be built on demand due to ComboBox initComponent
	 * lifecycle for the creation of on-demand stores (to account for automatic valueField/displayField setting)
     * @private
	 */
    getMultiSelectItemMarkup: function() {
        var me = this;

        if (!me.multiSelectItemTpl) {
            if (!me.labelTpl) {
                me.labelTpl = Ext.create('Ext.XTemplate',
                    '{[values.' + me.displayField + ']}'
                );
            } else if (Ext.isString(me.labelTpl) || Ext.isArray(me.labelTpl)) {
                me.labelTpl = Ext.create('Ext.XTemplate', me.labelTpl);
            }

            me.multiSelectItemTpl = [
            '<tpl for=".">',
            '<li class="x-boxselect-item ',
            '<tpl if="this.isSelected(values.'+ me.valueField + ')">',
            ' selected',
            '</tpl>',
            '" qtip="{[typeof values === "string" ? values : values.' + me.displayField + ']}">' ,
            '<div class="x-boxselect-item-text">{[typeof values === "string" ? values : this.getItemLabel(values)]}</div>',
            '<div class="x-tab-close-btn x-boxselect-item-close"></div>' ,
            '</li>' ,
            '</tpl>',
            {
                compile: true,
                disableFormats: true,
                isSelected: function(value) {
                    var i = me.valueStore.findExact(me.valueField, value);
                    if (i >= 0) {
                        return me.selectionModel.isSelected(me.valueStore.getAt(i));
                    }
                },
                getItemLabel: function(values) {
                    return me.getTpl('labelTpl').apply(values);
                }
            }
            ];
        }

        return this.getTpl('multiSelectItemTpl').apply(Ext.Array.pluck(this.valueStore.getRange(), 'data'));
    },

    /**
	 * Update the labelled items rendering
     * @private
	 */
    applyMultiselectItemMarkup: function() {
        var me = this,
        itemList = me.itemList,
        item;

        if (itemList) {
            while ((item = me.inputElCt.prev()) != null) {
                item.remove();
            }
            me.inputElCt.insertHtml('beforeBegin', me.getMultiSelectItemMarkup());
        }

        Ext.Function.defer(function() {
            if (me.picker && me.isExpanded) {
                me.alignPicker();
            }
            if (me.hasFocus) {
                me.inputElCt.scrollIntoView(me.listWrapper);
            }
        }, 15);
    },

    /**
	 * Returns the record from valueStore for the labelled item node
	 */
    getRecordByListItemNode: function(itemEl) {
        var me = this,
        itemIdx = 0,
        searchEl = me.itemList.dom.firstChild;

        while (searchEl && searchEl.nextSibling) {
            if (searchEl == itemEl) {
                break;
            }
            itemIdx++;
            searchEl = searchEl.nextSibling;
        }
        itemIdx = (searchEl == itemEl) ? itemIdx : false;

        if (itemIdx === false) {
            return false;
        }

        return me.valueStore.getAt(itemIdx);
    },

    /**
	 * Toggle of labelled item selection by node reference
	 */
    toggleSelectionByListItemNode: function(itemEl, keepExisting) {
        var me = this,
        rec = me.getRecordByListItemNode(itemEl);

        if (rec) {
            if (me.selectionModel.isSelected(rec)) {
                me.selectionModel.deselect(rec);
            } else {
                me.selectionModel.select(rec, keepExisting);
            }
        }
    },

    /**
	 * Removal of labelled item by node reference
	 */
    removeByListItemNode: function(itemEl) {
        var me = this,
        rec = me.getRecordByListItemNode(itemEl);

        if (rec) {
            me.valueStore.remove(rec);
            me.setValue(me.valueStore.getRange());
        }
    },

    /**
     * @inheritdoc
	 * Intercept calls to getRawValue to pretend there is no inputEl for rawValue handling,
	 * so that we can use inputEl for user input of just the current value.
	 */
    getRawValue: function() {
        var me = this,
        inputEl = me.inputEl,
        result;
        me.inputEl = false;
        result = me.callParent(arguments);
        me.inputEl = inputEl;
        return result;
    },

    /**
     * @inheritdoc
	 * Intercept calls to setRawValue to pretend there is no inputEl for rawValue handling,
	 * so that we can use inputEl for user input of just the current value.
	 */
    setRawValue: function(value) {
        var me = this,
        inputEl = me.inputEl,
        result;

        me.inputEl = false;
        result = me.callParent([value]);
        me.inputEl = inputEl;

        return result;
    },

    /**
	 * Adds a value or values to the current value of the field
	 * @param {Mixed} value The value or values to add to the current value, see {@link #setValue}
	 */
    addValue: function(value) {
        var me = this;
        if (value) {
            me.setValue(Ext.Array.merge(me.value, Ext.Array.from(value)));
        }
    },

    /**
	 * Removes a value or values from the current value of the field
	 * @param {Mixed} value The value or values to remove from the current value, see {@link #setValue}
	 */
    removeValue: function(value) {
        var me = this;

        if (value) {
            me.setValue(Ext.Array.difference(me.value, Ext.Array.from(value)));
        }
    },

    /**
     * Sets the specified value(s) into the field. The following value formats are recognised:
     *
     * - Single Values
     *
     *     - A string associated to this field's configured {@link #valueField}
     *     - A record containing at least this field's configured {@link #valueField} and {@link #displayField}
     *
     * - Multiple Values
     *
     *     - If {@link #multiSelect} is `true`, a string containing multiple strings as
     *       specified in the Single Values section above, concatenated in to one string
     *       with each entry separated by this field's configured {@link #delimiter}
     *     - An array of strings as specified in the Single Values section above
     *     - An array of records as specified in the Single Values section above
     *
     * In any of the string formats above, the following occurs if an associated record cannot be found:
     *
     * 1. If {@link #forceSelection} is `false`, a new record of the {@link #store}'s configured model type
     *    will be created using the given value as the {@link #displayField} and {@link #valueField}.
     *    This record will be added to the current value, but it will **not** be added to the store.
     * 2. If {@link #forceSelection} is `true` and {@link #queryMode} is `remote`, the list of unknown
     *    values will be submitted as a call to the {@link #store}'s load as a parameter named by
     *    the {@link #valueField} with values separated by the configured {@link #delimiter}.
     *    ** This process will cause setValue to asynchronously process. ** This will only be attempted
     *    once. Any unknown values that the server does not return records for will be removed.
     * 3. Otherwise, unknown values will be removed.
     *
     * @param {Mixed} value The value(s) to be set, see method documentation for details
     * @return {Ext.form.field.Field/Boolean} this, or `false` if asynchronously querying for unknown values
	 */
    setValue: function(value, doSelect, skipLoad) {
        var me = this,
        valueStore = me.valueStore,
        valueField = me.valueField,
        record, len, i, valueRecord, h,
        unknownValues = [];

        if (Ext.isEmpty(value)) {
            value = null;
        }
        if (Ext.isString(value) && me.multiSelect) {
            value = value.split(me.delimiter);
        }
        value = Ext.Array.from(value, true);

        for (i = 0, len = value.length; i < len; i++) {
            record = value[i];
            if (!record || !record.isModel) {
                valueRecord = valueStore.findExact(valueField, record);
                if (valueRecord >= 0) {
                    value[i] = valueStore.getAt(valueRecord);
                } else {
                    valueRecord = me.findRecord(valueField, record);
                    if (!valueRecord) {
                        if (me.forceSelection) {
                            unknownValues.push(record);
                        } else {
                            valueRecord = {};
                            valueRecord[me.valueField] = record;
                            valueRecord[me.displayField] = record;
                            valueRecord = new me.valueStore.model(valueRecord);
                        }
                    }
                    if (valueRecord) {
                        value[i] = valueRecord;
                    }
                }
            }
        }

        if ((skipLoad !== true) && (unknownValues.length > 0) && (me.queryMode === 'remote')) {
            var params = {};
            params[me.valueField] = unknownValues.join(me.delimiter);
            me.store.load({
                params: params,
                callback: function() {
                    if (me.itemList) {
                        me.itemList.unmask();
                    }
                    me.setValue(value, doSelect, true);
                    me.autoSize();
                }
            });
            return false;
        }

        // For single-select boxes, use the last good (formal record) value if possible
        if (!me.multiSelect && (value.length > 0)) {
            for (i = value.length - 1; i >= 0; i--) {
                if (value[i].isModel) {
                    value = value[i];
                    break;
                }
            }
            if (Ext.isArray(value)) {
                value = value[value.length - 1];
            }
        }

        return me.callParent([value, doSelect]);
    },

    /**
     * Returns the records for the field's current value
     * @return {Array} The records for the field's current value
     */
    getValueRecords: function() {
        return this.valueStore.getRange();
    },

    /**
     * @inheritdoc
     * Overridden to optionally allow for submitting the field as a json encoded array.
     */
    getSubmitData: function() {
        var me = this,
        val = me.callParent(arguments);

        if (me.multiSelect && me.encodeSubmitValue && val && val[me.name]) {
            val[me.name] = Ext.encode(val[me.name]);
        }

        return val;
    },

    /**
	 * Overridden to clear the input field if we are auto-setting a value as we blur.
     * @protected
	 */
    mimicBlur: function() {
        var me = this;

        if (me.selectOnTab && me.picker && me.picker.highlightedItem) {
            me.inputEl.dom.value = '';
        }

        me.callParent(arguments);
    },

    /**
	 * Overridden to handle partial-input selections more directly
	 */
    assertValue: function() {
        var me = this,
        rawValue = me.inputEl.dom.value,
        rec = !Ext.isEmpty(rawValue) ? me.findRecordByDisplay(rawValue) : false,
        value = false;

        if (!rec && !me.forceSelection && me.createNewOnBlur && !Ext.isEmpty(rawValue)) {
            value = rawValue;
        } else if (rec) {
            value = rec;
        }

        if (value) {
            me.addValue(value);
        }

        me.inputEl.dom.value = '';

        me.collapse();
    },

    /**
	 * Expand record values for evaluating change and fire change events for UI to respond to
	 */
    checkChange: function() {
        if (!this.suspendCheckChange && !this.isDestroyed) {
            var me = this,
            valueStore = me.valueStore,
            lastValue = me.lastValue,
            valueField = me.valueField,
            newValue = Ext.Array.map(Ext.Array.from(me.value), function(val) {
                if (val.isModel) {
                    return val.get(valueField);
                }
                return val;
            }, this).join(this.delimiter),
            isEqual = me.isEqual(newValue, lastValue);

            if (!isEqual || ((newValue.length > 0 && valueStore.getCount() < newValue.length))) {
                valueStore.suspendEvents();
                valueStore.removeAll();
                if (Ext.isArray(me.valueModels)) {
                    valueStore.add(me.valueModels);
                }
                valueStore.resumeEvents();
                valueStore.fireEvent('datachanged', valueStore);

                if (!isEqual) {
                    me.lastValue = newValue;
                    me.fireEvent('change', me, newValue, lastValue);
                    me.onChange(newValue, lastValue);
                }
            }
        }
    },

    /**
     * Overridden to be more accepting of varied value types
     */
    isEqual: function(v1, v2) {
        var fromArray = Ext.Array.from,
            valueField = this.valueField,
            i, len, t1, t2;

        v1 = fromArray(v1);
        v2 = fromArray(v2);
        len = v1.length;

        if (len !== v2.length) {
            return false;
        }

        for(i = 0; i < len; i++) {
            t1 = v1[i].isModel ? v1[i].get(valueField) : v1[i];
            t2 = v2[i].isModel ? v2[i].get(valueField) : v2[i];
            if (t1 !== t2) {
                return false;
            }
        }

        return true;
    },

    /**
	 * Overridden to use value (selection) instead of raw value and to avoid the use of placeholder
	 */
    applyEmptyText : function() {
        var me = this,
        emptyText = me.emptyText,
        inputEl, isEmpty;

        if (me.rendered && emptyText) {
            isEmpty = Ext.isEmpty(me.value) && !me.hasFocus;
            inputEl = me.inputEl;
            if (isEmpty) {
                inputEl.dom.value = emptyText;
                inputEl.addCls(me.emptyCls);
                me.listWrapper.addCls(me.emptyCls);
            } else {
                if (inputEl.dom.value === emptyText) {
                    inputEl.dom.value = '';
                }
                me.listWrapper.removeCls(me.emptyCls);
                inputEl.removeCls(me.emptyCls);
            }
            me.autoSize();
        }
    },

    /**
	 * Overridden to use inputEl instead of raw value and to avoid the use of placeholder
	 */
    preFocus : function(){
        var me = this,
        inputEl = me.inputEl,
        emptyText = me.emptyText,
        isEmpty;

        if (emptyText && inputEl.dom.value === emptyText) {
            inputEl.dom.value = '';
            isEmpty = true;
            inputEl.removeCls(me.emptyCls);
            me.listWrapper.removeCls(me.emptyCls);
        }
        if (me.selectOnFocus || isEmpty) {
            inputEl.dom.select();
        }
    },

    /**
	 * Intercept calls to onFocus to add focusCls, because the base field
     * classes assume this should be applied to inputEl
	 */
    onFocus: function() {
        var me = this,
        focusCls = me.focusCls,
        itemList = me.itemList;

        if (focusCls && itemList) {
            itemList.addCls(focusCls);
        }

        me.callParent(arguments);
    },

    /**
	 * Intercept calls to onBlur to remove focusCls, because the base field
     * classes assume this should be applied to inputEl
	 */
    onBlur: function() {
        var me = this,
        focusCls = me.focusCls,
        itemList = me.itemList;

        if (focusCls && itemList) {
            itemList.removeCls(focusCls);
        }

        me.callParent(arguments);
    },

    /**
	 * Intercept calls to renderActiveError to add invalidCls, because the base
     * field classes assume this should be applied to inputEl
	 */
    renderActiveError: function() {
        var me = this,
        invalidCls = me.invalidCls,
        itemList = me.itemList,
        hasError = me.hasActiveError();

        if (invalidCls && itemList) {
            itemList[hasError ? 'addCls' : 'removeCls'](me.invalidCls + '-field');
        }

        me.callParent(arguments);
    },

    /**
     * Initiate auto-sizing for height based on {@link #grow}, if applicable.
     */
    autoSize: function() {
        var me = this,
        height;

        if (me.grow && me.rendered) {
            me.autoSizing = true;
            me.updateLayout();
        }

        return me;
    },

    /**
     * Track height change to fire {@link #event-autosize} event, when applicable.
     */
    afterComponentLayout: function() {
        var me = this,
            width;

        if (me.autoSizing) {
            height = me.getHeight();
            if (height !== me.lastInputHeight) {
                if (me.isExpanded) {
                    me.alignPicker();
                }
                me.fireEvent('autosize', me, height);
                me.lastInputHeight = height;
                delete me.autoSizing;
            }
        }
    }
});

/**
 * Ensures the input element takes up the maximum amount of remaining list width,
 * or the entirety of the list width if too little space remains. In this case,
 * the list height will be automatically increased to accomodate the new line. This
 * growth will not occur if {@link Ext.ux.form.field.BoxSelect#multiSelect} or
 * {@link Ext.ux.form.field.BoxSelect#grow} is false.
 */
Ext.define('Ext.ux.layout.component.field.BoxSelectField', {

    /* Begin Definitions */

    alias: ['layout.boxselectfield'],

    extend: 'Ext.layout.component.field.Trigger',

    /* End Definitions */

    type: 'boxselectfield',

    beginLayout: function(ownerContext) {
        var me = this,
            owner = me.owner;

        me.callParent(arguments);

        ownerContext.inputElCt = ownerContext.getEl('inputElCt');
        ownerContext.itemList = ownerContext.getEl('itemList');

        me.skipInputGrowth = !owner.grow || !owner.multiSelect;
    },

    beginLayoutFixed: function(ownerContext, width, suffix) {
        var me = this,
            owner = ownerContext.target;

        owner.triggerEl.setStyle('height', '24px');

        me.callParent(arguments);

        if (ownerContext.heightModel.fixed && ownerContext.lastBox) {
            owner.listWrapper.setStyle('height', ownerContext.lastBox.height+'px');
            owner.itemList.setStyle('height', '100%');
        }

        var listBox = owner.itemList.getBox(true, true),
        listWidth = listBox.width,
        lastEntry = owner.inputElCt.dom.previousSibling,
        inputWidth = listWidth - 10;

        if (lastEntry) {
            inputWidth = inputWidth - (lastEntry.offsetLeft + Ext.fly(lastEntry).getWidth() + Ext.fly(lastEntry).getPadding('lr'));
        }

        if (!me.skipInputGrowth && (inputWidth < 35)) {
            inputWidth = listWidth - 10;
        } else if (inputWidth < 1) {
            inputWidth = 1;
        }

        owner.inputElCt.setStyle('width', inputWidth + 'px');
    }

});

// fin custom combobox
