<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayForward_FirstTime.aspx.cs" Inherits="Eventomatic.PayForward_FirstTime" MasterPageFile="~/Snappay_Promo.Master"%>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID=content1 ContentPlaceHolderID=body runat=server>

<center>

<asp:HiddenField ID=hdfbid runat=server Value="0"/>
    <asp:HiddenField ID=hdresource_key runat=server Value="0"/>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">   
    <script type="text/javascript">
        function pageLoad(sender, eventArgs) {
            if (!eventArgs.get_isPartialLoad()) {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("InitialPageLoad");
            }
        }      
</script>   
</telerik:RadCodeBlock>   
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>                    
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnNext1">
                <UpdatedControls>                
                    <telerik:AjaxUpdatedControl ControlID="hdfbid" />
                    <telerik:AjaxUpdatedControl ControlID="hdresource_key" />                    
                    <telerik:AjaxUpdatedControl ControlID="RadTabStrip1" />                    
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>                                                
            <telerik:AjaxSetting AjaxControlID="btnNext2">
                <UpdatedControls>                
                    <telerik:AjaxUpdatedControl ControlID="hdfbid" />
                    <telerik:AjaxUpdatedControl ControlID="hdresource_key" />                    
                    <telerik:AjaxUpdatedControl ControlID="RadTabStrip1" />                    
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnNext3">
                <UpdatedControls>                
                    <telerik:AjaxUpdatedControl ControlID="hdfbid" />
                    <telerik:AjaxUpdatedControl ControlID="hdresource_key" />                    
                    <telerik:AjaxUpdatedControl ControlID="RadTabStrip1" />                    
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnNext4">
                <UpdatedControls>                
                    <telerik:AjaxUpdatedControl ControlID="hdfbid" />
                    <telerik:AjaxUpdatedControl ControlID="hdresource_key" />                    
                    <telerik:AjaxUpdatedControl ControlID="RadTabStrip1" />                    
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnCreatePP">
                <UpdatedControls>                
                    <telerik:AjaxUpdatedControl ControlID="pnlcreatePayPal" />                    
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBillingBack">
                <UpdatedControls>                                    
                    <telerik:AjaxUpdatedControl ControlID="RadTabStrip1" />                    
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1" />                    
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnBillingNext">
                <UpdatedControls>                                    
                    <telerik:AjaxUpdatedControl ControlID="RadTabStrip1" />                    
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1" />                    
                    <telerik:AjaxUpdatedControl ControlID="chkTOS" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlCountry">
                <UpdatedControls>                
                    <telerik:AjaxUpdatedControl ControlID="pnlcreatePayPal" />                    
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>                                  
            <telerik:AjaxSetting AjaxControlID="ddlBizCountry">
                <UpdatedControls>                
                    <telerik:AjaxUpdatedControl ControlID="pnlcreatePayPal" />                    
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>                          
            <telerik:AjaxSetting AjaxControlID="ddlBizCategory">
                <UpdatedControls>                
                    <telerik:AjaxUpdatedControl ControlID="pnlcreatePayPal" />                    
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="ddlBizSubCategory" />                                        
                </UpdatedControls>
            </telerik:AjaxSetting>                          
            <telerik:AjaxSetting AjaxControlID="ddlPlan">
                <UpdatedControls>                
                    <telerik:AjaxUpdatedControl ControlID="pnldodirectpayment" />                    
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>                                  
            
            
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">  
</telerik:RadAjaxLoadingPanel> 



            <center>
            <table><tr><td>
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Vista" MultiPageID="RadMultiPage1"
                SelectedIndex="0" Align="Justify" ReorderTabsOnSelect="true" Width="600px" >
                <Tabs>
                    <telerik:RadTab Text="(1) PayPal">
                    </telerik:RadTab>
                    <telerik:RadTab Text="(2) Choose Plan" Enabled=false Visible=false>
                    </telerik:RadTab>
                    <telerik:RadTab Text="(3) Billing" Enabled=false Visible=false>
                    </telerik:RadTab>                    
                    <telerik:RadTab Text="(2) Settings" Enabled=false>
                    </telerik:RadTab>                    
                    <telerik:RadTab Text="(3) Begin Selling" Enabled=false>
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            
            <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" CssClass="pageView"
                Width="600px" style="background-color:White;">
                <telerik:RadPageView ID="RadPageView1" runat="server" BorderWidth=1>
                    <center>All the money gets deposited into a PayPal account
                    <br />Choose which PayPal account you want to connect:</center>
                    <br />
                    <br />
                    
                    <table width=100%>
                        <tr valign=top>
                            <td width=50% style="text-align:center;">1. Connect an existing PayPal account <br />                            
                            <asp:Button runat=server ID="btnNext2" Text="Connect PayPal" onclick="btnNext2_Click" CausesValidation=false /><br />
                            <span style=" color:Blue; font-size:smaller;">*You must have a PayPal Premier or PayPal Business account to proceed*</span>
                            </td>
                        
                            <td style="border-left:1px solid black;text-align:center;" width=50%>
                            2. Create & Connect a new PayPal account
                            <br />
                            <asp:Button runat=server ID="btnCreatePP" Text="Create PayPal" OnClick="btnCreatePP_Click" CausesValidation=false/>
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <center><a href="#" onclick="changeaccounttypedisplay()">How do I know what type of PayPal account I have?</a></center>
                    <div id="Account_Type" style="display:none;">
                    <br />                    
                    1. Log into your PayPal account<br />
                    2. You will see "Account type"<br />
                    3. If you have a Personal account then please upgrade it to a Premier or Business account.<br />
                    <a href="http://www.PayPal.com" target="_blank">Click here </a> to check your PayPal Account                    
                    <br />
                    <br />
                    <img src="/images/PayPal_Find_Accounttype.jpg" />
                    </div>
                    <asp:Panel ID=pnlcreatePayPal runat=server Visible=false>
                    <table align="center">	
					<tr>
						<td class="First_Time_Big_Questions">Personal Info</td>
					</tr>					
					<tr>
						<td class="label">Salutation</td>
						<td>
                        <telerik:RadComboBox ID=ddlSalutation runat=server>
                           <Items>                            
						    <telerik:RadComboBoxItem value="Mr." Text="Mr." Selected=True></telerik:RadComboBoxItem>
							<telerik:RadComboBoxItem value="Mrs." Text="Mrs."></telerik:RadComboBoxItem>
                            <telerik:RadComboBoxItem Value="Dr." Text="Dr." ></telerik:RadComboBoxItem>
                           </Items>
                        </telerik:RadComboBox>
                        </td>
					</tr>
					<tr>
                    <td class="label">
