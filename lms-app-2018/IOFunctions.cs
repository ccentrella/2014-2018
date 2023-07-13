using RecordPro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordPro
{
	public static class IOFunctions
	{
		/// <summary>
		/// Renames a file
		/// </summary>
		/// <param name="location">The location of the file to rename</param>
		/// <returns>Whether or not the file was successfully renamed</returns>
		public static bool RenameFile(string location)
		{
			string name = Path.GetFileName(location);
			char[] invalidChars = Path.GetInvalidFileNameChars();
			string message = string.Format("Please enter the new name ({0}).",
				name);
			var renameDialog = new InputDialog("Enter Name",
			message) { Owner = Application.mWindow };

			// Ensure that a valid location has been passed.
			if (location == null)
            {
                return false;
            }

            // Attempt to rename the file if the user agrees
            if (renameDialog.ShowDialog() == true)
			{
				string directoryLocation = Path.GetDirectoryName(location);
				string extension = Path.GetExtension(location);
				string title = renameDialog.userInput.Text;
				string newLocation = Path.Combine(directoryLocation, title + extension);

				// Ensure the name does not contains any invalid characters
				foreach (var @char in invalidChars)
				{
					if (title == null || title.Contains(@char))
					{
						var result = TaskDialog.ShowDialog("Invalid Name", "The specified name is invalid.",
											"File names must contain at least one character and cannot "
										+ "contain any of the following: " + invalidChars,
										TaskDialogButtons.Ok | TaskDialogButtons.Cancel, TaskDialogIcon.Warning);
						if (result == TaskDialogResult.Ok)
                        {
                            return RenameFile(location);
                        }
                    }
				}

				// Attempt to rename the file
				try
				{
					if (location != newLocation)
                    {
                        File.Move(location, newLocation);
                    }

                    return true;
				}
				catch (IOException)
				{
					TaskDialog.ShowDialog("File Error", "An error has occurred.",
						"The file could not be renamed.", TaskDialogButtons.Ok,
						TaskDialogIcon.Warning);
				}
				catch (UnauthorizedAccessException)
				{
					TaskDialog.ShowDialog("Access Denied", "The file could not be renamed.",
						"Access has been denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
				}
			}
			return false;
		}

		/// <summary>
		/// Deletes the specified file
		/// </summary>
		/// <param name="location">The file to delete</param>
		/// <returns>Whether or not the deletion was successful</returns>
		public static bool DeleteFile(string location)
		{
			try
			{
				File.Delete(location);
				return true;
			}
			catch (IOException ex)
			{
				string message = String.Format("The file \"{0}\" could not "
					+ "be deleted.", Path.GetFileName(location));
				TaskDialog.ShowDialog("File Error", message,
				ex.Message, TaskDialogButtons.Ok, TaskDialogIcon.Warning);
			}
			catch (UnauthorizedAccessException)
			{
				string message = String.Format("The file \"{0}\" could not "
					+ "be deleted.", Path.GetFileName(location));
				TaskDialog.ShowDialog("Access Denied", message,
				"Access was denied.", TaskDialogButtons.Ok, TaskDialogIcon.Warning);
			}
			return false;
		}

		/// <summary>
		/// Determines whether the file contains invalid characters
		/// </summary>
		/// <param name="location">The location of the file to check</param>
		/// <returns>True if the file name contains invalid characters. Otherwise, false.</returns>
		public static bool ContainsInvalidCharacters(string location)
		{
			// Ensure the name is valid
			char[] chars = Path.GetInvalidFileNameChars();
			foreach (var @char in chars)
            {
                if (location.Contains(@char))
				{
					return true;
				}
            }

            return false; // If we make it this far, we're okay
		}
	}
}
