<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="API.aspx.cs" Inherits="DartaGram.API" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>API Documentation</h3>
    <div class="row">
        <div class="col-md-4">
            <h2>Registration</h2>
            <p>
                This api is used to save the user details in the server of DarttaGram. The post request consists of fields: userName, firstName, lastName and password. Sending the details through MVC Web API will generate a unique user-id in response with an encrypted password.
            </p>
            <p>
                <a class="btn btn-default" href="http://dartagram.azurewebsites.net/Authentication/Save">Api link</a>
            </p>
        </div>
    </div>
     <div class="row">
        <div class="col-md-4">
            <h2>Login</h2>
            <p>
                This api is used to check the user details in the server of DarttaGram. The post request consists of fields: userName and password. Sending the details through MVC Web API will return a "success:true" message if the user is present in the server, else it returns a "success:false".
            </p>
            <p>
                <a class="btn btn-default" href="http://dartagram.azurewebsites.net/Authentication/Authenticate">Api link</a>
            </p>
        </div>
    </div>
     <div class="row">
        <div class="col-md-4">
            <h2>Save Video Details</h2>
            <p>
                This api is used to save the video details in the server of DarttaGram. The post request consists of fields: UserId, lat, long and videoStream. Sending the details through MVC Web API will return a "success:true" message if the video link is successfully generated in the server, else it returns a "success:false".
            </p>
            <p>
                <a class="btn btn-default" href="http://dartagram.azurewebsites.net/Video/Save">Api link</a>
            </p>
        </div>
    </div>
</asp:Content>