*First Name<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName"
ErrorMessage="First Name is required." Enabled=false>*</asp:RequiredFieldValidator>
</td>
	 <td>
     <telerik:RadTextBox Width="300px" ID="txtFirstName" runat="server"
                                EmptyMessage="John" >                                
                            </telerik:RadTextBox>
     </td>             
						
					</tr>
					<tr>
						<td class="label">Middle Name</td>
						<td><telerik:RadTextBox id="txtMiddleName" Width=300px
								runat="server" EmptyMessage="Jacob"/></td>
					</tr>
					<tr>
                    <td class="label">
                        *Last Name<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLastName"
                        ErrorMessage="Last Name is required." Enabled=false>*</asp:RequiredFieldValidator>
                        </td>
	                         <td>
                             <telerik:RadTextBox Width="300px" ID="txtLastName" runat="server"
                                                        EmptyMessage="Doe" >                                
                                                    </telerik:RadTextBox>
                             </td>             
					</tr>
					<tr>
						<td class="label">*Date of Birth</td>
						<td>
                            <telerik:RadDatePicker ID=DOB runat=server DateInput-EmptyMessage="05/10/1970" />
                        </td>
					</tr>
                    <tr>
						<td class="label">*Country</td>
						<td>
                        <telerik:RadComboBox ID=ddlCountry runat=server AutoPostBack=true 
                                onselectedindexchanged="ddlCountry_SelectedIndexChanged" CausesValidation=false>
                           <Items>                            
						    <telerik:RadComboBoxItem value="CA" Text="Canada"></telerik:RadComboBoxItem>
							<telerik:RadComboBoxItem value="US" Text="US"></telerik:RadComboBoxItem>
                           </Items>
                        </telerik:RadComboBox>
                        </td>
					</tr>
					<tr>
						<td class="label">*Address 1<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAddress1"
                        ErrorMessage="Personal Address is required." Enabled=false>*</asp:RequiredFieldValidator>
                        </td>
						<td>
                            <telerik:RadTextBox Width="300px" ID="txtAddress1" runat="server"
                                                        EmptyMessage="200 Main Street" >                                
                                                    </telerik:RadTextBox>
                        </td>
					</tr>
					<tr>
						<td class="label">Address 2</td>
						<td>
                            <telerik:RadTextBox Width="300" ID="txtAddress2" runat=server />
                        </td>
					</tr>
					<tr>
						<td class="label">*City<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCity"
                        ErrorMessage="Personal City is required." Enabled=false>*</asp:RequiredFieldValidator></td>
						<td>
                            <telerik:RadTextBox Width="300px" ID="txtCity" runat="server"
                                                        EmptyMessage="Toronto" />    
                        </td>
					</tr>
					<tr>
						<td class="label">*<asp:Label ID=lblStateProvince runat=server>Province</asp:Label>                        
                        </td>
						<td>
                            <telerik:RadComboBox ID=ddlProvince runat=server>
                            </telerik:RadComboBox>                        
                        </td>
					</tr>
					<tr>
						<td class="label">*<asp:Label ID=lblAreaZipCode runat=server>Postal Code</asp:Label></td>
						<td>
                            <telerik:RadTextBox ID=txtAreaZipCode runat=server Width=300 EmptyMessage="A6B 4J7"></telerik:RadTextBox>
                        </td>
					</tr>					
					<tr>
						<td class="label">*Country Citizenship</td>
						<td>
                         <telerik:RadComboBox ID=ddlCountryCitizenship runat=server>
                           <Items>                            
						    <telerik:RadComboBoxItem value="CA" Text="Canada"></telerik:RadComboBoxItem>
							<telerik:RadComboBoxItem value="US" Text="US"></telerik:RadComboBoxItem>
                           </Items>
                        </telerik:RadComboBox>
                        </td>
					</tr>
					<tr>
						<td class="label">*Contact Phone Number
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPhone1"
                        ErrorMessage="Personal Phone Number is required." Enabled=false>*</asp:RequiredFieldValidator>
                        </td>
						<td>
                            <telerik:RadMaskedTextBox ID=txtPhone1 runat=server Mask="(###) ###-####"></telerik:RadMaskedTextBox>
                        </td>
					</tr>					
					<tr>
						<td class="label">*Desired Currency</td>
						<td>
                        <telerik:RadComboBox ID=ddlCurrencyCode runat=server >
                                <Items>                                    
                                    <telerik:RadComboBoxItem Value="CAD" Text="CAD $" />                                    
                                    <telerik:RadComboBoxItem Value="USD" Text="USD $" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
					</tr>					
					<tr>
						<td class="label">*Email
                        <asp:RegularExpressionValidator Display="Dynamic" ID="rqPayPal" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
