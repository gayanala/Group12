<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DartaGram._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            var response = false;
            $.ajax({
                url: '/Services/Authentication.asmx/Register',
                type: 'POST',
                data: {
                    username: 'abc'
                },
                dataType: "json",
                success: function (data) {
                    debugger;
                    response = true;
                    console.log(data)
                },
                error: function (data) {
                    debugger;
                    response = false;
                    console.log(data)
                }
            });

        });
</script>
    <div class="jumbotron">
        <p class="lead">DartaGram is used to stream videos near you.</p>
    </div>

</asp:Content>
