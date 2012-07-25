Ext.define('App.view.egreso.Panel',{ 
    extend: 'Ext.panel.Panel',
    alias : 'widget.egresopanel',
    frame: true,
    anchor:'100%',
    layout: {
        type: 'table',
        columns: 2
    },
    items:[{
    	xtype: 'toolbar',
    	name: 'egresoToolbar',
        colspan:2,
        items: [{
            tooltip:'Crear Egreso',
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
           tooltip:'Buscar por los criterios indicados..', iconCls:'find',  action: 'buscarEgresos'
        }]		
    },{
    	xtype:'egresolist'
    },{
    	xtype:'egresoform'
    },{
    	xtype: 'toolbar',
    	name: 'egresoItemToolbar',
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
   		xtype:'egresoitemlist'
   	},{	
   		xtype:'egresoitemform'
   	}]
});


Ext.define('App.view.egreso.List',{ 
    extend: 'Ext.grid.Panel',
    alias : 'widget.egresolist', 
    constructor: function(config){
    	config= config|| {};
    	config.store= config.store|| 'Egreso',
        config.frame = config.frame==undefined? false:config.frame;
		config.selType = config.selType || 'rowmodel';
    	config.height = config.height||340;
    	config.width = config.width || 600;
    	config.viewConfig = config.viewConfig || {stripeRows: true };
	    
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
		text: 'Pagar a',
		dataIndex: 'NombreReceptor',
		renderer: function(value, metadata, record, store){
			return record.get('NombreReceptor') || record.get('NombreTercero') +
			' || ' + record.get('DocumentoReceptor') || record.get('DocumentoTercero')+
			'-'+record.get('DVReceptor')|| record.get('DVTercero');
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

Ext.define('App.view.egreso.Form', {
    extend: 'Ext.form.Panel',
    alias : 'widget.egresoform',
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
		xtype:'codigoegresocombo',fieldLabel: 'Tipo Documento'
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
		name: 'Documento',
		fieldLabel: 'Documento',
		allowBlank: false,
		maxLength: 12,
		enforceMaxLength: true
	},{
		xtype:'numberfield',allowDecimals: false,name: 'DiasCredito',fieldLabel: 'Dias Credito',allowBlank:false
	},{
		xtype:'remotereceptorcombo', fieldLabel: 'Pagar a'
	}];
    
        this.callParent(arguments);
    }
});


Ext.define('App.view.egresoitem.List',{ 
    extend: 'Ext.grid.Panel',
    alias : 'widget.egresoitemlist', 
    constructor: function(config){
    	config= config|| {};
    	config.store= config.store|| 'EgresoItem',
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
		dataIndex: 'NombreItem'
	},{
		text: 'TipoPartida',
		dataIndex: 'TipoPartida',
		align: 'center',
		renderer: function(value, metadata, record, store){
           	if(value==1){
            	return '<div class="x-cell-positive" style="text-align:center">D</div>';
        	}else{
            	return '<div class="x-cell-negative" style="text-align:center">C</div>';
        	}
        }
	},{
		text: 'Valor',
		dataIndex: 'Valor',
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatNumber(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatNumber(value)+'</div>';
        	}
        }
	},{
		text: 'Centro',
		dataIndex: 'NombreCentro'
	},{
		text: 'A favor de',
		dataIndex: 'NombreTercero',
		renderer: function(value, metadata, record, store){
			var tercero=record.get('NombreTercero');
			return (!tercero)?'': tercero + ' || ' + record.get('DocumentoTercero')+'-'+record.get('DVTercero');
		}
	}]
        
        this.callParent(arguments);
    }
});


Ext.define('App.view.egresoitem.Form', {
    extend: 'Ext.form.Panel',
    alias : 'widget.egresoitemform',
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
		xtype: 'hidden',name: 'IdEgreso'
	},{
		xtype: 'hidden',name: 'TipoPartida'
	},
	{
		xtype:'tipoegresoitemcombo',fieldLabel: 'Tipo', name:'TipoEgresoItem', submitValue:false
	},{
		xtype: 'centroautorizadocombo',fieldLabel: 'Centro'
	},
	{
		xtype: 'rubrocombo',fieldLabel: 'Rubro'
	},{
		xtype: 'remoteterceroitemcombo',fieldLabel: 'Tercero'
	},
	{
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
    //typeAhead: true,
    forceSelection:true,
    pageSize: 12,
    multiSelect:false,
    queryMode: 'remote',
    queryParam :'Nombre',
    // Value delimiter examples
    delimiter: '|',
	//value: 'NC|VA|ZZ',
    // Click behavior
    triggerOnClick: false,
    // Display template modifications
    labelTpl: '{Nombre} ({Documento})',
    listConfig: {
        tpl: Ext.create('Ext.XTemplate',
            '<ul><tpl for=".">',
                '<li role="option" class="' + Ext.baseCSSPrefix + 'boundlist-item' + '">{Nombre}: {Documento}</li>',
            '</tpl></ul>'
    )}
});

Ext.define('remotereceptor.ComboBox', {
	extend:'Ext.ux.form.field.BoxSelect',
	alias : 'widget.remotereceptorcombo',
    displayField: 'Nombre',
	valueField: 'Id',
	name:'IdTerceroReceptor',
	store:'RemoteReceptor',
    forceSelection:true,
    pageSize: 12,
    multiSelect:false,
    queryMode: 'remote',
    queryParam :'Nombre',
    triggerOnClick: false,
    labelTpl: '{Nombre} ({Documento})',
    listConfig: {
        tpl: Ext.create('Ext.XTemplate',
            '<ul><tpl for=".">',
                '<li role="option" class="' + Ext.baseCSSPrefix + 'boundlist-item' + '">{Nombre}: {Documento}</li>',
            '</tpl></ul>'
    )} 
});


// combos del item

Ext.define('TipoEgresoItemModel', {
    extend: 'Ext.data.Model',
    idProperty: 'Id',
    fields: [ {type: 'int', name: 'Id'}, {type: 'string', name: 'Concepto'}]
});


function createTipoEgresoItemStore() {
    return Ext.create('Ext.data.Store', {
        autoDestroy: true,
        model: 'TipoEgresoItemModel',
        data: [{Id:1,Concepto:'Egreso'},{Id:2,Concepto:'Descuento'},{Id:3,Concepto:'Pago'}]
    });
}

Ext.define('tipoegresoitem.ComboBox', {
	extend:'Ext.form.field.ComboBox',
	alias : 'widget.tipoegresoitemcombo',
    displayField: 'Concepto',
	valueField: 'Id',
    store: createTipoEgresoItemStore(),
    queryMode: 'local',
    typeAhead: true,
    forceSelection:true,
    listeners   : {  
     	beforerender: function(combo){
       		combo.setValue(1);  
       	}
    }
});

Ext.define('remoteterceroitem.ComboBox', {
	extend:'Ext.ux.form.field.BoxSelect',
	alias : 'widget.remoteterceroitemcombo',
    displayField: 'Nombre',
	valueField: 'Id',
	name:'IdTercero',
	store:'RemoteTerceroItem',
    forceSelection:true,
    pageSize: 12,
    multiSelect:false,
    queryMode: 'remote',
    queryParam :'Nombre',
    triggerOnClick: false,
    labelTpl: '{Nombre} ({Documento})',
    listConfig: {
        tpl: Ext.create('Ext.XTemplate',
            '<ul><tpl for=".">',
                '<li role="option" class="' + Ext.baseCSSPrefix + 'boundlist-item' + '">{Nombre}: {Documento}</li>',
            '</tpl></ul>'
    )} 
});

