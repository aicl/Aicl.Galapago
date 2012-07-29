Ext.define('App.view.comprobanteingreso.Panel',{ 
    extend: 'Ext.panel.Panel',
    alias : 'widget.comprobanteingresopanel',
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
    	xtype:'comprobanteingresolist'
    },{
    	xtype:'comprobanteingresoform'
    //},{
    	//xtype:'comprobanteingresotabpanel', colspan:2
    },{
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
   		xtype:'comprobanteingresoitemlist'
   	},{	
   		xtype:'comprobanteingresoitemform'
   	}]
});
/*
Ext.define('App.view.comprobanteingreso.TabPanel',{
	extend: 'Ext.tab.Panel',
    alias : 'widget.comprobanteingresotabpanel',
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
   			xtype:'comprobanteingresoitemlist'
   		},{	
   			xtype:'comprobanteingresoitemform'
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
   			xtype:'comprobanteingresoretencionlist'
   		},{	
   			xtype:'comprobanteingresoretencionform'
   		}]    		
   	}]
});
*/
Ext.define('App.view.comprobanteingreso.List',{ 
    extend: 'Ext.grid.Panel',
    alias : 'widget.comprobanteingresolist', 
    constructor: function(config){
    	config= config|| {};
    	config.store= config.store|| 'ComprobanteIngreso',
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
            displayMsg: 'Ingresos del {0} al {1} de {2}',
            emptyMsg: "No hay Comprobantes para Mostrar"
        });
        
        config.margin=config.margin|| '2 2 2 2';	
    	if (arguments.length==0 )
    		this.callParent([config]);
    	else
    		this.callParent(arguments); 
    },
    
    initComponent: function() {
        
        this.columns=[{
		text: 'Periodo',
		dataIndex: 'Periodo',
		width: 60
	},{
		text: 'Numero',
		dataIndex: 'Numero',
		align: 'center',
		width: 60,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive" style="text-align:center">'+Aicl.Util.formatInt(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative" style="text-align:center">'+Aicl.Util.formatInt(value)+'</div>';
        	}
        }
	},{
		text: 'Descripcion',
		dataIndex: 'Descripcion',
		width: 140
	},
	{
		text: 'Valor',
		dataIndex: 'Valor',
		align: 'center',
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatNumber(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatNumber(value)+'</div>';
        	}
        }
	},{
		text: 'Asentado',
		dataIndex: 'FechaAsentado',
		width: 80,
		renderer: Ext.util.Format.dateRenderer('d.m.Y')
	},{
		text: 'Anulado',
		dataIndex: 'FechaAnulado',
		width: 80,
		renderer: Ext.util.Format.dateRenderer('d.m.Y')
	},{
		text: 'Externo',
		dataIndex: 'Externo',
		xtype: 'booleancolumn',
		trueText: 'Si',
		falseText: 'No',
		align: 'center'
	},{
		text: 'DocumentoTercero',
		dataIndex: 'DocumentoTercero'
	},{
		text: 'DVTercero',
		dataIndex: 'DVTercero'
	},{
		text: 'NombreTercero',
		dataIndex: 'NombreTercero'
	},{
		text: 'NombreDocumentoTercero',
		dataIndex: 'NombreDocumentoTercero'
	}];
               
        this.callParent(arguments);
    }
});

Ext.define('App.view.comprobanteingreso.Form', {
    extend: 'Ext.form.Panel',
    alias : 'widget.comprobanteingresoform',
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
        this.items = [{
		xtype: 'hidden',
		name: 'Id'
	},{
		xtype: 'sucursalautorizadacombo',fieldLabel: 'Sucursal'
	},{
		xtype: 'datefield',
		name: 'Fecha',
		fieldLabel: 'Fecha',
		allowBlank: false,
		format: 'd.m.Y',
		value : new Date()
	},{
		name: 'Descripcion',
		fieldLabel: 'Descripcion',
		allowBlank: false,
		maxLength: 50,
		enforceMaxLength: true
	},{
		xtype: 'remotesaldotercerocombo',fieldLabel: 'Tercero' 
	},{
		xtype: 'rubrocombo',fieldLabel: 'Cuenta', name:'IdCuentaReceptora'
	}];
         this.callParent(arguments);
    }
});


