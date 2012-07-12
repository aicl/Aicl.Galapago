Ext.define('App.view.egreso.List',{ 
    extend: 'Ext.grid.Panel',
    alias : 'widget.egresolist', 
    constructor: function(config){
    	config= config|| {};
    	config.store= config.store|| 'Egreso',
        config.frame = config.frame==undefined? false:config.frame;
		config.selType = config.selType || 'rowmodel';
    	config.height = config.height||350;
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
	},
	{
		text: 'Periodo',
		dataIndex: 'Periodo',
		width    : 50
	},
	{
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
	},
	{
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
	},
	{
		text: 'Asentado',
		dataIndex: 'FechaAsentado',
		width    : 80,
		renderer: Ext.util.Format.dateRenderer('d.m.Y')
	},
	{
		text: 'Anulado',
		dataIndex: 'FechaAnulado',
		width    : 80,
		renderer: Ext.util.Format.dateRenderer('d.m.Y')
	},
	{
		text: 'Externo',
		dataIndex: 'Externo',
		width    : 50,
		xtype: 'booleancolumn',
		trueText: 'Si',
		falseText: 'No',
		align: 'center'
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
		text: 'NombreDocumentoReceptor',
		dataIndex: 'NombreDocumentoReceptor',
		sortable: true
	}
];
 
        this.dockedItems=[{
            xtype: 'toolbar',
            items: [{
                //text:'Nuevo',
                tooltip:'Agregar nuevo Documento',
                iconCls:'add',
                disabled:true,
                action: 'new'
            },{
                tooltip:'Asentar el documento seleccionado',
                iconCls:'asentar',
                disabled:true,
                action: 'asentar'
            },{
                //text:'Anular',
                tooltip:'Anular el documento seleccionado',
                iconCls:'remove',
                disabled:true,
                action: 'delete'
            },'-','-', {
            	xtype: 'numberfield', name: 'textBuscarAnio', width: 60, allowBlank: false, value: (new Date()).getFullYear()
            },{
            	xtype: 'numberfield', name: 'textBuscarMes', width: 40, value: (new Date()).getMonth() +1 
            },{
                xtype:'textfield',  emptyText:'Nombre del Tercero',  name: 'textBuscarTercero'  },
              {
              	xtype:'estadoasentado', name:'comboBuscarEstadoAsentado'
            },{
                tooltip:'Buscar por los criterios indicados..', iconCls:'find',  action: 'buscarEgresos'
            }]		
        }]
                
        this.callParent(arguments);
    }
});
