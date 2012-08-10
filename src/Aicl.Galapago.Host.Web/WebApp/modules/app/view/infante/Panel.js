Ext.define('App.view.infante.Panel',{ 
    extend: 'Ext.panel.Panel',
    alias : 'widget.infantepanel',
    frame: true,
    width:965,
    layout: 'hbox',
    dockedItems: [{
    	xtype: 'toolbar',
    	dock: 'top',
    	items: [{
            tooltip:'Nuevo', iconCls:'new_document',  disabled:true,
            action: 'new'
        },{
            xtype:'textfield', emptyText:'Documento/Nombre,Apellido Infante',
            width: 300,
            name: 'buscarInfanteText'
        },{
           tooltip:'Buscar por los criterios indicados..', iconCls:'find',
           action: 'buscarInfante'
        }]
	}],
    items:[{
        xtype: 'infantetabpanel'
    }],
    showSearchWindow:function(){
    	this.searchWindow.show();
    },
    hideSearchWindow:function(){
    	this.searchWindow.hide();
    },
    showCreateTerceroWindow:function(){
    	this.createTerceroWindow.show();
    },
    hideCreateTerceroWindow:function(){
    	this.createTerceroWindow.hide();
    },
   	initComponent: function(){
   		this.searchWindow=Ext.create('App.view.infante.SearchWindow');
   		this.createTerceroWindow= Ext.create('App.view.infante.CreateTerceroWindow');
   		this.callParent(arguments);
   	}
});

Ext.define('App.view.infante.TabPanel',{
	extend: 'Ext.tab.Panel',
    alias : 'widget.infantetabpanel',
    frame: true,
    anchor:'100%',
    items:[{
    	title:'Infante',
    	xtype:'panel',
    	layout:'hbox',
    	items:[{
        	xtype: 'panel', ui:'default-framed',
        	style: {border: 0, padding: 0},
        	width: 535, height:520,
        	layout:'hbox',
        	items:[{xtype:'infantephotoform'}, {xtype:'infanteform'}]
    	},{
        	xtype: 'panel', ui:'default-framed',
        	style: {border: 0, padding: 0},
        	width: 410, height:520
    	}]
   	},{
    	title:'Cursos',
    	xtype:'panel'
    	    		
   	}]
});


Ext.define('App.view.infante.PhotoForm', {
    extend: 'Ext.form.Panel',
    alias : 'widget.infantephotoform',
    frame:false,
    margin: '0 0 0 5',
    bodyStyle: 'padding:10px 10px 0',
    width: 150,
    height: 200,
    initComponent: function() {
    	this.layout= {
    		type: 'vbox',    
        	align: 'center'
    	};
        this.items = [{
            xtype:'hidden',
            name: 'Id'
        },{
        	xtype:'infanteimg'
        },{
        	xtype: 'filefield',
        	name: 'fileupload',
        	msgTarget: 'side',
        	allowBlank: false,
        	disabled:true,
        	anchor: '100%',
        	buttonText: 'Select...',
    		buttonConfig: {
    			iconCls: 'select'
    		},
    		buttonOnly:true,
    		width:70
        }];
 
        this.buttons = [{ 
            text:'Upload',
            formBind: false,
            disabled:true,
            action:'upload',
            iconCls: 'upload'
	    }];
 
        this.callParent(arguments);
    }
});

Ext.define('App.view.infante.Img', {
	extend:'Ext.Img',
	alias:'widget.infanteimg',
	src: Aicl.Util.getEmpytImgUrl(),
    width:75, height: 100
});

