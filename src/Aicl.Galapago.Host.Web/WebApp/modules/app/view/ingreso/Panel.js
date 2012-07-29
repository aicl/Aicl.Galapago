Ext.define('App.view.ingreso.Panel',{ 
    extend: 'Ext.panel.Panel',
    alias : 'widget.ingresopanel',
    frame: true,
    anchor:'100%',
    layout: {
        type: 'table',
        columns: 2
    },
    items:[{
    	xtype: 'toolbar',
    	name: 'ingresoToolbar',
        colspan:2,
        items: [{
            tooltip:'Crear Ingreso',
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
           tooltip:'Buscar por los criterios indicados..', iconCls:'find',  action: 'buscarIngresos'
        }]		
    },{
    	xtype:'ingresolist'
    },{
    	xtype:'ingresoform'
    },{
    	xtype: 'toolbar',
    	name: 'ingresoItemToolbar',
        colspan:2,
        items:[{
          	tooltip:'Crear item',
           	iconCls:'new_document',
           	disabled:true,
           	action: 'new_item'
        },{
           	tooltip:'Guardar',
      		iconCls:'save_document',
        	disabled:true,
        	action:'save_item'
        },'-',{
           	tooltip:'Borrar item Seleccionado',
           	iconCls:'remove',
            disabled:true,
            action: 'delete_item'
        }]		
    },{	
   		xtype:'ingresoitemlist'
   	},{	
   		xtype:'ingresoitemform'
   	}]
});


Ext.define('App.view.ingreso.List',{ 
    extend: 'Ext.grid.Panel',
    alias : 'widget.ingresolist', 
    constructor: function(config){
    	config= config|| {};
    	config.store= config.store|| 'Ingreso',
        config.frame = config.frame==undefined? false:config.frame;
		config.selType = config.selType || 'rowmodel';
    	config.height = config.height||340;
    	config.width = config.width || 600;
    	config.viewConfig = config.viewConfig || {stripeRows: true };
	    
	    config.bbar= Ext.create('Ext.PagingToolbar', {
            store: config.store,
            displayInfo: true,
            displayMsg: 'Ingresos del {0} al {1} de {2}',
            emptyMsg: "No hay Ingresos para Mostrar"
        });
        
        config.margin=config.margin|| '2 2 2 2';	
    	if (arguments.length==0 )
    		this.callParent([config]);
    	else
    		this.callParent(arguments); 
    },
    
    initComponent: function() {
    	
    	this.columns=[{
		text: 'Numero',
		dataIndex: 'Numero',
		width    : 50,
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatInt(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatInt(value)+'</div>';
        	}
        }
	},{
		text: 'Periodo',
		dataIndex: 'Periodo',
		width    : 50
	},{
		text: 'Descripcion',
		dataIndex: 'Descripcion',
		width    : 150
	},{
		text: 'Valor',
		dataIndex: 'Valor',
		width    : 80,
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
		width    : 80,
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
		width    : 80,
		renderer: Ext.util.Format.dateRenderer('d.m.Y')
	},{
		text: 'Anulado',
		dataIndex: 'FechaAnulado',
		width    : 80,
		renderer: Ext.util.Format.dateRenderer('d.m.Y')
	},{
		text: 'Tercero',
		dataIndex: 'NombreTercero',
		renderer: function(value, metadata, record, store){
			return record.get('NombreTercero')+ ' || ' + record.get('DocumentoTercero')+'-'+record.get('DVTercero');
		}
	},{
		text: 'Externo',
		dataIndex: 'Externo',
		width    : 50,
		xtype: 'booleancolumn',
		trueText: 'Si',
		falseText: 'No',
		align: 'center'
	}];
               
        this.callParent(arguments);
    }
});

