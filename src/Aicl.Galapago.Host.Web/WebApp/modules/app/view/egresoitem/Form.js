Ext.define('App.view.egresoitem.Form', {
    extend: 'Ext.form.Panel',
    alias : 'widget.egresoitemform',
    ui:'default-framed',
    constructor: function(config){
    	config=config|| {};
    	config.frame=config.frame==undefined?false: config.frame;
    	config.margin=config.margin|| '0 0 0 5px';
    	config.bodyStyle = config.bodyStyle ||'padding:0px 0px 0px 0px';
    	config.width = config.width|| 365;
        config.height = config.height|| 350;
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
		name: 'IdEgreso',
		fieldLabel: 'IdEgreso',
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
		allowDecimals: false,
		name: 'TipoPartida',
		fieldLabel: 'TipoPartida',
		allowBlank: false
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
		name: 'IdCentro',
		fieldLabel: 'IdCentro',
		allowBlank: false
	},
	{
		xtype: 'numberfield',
		allowDecimals: false,
		name: 'IdTercero',
		fieldLabel: 'IdTercero'
	},
	{
		name: 'CodigoItem',
		fieldLabel: 'CodigoItem'
	},
	{
		name: 'NombreItem',
		fieldLabel: 'NombreItem'
	},
	{
		name: 'NombreCentro',
		fieldLabel: 'NombreCentro'
	},
	{
		name: 'NombreTercero',
		fieldLabel: 'NombreTercero'
	},
	{
		name: 'DocumentoTercero',
		fieldLabel: 'DocumentoTercero'
	},
	{
		name: 'DVTercero',
		fieldLabel: 'DVTercero'
	}
];
 
        this.buttons = [{ 
            text:'Add',
            formBind: false,
            disabled:true,
            action:'save'      
	    }];
 
        this.callParent(arguments);
    }
});