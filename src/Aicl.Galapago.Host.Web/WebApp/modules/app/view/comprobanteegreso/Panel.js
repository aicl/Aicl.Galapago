Ext.define('App.view.comprobanteegreso.Panel',{ 
    extend: 'Ext.panel.Panel',
    alias : 'widget.comprobanteegresopanel',
    frame: true,
    anchor:'100%',
    layout: {
        type: 'table',
        columns: 2
    },
    items:[{
    	xtype: 'toolbar',
        colspan:2,
        items: [{
            tooltip:'Crear Comprobante',
            iconCls:'new_document',
            disabled:true,
            action: 'new'
        },{
          	tooltip:'Guardar',
      		iconCls:'save_document',
        	disabled:true,
        	action:'save'
        },'-',{
            tooltip:'Asentar el documento seleccionado',
            iconCls:'asentar',
            disabled:true,
            action: 'asentar'
        },{
            tooltip:'Anular el documento seleccionado',
            iconCls:'remove',
            disabled:true,
            action: 'delete'
        },'-', {
            xtype: 'numberfield', name: 'buscarAnioText', width: 60, allowBlank: false, value: (new Date()).getFullYear()
        },{
          	xtype: 'numberfield', name: 'buscarMesText', width: 40, value: (new Date()).getMonth() +1 
        },{
            xtype:'textfield',  emptyText:'Nombre del Tercero',  name: 'buscarTerceroText'
        },{
         	xtype:'estadoasentadocombo', name:'buscarEstadoAsentadoCombo'
        },{
           tooltip:'Buscar por los criterios indicados..', iconCls:'find',  action: 'buscarComprobantes'
        }]		
    },{
    	xtype:'comprobanteegresolist'
    },{
    	xtype:'comprobanteegresoform'
    },{
    	xtype:'comprobanteegresotabpanel', colspan:2
    }]
});

Ext.define('App.view.comprobanteegreso.TabPanel',{
	extend: 'Ext.tab.Panel',
    alias : 'widget.comprobanteegresotabpanel',
    frame: true,
    anchor:'100%',
    items:[{
    	title:'Cuentas',
    	xtype:'panel',
    	layout:{type: 'table',columns: 2},
    	items:[{
    		xtype: 'toolbar',
    		 name:'itemToolbar',
           	colspan:2,
           	items:[{
               	tooltip:'Agregar Cuenta',
               	iconCls:'new_document',
               	disabled:true,
               	action: 'new_item'
           	},{
           		tooltip:'Guardar',
      			iconCls:'save_document',
        		disabled:true,
        		action:'save_item'
           	},'-',{
               	tooltip:'Borrar cuenta Seleccionada',
               	iconCls:'remove',
               	disabled:true,
               	action: 'delete_item'
           	}]		
       	},{	
   			xtype:'comprobanteegresoitemlist'
   		},{	
   			xtype:'comprobanteegresoitemform'
   		}]
   	},{
    	title:'Retenciones',
    	xtype:'panel',
    	layout: {type: 'table', columns:2},
    	items:[{
    		xtype: 'toolbar',
           	colspan:2,
           	items:[{
               	tooltip:'Agregar Retencion',
               	iconCls:'new_document',
               	disabled:true,
               	action: 'new_retencion'
           	},{
           		tooltip:'Guardar',
      			iconCls:'save_document',
        		disabled:true,
        		action:'save_item'
           	},'-',{
               	tooltip:'Borrar Retencion Seleccionada',
               	iconCls:'remove',
               	disabled:true,
               	action: 'delete_retencion'
           	}]		
       	},{	
   			xtype:'comprobanteegresoretencionlist'
   		},{	
   			xtype:'comprobanteegresoretencionform'
   		}]    		
   	}]
});

