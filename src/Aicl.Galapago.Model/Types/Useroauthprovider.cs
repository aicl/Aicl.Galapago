using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("USEROAUTHPROVIDER")]
	public partial class Useroauthprovider:IHasId<System.Int32>{

		public Useroauthprovider(){}

		[Alias("ID")]
		[Sequence("USEROAUTHPROVIDER_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("USERAUTHID")]
		public System.Int32 Userauthid { get; set;} 

		[Alias("PROVIDER")]
		[StringLength(1024)]
		public System.String Provider { get; set;} 

		[Alias("USERID")]
		[StringLength(1024)]
		public System.String Userid { get; set;} 

		[Alias("USERNAME")]
		[StringLength(1024)]
		public System.String Username { get; set;} 

		[Alias("DISPLAYNAME")]
		[StringLength(1024)]
		public System.String Displayname { get; set;} 

		[Alias("FIRSTNAME")]
		[StringLength(1024)]
		public System.String Firstname { get; set;} 

		[Alias("LASTNAME")]
		[StringLength(1024)]
		public System.String Lastname { get; set;} 

		[Alias("EMAIL")]
		[StringLength(1024)]
		public System.String Email { get; set;} 

		[Alias("REQUESTTOKEN")]
		[StringLength(1024)]
		public System.String Requesttoken { get; set;} 

		[Alias("REQUESTTOKENSECRET")]
		[StringLength(1024)]
		public System.String Requesttokensecret { get; set;} 

		[Alias("ITEMS")]
		[StringLength(1024)]
		public System.String Items { get; set;} 

		[Alias("ACCESSTOKEN")]
		[StringLength(1024)]
		public System.String Accesstoken { get; set;} 

		[Alias("ACCESSTOKENSECRET")]
		[StringLength(1024)]
		public System.String Accesstokensecret { get; set;} 

		[Alias("CREATEDDATE")]
		public System.DateTime Createddate { get; set;} 

		[Alias("MODIFIEDDATE")]
		public System.DateTime Modifieddate { get; set;} 

	}
}