runat="server" ControlToValidate="txtEmail1" ErrorMessage="Valid E-mail address is required." Enabled=false>*</asp:RegularExpressionValidator>                        
                        </td>
						<td>
                            <telerik:RadTextBox Width="300px" ID="txtEmail1" runat="server"
                                EmptyMessage="paypal@email.com" />
                        </td>
					</tr>
					
	   <tr>
	            <td class="First_Time_Big_Questions">
                <br />
                Business Info</td>
	   </tr>
	   
				
	<tr>
	<td class="label">    
*Business Name<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBizName"
ErrorMessage="Store Name is required." Enabled=false>*</asp:RequiredFieldValidator>
</td>
	 <td>
     <telerik:RadTextBox Width="300px" ID="txtBizName" runat="server"
                                EmptyMessage="Johnies Jewelery" >                                
                            </telerik:RadTextBox>
     </td>             
                            
	</tr>

        <tr>
	<td class="label">*Business Address1<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtBizAddress1"
ErrorMessage="Business Address is required." Enabled=false>*</asp:RequiredFieldValidator></td>

	 <td>
        <telerik:RadTextBox Width="300px" ID="txtBizAddress1" runat="server"
                                EmptyMessage="200 Main Street" >                                
                            </telerik:RadTextBox>
     </td>
