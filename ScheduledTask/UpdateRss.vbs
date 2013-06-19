Call RunIt()

Sub RunIt()
        'Let the script to finish on an error.
        On Error Resume Next
        Dim RequestObj
        Dim URL
        Set RequestObj = CreateObject("Microsoft.XMLHTTP")
        'Request URL... 
        URL = "http://www.thegroupstore.com/ScheduledTask/UpdateRss.aspx"
        'Open request and pass the URL
        RequestObj.open "POST", URL , false
        'Send Request
        RequestObj.Send
        'cleanup
        Set RequestObj = Nothing
End Sub