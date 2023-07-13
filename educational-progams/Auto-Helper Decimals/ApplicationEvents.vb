Namespace My
	' The following events are available for MyApplication:
	' 
	' Startup: Raised when the application starts, before the startup form is created.
	' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
	' UnhandledException: Raised if the application encounters an unhandled exception.
	' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
	' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
	Partial Friend Class MyApplication
		Private Sub MyApplication_UnhandledException(sender As Object, e As ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
			Try
				My.Computer.FileSystem.WriteAllText(File, DateAndTime.Today & "  " & DateAndTime.TimeOfDay & "  " & CurrentUser & "  " & "ERROR: " & e.Exception.Message & vbNewLine, True)

			Catch ex As Exception
				If MessageBox.Show("An error has occurred. Ensure that this computer does not have advanced restrictions. Would you like to restart the program.", "Error (Auto-Helper)", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = DialogResult.Yes Then Windows.Forms.Application.Restart()
			End Try
		End Sub
	End Class


End Namespace

