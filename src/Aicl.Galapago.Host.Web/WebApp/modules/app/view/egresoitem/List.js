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
		text: 'Codigo',
		dataIndex: 'CodigoItem'
	},
	{
		text: 'Rubro',
		dataIndex: 'NombreItem'
	},
	{
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
	},
	{
		text: 'Valor',
		dataIndex: 'Valor',
		renderer: function(value, metadata, record, store){
           	if(value>=0){
            	return '<div class="x-cell-positive">'+Aicl.Util.formatNumber(value)+'</div>';
        	}else{
            	return '<div class="x-cell-negative">'+Aicl.Util.formatNumber(value)+'</div>';
        	}
        }
	},
	{
		text: 'Centro',
		dataIndex: 'NombreCentro'
	},
	{
		text: 'A favor de',
		dataIndex: 'NombreTercero',
		renderer: function(value, metadata, record, store){
			var tercero=record.get('NombreTercero');
			return (!tercero)?'': tercero + ' || ' + record.get('DocumentoTercero')+'-'+record.get('DVTercero');
		}
		
	}
];
 
        this.dockedItems=[{
            xtype: 'toolbar',
            items: [{
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
