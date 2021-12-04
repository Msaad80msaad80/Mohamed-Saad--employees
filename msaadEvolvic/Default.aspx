<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="msaadEvolvic.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="200" border="1" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td nowrap>&nbsp;Select Your Text File:&nbsp;</td>
      
    <td nowrap>
     &nbsp;<asp:FileUpload ID="flupld" runat="server" /> &nbsp;
    </td>
    <td nowrap>&nbsp;<asp:Button ID="btncrt" runat="server" Text="Get Details" OnClick="btncrt_Click" />&nbsp;</td>
  </tr>
        
</table>

<br />
        <asp:Label ID="lblmsg" runat="server" ForeColor="#FF3300"></asp:Label>
    <asp:repeater runat="server" ID="rptpairs">

        <HeaderTemplate>
  <table width="50%" border="1" align="center" cellpadding="2" cellspacing="2"> 
  <tr>
    <td bgcolor="#EEEEEE" width="20%" nowrap>&nbsp;<strong>Rank</strong></td>
    <td bgcolor="#EEEEEE" width="20%" nowrap>&nbsp;<strong>Employee ID #1</strong></td>
    <td bgcolor="#EEEEEE" width="20%" nowrap>&nbsp;<strong>Employee ID #2</strong></td>
    <td bgcolor="#EEEEEE" width="20%" nowrap>&nbsp;<strong>Project ID</strong></td>
    <td bgcolor="#EEEEEE" width="20%" nowrap>&nbsp;<strong>Days worked</strong></td>
  </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
   <td><asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" /></td>  
    <td nowrap>&nbsp;<%# Eval("EmpID1") %></td>
    <td nowrap>&nbsp;<%# Eval("EmpID2") %></td>
    <td nowrap>&nbsp;<%# Eval("ProjectID") %></td>
    <td nowrap>&nbsp;<%# Eval("TotalDays") %></td>
  </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:repeater>

        <br />




    </div>
    </form>
</body>
</html>