Ext.define('App.view.comprobanteingresoitem.List',{ 
    extend: 'Ext.grid.Panel',
    alias : 'widget.comprobanteingresoitemlist', 
    constructor: function(config){
    	config= config|| {};
    	config.store= config.store|| 'ComprobanteIngresoItem',
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
        
        this.columns=[{
    	text:'Descripcion',
    	dataIndex:'Descripcion',
    	width: 120
    },{
    	text:'Documento',
    	dataIndex:'Documento',
    	width: 100
    },{
		text: 'Abono',
		dataIndex: 'Abono',
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatNumber(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatNumber(value)+'</div>';
        	}
        }
	},{
		text: 'Numero',
		dataIndex: 'Numero',
		align:'center',
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive" style="text-align:center">'+Aicl.Util.formatInt(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative" style="text-align:center">'+Aicl.Util.formatInt(value)+'</div>';
        	}
        }
	},{
		text: 'Valor',
		dataIndex: 'Valor',
		align:'center',
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatNumber(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatNumber(value)+'</div>';
        	}
        }
	},{
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
	},{
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
	},{
		text: 'Fecha',
		dataIndex: 'Fecha',
		sortable: true,
		renderer: Ext.util.Format.dateRenderer('d.m.Y')
	}];

        this.callParent(arguments);
    }
});


Ext.define('App.view.comprobanteingresoitem.Form', {
    extend: 'Ext.form.Panel',
    alias : 'widget.comprobanteingresoitemform',
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
        this.items = [{
		xtype: 'hidden',
		name: 'Id'
	},{
		xtype: 'hidden',
		name: 'IdComprobanteIngreso'
	},{
		xtype: 'ingresocombo',
		fieldLabel: 'Documento'
	},{
		xtype: 'numberfield',
		name: 'Abono',
		fieldLabel: 'Abono',
		allowBlank: false
	}];

        this.callParent(arguments);
    }
});


Ext.define('App.view.comprobanteingresoretencion.List',{ 
    extend: 'Ext.grid.Panel',
    alias : 'widget.comprobanteingresoretencionlist', 
    constructor: function(config){
    	config= config|| {};
    	config.store= config.store|| 'ComprobanteIngresoRetencion',
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
		text: 'IdComprobanteIngresoItem',
		dataIndex: 'IdComprobanteIngresoItem',
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
		text: 'IdComprobanteIngreso',
		dataIndex: 'IdComprobanteIngreso',
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

Ext.define('App.view.comprobanteingresoretencion.Form', {
    extend: 'Ext.form.Panel',
    alias : 'widget.comprobanteingresoretencionform',
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
		name: 'IdComprobanteIngresoItem',
		fieldLabel: 'IdComprobanteIngresoItem',
		allowBlank: false
	},
	{
		xtype: 'numberfield',
		allowDecimals: false,
		name: 'IdComprobanteIngreso',
		fieldLabel: 'IdComprobanteIngreso',
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
    labelTpl: '{Nombre} ({Documento}-{DigitoVerificacion})',
    listConfig: {
    	loadingText: 'buscando...',
        emptyText: 'sin informacion.',
       	getInnerTpl: function(){	
        	return   '<ul><li role="option" class="search-item" >' +
                        '<h3><span>{Documento}-{DigitoVerificacion}</span>{Nombre}</h3>' +
                    '</li></ul>'	
    	}
    }
});


Ext.define('ingreso.ComboBox', {
	extend:'Ext.ux.form.field.BoxSelect',
	alias : 'widget.ingresocombo',
    displayField: 'Documento',
	valueField: 'Id',
	name:'IdIngreso',
	store:'Ingreso',
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