Ext.define('App.view.ingreso.Form', {
    extend: 'Ext.form.Panel',
    alias : 'widget.ingresoform',
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
		xtype:'codigoingresocombo',fieldLabel: 'Tipo Documento'
	},{
		xtype: 'datefield',
		name: 'Fecha',
		fieldLabel: 'Fecha',
		allowBlank: false,
		format: 'd.m.Y',
		value:new Date()
	},{
		name: 'Descripcion',
		fieldLabel: 'Descripcion',
		maxLength: 50,
		enforceMaxLength: true
	},{
		xtype: 'remotetercerocombo',fieldLabel: 'Tercero'
	},{
		xtype:'numberfield',allowDecimals: false,name: 'DiasCredito',fieldLabel: 'Dias Credito',allowBlank:false
	},{
		name:'Documento',
		fieldLabel:'Documento',
		readOnly:true
	
	}];
    
        this.callParent(arguments);
    }
});


Ext.define('App.view.ingresoitem.List',{ 
    extend: 'Ext.grid.Panel',
    alias : 'widget.ingresoitemlist', 
    constructor: function(config){
    	config= config|| {};
    	config.store= config.store|| 'IngresoItem',
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
		text: 'Codigo',
		dataIndex: 'CodigoItem'
	},{
		text: 'Rubro',
		dataIndex: 'NombreItem',
		flex:1
	},{
		text: 'Valor',
		dataIndex: 'Valor',
		renderer: function(value, metadata, record, store){
           	if(record.get('TipoPartida')==2){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatNumber(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatNumber(value)+'</div>';
        	}
        }
	},{
		text: 'Centro',
		dataIndex: 'NombreCentro'
	}]
        
        this.callParent(arguments);
    }
});


Ext.define('App.view.ingresoitem.Form', {
    extend: 'Ext.form.Panel',
    alias : 'widget.ingresoitemform',
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
		xtype: 'hidden',name: 'Id'
	},{
		xtype: 'hidden',name: 'IdIngreso'
	},{
		xtype: 'hidden',name: 'TipoPartida'
	},
	{
		xtype:'tipoingresoitemcombo',fieldLabel: 'Tipo', name:'TipoIngresoItem', submitValue:false
	},{
		xtype: 'centroautorizadocombo',fieldLabel: 'Centro'
	},
	{
		xtype: 'rubrocombo',fieldLabel: 'Rubro'
	},{
		xtype: 'numberfield',	name: 'Valor',	fieldLabel: 'Valor', allowBlank: false
	}];

        this.callParent(arguments);
    }
});

// combos terceros

Ext.define('remotetercero.ComboBox', {
	extend:'Ext.ux.form.field.BoxSelect',
	alias : 'widget.remotetercerocombo',
    displayField: 'Nombre',
	valueField: 'Id',
	name:'IdTercero',
    store: 'RemoteTercero',
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
        	return  '<ul><li role="option" class="search-item" >' +
                    '<h3><span>{Documento}-{DigitoVerificacion}</span>{Nombre}</h3>' +
                    '</li></ul>'	
        }   
    }
});


// combos del item

Ext.define('TipoIngresoItemModel', {
    extend: 'Ext.data.Model',
    idProperty: 'Id',
    fields: [ {type: 'int', name: 'Id'}, {type: 'string', name: 'Concepto'}]
});

function createTipoIngresoItemStore() {
    return Ext.create('Ext.data.Store', {
        autoDestroy: true,
        model: 'TipoIngresoItemModel',
        data: [{Id:1,Concepto:'Ingreso'},{Id:2,Concepto:'Descuento'},{Id:3,Concepto:'Pago'}]
    });
}

Ext.define('tipoingresoitem.ComboBox', {
	extend:'Ext.form.field.ComboBox',
	alias : 'widget.tipoingresoitemcombo',
    displayField: 'Concepto',
	valueField: 'Id',
    store: createTipoIngresoItemStore(),
    queryMode: 'local',
    typeAhead: true,
    forceSelection:true,
    listeners   : {  
     	beforerender: function(combo){
       		combo.setValue(1);  
       	}
    }
});