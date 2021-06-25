<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CRUDProgram._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="mainBox">
        <div id="openSesame" class="row">
            <div class="col-md-4">
                <h2>Save / Update Token</h2>
                <hr />
                <table class="tokenTable" style="width: 100%">
                    <tr>
                        <td>Name</td>
                        <td><input class="form-control" type="text" id="name" placeholder="Name" /></td>
                    </tr>
                    <tr>
                        <td>Symbol</td>
                        <td><input class="form-control" type="text" id="symbol" placeholder="Symbol" /></td>
                    </tr>
                    <tr>
                        <td>Contact Address</td>
                        <td><input class="form-control" type="text" id="contact" placeholder="Contact Address" /></td>
                    </tr>
                    <tr>
                        <td>Total Supply</td>
                        <td><input class="form-control" type="text" id="supply" placeholder="Total Supply" /></td>
                    </tr>
                    <tr>
                        <td>Total Holders</td>
                        <td><input class="form-control" type="text" id="holder" placeholder="Total Holders" /></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <button type="button" class="btn btn-primary" onclick="postToken(); return false;">Save &raquo;</button>
                            <button type="button" class="btn btn-info" onclick="resetForm(); return false;">Reset</button>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-md-8">
                <h2>Token Statistics by Token Supply</h2>
                <hr />
                <div id="chartdiv"></div>
            </div>
            <h2 style="color:transparent">-</h2>
            <button type="button" class="btn btn-success exportBtn" onclick="exportToken(); return false;">Export <img src="Images/excel.png" style="width:25%"/></button>
            <hr style="width:97%"/>
            <div class="col-md-12">
                <table id="tokenTable" class="table" style="width: 100%">
                    <thead>
                        <tr>
                            <th style="width: 5%">Rank</th>
                            <th style="width: 10%">Symbol</th>
                            <th style="width: 10%">Name</th>
                            <th style="width: 35%">Contact Address</th>
                            <th style="width: 10%">Total Holders</th>
                            <th style="width: 10%">Total Supply</th>
                            <th style="width: 12%">Total Supply (%)</th>
                            <th style="width: 10%"></th>
                        </tr>
                    </thead>
                    <tbody id="tokenTableBody">
                    </tbody>
                </table>
                <div style="float: right">
                    <div id="pagination" class="pagination"></div>
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="btnExportMasterList" OnClick="btnExportMasterList_Click" runat="server" CssClass="hideBtn" />
    <script src="/Scripts/Default.js"></script>
    <script src="https://cdn.amcharts.com/lib/4/core.js"></script>
    <script src="https://cdn.amcharts.com/lib/4/charts.js"></script>
    <script src="https://cdn.amcharts.com/lib/4/themes/animated.js"></script>
</asp:Content>
