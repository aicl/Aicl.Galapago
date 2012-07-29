Ext.define('App.model.ComprobanteIngreso',{
	extend: 'Ext.data.Model',
	idProperty: 'Id',
	fields:[{
			name: 'Id',
			type: 'int'
		},{
			name: 'Fecha',
			type: 'date',
			convert: function(v){return Aicl.Util.convertToDate(v);}
		},{
			name: 'Periodo',
			type: 'string'
		},{
			name: 'Numero',
			type: 'int'
		},{
			name: 'Descripcion',
			type: 'string'
		},{
			name: 'Valor',
			type: 'number'
		},{
			name: 'IdCuentaReceptora',
			type: 'int'
		},{
			name: 'IdTercero',
			type: 'int'
		},{
			name: 'IdSucursal',
			type: 'int'
		},{
			name: 'FechaAsentado',
			type: 'date',
			convert: function(v){return Aicl.Util.convertToDate(v);}
		},{
			name: 'FechaAnulado',
			type: 'date',
			convert: function(v){return Aicl.Util.convertToDate(v);}
		},{
			name: 'Externo',
			type: 'boolean'
		},{
			name: 'IdTerceroReceptora',
			type: 'int'
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
		},{
			name: 'CodigoItem',
			type: 'string'
		},{
			name: 'NombreItem',
			type: 'string'
		}]
});