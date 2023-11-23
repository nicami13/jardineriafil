<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ejemplo.aspx.cs" Inherits="NombreDeTuProyecto.Ejemplo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ejemplo ASPX</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Ejemplo ASPX</h1>
            <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            <br />
            <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
            <asp:Button ID="btnSaludar" runat="server" Text="Saludar" OnClick="btnSaludar_Click" />
        </div>
    </form>
</body>
</html>