Ext.define('App.view.comprobanteegreso.List',{ 
    extend: 'Ext.grid.Panel',
    alias : 'widget.comprobanteegresolist', 
    constructor: function(config){
    	config= config|| {};
    	config.store= config.store|| 'ComprobanteEgreso',
        config.frame = config.frame==undefined? false:config.frame;
		config.selType = config.selType || 'rowmodel';
    	config.height = config.height||340;
    	config.width = config.width || 600;
    	config.viewConfig = config.viewConfig || {
        	stripeRows: true
	    };
	    
	    config.bbar= Ext.create('Ext.PagingToolbar', {
            store: config.store,
            displayInfo: true,
            displayMsg: 'Egresos del {0} al {1} de {2}',
            emptyMsg: "No hay Egresos para Mostrar"
        });
        
        config.margin=config.margin|| '2 2 2 2';	
    	if (arguments.length==0 )
    		this.callParent([config]);
    	else
    		this.callParent(arguments); 
    },
    
    initComponent: function() {
        
        this.columns=[
	{
		text: 'Fecha',
		dataIndex: 'Fecha',
		flex: 1,
		sortable: true,
		renderer: Ext.util.Format.dateRenderer('d.m.Y')
	},
	{
		text: 'Periodo',
		dataIndex: 'Periodo',
		sortable: true
	},
	{
		text: 'Numero',
		dataIndex: 'Numero',
		sortable: true,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatInt(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatInt(value)+'</div>';
        	}
        }
	},
	{
		text: 'Descripcion',
		dataIndex: 'Descripcion',
		sortable: true
	},
	{
		text: 'Valor',
		dataIndex: 'Valor',
		sortable: true,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatNumber(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatNumber(value)+'</div>';
        	}
        }
	},
	{
		text: 'IdCuentaGiradora',
		dataIndex: 'IdCuentaGiradora',
		sortable: true,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatInt(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatInt(value)+'</div>';
        	}
        }
	},
	{
		text: 'IdTercero',
		dataIndex: 'IdTercero',
		sortable: true,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatInt(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatInt(value)+'</div>';
        	}
        }
	},
	{
		text: 'FechaAsentado',
		dataIndex: 'FechaAsentado',
		sortable: true,
		renderer: Ext.util.Format.dateRenderer('d.m.Y')
	},
	{
		text: 'FechaAnulado',
		dataIndex: 'FechaAnulado',
		sortable: true,
		renderer: Ext.util.Format.dateRenderer('d.m.Y')
	},
	{
		text: 'Externo',
		dataIndex: 'Externo',
		sortable: true,
		xtype: 'booleancolumn',
		trueText: 'Si',
		falseText: 'No',
		align: 'center'
	},
	{
		text: 'IdTerceroReceptor',
		dataIndex: 'IdTerceroReceptor',
		sortable: true,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatInt(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatInt(value)+'</div>';
        	}
        }
	},
	{
		text: 'IdTerceroGiradora',
		dataIndex: 'IdTerceroGiradora',
		sortable: true,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatInt(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatInt(value)+'</div>';
        	}
        }
	},
	{
		text: 'NombreSucursal',
		dataIndex: 'NombreSucursal',
		sortable: true
	},
	{
		text: 'DocumentoTercero',
		dataIndex: 'DocumentoTercero',
		sortable: true
	},
	{
		text: 'DVTercero',
		dataIndex: 'DVTercero',
		sortable: true
	},
	{
		text: 'NombreTercero',
		dataIndex: 'NombreTercero',
		sortable: true
	},
	{
		text: 'NombreDocumentoTercero',
		dataIndex: 'NombreDocumentoTercero',
		sortable: true
	},
	{
		text: 'DocumentoReceptor',
		dataIndex: 'DocumentoReceptor',
		sortable: true
	},
	{
		text: 'DVReceptor',
		dataIndex: 'DVReceptor',
		sortable: true
	},
	{
		text: 'NombreReceptor',
		dataIndex: 'NombreReceptor',
		sortable: true
	},
	{
		text: 'NombreDocumentoReceptor',
		dataIndex: 'NombreDocumentoReceptor',
		sortable: true
	},
	{
		text: 'CodigoItem',
		dataIndex: 'CodigoItem',
		sortable: true
	},
	{
		text: 'NombreItem',
		dataIndex: 'NombreItem',
		sortable: true
	}
];
               
        this.callParent(arguments);
    }
});

