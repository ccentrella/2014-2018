using Microsoft.Win32;
using System;
using System.IO;
using System.Security;

namespace Record_Pro_Validator
{
    class Program
    {
        // Copyright (c) 2020 Autosoft Corporation. All rights reserved.
        private static string DefaultFileLocation = Path.Combine(Environment.GetFolderPath(
    Environment.SpecialFolder.CommonApplicationData), "Autosoft", "Record Pro", "2017");
        public const string RegistryLocation = @"Software\Autosoft\Record Pro\2017";

        static void Main()
        {
            Console.Clear();
            Console.Title = "Record Pro Activator";
            Console.WriteLine("Activating keys for Record Pro...");
            if (CreateKeys())
                Console.WriteLine("Activation successful");
            else
                Console.WriteLine("Activation complete, with errors");
        }

        static bool CreateKeys()
        {
            string fileLocation = DefaultFileLocation;
            bool successful = true;

            // Retrieve file location from registry
            try
            {
                using (var registryKey = Registry.CurrentUser.CreateSubKey(RegistryLocation))
                {
                    fileLocation = registryKey.GetValue("File Location", fileLocation).ToString();
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Validation failed: " + ex.Message);
                successful = false;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Validation failed: " + ex.Message);
                successful = false;
            }
            catch (SecurityException ex)
            {
                Console.WriteLine("Validation failed: " + ex.Message);
                successful = false;
            }

            // Create validation file
            string usersLocation = Path.Combine(fileLocation, "Users");
            foreach (var folder in Directory.EnumerateDirectories(usersLocation))
            {
                if (!CreateValidationFile(folder))
                    successful = false;
            }

            return successful; // Return whether or not the operation was succesful
        }

        static bool CreateValidationFile(string folderLocation)
        {
            // Create account validation data
            string validationFile = System.IO.Path.Combine(folderLocation, "Validation.txt");
            string folderName = System.IO.Path.GetFileName(folderLocation);
            char[] folderArray = folderName.ToCharArray();
            string modificationDateString = DateTime.Today.ToString();
            string separator = modificationDateString[6].ToString() + modificationDateString[0].ToString()
            + modificationDateString[5].ToString() + modificationDateString[4].ToString();
            string data = string.Join(separator, folderArray);

            // Attempt to create validation file for the account
            try
            {
                using (var newReader = new StreamWriter(validationFile))
                    newReader.Write(data);
                Console.WriteLine("Account: " + folderName + " validated successfully.");
                return true; // The operation was successful
            }
            catch (IOException ex)
            {
                Console.WriteLine("An error occurred. Account: " + folderName + " could not be validated.\nError Details: " + ex.Message);
                return false;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("An error occurred. Account: " + folderName + " could not be validated.\nError Details: " + ex.Message);
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("An error occurred. Account: " + folderName + " could not be validated.\nError Details: " + ex.Message);
                return false;
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine("An error occurred. Account: " + folderName + " could not be validated.\nError Details: " + ex.Message);
                return false;
            }
            catch (SecurityException ex)
            {
                Console.WriteLine("An error occurred. Account: " + folderName + " could not be validated.\nError Details: " + ex.Message);
                return false;
            }
        }
    }
}
