using System;

namespace Aicl.Galapago.BusinessLogic
{
	public static class Extensions
	{
		public static string ObtenerPeriodo(this DateTime date){
            return date.Year.ToString() + date.Month.ToString().PadLeft(2,'0');
        }
	}
}