</tr>

        <tr>
	<td class="label">Business Address2</td>
	 <td>
        <telerik:RadTextBox Width="300px" ID="txtBizAddress2" runat="server">                                
                            </telerik:RadTextBox>
     </td>
</tr>

        <tr>
	<td class="label">*City<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtBizCity"
                        ErrorMessage="Business City is required." Enabled=false>*</asp:RequiredFieldValidator></td>
						<td>
                            <telerik:RadTextBox Width="300px" ID="txtBizCity" runat="server"
                                                        EmptyMessage="Toronto" />    
                        </td>
	
</tr>

        <tr>
	                <td class="label">*<asp:Label ID=lblBizProvince runat=server>Province</asp:Label>                        
                        </td>
						<td>
                            <telerik:RadComboBox ID=ddlBizProvince runat=server>
                            </telerik:RadComboBox>                        
                        </td>
</tr>

        <tr>
	            <td class="label">*<asp:Label ID=lblBizAreaZipCode runat=server>Postal Code</asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtBizAreaZipCode"
                        ErrorMessage="Business Area/Zip Code is required." Enabled=false>*</asp:RequiredFieldValidator>
                </td>
						<td>
                            <telerik:RadTextBox ID=txtBizAreaZipCode runat=server Width=300 EmptyMessage="A6B 4J7"></telerik:RadTextBox>
                        </td>
</tr>

        <tr>
	    <td class="label">*Country</td>
						<td>
                        <telerik:RadComboBox ID=ddlBizCountry runat=server 
                                onselectedindexchanged="ddlBizCountry_SelectedIndexChanged" AutoPostBack=true CausesValidation=false>
                           <Items>                            
						    <telerik:RadComboBoxItem value="CA" Text="Canada"></telerik:RadComboBoxItem>
							<telerik:RadComboBoxItem value="US" Text="US"></telerik:RadComboBoxItem>
                           </Items>
                        </telerik:RadComboBox>
                        </td>
</tr>
   
     <tr>
	<td class="label">*Business Phone Number
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtBizPhone"
                        ErrorMessage="Business Phone Number is required." Enabled=false>*</asp:RequiredFieldValidator>
                        </td>
						<td>
                            <telerik:RadMaskedTextBox ID=txtBizPhone runat=server Mask="(###) ###-####"></telerik:RadMaskedTextBox>
                        </td>
