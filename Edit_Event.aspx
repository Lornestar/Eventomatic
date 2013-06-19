<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit_Event.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Eventomatic.Edit_Event" %>


<%@ MasterType VirtualPath="~/Site.Master" %>

<%@ Register src="Addons/Upload.ascx" tagname="Upload" tagprefix="uc1" %>

<%@ Register src="Addons/Questions.ascx" tagname="Questions" tagprefix="uc2" %>

<%@ Register src="Addons/BackgroundImage.ascx" tagname="BackgroundImage" tagprefix="uc3" %>

<%@ Register src="Addons/Tickets.ascx" tagname="Tickets" tagprefix="uc4" %>


<asp:Content runat="server" ID="content" ContentPlaceHolderID="body">
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat=server  LoadingPanelID="LoadingPanel1" >
<asp:HiddenField ID="hdeventkey" runat=server Value=0 />
    <table>
    <tr>
        <td align=left><h3>Event Details</h3></td>        
        <td align=right>
        <div id="ci3aLf" style="z-index:100;position:absolute"></div><div id="sc3aLf" style="display:inline"></div><div id="sd3aLf" style="display:none"></div><script type="text/javascript">var se3aLf=document.createElement("script");se3aLf.type="text/javascript";var se3aLfs=(location.protocol.indexOf("https")==0?"https":"http")+"://image.providesupport.com/js/lornestar/safe-standard.js?ps_h=3aLf&ps_t="+new Date().getTime();setTimeout("se3aLf.src=se3aLfs;document.getElementById('sd3aLf').appendChild(se3aLf)",1)</script><noscript><div style="display:inline"><a href="http://www.providesupport.com?messenger=lornestar">Live Help Desk</a></div></noscript>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <igtab:UltraWebTab ID="UltraWebTab1" runat="server" Width=100% 
                TabOrientation="LeftTop" SelectedTab="0">
                <Tabs>
                    <igtab:Tab Text="General" >
                        <ContentTemplate>
