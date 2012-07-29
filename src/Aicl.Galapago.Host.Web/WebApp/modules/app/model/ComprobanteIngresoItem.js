Ext.define('App.model.ComprobanteIngresoItem',{
	extend: 'Ext.data.Model',
	idProperty: 'Id',
	fields:[{
			name: 'Id',
			type: 'int'
		},{
			name: 'IdComprobanteIngreso',
			type: 'int'
		},{
			name: 'IdIngreso',
			type: 'int'
		},{
			name: 'Abono',
			type: 'number'
		},{
			name: 'Numero',
			type: 'int'
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
			name: 'IdSucursal',
			type: 'int'
		},{
			name: 'IdTercero',
			type: 'int'
		},{
			name: 'Fecha',
			type: 'date',
			convert: function(v){return Aicl.Util.convertToDate(v);}
		},{
			name:'Documento',
			type:'string'
			
		},{
			name:'Descripcion',
			type:'string'
			
		}]
});