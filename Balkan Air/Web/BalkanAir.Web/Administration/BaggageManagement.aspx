﻿<%@ Page Title="Baggage Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BaggageManagement.aspx.cs" Inherits="BalkanAir.Web.Administration.BaggageManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:Panel ID="SuccessPanel" ClientIDMode="Static" Visible="false" runat="server" CssClass="alert alert-success" role="alert"
        ViewStateMode="Disabled">
        <span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>
        <span class="sr-only">Error:</span>
        New bag with ID <asp:Literal ID="AddedBagIdLiteral" runat="server" /> 
        was added successfully!
    </asp:Panel>

    <asp:GridView ID="BaggageGridView" runat="server" CssClass="administrationGridView"
        ItemType="BalkanAir.Data.Models.Baggage"
        DataKeyNames="Id"
        AutoGenerateColumns="false"
        ShowHeaderWhenEmpty="true"
        AllowPaging="true"
        PageSize="50"
        SelectMethod="BaggageGridView_GetData"
        UpdateMethod="BaggageGridView_UpdateItem"
        DeleteMethod="BaggageGridView_DeleteItem"
        AllowSorting="true">
        <Columns>
            <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" />
            <asp:BoundField DataField="Type" SortExpression="Type" HeaderText="Type" />
            <asp:BoundField DataField="MaxKilograms" SortExpression="MaxKilograms" HeaderText="Max Kilograms" />
            <asp:BoundField DataField="Size" SortExpression="Size" HeaderText="Size" />
            <asp:BoundField DataField="Price" SortExpression="Price" HeaderText="Price" />
            <asp:BoundField DataField="BookingId" SortExpression="BookingId" HeaderText="BookingId" />
            <asp:CheckBoxField DataField="IsDeleted" HeaderText="Is Deleted" />

            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-info" />
            <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
        <EmptyDataTemplate>
            <h4>No baggage found!</h4>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:RangeValidator ErrorMessage="Baggage kilograms must be in the range [0 - 32]!" ValidationGroup="AddBaggage" CssClass="validatorSpan"
        ControlToValidate="MaxKilogramsTextBox" runat="server" ForeColor="Red" MinimumValue="0" MaximumValue="32" Type="Integer" />

    <asp:Panel runat="server" CssClass="administrationAddEntityPanel">
        <h3>Add new baggage</h3>

        <asp:Label Text="Type:" runat="server" AssociatedControlID="BaggageTypeDropDownList" />
        <asp:DropDownList runat="server" ID="BaggageTypeDropDownList" />

        <asp:Label Text="Max Kilograms:" runat="server" AssociatedControlID="MaxKilogramsTextBox" />
        <asp:TextBox ID="MaxKilogramsTextBox" required runat="server" TextMode="Number" />

        <asp:Label Text="Size:" runat="server" AssociatedControlID="SizeTextBox" />
        <asp:TextBox ID="SizeTextBox" required runat="server" />

        <asp:Label Text="Price:" runat="server" AssociatedControlID="PriceTextBox" />
        <asp:TextBox ID="PriceTextBox" required runat="server" />

        <asp:Label Text="For booking:" runat="server" AssociatedControlID="BookingsDropDownList" />
        <asp:DropDownList ID="BookingsDropDownList" runat="server"
            DataValueField="Id"
            DataTextField="BookingInfo"
            SelectMethod="BookingsDropDownList_GetData" />

        <p>
            <asp:Button ID="CreateBaggageBtn" runat="server" Text="Create" CssClass="btn btn-info" ValidationGroup="AddBaggage"
                OnClick="CreateBaggageBtn_Click" />
            <asp:Button ID="CancelBtn" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-danger"
                UseSubmitBehavior="false" OnClick="CancelBtn_Click" />
        </p>
    </asp:Panel>
</asp:Content>