</tr>

        <tr>
	<td class="label">Category</td>
	 <td>
     <table>
        <tr>
            <td>
                <telerik:RadComboBox ID=ddlBizCategory runat=server AutoPostBack=true CausesValidation=false
                onselectedindexchanged="ddlBizCategory_SelectedIndexChanged">
                                <Items>                                    
                                    <telerik:RadComboBoxItem Value="1000" Text="Arts, crafts, and collectibles" />                                    
                                    <telerik:RadComboBoxItem Value="1001" Text="Baby" />
                                    <telerik:RadComboBoxItem Value="1002" Text="Beauty and fragrances" />
                                    <telerik:RadComboBoxItem Value="1003" Text="Books and magazines" />
                                    <telerik:RadComboBoxItem Value="1004" Text="Business to business" />
                                    <telerik:RadComboBoxItem Value="1005" Text="Clothing, accessories, and shoes" />
                                    <telerik:RadComboBoxItem Value="1006" Text="Computers, accessories, and services" />
                                    <telerik:RadComboBoxItem Value="1007" Text="Education" />
                                    <telerik:RadComboBoxItem Value="1008" Text="Electronics and telecom" />
                                    <telerik:RadComboBoxItem Value="1009" Text="Entertainment and media" />
                                    <telerik:RadComboBoxItem Value="1010" Text="Financial services and products" />
                                    <telerik:RadComboBoxItem Value="1011" Text="Food retail and service" />
                                    <telerik:RadComboBoxItem Value="1012" Text="Gifts and flowers" />
                                    <telerik:RadComboBoxItem Value="1013" Text="Government" />
                                    <telerik:RadComboBoxItem Value="1014" Text="Health and personal care" />
                                    <telerik:RadComboBoxItem Value="1015" Text="Home and garden" />
                                    <telerik:RadComboBoxItem Value="1016" Text="Nonprofit" />
                                    <telerik:RadComboBoxItem Value="1017" Text="Pets and animals" />
                                    <telerik:RadComboBoxItem Value="1018" Text="Religion and spirituality (for profit)" />
                                    <telerik:RadComboBoxItem Value="1019" Text="Retail (not elsewhere classified)" />
                                    <telerik:RadComboBoxItem Value="1020" Text="Services - other" />
                                    <telerik:RadComboBoxItem Value="1021" Text="Sports and outdoors" />
                                    <telerik:RadComboBoxItem Value="1022" Text="Toys and hobbies" />
                                    <telerik:RadComboBoxItem Value="1023" Text="Travel" />
                                    <telerik:RadComboBoxItem Value="1024" Text="Vehicle sales" />
                                    <telerik:RadComboBoxItem Value="1025" Text="Vehicle service and accessories" />
                                </Items>
                            </telerik:RadComboBox>
            </td>
            <td>
                
                    <telerik:RadComboBox ID=ddlBizSubCategory runat=server>
                    </telerik:RadComboBox>
                
            </td>
        </tr>
     </table>
                        
     </td>
</tr>     
        <tr>
	<td class="label">Customer Service Phone    
                        </td>
						<td>
                            <telerik:RadMaskedTextBox ID=txtBizCustomerPhone runat=server Mask="(###) ###-####"></telerik:RadMaskedTextBox>
                        </td>
	
</tr>

        <tr>
	<td class="label">*Customer Service Email
    <asp:RegularExpressionValidator Display="Dynamic" ID="RequiredFieldValidator11" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
runat="server" ControlToValidate="txtBizCustomerEmail" ErrorMessage="Business Customer Service Email is required." Enabled=false>*</asp:RegularExpressionValidator>                        
    
                        </td>
	   <td>
       <telerik:RadTextBox ID=txtBizCustomerEmail runat=server EmptyMessage="info@yourbiz.com"></telerik:RadTextBox>
       
       </td>

</tr>
     <tr>
	<td class="label">Website    
    </td>
	<td>
        <telerik:RadTextBox ID=txtBizWebsite runat=server EmptyMessage="www.yourbiz.com"></telerik:RadTextBox>
    </td>
</tr>

<tr>
	<td class="label">*Date Of Establishment</td>
	<td>
        <telerik:RadDatePicker ID=txtBizDate runat=server DateInput-EmptyMessage="05/10/2008"/>
    </td>
</tr>

   <tr>
	<td class="label">Business Type</td>
	<td>
        <telerik:RadComboBox id="ddlBusinessType" runat="server" >			
            <Items>
                <telerik:RadComboBoxItem value="CORPORATION" Text=CORPORATION />
			    <telerik:RadComboBoxItem value="GOVERNMENT" Text=GOVERNMENT />
			    <telerik:RadComboBoxItem value="INDIVIDUAL" selected=true Text=INDIVIDUAL />
			    <telerik:RadComboBoxItem value="NONPROFIT" Text=NONPROFIT />
			    <telerik:RadComboBoxItem value="PARTNERSHIP" Text=PARTNERSHIP />
			    <telerik:RadComboBoxItem value="PROPRIETORSHIP" Text=PROPRIETORSHIP />
            </Items>			
		</telerik:RadComboBox>
	</td>
