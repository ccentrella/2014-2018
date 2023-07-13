using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordPro
{
	static class Compatibility
	{
		/// <summary>
		/// Determines whether the computer is running Windows 8 or higher
		/// </summary>
		public static bool IsWindows8OrHigher
		{
			get
			{
				if (Environment.OSVersion.Version.Major > 6)
                {
                    return true;
                }
                else if (Environment.OSVersion.Version.Major == 6 & Environment.OSVersion.Version.Minor >= 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
		}
	}
}
