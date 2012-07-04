Ext.define('App.view.egreso.Form', {
    extend: 'Ext.form.Panel',
    alias : 'widget.egresoform',
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
	},{
		xtype: 'sucursalautorizadacombo'
	},{
		xtype: 'remotetercerocombo'
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
		name: 'Numero',
		fieldLabel: 'Numero',
		allowBlank: false
	},
	{
		name: 'Descripcion',
		fieldLabel: 'Descripcion',
		maxLength: 50,
		enforceMaxLength: true
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
		name: 'Valor',
		fieldLabel: 'Valor',
		allowBlank: false
	},
	{
		xtype: 'numberfield',
		name: 'Saldo',
		fieldLabel: 'Saldo',
		allowBlank: false
	},
	{
		xtype: 'numberfield',
		allowDecimals: false,
		name: 'DiasCredito',
		fieldLabel: 'DiasCredito',
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
		fieldLabel: 'Externo',
		allowBlank: false
	},
	{
		name: 'CodigoDocumento',
		fieldLabel: 'CodigoDocumento',
		allowBlank: false,
		maxLength: 4,
		enforceMaxLength: true
	},
	{
		name: 'Documento',
		fieldLabel: 'Documento',
		allowBlank: false,
		maxLength: 12,
		enforceMaxLength: true
	},
	{
		xtype: 'numberfield',
		allowDecimals: false,
		name: 'IdTerceroReceptor',
		fieldLabel: 'IdTerceroReceptor'
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

Ext.define('remotetercero.ComboBox', {
	extend:'Ext.ux.form.field.BoxSelect',
	alias : 'widget.remotetercerocombo',
    // fieldLabel: 'Job City',
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
