Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.Devices

Namespace My
    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active.
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Private Sub MyApplication_NetworkAvailabilityChanged(sender As Object, e As NetworkAvailableEventArgs) Handles Me.NetworkAvailabilityChanged
            If e.IsNetworkAvailable = False Then
                MsgBox("Lost internet connection. Unfortunatly, you need internet to run. Please try again later.", MsgBoxStyle.Critical)
                End
            End If
        End Sub

        Private Sub MyApplication_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs) Handles Me.UnhandledException
            e.ExitApplication = False
            MsgBox("Something went seriously wrong. Please email admin@edxtech.uk with the info: " & e.Exception.ToString, MsgBoxStyle.Critical)
            End
        End Sub

    End Class
End Namespace