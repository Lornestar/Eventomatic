<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calendar.aspx.cs" Inherits="Eventomatic.Calendar" MasterPageFile="~/Site.Master"%>


<%@ MasterType VirtualPath="~/Site.Master" %>


<asp:Content runat="server" ID="content" ContentPlaceHolderID="body">
    <link href="Images/Webschedule/XpBlue/ig_webmonthview.css" rel="stylesheet" type="text/css" />
<script language=javascript>

function newapp()
{
    var scheduleInfo = ig_getWebScheduleInfoById("wsInfo");
	var dateTime = new Date();
	var appointment = scheduleInfo.getActivities().createActivity();
	var minutes = dateTime.getMinutes();
	if(minutes < 30)
		dateTime.setMinutes(30);
	else
		dateTime.setHours(dateTime.getHours() + 1,0);
	appointment.setStartDateTime(dateTime);
	scheduleInfo.showAddAppointmentDialog(appointment, "");
	oEvent.needPostBack = false;					
}
</script>
<table>
    <tr>
        <td>
        
        <ig_sched:webdayview id="WebDayView1" runat="server" EnableMultiResourceCaption=false
        WebScheduleInfoID="wsInfo" Height="700px" Width="400px" NavigationButtonsVisible=false
        ActivityHeightMinimum="20" VisibleDays=3 ></ig_sched:webdayview>    
        </td>
        <td>
            <table>
                <tr>
                    <td>
                    <ig_sched:WebWeekView ID=WebWeekView1 WebScheduleInfoID=wsInfo runat=server
                    width=300px Height=400px NavigationButtonsVisible=false EnableMultiResourceCaption=false></ig_sched:WebWeekView>
                    </td>                    
                </tr>
                <tr>
                    <td>
                    <ig_sched:WebMonthView ID="WebMonthView1" WebScheduleInfoID="wsInfo" runat="server"
                Height="300px" Width="300px" CaptionHeaderText="Month"
                NavigationAnimation=Linear WeekendDisplayFormat=Full EnableAppStyling="False"
                StyleSetName="Default" EnableMultiResourceCaption=false >
            </ig_sched:WebMonthView>    
            
                    </td>                    
                </tr>
            </table>
        
        </td>
    </tr>
</table>
    
            <ig_scheduledata:WebScheduleGenericDataProvider runat="server" ID="WebScheduleGenericProvider1"
                WebScheduleInfoID="wsInfo" StyleSetName="" StyleSetPath="" StyleSheetDirectory="">
                <AppointmentBinding DataKeyMember="ID" ResourceKeyMember="ResourceKey" />
                <ResourceBinding DataKeyMember="ID" />
                <VarianceBinding DataKeyMember="ID" ResourceKeyMember="ResourceKey" />
            </ig_scheduledata:WebScheduleGenericDataProvider>
            <ig_sched:WebScheduleInfo ID="wsInfo" runat="server" AppointmentFormPath="Calendar_Files/AppointmentAdd.aspx" EnableReminders=false
                ReminderFormPath="forms/Reminder.aspx" EnableRecurringActivities="True" EnableSmartCallbacks="false"
                EnableAppStyling="True" EnableMultiResourceView="True" OnActivityAdding="WebScheduleInfo1_ActivityAdding"
                OnVarianceAdding="wsInfo_VarianceAdding" >
            </ig_sched:WebScheduleInfo>    
</asp:Content>