<table width="100%">
    
                        <tr>
                            <td >Event name:</td>
                            <td><asp:TextBox ID="txtEventName" runat="server" Width="300px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Host:</td>
                            <td><asp:TextBox ID="txtHost" runat="server" Width="300px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Event Begins:</td>
                            <td>
                                <table>
                                    <tr>
                                        <td><igsch:WebDateChooser ID="StartDate" runat="server" Value="" Width="200px">
                                    <DropButton ImageUrl1="./Images/WebDateChooser/datePickerImage.gif">
                                    </DropButton>
                                    <CalendarLayout NextMonthImageUrl="./Images/WebDateChooser/ig_cal_blueN0.gif" 
                                        PrevMonthImageUrl="./Images/WebDateChooser/ig_cal_blueP0.gif" ShowMonthDropDown="False" 
                                        ShowYearDropDown="False" TitleFormat="Month">
                                        <CalendarStyle BackColor="#E0EEFF" BorderColor="SteelBlue" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Verdana" 
                                            Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" 
                                            Font-Underline="False">
                                        </CalendarStyle>
                                        <DayHeaderStyle BackColor="#7AA7E0" Font-Bold="True" Font-Size="8pt" 
                                            ForeColor="#606090" Height="1pt">
                                        <BorderDetails ColorBottom="LightSteelBlue" StyleBottom="Solid" 
                                            WidthBottom="1px" />
                                        </DayHeaderStyle>
                                        <DayStyle BackColor="#7AA7E0" />
                                        <NextPrevStyle BackgroundImage="../images/ig_cal_blue1.gif" />
                                        <OtherMonthDayStyle ForeColor="SlateGray" />
                                        <SelectedDayStyle BackColor="SteelBlue" />
                                        <TitleStyle BackColor="#7AA7E0" BackgroundImage="ig_cal_blue1.gif" 
                                            Font-Bold="True" Font-Size="10pt" ForeColor="#505080" Height="18pt" />
                                        <TodayDayStyle BackColor="#97B0F0" />
                                        <FooterStyle BackgroundImage="ig_cal_blue2.gif" Font-Size="8pt" 
                                            ForeColor="#505080" Height="16pt">
                                        <BorderDetails ColorTop="LightSteelBlue" StyleTop="Solid" WidthTop="1px" />
                                        </FooterStyle>
                                    </CalendarLayout>
                                </igsch:WebDateChooser></td>
                                        <td><igtxt:WebDateTimeEdit ID="StartTime" runat="server" EditModeFormat="t" 
                                    DisplayModeFormat="t" Fields="2009-5-1-20-0-0-0" Nullable="False" Width="100">
                                </igtxt:WebDateTimeEdit></td>
                                    </tr>
                                </table>
                                
                                
                                </td>
                        </tr>
                        <tr>
                            <td>Event Ends:</td>
                            <td><table>
                                <tr>
                                    <td><igsch:WebDateChooser ID="EndDate" runat="server" Value="" Width="200px">
                                    <DropButton ImageUrl1="./Images/WebDateChooser/datePickerImage.gif">
                                    </DropButton>
                                    <CalendarLayout NextMonthImageUrl="./Images/WebDateChooser/ig_cal_blueN0.gif" 
                                        PrevMonthImageUrl="./Images/WebDateChooser/ig_cal_blueP0.gif" ShowMonthDropDown="False" 
                                        ShowYearDropDown="False" TitleFormat="Month">
                                        <CalendarStyle BackColor="#E0EEFF" BorderColor="SteelBlue" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Verdana" 
                                            Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" 
                                            Font-Underline="False">
                                        </CalendarStyle>
                                        <DayHeaderStyle BackColor="#7AA7E0" Font-Bold="True" Font-Size="8pt" 
                                            ForeColor="#606090" Height="1pt">
                                        <BorderDetails ColorBottom="LightSteelBlue" StyleBottom="Solid" 
                                            WidthBottom="1px" />
                                        </DayHeaderStyle>
                                        <DayStyle BackColor="#7AA7E0" />
                                        <NextPrevStyle BackgroundImage="../images/ig_cal_blue1.gif" />
                                        <OtherMonthDayStyle ForeColor="SlateGray" />
                                        <SelectedDayStyle BackColor="SteelBlue" />
                                        <TitleStyle BackColor="#7AA7E0" BackgroundImage="ig_cal_blue1.gif" 
                                            Font-Bold="True" Font-Size="10pt" ForeColor="#505080" Height="18pt" />
                                        <TodayDayStyle BackColor="#97B0F0" />
                                        <FooterStyle BackgroundImage="ig_cal_blue2.gif" Font-Size="8pt" 
                                            ForeColor="#505080" Height="16pt">
                                        <BorderDetails ColorTop="LightSteelBlue" StyleTop="Solid" WidthTop="1px" />
                                        </FooterStyle>
                                    </CalendarLayout>
                                </igsch:WebDateChooser></td>
                                    <td><igtxt:WebDateTimeEdit ID="EndTime" runat="server" EditModeFormat="t" 
                                    DisplayModeFormat="t" Fields="2009-5-1-1-0-0-0" Nullable="False" Width="100">
                                </igtxt:WebDateTimeEdit></td>
                                </tr>
                            </table>
                             
                                
                            </td>
                        </tr>
                        <tr>
                            <td>Timezone:</td>
                            <td><asp:DropDownList ID=ddlTimezone runat=server Width=300px></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Location:</td>
                            <td><asp:TextBox ID="txtLocation" runat="server" Width="300px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Street:</td>
                            <td><asp:TextBox ID="txtStreet" runat="server" Width="300px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>City/Town:</td>
                            <td><asp:TextBox ID="txtCity" runat="server" Width="300px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Phone:</td>
                            <td><asp:TextBox ID="txtPhone" runat="server" Width="300px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Email:</td>
                            <td><asp:TextBox ID="txtEmail" runat="server" Width="300px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Additional Comments:</td>
                            <td><asp:TextBox ID="txtComments" runat="server" Rows="3" TextMode="MultiLine" 
                                    Width="300px" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Event Image:<br />
                            -Must be jpg, max 500 Kb
                            </td>
                            <td>
                                <uc1:Upload ID="Upload1" runat="server" />
                            </td>
                            
                        </tr>                                             
                        <tr valign=top>
                            <td>Background Image:</td>
                            <td>
                                <uc3:BackgroundImage ID="BackgroundImage1" runat="server" />
                            </td>
                        </tr>
                    </table>
                    </ContentTemplate>
                    </igtab:Tab>
           
                    <igtab:Tab Text="Ticket Information" >
                        <ContentTemplate>
                        
        <table>
        
                    <tr>
                        <td>
        <uc4:Tickets Visible=false runat=server id=Tickets1/>
        </td>
        </tr>
            <tr>
                <td>
                <telerik:RadListView ID="RadListView1" runat="server" Width=500
                        ItemPlaceholderID="ProductTitlePlaceHolder" OnItemDataBound="RadListView1_ItemDataBound">                        
                        <LayoutTemplate>       
                             <table id="products" class="Edit_Events_Tickets">
                        <thead>
                            <tr>                                
                                <th style="width:130px;">
                                    Description
                                </th>
                                <th style="width:60px;">
                                    Sale Begins
                                </th>
                                <th style="width:60px;">
                                    Sale Ends
                                </th>
                                <th style="width:60px;">
                                    Price
                                </th>
                                <th style="width:60px;">
                                    Capacity
                                </th>
                                <th style="width:60px; text-align:center;">
                                Action      
                                </th>
                            </tr>
                        </thead>
                        <tbody >
                            <tr id="ProductTitlePlaceHolder" runat="server">
                            </tr>
                        </tbody>
                    </table>         
                            
                        </LayoutTemplate>            
                        <ItemTemplate>                                                            
                                <tr valign=top >                                    
                                    <td >
                                    <asp:label ID=lbldescription runat=server Text='<%#Eval("Ticket_Description")%>'/> </td>
                                    <td><asp:Label runat=server ID=lblSaleBegins Text='<%#Eval("Begin_Selling")%>'></asp:Label> </td>
                                    <td><asp:Label runat=server ID=lblSaleEnds Text='<%#Eval("Selling_Deadline")%>'></asp:Label> </td>
                                    <td><asp:Label runat=server ID=lblPrice Text='<%#Eval("PriceRounded","{0:0.00}")%>'></asp:Label> </td>
                                    <td><asp:label ID=lblcapacity runat=server Text='<%#Eval("Capacity")%>'/></td>
                                    <td class="Action_Background" style="text-align:center;"><asp:label ID=lblAction runat=server />
                                    <asp:label ID=lblticketkey runat=server Text='<%#Eval("Ticket_Key")%>' Visible=false />
                                    </td>
                                </tr>                            
                        </ItemTemplate>
                        </telerik:RadListView>
                </td>
            </tr>
            <tr>
                <td>
                <a href=#>     
        <asp:Label ID=lblAddNew runat=server Text="Add Ticket"  
        CssClass="QuestionsButton" ></asp:Label>  </a>    
        
                </td>
            </tr>
        
                
                    <tr>    
                        <td>Display how many tickets are available? <asp:CheckBox ID="chkDisplayAvailable" runat="server" /></td>
                    </tr>
                    <tr>
                        <td>Maximum amount of tickets can be purchased at a time<asp:DropDownList ID=ddlMaxTickets runat=server>                        
                                <asp:ListItem value="1">1</asp:ListItem>
                                <asp:ListItem value="2">2</asp:ListItem>
                                <asp:ListItem value="3">3</asp:ListItem>
                                <asp:ListItem value="4">4</asp:ListItem>
                                <asp:ListItem value="5">5</asp:ListItem>
                                <asp:ListItem value="6">6</asp:ListItem>
                                <asp:ListItem value="7">7</asp:ListItem>
                                <asp:ListItem value="8">8</asp:ListItem>
                                <asp:ListItem value="9">9</asp:ListItem>
                                <asp:ListItem value="10" selected="True">10</asp:ListItem>
                        </asp:DropDownList></td>
                    </tr>
                    <tr>    
                        <td><igsch:WebDateChooser ID="WebDateChooser2" runat="server"  Value="" Visible=false>
                                <DropButton ImageUrl1="./Images/WebDateChooser/datePickerImage.gif">
                                </DropButton>
                                <CalendarLayout NextMonthImageUrl="./Images/WebDateChooser/ig_cal_blueN0.gif" 
                                    PrevMonthImageUrl="./Images/WebDateChooser/ig_cal_blueP0.gif" 
                                    ShowMonthDropDown="False" ShowYearDropDown="False" TitleFormat="Month">
                                    <CalendarStyle BackColor="#E0EEFF" BorderColor="SteelBlue" BorderStyle="Solid" 
                                        BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Verdana" 
                                        Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" 
                                        Font-Underline="False">
                                    </CalendarStyle>
                                    <DayHeaderStyle BackColor="#7AA7E0" Font-Bold="True" Font-Size="8pt" 
                                        ForeColor="#606090" Height="1pt">
                                    <BorderDetails ColorBottom="LightSteelBlue" StyleBottom="Solid" 
                                        WidthBottom="1px" />
                                    </DayHeaderStyle>
                                    <DayStyle BackColor="#7AA7E0" />
                                    <NextPrevStyle BackgroundImage="../images/ig_cal_blue1.gif" />
                                    <OtherMonthDayStyle ForeColor="SlateGray" />
                                    <SelectedDayStyle BackColor="SteelBlue" />
                                    <TitleStyle BackColor="#7AA7E0" BackgroundImage="ig_cal_blue1.gif" 
                                        Font-Bold="True" Font-Size="10pt" ForeColor="#505080" Height="18pt" />
                                    <TodayDayStyle BackColor="#97B0F0" />
                                    <FooterStyle BackgroundImage="ig_cal_blue2.gif" Font-Size="8pt" 
                                        ForeColor="#505080" Height="16pt">
                                    <BorderDetails ColorTop="LightSteelBlue" StyleTop="Solid" WidthTop="1px" />
                                    </FooterStyle>
                                </CalendarLayout>
                            </igsch:WebDateChooser></td>
                    </tr>
                </table>
                
                </ContentTemplate>
                </igtab:Tab>
              
                    <igtab:Tab Text="Questions">
                        <ContentTemplate>
                                    <div style="width:500px;">
                               <uc2:Questions ID="Questions1" runat="server" Visible=false/>
                                    
                                    <table>
                                    <tr>
                                        <td>
                                        <telerik:RadListView ID="RadListView2" runat="server" Width=500
                        ItemPlaceholderID="ProductTitlePlaceHolder" OnItemDataBound="RadListView2_ItemDataBound">                        
                        <LayoutTemplate>       
                             <table id="products" class="Edit_Events_Tickets">                        
                        <tbody >
                            <tr id="ProductTitlePlaceHolder" runat="server">
                            </tr>
                        </tbody>
                    </table>         
                            
                        </LayoutTemplate>            
                        <ItemTemplate>                                                            
                                <tr valign=top >                                    
                                    <td >
                                    <asp:Label ID=lblQuestionsMandatory runat=server ForeColor=Red Text="*" Visible=false></asp:Label>
                             <asp:Label ID=lblQuestion_Text Text='<%#Eval("The_Question")%>' runat=server CssClass="Each_Question"></asp:Label>:<br />
                             <asp:TextBox ID=txtQuestionsAnswer runat=server Width=400></asp:TextBox>
                             <asp:DropDownList ID=ddlQuestionsAnswer runat=server Visible=false></asp:DropDownList>                                                                 
                             <asp:Label ID=lblQuestion_Type runat=server Text='<%#Eval("Question_Type")%>' Visible=false/>
                             <asp:Label ID=lblMandatory runat=server Text='<%#Eval("Mandatory")%>' Visible=false/>
                             <asp:Label ID=lblquestionkey runat=server Text='<%#Eval("Question_Key")%>' Visible=false/>
                                    </td>
                                    <td class="Action_Background" style="text-align:center;"><asp:label ID=lblQuestionAction runat=server />
                                    </td>
                                </tr>                            
                        </ItemTemplate>
                        </telerik:RadListView>
                                        
                                        </td>
                                    </tr>
                                    
        <tr>
            <td>
                   <a href='#'>                                    
        <asp:Label ID=lblAddnewQuestion runat=server Text="Add Question" CssClass="QuestionsButton"/>
                                    </a>
            </td>
        </tr>                     
                                    <tr>
        <td style="font-size:smaller;"><asp:Label ID=lblPleaseNote runat=server>*Please note that First name & Last name are already being asked for.</asp:Label></td>
    </tr>
