<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FacebookEvents.aspx.cs" Inherits="Eventomatic.Addons.FacebookEvents" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sell Event</title>
    <script src="../Scripts.js" language="javascript" type="text/javascript"></script>
    <link href="../Eventomatic.css" media="screen" rel="stylesheet" type="text/css" />  
</head>
<body onload="AdjustRadWidow();">
    <form id="Form2" method="post" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat=server>
    <script type="text/javascript">
    
        function returnToParent(eid)
        {
            //create the argument that will be returned to the parent page
            var oArg = new Object();

            //get the city's name 
            oArg.eid = eid;         

            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();

            //Close the RadWindow and send the argument to the parent page
            oWnd.close(oArg);
        }

        function returnToParent2(url, resourcekey, prod) {
            //create the argument that will be returned to the parent page            
            var oArg = new Object();

            //get the city's name 
            oArg.url = url;
            oArg.prod = prod;
            oArg.resourcekey = resourcekey;

            //get a reference to the current RadWindow
            var oWnd = GetRadWindow();

            //Close the RadWindow and send the argument to the parent page
            oWnd.close(oArg);
        }        
    </script>
    <div class="FacebookEvents">
    <table>
        <tr>
            <td colspan=3 style="text-align:center;"><asp:HyperLink id=hypnotfb runat=server Text="Click here to sell an event not on Facebook" Target="_blank"></asp:HyperLink></td>
        </tr>
            <telerik:RadListView ID="RadListView1" runat="server" Width="300"
             ItemPlaceholderID="ListViewContainer" OnItemDataBound="RadListView1_ItemDataBound">
                        <LayoutTemplate>                
                            <asp:PlaceHolder runat="server" id="ListViewContainer" />
                        </LayoutTemplate>            
                        <ItemTemplate>    
                            
                                <tr valign=top>
                                    <td>
                                    <telerik:RadBinaryImage ID=radimage runat=server Height=30px/>
                                    <asp:HyperLink ID=hypevent runat=server Text='<%#Eval("name")%>'></asp:HyperLink> </td>                                    
                                    <td><asp:label ID=lbleventid runat=server Text='<%#Eval("eid")%>' Visible=false></asp:label></td>
                                    <td><asp:Label ID=lblaction runat=server></asp:Label></td>
                                </tr>
                            
                        </ItemTemplate>
                        </telerik:RadListView>        
                        </table>
    </div>
    </telerik:RadAjaxPanel>
    </form>
</body>
</html>