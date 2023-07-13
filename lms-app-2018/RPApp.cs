using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordPro
{
	/// <summary>
	/// Represents an app
	/// </summary>
	class RPApp
	{
		/// <summary>
		/// Creates a new instance of RPApp.
		/// </summary>
		/// <param name="appLocation">The full location of the app</param>
		/// <param name="title">The title of the app</param>
		public RPApp(string appLocation, string title)
		{
			AppLocation = appLocation;
			Title = title;
		}

		/// <summary>
		/// The full location of the app
		/// </summary>
		public string AppLocation { get; protected set; }

		/// <summary>
		/// The title of the app
		/// </summary>
		public string Title { get; protected set; }
	}
}
