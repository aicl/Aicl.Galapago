Ext.define('App.model.Infante',{
	extend: 'Ext.data.Model',
	idProperty: 'Id',
	fields:[{
		name: 'Id',	type: 'int'
	},{
		name: 'Documento',	type: 'string'
	},{
		name: 'Nombres',type: 'string'
	},{
		name: 'Apellidos',	type: 'string'
	},{
		name: 'IdTerceroFactura',	type: 'int'
	},{
		name: 'FechaNacimiento',type: 'date',
		convert: function(v){return Aicl.Util.convertToDate(v);}
	},{
		name: 'Sexo',	type: 'string'
	},{
		name: 'Direccion',	type: 'string'
	},{
		name: 'Telefono',	type: 'string'
	},{
		name: 'Celular',	type: 'string'
	},{
		name: 'Mail',		type: 'string'
	},{
		name: 'Comentario',	type: 'string'
	},{
		name: 'DocumentoTercero',type: 'string'
	},{
		name: 'DVTercero',	type: 'string'
	},{
		name: 'NombreTercero',	type: 'string'
	},{
		name: 'CelularTercero',	type: 'string'
	},{
		name: 'TelefonTercero',	type: 'string'
	},{
		name: 'MailTercero',	type: 'string'
	}]
});