Ext.define('App.view.infante.Form', {
    extend: 'Ext.form.Panel',
    alias : 'widget.infanteform',
    ui:'default-framed',
    style: {border: 0, padding: 0},
    frame:false,
    margin: '0 0 0 5px',
    bodyStyle :'padding:0px 0px 0px 0px',
    width:370,
    height: 480,
    autoScroll: true,
	fieldDefaults :{ msgTarget: 'side',  labelWidth: 100, labelAlign: 'right'},
	defaultType:'textfield',
	defaults : { anchor: '100%', labelStyle: 'padding-left:4px;' },
     
    initComponent: function() {
        this.items = [{
    		xtype: 'toolbar',
    		dock: 'top',
    		items: [{
           		tooltip:'Guardar',
      			iconCls:'save_document',
        		disabled:true,
        		action:'save'
           	},'-',{
               	tooltip:'Borrar',
               	iconCls:'remove',
               	disabled:true,
               	action: 'delete'
           	}]
	},{
		xtype: 'hidden',
		name: 'Id'
	},{
		name: 'Documento',
		fieldLabel: 'Documento',
		allowBlank: false,
		maxLength: 15,
		enforceMaxLength: true
	},{
		name: 'Nombres',
		fieldLabel: 'Nombres',
		allowBlank: false,
		maxLength: 60,
		enforceMaxLength: true
	},{
		name: 'Apellidos',
		fieldLabel: 'Apellidos',
		allowBlank: false,
		maxLength: 60,
		enforceMaxLength: true
	},{
		xtype: 'datefield',
		name: 'FechaNacimiento',
		fieldLabel: 'FechaNacimiento',
		allowBlank: false,
		format: 'd.m.Y'
	},{
		name: 'Sexo',
		fieldLabel: 'Sexo',
		maxLength: 1,
		enforceMaxLength: true
	},{
		name: 'Direccion',
		fieldLabel: 'Direccion',
		maxLength: 80,
		enforceMaxLength: true
	},{
		name: 'Telefono',
		fieldLabel: 'Telefono',
		maxLength: 15,
		enforceMaxLength: true
	},{
		name: 'Celular',
		fieldLabel: 'Celular',
		maxLength: 15,
		enforceMaxLength: true
	},{
		name: 'Mail',
		fieldLabel: 'Mail',
		maxLength: 80,
		enforceMaxLength: true,
		vtype: 'email'
	},{
		xtype:'textarea',
		name: 'Comentario',
		fieldLabel: 'Comentario',
		maxLength: 120,
		enforceMaxLength: true
	},{
		xtype:'fieldset',
		title: 'Facturar a:',
		collapsible: false,
		fieldDefaults :{ msgTarget: 'side',  labelWidth: 50, labelAlign: 'right'},
        defaultType: 'textfield',
        defaults: {anchor: '100%',labelStyle: 'padding-left:4px;'},
        layout: 'anchor',
		items:[{
			xtype:'fieldset',
			style: {border: 0, padding: 0},
			items:[{
				layout:'hbox',
				ui:'default-framed',
				style: {border: 0, padding: 0},
				
				items:[{
					xtype: 'infantetercerocombo',
					fieldLabel: 'Nombre',
					width:320
				},{
					xtype:"button",
					tooltip:'Crear Tercero', 
					iconCls:'add',
           			action: 'newTercero',
					hideLabel:true,
					style:{marginLeft:"3px"}
				}]
			}]
		},{	
			xtype:'fieldset',
			style: {border: 0, padding: 0},
			items:[{
				layout:'hbox',
				ui:'default-framed',
				style: {border: 0, padding: 0},
				defaultType: 'textfield',
				items:[{
					name: 'DocumentoTercero',fieldLabel: 'Documento',
					readOnly:true,
					width:320
				},{
					name: 'DVTercero',hideLabel:true,
					width:22,
					readOnly:true,
					style:{marginLeft:"3px"}
				}]
			}]
	
		}]	
	}];
	
    this.callParent(arguments);
	}
});

Ext.define('App.view.infante.List',{ 
    extend: 'Ext.grid.Panel',
    alias : 'widget.infantelist', 
    constructor: function(config){
    	config= config|| {};
    	config.store= config.store|| 'Infante',
        config.frame = config.frame==undefined? false:config.frame;
		config.selType = config.selType || 'rowmodel';
    	config.height = config.height|| '100%'; //450;
    	//config.width = config.width || 600;
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
		text: 'Documento',
		dataIndex: 'Documento'
	},{
		text: 'Nombres',
		dataIndex: 'Nombres'
	},{
		text: 'Apellidos',
		dataIndex: 'Apellidos'
	},{
		text: 'FechaNacimiento',
		dataIndex: 'FechaNacimiento',
		renderer: Ext.util.Format.dateRenderer('d.m.Y')
	},{
		text: 'Sexo',
		dataIndex: 'Sexo'
	},{
		text: 'Direccion',
		dataIndex: 'Direccion'
	},{
		text: 'Telefono',
		dataIndex: 'Telefono'
	},{
		text: 'Celular',
		dataIndex: 'Celular'
	},{
		text: 'Mail',
		dataIndex: 'Mail'
	},{
		text: 'Comentario',
		dataIndex: 'Comentario'
	},{
		text: 'DocumentoTercero',
		dataIndex: 'DocumentoTercero'
	},{
		text: 'DVTercero',
		dataIndex: 'DVTercero'
	},{
		text: 'NombreTercero',
		dataIndex: 'NombreTercero'
	}];
	
    this.dockedItems=[{
        xtype: 'toolbar',
        items: [{
            text:'Seleccionar',
     		tooltip:'Seleccionar usuario',
      		iconCls:'select',
    		disabled:true,
    		action:'selectInfante'
        }]		
    }]
                
    this.callParent(arguments);
    
    }
});

