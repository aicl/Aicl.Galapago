Ext.define('App.view.egresoitem.List',{ 
    extend: 'Ext.grid.Panel',
    alias : 'widget.egresoitemlist', 
    constructor: function(config){
    	config= config|| {};
    	config.store= config.store|| 'EgresoItem',
        config.frame = config.frame==undefined? false:config.frame;
		config.selType = config.selType || 'rowmodel';
    	config.height = config.height||350;
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
		text: 'IdEgreso',
		dataIndex: 'IdEgreso',
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
		text: 'TipoPartida',
		dataIndex: 'TipoPartida',
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
		text: 'IdCentro',
		dataIndex: 'IdCentro',
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
		text: 'CodigoItem',
		dataIndex: 'CodigoItem',
		sortable: true
	},
	{
		text: 'NombreItem',
		dataIndex: 'NombreItem',
		sortable: true
	},
	{
		text: 'NombreCentro',
		dataIndex: 'NombreCentro',
		sortable: true
	},
	{
		text: 'NombreTercero',
		dataIndex: 'NombreTercero',
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
	}
];
 
        this.dockedItems=[{
            xtype: 'toolbar',
            items: [{
                //text:'Nuevo',
                tooltip:'Agregar nuevo item',
                iconCls:'add',
                disabled:true,
                action: 'new'
            },'-',{
                //text:'Borrar',
                tooltip:'Borrar item seleccionado',
                iconCls:'remove',
                disabled:true,
                action: 'delete'
            }]		
        }]
                
        this.callParent(arguments);
    }
});