</tr>
     <tr>
		<td class="label">Average Transaction Price</td>
		<td>
            <telerik:RadNumericTextBox ID=txtBizAvgTx runat=server NumberFormat-DecimalDigits="2" NumberFormat-PositivePattern="$ n" Value="0"></telerik:RadNumericTextBox>
        </td>
	</tr>
	<tr>
		<td class="label">Average Monthly Volume</td>
		<td>
        <telerik:RadNumericTextBox ID=txtBizAvgMonthly NumberFormat-DecimalDigits="2" NumberFormat-PositivePattern="$ n" runat=server Value="0"></telerik:RadNumericTextBox>
        </td>
	</tr>
    <tr>
        <td class="label">Percentage Revenue from Online</td>
	    <td>
            <telerik:RadNumericTextBox ID=txtpercentonline NumberFormat-DecimalDigits="0" NumberFormat-PositivePattern="n %" runat=server Value="50"></telerik:RadNumericTextBox>
	    </td>
    </tr>
    <tr valign=top>
        <td class="label">Sales Venue</td>
	    <td>
            <telerik:RadComboBox id="ddlsalesvenue" runat="server" AutoPostBack=true CausesValidation=false>			
                <Items>
                    <telerik:RadComboBoxItem value="OTHER" selected=true Text="Other" />
                    <telerik:RadComboBoxItem value="WEB" Text="Web" />
			        <telerik:RadComboBoxItem value="EBAY" Text="Ebay" />
			        <telerik:RadComboBoxItem value="OTHER_MARKETPLACE"  Text="Other Marketplace />			        
                </Items>			
		    </telerik:RadComboBox>            
            <br />
            If other, describe where you sell:
            <br />            
            <telerik:RadTextBox ID=txtsalesvenuedesc runat=server Width=300 EmptyMessage="Trade Shows"></telerik:RadTextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtsalesvenuedesc"
                        ErrorMessage="Sales Venue description is required" Enabled=false>*</asp:RequiredFieldValidator>
	    </td>
    </tr>
					<tr>                    
						<td colspan=2 class=submit align=center>
									<asp:Button runat=server ID="btnCreatePayPal" Text="Create PayPal Account" onclick="btnCreatePayPal_Click" CausesValidation=true/>
                                    <br />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Width="400px" BorderColor="Red" BorderWidth="1px" HeaderText="List of errors" />							
						</td>
					</tr>
				</table>
                    </asp:Panel>
                    <br />
                    <br />

                    <!--
                    <b>Groupstore will never touch your money</b>, it will always be deposited directly into your PayPal account.
                    <br />
                    You have the ability to remove these permissions at anytime through PayPal.
                    <br />
                    <br />
                    
                    <br />
                    <br />
                    You will be redirected to PayPal.-->
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView2" runat="server" BorderWidth=1>
                    <table width=100%>
                        <tr>
                            <td class=First_Time_Big_Questions>Choose your Plan</td>
                        </tr>                        
                        <tr>
                            <td>
                            <telerik:RadComboBox ID=ddlPlan runat=server Label="Choose Plan" AutoPostBack=true 
                            onselectedindexchanged="ddlPlan_SelectedIndexChanged">
                                <Items>
                                    <telerik:RadComboBoxItem Value="0" Text="Pay as you Go" Selected=true/>
                                    <telerik:RadComboBoxItem Value="1" Text="High Volume" />                                    
                                </Items>
                            </telerik:RadComboBox>
                            <span style="font-size:smaller;"><a href="http://www.snap-pay.com/site/pricing.aspx" target="_blank">See Plan Details</a></span>
                            </td>
                        </tr>   
                        <tr>
                            <td>
                            <div style="color:Blue; font-size:small;">
                                Pricing Schedule:
                            </div>
                            <div style="font-size:small;">
                                <asp:Label ID=lblSchedulePayasyougo runat=server >
                                    The transaction fees will be charged at the time you complete a transaction.
                                </asp:Label>
                                <asp:Label ID=lblScheduleHighVolume runat=server Visible=false>
                                    Snappay's monthly fee of ## will be charged on a monthly billing cycle beginning immediatly after you complete this billing agreement.
                                    <br />You will be able to cancel this plan without penalty at any time.
                                </asp:Label>
                            </div>
                            </td>
                        </tr> 
                        <tr>
                            <td>
                                <br />
                                <asp:Label ID=lblnotverified runat=server Visible=false Font-Size=Smaller><div style="color:Blue;">Warning: Your PayPal account is currently unverified.</div>
                                 As long as your PayPal account is unverified, your customers will be required to create a PayPal account when completing a transaction.
                                 <br />Once your account is verified, the above requirement will be removed.
                                 <br />A link will be provided at the end of this signup to Verify your PayPal account.</asp:Label>
                                <br />                          
                                <asp:Panel ID=pnldodirectpayment runat=server Visible=false>
                                    <asp:Label ID=lbldodirectpayment runat=server Visible=false Font-Size=Smaller><div style="color:Blue;">Warning: Your PayPal account does not have Website Payments Pro Activated.</div>
                                        Website Payments Pro (WPP) is optional, and is only required to ensure a faster checkout experience.<br />
                                        Without WPP you will need to collect your customers home billing address to complete the transaction.
                                        <br />A link will be provided at the end of this signup if you wish to apply for WPP.
                                     </asp:Label>
                                 </asp:Panel>      
                            </td>
                        </tr>   
                        <tr>
                            <td>
                                <asp:Label ID=lblresult runat=server></asp:Label>
                            </td>
                        </tr>                                         
                        <tr>
                            <td style="text-align:right;">
                            <asp:Button runat=server ID="btnNext1" Text="Next - Billing" onclick="btnNext1_Click" />                            
                            </td>
                        </tr>                        
                    </table>                   
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView3" runat="server" BorderWidth=1>
                    <table width=100%>
                        <tr>
                            <td class="First_Time_Big_Questions">Billing Agreement</td>
                        </tr>                        
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td><b>Pay Schedule</b>
                                        <br />
                                        This billing agreement allows Snappay to collect payment from your PayPal account when you use Snappay services.  Below outlines when Snappay will collect payment.                                                                                
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Pay as you go Plan: Snappay will charge you a service fee each time a transaction is completed. The service fee is 1% of the amount you charge your customer.</td>
                                    </tr>
                                    <tr>
                                        <td>High Volume Plan: Snappay will charge you service fee on a monthly basis. This month will begin on the day you perform your first transaction.  You will not be charged until you complete your first transaction.</td>
                                    </tr>
                                    <tr>
                                        <td style="font-size:smaller;">
                                            Please note that all PayPal service fees are completely separate from Snappay's service fees. You can at any time cancel your Billing Agreement which will also disable your Snappay service.  This is no charge or penalty for cancelling your service.
                                            <br />For more details, click here to visit the faqs section
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width=100%>
                                    <tr>
                                        <td colspan=2>
                                            <table>
                                                <tr>
                                                    <td><asp:CheckBox ID=chkTOS runat=server Checked=false /></td>
                                                    <td>I have read and accept the Snappay <a href=Snappay_TOS.html>Terms of Service</a></td>
                                                </tr>
                                                <tr>
                                                    <td colspan=2><asp:Label runat=server ID=lbltoserror Visible=false ForeColor=Red Font-Size=Smaller>Please agree to the Snappay Terms of Service to continue</asp:Label></td>
                                                </tr>
                                            </table>                                                                                
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><asp:Button runat=server ID="btnBillingBack" Text="Back" onclick="btnBillingBack_Click" /></td>
                                        <td style="text-align:right;"><asp:Button runat=server ID="btnBillingNext" Text="Confirm Billing Agreement" onclick="btnBillingNext_Click" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView ID="RadPageView4" runat="server" BorderWidth=1>
                    <table width=100%>
                        <tr>
                            <td class="First_Time_Big_Questions"><b>Choose your Settings</b></td>
                        </tr>
                        <tr>
                            <td><br /><br />
                                 <asp:Label ID=lblstorenameReq runat=server>*</asp:Label><telerik:RadTextBox ID=txtStoreName2 runat=server Label="Business Name" EmptyMessage="Jewelery Makers Inc" Width=300></telerik:RadTextBox>
                            </td>
                        </tr>                        
                        <tr>
                            <td>
                                <telerik:RadComboBox ID=ddlCurrency runat=server Label="Desired Currency">
                                <Items>
                                    <telerik:RadComboBoxItem Value="CAD" Text="CAD" Selected=true/>
                                    <telerik:RadComboBoxItem Value="USD" Text="USD" />                                    
                                </Items>
                            </telerik:RadComboBox>
                            </td>
                        </tr>                        
                        <tr >
                            <td style="vertical-align:top;">
                                <telerik:RadTextBox ID=txtReceipt runat=server Label="e-Receipt" EmptyMessage="Thank you for your purchase.  We will accept refunds up to 14 days after purchase.  Please call 1-800-555-555 for questions." TextMode=MultiLine Width=300 MaxLength=500 Rows=3></telerik:RadTextBox>                            
                                <br />
                               <span style=" font:smaller;">*Your customers will receive their e-receipt immediately after a purchase.</span>
                               <br /><asp:Label ID=lblerror runat=server ForeColor=Red Visible=false>Please enter a Business Name</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">
                            <asp:Button runat=server ID="btnNext3" Text="Begin Selling" onclick="btnNext3_Click" />                            
                            </td>
                        </tr>                        
                    </table>
                </telerik:RadPageView>                
                <telerik:RadPageView ID="RadPageView5" runat="server" BorderWidth=1>
                    <table width=100%>
                        <tr>
                            <td class="First_Time_Big_Questions"><b>Begin Selling on your Phone</b></td>
                        </tr>
                        <tr>
                            <td ><br /><br />That's it! Your account is all setup and you can begin accepting payment right now.  <br /><br />
                            Enter below the number to your smartphone, and we'll send you an sms on how to download the mobile app.</td>
                        </tr>
                        <tr>
                            <td style="text-align:center;">
                             <asp:Label ID=lblsmartphonereq runat=server>*</asp:Label>
                             <telerik:RadMaskedTextBox ID="txtsmartphonenum" Label="Smartphone Number" runat=server Mask="(###) ###-####" Width=200></telerik:RadMaskedTextBox>                            
                             <asp:Button runat=server ID="btnNext4" Text="Get Mobile App" onclick="btnNext4_Click" />                            
                             <br />
                             <br />
                             <center> <a href="http://itunes.apple.com/app/snappay/id492807738?mt=8&ls=1"  target=_blank><img src="/images/appstore.png" width="120" /></a></center>
                            </td>
                        </tr>                        
                        <tr>
                            <td style="text-align:right;">
                            
                            </td>
                        </tr>                            
                        <tr>
                            <td>
                                <asp:Label id=lblVerifyLink runat=server Visible=false>
                                    <div style="color:Blue;">Warning: Your PayPal account is currently unverified.</div>
                                    As long as your PayPal account is unverified, your customers will be required to create a PayPal account when completing a transaction.
                                    <br />Once your account is verified, the above requirement will be removed.
                                    <br />
                                    <a href="https://www.paypal.com/cgi-bin/webscr?cmd=p/acc/seal-CA-unconfirmed-outside" target=_blank>Click here</a> to verify your PayPal account.
                                    <br />
                                </asp:Label>
                                <asp:Label ID=lblWPPLink runat=server Visible=false>
                                    <div style="color:Blue;">Warning: Your PayPal account does not have Website Payments Pro Activated.</div>
                                    Website Payments Pro (WPP) is optional, and is only required to ensure a faster checkout experience.<br />
                                    Without WPP you will need to collect your customers home billing address to complete the transaction.
                                    <br /><a href="##" target=_blank>Click here to activate Website Payments Pro</a>                                    

                                </asp:Label>
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>                               
            </telerik:RadMultiPage>
            </td></tr></table>
            </center>
    
 <script type="text/javascript" charset="utf-8">        

        function changeaccounttypedisplay() {
            if (document.getElementById("Account_Type").style.display == "none") {
                document.getElementById("Account_Type").style.display = "block";
            }
            else {
                document.getElementById("Account_Type").style.display = "none";
            }
        }

        
</script>

</center>
</asp:Content>