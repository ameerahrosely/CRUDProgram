<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CRUDProgram._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>CRUD Operations</h1>
        <p class="lead">This program uses HTML, CSS, C# and JavaScript.</p>
        <button type="button" class="btn btn-primary btn-lg" onclick="openPost(); return false;">Let's Start! &raquo;</button>
    </div>

    <div id="openSesame" class="row" style="display:none">  
        <div class="col-md-4">
            <h2>Sign Up</h2>
            <hr />
            <input class="form-control" type="text" id="clubname" placeholder="What is the club's name?"/><p></p>
            <input class="form-control" type="text" id="memberAmount" placeholder="How many members?"/><p></p>
            <input class="form-control" type="datetime-local" id="dateReg"/><p></p>
            <button type="button" class="btn btn-default" onclick="postClub(); return false;">Go &raquo;</button>
        </div>
        <div class="col-md-8">
            <h2><i>Welcome to the</i> <img src="/Images/club.png" style="width: 4%" /></h2>
            <hr />
            <table id="clubTable" class="table" style="width:100%">
                <thead>
                    <tr>
                        <th style="width:10%">Club ID</th>
                        <th style="width:30%">Club Name</th>
                        <th style="width:10%">Member(s)</th>
                        <th style="width:30%">Date Registered</th>
                        <th style="width:20%"></th>
                    </tr>
                </thead>
                <tbody id="clubTableBody">
                </tbody>
            </table>
        </div>
    </div>

    <script src="/Scripts/Default.js"></script>
</asp:Content>