</table>
</div>
                               </ContentTemplate>
                               </igtab:Tab>
                    
                    <igtab:Tab Text="Confirmation" >
                        <ContentTemplate>Type in the message below your customers recieve after they have purchased their tickets.<br /><asp:TextBox ID="txtConfirmation" runat="server" TextMode=MultiLine Width="450"></asp:TextBox>
                        </ContentTemplate>
                        </igtab:Tab>
                        <igtab:Tab Text="Fundraiser" Visible=false >
                        <ContentTemplate>Is this a fundraising campaign? <asp:CheckBox ID=chkfundraiser runat=server /><br />
                        Checking the box, changes the email sent out to the buyer. Instead of it saying you have bought a ticket it says you have made a donation.
                        </ContentTemplate>
                        </igtab:Tab>
                         </Tabs>
                         <SelectedTabStyle CssClass="EditEvent_SelectedTab" BorderStyle="None">
                </SelectedTabStyle>                
                <DefaultTabStyle CssClass="EditEvent_UnSelectedTab" BorderStyle="None"></DefaultTabStyle>
                         </igtab:UltraWebTab>           
                        </td>
    </tr>   
    <tr>
        <td colspan="2" align="center"><asp:Button ID="btnSave" runat="server" 
                Text="Save Changes" onclick="btnSave_Click" /></td>
    </tr>    
</table>
<table width=100% height=300px>
    <tr>
        <td>&nbsp;</td>
    </tr>
</table>
<script language=javascript>
    function doRemoveTicket(ticketid){
    var answer = confirm("Are you sure you want to Remove the Ticket?")
	if (answer){
		__doPostBack('doRemoveTicket',ticketid);
	}
	else{
	//
	}
}

function doRemoveQuestion(ticketid) {
    var answer = confirm("Are you sure you want to Remove the Question?")
    if (answer) {
        __doPostBack('doRemoveQuestion', ticketid);
    }
    else {
        //
    }
}

function PromptSaved() {
    alert('Your Changes have been Saved'); location.href = 'Default.aspx';
}

</script>
</telerik:RadAjaxPanel>


</asp:Content>
