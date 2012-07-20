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
            xtype: 'numberfield', name: 'textBuscarAnio', width: 60, allowBlank: false, value: (new Date()).getFullYear()
        },{
          	xtype: 'numberfield', name: 'textBuscarMes', width: 40, value: (new Date()).getMonth() +1 
        },{
            xtype:'textfield',  emptyText:'Nombre del Tercero',  name: 'textBuscarTercero'
        },{
         	xtype:'estadoasentado', name:'comboBuscarEstadoAsentado'
        },{
           tooltip:'Buscar por los criterios indicados..', iconCls:'find',  action: 'buscarEgresos'
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
            labelWidth: 120,
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
		xtype: 'datefield',
		name: 'Fecha',
		fieldLabel: 'Fecha',
		allowBlank: false,
		format: 'd.m.Y'
	},
	{
		name: 'Periodo',
		fieldLabel: 'Periodo',
		allowBlank: false,
		maxLength: 6,
		enforceMaxLength: true
	},
	{
		xtype: 'numberfield',
		allowDecimals: false,
		name: 'Numero',
		fieldLabel: 'Numero',
		allowBlank: false
	},
	{
		name: 'Descripcion',
		fieldLabel: 'Descripcion',
		allowBlank: false,
		maxLength: 50,
		enforceMaxLength: true
	},
	{
		xtype: 'numberfield',
		name: 'Valor',
		fieldLabel: 'Valor',
		allowBlank: false
	},
	{
		xtype: 'numberfield',
		allowDecimals: false,
		name: 'IdCuentaGiradora',
		fieldLabel: 'IdCuentaGiradora',
		allowBlank: false
	},
	{
		xtype: 'numberfield',
		allowDecimals: false,
		name: 'IdTercero',
		fieldLabel: 'IdTercero',
		allowBlank: false
	},
	{
		xtype: 'numberfield',
		allowDecimals: false,
		name: 'IdSucursal',
		fieldLabel: 'IdSucursal',
		allowBlank: false
	},
	{
		xtype: 'datefield',
		name: 'FechaAsentado',
		fieldLabel: 'FechaAsentado',
		format: 'd.m.Y'
	},
	{
		xtype: 'datefield',
		name: 'FechaAnulado',
		fieldLabel: 'FechaAnulado',
		format: 'd.m.Y'
	},
	{
		xtype: 'checkboxfield',
		name: 'Externo',
		fieldLabel: 'Externo'
	},
	{
		xtype: 'numberfield',
		allowDecimals: false,
		name: 'IdTerceroReceptor',
		fieldLabel: 'IdTerceroReceptor',
		allowBlank: false
	},
	{
		xtype: 'numberfield',
		allowDecimals: false,
		name: 'IdTerceroGiradora',
		fieldLabel: 'IdTerceroGiradora'
	},
	{
		name: 'NombreSucursal',
		fieldLabel: 'NombreSucursal'
	},
	{
		name: 'DocumentoTercero',
		fieldLabel: 'DocumentoTercero'
	},
	{
		name: 'DVTercero',
		fieldLabel: 'DVTercero'
	},
	{
		name: 'NombreTercero',
		fieldLabel: 'NombreTercero'
	},
	{
		name: 'NombreDocumentoTercero',
		fieldLabel: 'NombreDocumentoTercero'
	},
	{
		name: 'DocumentoReceptor',
		fieldLabel: 'DocumentoReceptor'
	},
	{
		name: 'DVReceptor',
		fieldLabel: 'DVReceptor'
	},
	{
		name: 'NombreReceptor',
		fieldLabel: 'NombreReceptor'
	},
	{
		name: 'NombreDocumentoReceptor',
		fieldLabel: 'NombreDocumentoReceptor'
	},
	{
		name: 'CodigoItem',
		fieldLabel: 'CodigoItem'
	},
	{
		name: 'NombreItem',
		fieldLabel: 'NombreItem'
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
            labelWidth: 120,
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
		name: 'IdComprobanteEgreso',
		fieldLabel: 'IdComprobanteEgreso',
		allowBlank: false
	},
	{
		xtype: 'numberfield',
		allowDecimals: false,
		name: 'IdEgreso',
		fieldLabel: 'IdEgreso',
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
            labelWidth: 120,
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