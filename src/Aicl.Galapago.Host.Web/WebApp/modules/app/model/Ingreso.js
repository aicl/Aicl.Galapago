Ext.define('App.model.Ingreso',{
	extend: 'Ext.data.Model',
	idProperty: 'Id',
	fields:[{
			name: 'Id',
			type: 'int'
		},{
			name: 'IdTercero',
			type: 'int'
		},{
			name: 'Numero',
			type: 'int'
		},{
			name: 'Descripcion',
			type: 'string'
		},{
			name: 'Fecha',
			type: 'date',
			convert: function(v){return Aicl.Util.convertToDate(v);}
		},{
			name: 'Periodo',
			type: 'string'
		},{
			name: 'Valor',
			type: 'number'
		},{
			name: 'Saldo',
			type: 'number'
		},{
			name: 'DiasCredito',
			type: 'int'
		},{
			name: 'FechaAsentado',
			type: 'date',
			convert: function(v){return Aicl.Util.convertToDate(v);}
		},{
			name: 'IdSucursal',
			type: 'int'
		},{
			name: 'FechaAnulado',
			type: 'date',
			convert: function(v){return Aicl.Util.convertToDate(v);}
		},{
			name: 'Externo',
			type: 'boolean'
		},{
			name: 'CodigoDocumento',
			type: 'string'
		},{
			name: 'Documento',
			type: 'string'
		},{
			name: 'NombreSucursal',
			type: 'string'
		},{
			name: 'DocumentoTercero',
			type: 'string'
		},{
			name: 'DVTercero',
			type: 'string'
		},{
			name: 'NombreTercero',
			type: 'string'
		},{
			name: 'NombreDocumentoTercero',
			type: 'string'
		}]
});