Ext.define('App.view.infante.TerceroComboBox', {
	extend:'Ext.ux.form.field.BoxSelect',
	alias : 'widget.infantetercerocombo',
    displayField: 'Nombre',
	valueField: 'Id',
	name:'IdTerceroFactura',
    store: 'RemoteTercero',
    forceSelection:true,
    pageSize: 12,
    multiSelect:false,
    queryMode: 'remote',
    queryParam :'Nombre',
    delimiter: '|',
    triggerOnClick: false,
    labelTpl: '{Nombre}',
    listConfig: {
        tpl: Ext.create('Ext.XTemplate',
            '<ul><tpl for=".">',
                '<li role="option" class="' + Ext.baseCSSPrefix + 'boundlist-item' + '">{Nombre}: {Documento}</li>',
            '</tpl></ul>'
    )}
});

Ext.define('App.view.infante.SearchWindow',{
	extend: 'Ext.Window',
    alias : 'widget.infantesearchwindow',
	closable: true,
    closeAction: 'hide',
    y:35,
    x:30,
    autoHeight:true,
    width: 400,
    modal: false,
    items:[{
    	xtype:'infantelist'
    }]
});


Ext.define('App.view.infante.CreateTerceroWindow',{
	extend: 'Ext.Window',
    alias : 'widget.infantecreatetercerowindow',
	closable: true,
    closeAction: 'hide',
    y:35,
    autoHeight:true,
    autoWidth: true,
    modal: false,
    items:[{
    	xtype:'infanteterceroform'
    }]
});


Ext.define('App.view.infante.TerceroForm', {
    extend: 'Ext.form.Panel',
    alias : 'widget.infanteterceroform',
    ui:'default-framed',
    frame:false,
    margin: '5 5 5 5px',
    bodyStyle :'padding:0px 0px 0px 0px',
    width:400,
    autoScroll: true,
    fieldDefaults : {msgTarget: 'side', labelWidth: 120,labelAlign: 'right' },
    defaultType: 'textfield',
    defaults : {anchor: '100%',labelStyle: 'padding-left:4px;'},
    
    initComponent: function() {
    this.items = [{	
    	xtype: 'toolbar',
    	name: 'terceroToolbar',
        colspan:2,
        items: [{
          	tooltip:'Guardar',
      		iconCls:'save_document',
        	disabled:true,
        	action:'save'
        }]
    },{
		xtype: 'hidden',
		name: 'Id'
	},{
		xtype: 'tipodocumentocombo',
		name: 'IdTipoDocumento',
		fieldLabel: 'TipoDocumento'
	},{
		name: 'Documento',
		fieldLabel: 'Documento',
		allowBlank: false,
		maxLength: 13,
		enforceMaxLength: true
	},{
		name: 'DigitoVerificacion',
		fieldLabel: 'DigitoVerificacion',
		maxLength: 1,
		enforceMaxLength: true
	},{
		name: 'Nombre',
		fieldLabel: 'Nombre',
		allowBlank: false,
		maxLength: 120,
		enforceMaxLength: true
	},{
		xtype: 'ciudadcombo',
		name: 'IdCiudad',
		fieldLabel: 'Ciudad'
	},{
		name: 'Direccion',
		fieldLabel: 'Direccion',
		maxLength: 80,
		enforceMaxLength: true
	},{
		name: 'Telefono',
		fieldLabel: 'Telefono',
		maxLength: 15,
		enforceMaxLength: true
	},{
		name: 'Celular',
		fieldLabel: 'Celular',
		maxLength: 15,
		enforceMaxLength: true,
		allowBlank: false
	},{
		name:'Mail',
		fieldLabel:'Mail',
		maxLength: 80,
		enforceMaxLength: true,
		vtype: 'email',
		allowBlank: false
	},{
		xtype: 'checkboxfield',
		name: 'Activo',
		fieldLabel: 'Activo',
		allowBlank: false,
		checked:true
	},{
		xtype: 'checkboxfield',
		name: 'EsProveedor',
		fieldLabel: 'EsProveedor',
		allowBlank: false
	},{
		xtype: 'checkboxfield',
		name: 'EsCliente',
		fieldLabel: 'EsCliente',
		allowBlank: false,
		checked:true
	},{
		xtype: 'checkboxfield',
		name: 'EsAutoRetenedor',
		fieldLabel: 'EsAutoRetenedor',
		allowBlank: false
	},{
		xtype: 'checkboxfield',
		name: 'EsEmpleado',
		fieldLabel: 'EsEmpleado',
		allowBlank: false
	},{
		xtype: 'checkboxfield',
		name: 'EsEps',
		fieldLabel: 'EsEps',
		allowBlank: false
	},{
		xtype: 'checkboxfield',
		name: 'EsFp',
		fieldLabel: 'EsFp',
		allowBlank: false
	},{
		xtype: 'checkboxfield',
		name: 'EsParafiscal',
		fieldLabel: 'EsParafiscal',
		allowBlank: false
	}];
 
    this.callParent(arguments);
    }
});