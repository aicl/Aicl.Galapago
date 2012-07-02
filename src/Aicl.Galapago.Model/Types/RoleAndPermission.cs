using System;

namespace Aicl.Galapago.Model.Types
{
	public class RoleAndPermission
	{
		public RoleAndPermission ()
		{
		}
		
		public int IdRole {get; set;}
		public string Role { get; set;}
		public string Directory { get; set;}
		public string ShowOrder { get; set;}
		public string Permission { get; set;}
		public string Title { get; set;}
	}
}
