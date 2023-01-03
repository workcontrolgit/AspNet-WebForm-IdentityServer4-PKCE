<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForms._Default" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Login as: <%= User.Identity.Name %></h1>


<div class="card">
  <div class="card-header">
    <h3>Id Token</h3>
  </div>
  <div class="card-body">
    <p class="card-text">
    <asp:Label ID="lblIdToken" runat="server" Text=""></asp:Label>
    </p>
  </div>
</div>


<div class="card">
  <div class="card-header">
    <h3>Access Token</h3>
  </div>
  <div class="card-body">
    <p class="card-text">
        <asp:Label ID="lblAccessToken" runat="server" Text=""></asp:Label>
    </p>
  </div>
</div>

<div class="card">
  <div class="card-header">
    <h3>Claims</h3>
  </div>
  <div class="card-body">
    <p class="card-text">
    <div class="table-responsive">
        <asp:GridView ID="grdClaims" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="Type" HeaderText="Type"></asp:BoundField>
                <asp:BoundField DataField="Value" HeaderText="Value" ItemStyle-Wrap="true"></asp:BoundField>
            </Columns>

        </asp:GridView>
    </div>
    </p>
  </div>
</div>

<div class="card">
  <div class="card-header">
    <h3>Dictionaries</h3>
  </div>
  <div class="card-body">
    <p class="card-text">
    <div class="table-responsive">
        <asp:GridView ID="grdDictionaries" runat="server" CssClass="table table-striped table-bordered table-hover">
        </asp:GridView>
    </div>
    </p>
  </div>
</div>

</asp:Content>