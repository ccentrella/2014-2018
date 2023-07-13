using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;

namespace RecordPro
{
	static class RecentStringFunctions
	{
		/// <summary>
		/// Add the file to the user's recent list
		/// </summary>
		/// <param name="file">The file to add to the user's recent list</param>
		public static void UpdateRecent(this string file)
		{
			var recentFiles = new List<string>((Collection<string>)Application.Current.Properties["Recent"]);

			// Only continue if the user config location and file exist
			if (!File.Exists(file))
            {
                return;
            }

            // Remove the location and then add it to the top
            recentFiles.Remove(file);
			recentFiles.Insert(0, file);

			// Set the new list of recent files
			Application.Current.Properties["Recent"] = new Collection<string>(recentFiles);
		}

		/// <summary>
		/// Delete the file from the user's recent list
		/// </summary>
		/// <param name="file">The file to delete from the user's recent list</param>
		public static void DeleteRecent(this string file)
		{
			var recentFiles = new List<string>((Collection<string>)Application.Current.Properties["Recent"]);

			// Remove the location and then add it to the top
			recentFiles.Remove(file);

			// Set the new list of recent files
			Application.Current.Properties["Recent"] = new Collection<string>(recentFiles);
		}
	}
}