Ext.define('App.view.comprobanteegreso.Form', {
    extend: 'Ext.form.Panel',
    alias : 'widget.comprobanteegresoform',
    ui:'default-framed',
    constructor: function(config){
    	config=config|| {};
    	config.frame=config.frame==undefined?false: config.frame;
    	config.margin=config.margin|| '0 0 0 5px';
    	config.bodyStyle = config.bodyStyle ||'padding:0px 0px 0px 0px';
    	config.height = config.height|| 340;
    	config.width = config.width|| 365;
        config.autoScroll= config.autoScroll==undefined? true: config.autoScroll,
		config.fieldDefaults = config.fieldDefaults || {
            msgTarget: 'side',
            labelWidth: 80,
			labelAlign: 'right'
        };
        config.defaultType = config.defaultType|| 'textfield';
        config.defaults = config.defaults || {
            anchor: '100%',
			labelStyle: 'padding-left:4px;'
        };
    	if (arguments.length==0 )
    		this.callParent([config]);
    	else
    		this.callParent(arguments);
    },
     
    initComponent: function() {
        this.items = [        
	{
		xtype: 'hidden',
		name: 'Id'
	},
	{
		xtype: 'sucursalautorizadacombo',fieldLabel: 'Sucursal'
	},
	{
		xtype: 'datefield',
		name: 'Fecha',
		fieldLabel: 'Fecha',
		allowBlank: false,
		format: 'd.m.Y'
	},
	{
		name: 'Descripcion',
		fieldLabel: 'Descripcion',
		allowBlank: false,
		maxLength: 50,
		enforceMaxLength: true
	},
	{
		xtype: 'remotesaldotercerocombo',fieldLabel: 'Tercero' 
	},
	{
		xtype: 'rubrocombo',fieldLabel: 'Cuenta', name:'IdCuentaGiradora'
	},
	{
		xtype:'remotereceptorcombo', fieldLabel: 'Pagar a'
	}
	
];
         this.callParent(arguments);
    }
});


Ext.define('App.view.comprobanteegresoitem.List',{ 
    extend: 'Ext.grid.Panel',
    alias : 'widget.comprobanteegresoitemlist', 
    constructor: function(config){
    	config= config|| {};
    	config.store= config.store|| 'ComprobanteEgresoItem',
        config.frame = config.frame==undefined? false:config.frame;
		config.selType = config.selType || 'rowmodel';
    	config.height = config.height||235;
    	config.width = config.width || 600;
    	config.viewConfig = config.viewConfig || {
        	stripeRows: true
	    };
        config.margin=config.margin|| '2 2 2 2';	
    	if (arguments.length==0 )
    		this.callParent([config]);
    	else
    		this.callParent(arguments); 
    },
    
    initComponent: function() {
        
        this.columns=[
    {
    	text:'Documento',
    	dataIndex:'Documento'
    },
	{
		text: 'IdComprobanteEgreso',
		dataIndex: 'IdComprobanteEgreso',
		flex: 1,
		sortable: true,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatInt(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatInt(value)+'</div>';
        	}
        }
	},
	{
		text: 'IdEgreso',
		dataIndex: 'IdEgreso',
		sortable: true,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatInt(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatInt(value)+'</div>';
        	}
        }
	},
	{
		text: 'Abono',
		dataIndex: 'Abono',
		sortable: true,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatNumber(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatNumber(value)+'</div>';
        	}
        }
	},
	{
		text: 'Numero',
		dataIndex: 'Numero',
		sortable: true,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatInt(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatInt(value)+'</div>';
        	}
        }
	},
	{
		text: 'Valor',
		dataIndex: 'Valor',
		sortable: true,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatNumber(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatNumber(value)+'</div>';
        	}
        }
	},
	{
		text: 'Saldo',
		dataIndex: 'Saldo',
		sortable: true,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatNumber(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatNumber(value)+'</div>';
        	}
        }
	},
	{
		text: 'DiasCredito',
		dataIndex: 'DiasCredito',
		sortable: true,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatInt(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatInt(value)+'</div>';
        	}
        }
	},
	{
		text: 'IdSucursal',
		dataIndex: 'IdSucursal',
		sortable: true,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatInt(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatInt(value)+'</div>';
        	}
        }
	},
	{
		text: 'IdTercero',
		dataIndex: 'IdTercero',
		sortable: true,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatInt(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatInt(value)+'</div>';
        	}
        }
	},
	{
		text: 'Fecha',
		dataIndex: 'Fecha',
		sortable: true,
		renderer: Ext.util.Format.dateRenderer('d.m.Y')
	}
];

        this.callParent(arguments);
    }
});


