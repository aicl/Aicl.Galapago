using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("USERAUTH")]
	public partial class Userauth:IHasId<System.Int32>{

		public Userauth(){}

		[Alias("ID")]
		[Sequence("USERAUTH_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("USERNAME")]
		[StringLength(40)]
		public System.String Username { get; set;} 

		[Alias("EMAIL")]
		[StringLength(80)]
		public System.String Email { get; set;} 

		[Alias("PRIMARYEMAIL")]
		[StringLength(80)]
		public System.String Primaryemail { get; set;} 

		[Alias("FIRSTNAME")]
		[StringLength(60)]
		public System.String Firstname { get; set;} 

		[Alias("LASTNAME")]
		[StringLength(60)]
		public System.String Lastname { get; set;} 

		[Alias("DISPLAYNAME")]
		[StringLength(60)]
		public System.String Displayname { get; set;} 

		[Alias("SALT")]
		[StringLength(512)]
		public System.String Salt { get; set;} 

		[Alias("PASSWORDHASH")]
		[StringLength(512)]
		public System.String Passwordhash { get; set;} 

		[Alias("DIGESTHA1HASH")]
		[StringLength(512)]
		public System.String Digestha1hash { get; set;} 

		[Alias("ROLES")]
		[StringLength(30)]
		public System.String Roles { get; set;} 

		[Alias("PERMISSIONS")]
		[StringLength(30)]
		public System.String Permissions { get; set;} 

		[Alias("CREATEDDATE")]
		public System.DateTime Createddate { get; set;} 

		[Alias("MODIFIEDDATE")]
		public System.DateTime Modifieddate { get; set;} 

		[Alias("META")]
		[StringLength(1024)]
		public System.String Meta { get; set;} 

	}
}