Ext.define('App.view.comprobanteegresoitem.Form', {
    extend: 'Ext.form.Panel',
    alias : 'widget.comprobanteegresoitemform',
    ui:'default-framed',
    constructor: function(config){
    	config=config|| {};
    	config.frame=config.frame==undefined?false: config.frame;
    	config.margin=config.margin|| '0 0 0 5px';
    	config.bodyStyle = config.bodyStyle ||'padding:0px 0px 0px 0px';
    	config.height = config.height|| 235;
    	config.width = config.width|| 365;
        config.autoScroll= config.autoScroll==undefined? true: config.autoScroll,
		config.fieldDefaults = config.fieldDefaults || {
            msgTarget: 'side',
            labelWidth: 80,
			labelAlign: 'right'
        };
        config.defaultType = config.defaultType|| 'textfield';
        config.defaults = config.defaults || {
            anchor: '100%',
			labelStyle: 'padding-left:4px;'
        };
    	if (arguments.length==0 )
    		this.callParent([config]);
    	else
    		this.callParent(arguments);
    },
     
    initComponent: function() {
        this.items = [
	{
		xtype: 'hidden',
		name: 'Id'
	},
	{
		xtype: 'hidden',
		name: 'IdComprobanteEgreso'
	},
	{
		xtype: 'egresocombo',
		fieldLabel: 'Documento'
	},
	{
		xtype: 'numberfield',
		name: 'Abono',
		fieldLabel: 'Abono',
		allowBlank: false
	}
];

        this.callParent(arguments);
    }
});


Ext.define('App.view.comprobanteegresoretencion.List',{ 
    extend: 'Ext.grid.Panel',
    alias : 'widget.comprobanteegresoretencionlist', 
    constructor: function(config){
    	config= config|| {};
    	config.store= config.store|| 'ComprobanteEgresoRetencion',
        config.frame = config.frame==undefined? false:config.frame;
		config.selType = config.selType || 'rowmodel';
    	config.height = config.height||235;
    	config.width = config.width || 600;
    	config.viewConfig = config.viewConfig || {
        	stripeRows: true
	    };
        config.margin=config.margin|| '2 2 2 2';	
    	if (arguments.length==0 )
    		this.callParent([config]);
    	else
    		this.callParent(arguments); 
    },
    
    initComponent: function() {
        
        this.columns=[
	{
		text: 'IdComprobanteEgresoItem',
		dataIndex: 'IdComprobanteEgresoItem',
		flex: 1,
		sortable: true,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatInt(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatInt(value)+'</div>';
        	}
        }
	},
	{
		text: 'IdComprobanteEgreso',
		dataIndex: 'IdComprobanteEgreso',
		sortable: true,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatInt(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatInt(value)+'</div>';
        	}
        }
	},
	{
		text: 'IdPresupuestoItem',
		dataIndex: 'IdPresupuestoItem',
		sortable: true,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatInt(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatInt(value)+'</div>';
        	}
        }
	},
	{
		text: 'Valor',
		dataIndex: 'Valor',
		sortable: true,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatNumber(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatNumber(value)+'</div>';
        	}
        }
	}
];
        this.callParent(arguments); 
    }
});

Ext.define('App.view.comprobanteegresoretencion.Form', {
    extend: 'Ext.form.Panel',
    alias : 'widget.comprobanteegresoretencionform',
    ui:'default-framed',
    constructor: function(config){
    	config=config|| {};
    	config.frame=config.frame==undefined?false: config.frame;
    	config.margin=config.margin|| '0 0 0 5px';
    	config.bodyStyle = config.bodyStyle ||'padding:0px 0px 0px 0px';
    	config.height = config.height|| 235;
    	config.width = config.width|| 365;
        config.autoScroll= config.autoScroll==undefined? true: config.autoScroll,
		config.fieldDefaults = config.fieldDefaults || {
            msgTarget: 'side',
            labelWidth: 80,
			labelAlign: 'right'
        };
        config.defaultType = config.defaultType|| 'textfield';
        config.defaults = config.defaults || {
            anchor: '100%',
			labelStyle: 'padding-left:4px;'
        };
    	if (arguments.length==0 )
    		this.callParent([config]);
    	else
    		this.callParent(arguments);
    },
     
    initComponent: function() {
        this.items = [
	{
		xtype: 'hidden',
		name: 'Id'
	},
	{
		xtype: 'numberfield',
		allowDecimals: false,
		name: 'IdComprobanteEgresoItem',
		fieldLabel: 'IdComprobanteEgresoItem',
		allowBlank: false
	},
	{
		xtype: 'numberfield',
		allowDecimals: false,
		name: 'IdComprobanteEgreso',
		fieldLabel: 'IdComprobanteEgreso',
		allowBlank: false
	},
	{
		xtype: 'numberfield',
		allowDecimals: false,
		name: 'IdPresupuestoItem',
		fieldLabel: 'IdPresupuestoItem',
		allowBlank: false
	},
	{
		xtype: 'numberfield',
		name: 'Valor',
		fieldLabel: 'Valor',
		allowBlank: false
	}
];
        this.callParent(arguments);
    }
});

//combos

Ext.define('remotesaldotercero.ComboBox', {
	extend:'Ext.ux.form.field.BoxSelect',
	alias : 'widget.remotesaldotercerocombo',
    displayField: 'Nombre',
	valueField: 'IdTercero',
	name:'IdTercero',
    store: 'RemoteSaldoTercero',
    forceSelection:true,
    pageSize: 12,
    multiSelect:false,
    queryMode: 'remote',
    queryParam :'Nombre',
    triggerOnClick: false,
    labelTpl: '{Nombre} ({Documento})',
    listConfig: {
    	loadingText: 'buscando...',
        emptyText: 'sin informacion.',
       /* tpl: Ext.create('Ext.XTemplate',
            '<ul><tpl for=".">',
                '<li role="option" class="' + Ext.baseCSSPrefix + 'boundlist-item' + '">{Nombre}: {Documento}</li>',
            '</tpl></ul>'
        )
     */
    	getInnerTpl: function() {	
        	return   '<ul><li role="option" class="search-item" >' +
                        '<h3><span>{Documento}-{DigitoVerificacion}</span>{Nombre}</h3>' +
                    '</li></ul>'	
    	}
    }
});


Ext.define('remotereceptor.ComboBox', {
	extend:'Ext.ux.form.field.BoxSelect',
	alias : 'widget.remotereceptorcombo',
    displayField: 'Nombre',
	valueField: 'Id',
	name:'IdTerceroReceptor',
	store:'RemoteTercero',
    forceSelection:true,
    pageSize: 12,
    multiSelect:false,
    queryMode: 'remote',
    queryParam :'Nombre',
    triggerOnClick: false,
    labelTpl: '{Nombre} ({Documento})',
    listConfig: {
    	loadingText: 'buscando...',
        emptyText: 'sin informacion.',
        getInnerTpl: function() {	
        	return   '<ul><li role="option" class="search-item" >' +
                        '<h3><span>{Documento}-{DigitoVerificacion}</span>{Nombre}</h3>' +
                    '</li></ul>'	
        }
        /*tpl: Ext.create('Ext.XTemplate',
            '<ul><tpl for=".">',
            //'<li role="option" class="' + Ext.baseCSSPrefix + 'boundlist-item' + '">{Nombre}: {Documento}</li>',
           	'<li role="option" class="search-item" >' +
            	'<h3><span>{Documento}<br /></span>{DigitoVerificacion}</h3>' +
                '{Nombre}' +
             '</li>',
            '</tpl></ul>'
        )*/   
    } 
    	
});

Ext.define('egreso.ComboBox', {
	extend:'Ext.ux.form.field.BoxSelect',
	alias : 'widget.egresocombo',
    displayField: 'Documento',
	valueField: 'Id',
	name:'IdEgreso',
	store:'Egreso',
    forceSelection:true,
    pageSize: 12,
    multiSelect:false,
    queryMode: 'local',
    queryParam :'Documento',
    triggerOnClick: false,
    labelTpl: 'Nro: {Documento} - Saldo: {Saldo}',
    listConfig: {
    	loadingText: 'buscando...',
        emptyText: 'sin informacion.',
        getInnerTpl: function() {	
        	return   '<ul><li role="option" class="search-item" >' +
                        '<h3><span>{Valor}-{Saldo}</span>{Documento}</h3>' +
                    '</li></ul>'	
        }   
    } 
});