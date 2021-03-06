﻿<%@ Page Title="Select Flight" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="SelectFlight.aspx.cs" Inherits="BalkanAir.Web.Booking.SelectFlight" %>

<%@ Import Namespace="System.Globalization" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: this.Page.Title %></h2>

    <asp:CustomValidator ID="OneWayRouteTravelClassCustomValidator" runat="server" Display="Dynamic" ForeColor="Red" />

    <asp:CustomValidator ID="ReturnRouteTravelClassCustomValidator" runat="server" Display="Dynamic" ForeColor="Red" />

    <asp:CustomValidator ID="InvalidArrivalDateCustomValidator" runat="server" Display="Dynamic" ForeColor="Red" />

    <asp:Panel ID="FlightDetailsPanel" runat="server" ClientIDMode="Static">
        <input type="image" class="airplaneFlyOutImage" alt="Airplane image" src="../Content/Images/airplane_fly_out_image.png" />
        <h3 class="routeInfo">
            <%: this.RouteInfo.Origin.Name %>, <small>(<%: this.RouteInfo.Origin.Abbreviation %>)</small>  to 
            <%: this.RouteInfo.Destination.Name %>, <small>(<%: this.RouteInfo.Destination.Abbreviation %>)</small>
        </h3>
            
        <div id="OneWayRouteDepartureDatesDiv" class="oneWayRouteSlider slider">
            <asp:Repeater ID="OneWayRouteDepartureDatesRepeater" runat="server"
                ItemType="BalkanAir.Data.Models.LegInstance"
                SelectMethod="OneWayRouteDepartureDatesRepeater_GetData">
                <ItemTemplate>
                    <div class='flightDatesDiv oneWayRouteFlights <%#: Item.Id == 0 ? "noFlightDatesDiv" : string.Empty %>'
                        data-value="<%#: Item.Id != 0 ? Item.Id : 0 %>">
                        <span class="date">
                            <%#: Item.DepartureDateTime.ToString("ddd dd, MMM", CultureInfo.InvariantCulture) %>
                        </span>
                        <span class="price">
                            <%#: Item.Id != 0 ? "\u20AC" + (Item.Price + Item.Aircraft.GetCheapestTravelClassPrice) : "No flight" %>
                        </span>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <div id="NoOneWayRouteFlightsDiv" class="warningPanel">
            <h5>Sorry, there are no flights available on this day!</h5>
        </div>

        <div id="OneWayRouteSelectedFlightDetailsDiv" class="selectedFlightDetailsDiv">
            <asp:UpdatePanel runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Button ID="ShowOneWayFlgihtInfoHiddenButton" ClientIDMode="Static" runat="server"
                        UseSubmitBehavior="false" OnClick="ShowOneWayFlgihtInfoHiddenButton_Click" ValidateRequestMode="Disabled" />

                    <asp:FormView ID="OneWayFlightDetailsFormView" runat="server" ItemType="BalkanAir.Data.Models.LegInstance">
                        <ItemTemplate>
                            <div class="flightDetailsDiv">
                                <h4>Flight details</h4>
                                <h5>
                                    <strong>
                                        <%#: Item.DepartureDateTime.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) %>
                                    </strong>
                                </h5>
                                <br />
                                <div class="flightDepartureDetailsDiv">
                                    <span class="departureFlightNumberSpan"><%#: Item.FlightLeg.Flight.Number %></span>
                                    <span class="departureAirportSpan"><%#: Item.FlightLeg.Route.Origin.Name %></span>
                                    <span class="departureSpan"><%#: Item.DepartureDateTime.ToString("HH:mm", CultureInfo.InvariantCulture) %></span>
                                </div>
                                <div class="flightMiddleImage">
                                    <span>
                                        <input type="image" alt="Airplane image" src="../Content/Images/airplane_between_airports.png"
                                            class="airplaneBetweenAirports" />
                                    </span>
                                </div>
                                <div class="flightDestinationDetailsDeiv">
                                    <span class="flightDurationSpan"><%#: Item.Duration.Hours %> hr <%#: Item.Duration.Minutes %> min</span>
                                    <span class="destinationAirportSpan"><%#: Item.FlightLeg.Route.Destination.Name %></span>
                                    <span class="arrivalSpan"><%#: Item.ArrivalDateTime.ToString("HH:mm", CultureInfo.InvariantCulture) %></span>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:FormView>

                    <div class="flightTravelClassesDiv oneWayRouteTravelClasses">
                        <asp:Repeater ID="OneWayFlightTravelClassesRepeater" runat="server" ClientIDMode="Static"
                            ItemType="BalkanAir.Data.Models.TravelClass">
                            <ItemTemplate>
                                <div class="travelClass <%#: Item.Type %>ClassDiv" title="">
                                    <span class="<%#: Item.Type %>ClassSpan travelClassTypeSpan"><%#: Item.Type %> Class
                                <span class="travelClassTypeDetailsSpan">
                                    <img src="../Content/Images/online-check-in-icon.png" class="availableIcon" alt="Online ckeck-in Icon" />
                                    <img src="../Content/Images/meal-icon.png" class="availableIcon" alt="Meal Icon" />
                                    <img src="../Content/Images/reserved-seat-icon.png" class="availableIcon" alt="Reserved Seat Icon" />
                                    <img src="../Content/Images/priority-boarding-icon.png" class="priorityBoardingIcons" alt="Priority Boarding Icon" />
                                    <img src="../Content/Images/extra-baggage-icon.png" class="extraBaggageIcons" alt="Extra Baggage Icon" />
                                    <img src="../Content/Images/earn-miles-icon.png" class="earnMilesIcons" alt="Earn Miles Icon" />
                                </span>
                                    </span>
                                    <span class="travelClassPriceSpan">
                                        <label>
                                            <input type="radio" required name="oneWayRoutePrice" value="<%# Item.Id %>"
                                                class="<%#: Item.NumberOfAvailableSeats == 0 ? "noMoreSeats" : "" %>" />
                                            &#8364; <%# Item.Price + this.LegInstance.Price %>
                                        </label>
                                        <span class="travelClassSeats">
                                            <%#: Item.NumberOfAvailableSeats %> seats remaining at this price
                                        </span>
                                    </span>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <asp:HiddenField ID="OneWayRouteInitialSlideIndexHiddenField" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="OneWayRouteCurrentFlightInfoIdHiddenField" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="OneWayRouteSelectedFlightIdHiddenField" runat="server" />
                    <asp:HiddenField ID="OneWayRouteSelectedTravelClassIdHiddenField" ClientIDMode="Static" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <hr />

        <asp:Panel ID="ReturnRouteFlightsPanel" ClientIDMode="Static" runat="server">
            <input type="image" class="airplaneFlyOutImage returnFlyOutImage" alt="Airplane image"
                src="../Content/Images/airplane_fly_out_image.png" />
            <h3 class="routeInfo">
                <%: this.RouteInfo.Destination.Name %>, <small>(<%: this.RouteInfo.Destination.Abbreviation %>)</small> to 
                <%: this.RouteInfo.Origin.Name %>, <small>(<%: this.RouteInfo.Origin.Abbreviation %>)</small>
            </h3>

            <div id="ReturnRouteDepartureDatesDiv" class="returnRouteSlider slider">
                <asp:Repeater ID="ReturnRouteDepartureDatesRepeater" runat="server"
                    ItemType="BalkanAir.Data.Models.LegInstance"
                    SelectMethod="ReturnRouteDepartureDatesRepeater_GetData">
                    <ItemTemplate>
                        <div class='flightDatesDiv returnRouteFlights <%#: Item.Id == 0 ? "noFlightDatesDiv" : string.Empty %>'
                            data-value="<%#: Item.Id != 0 ? Item.Id : 0 %>">
                            <span class="date">
                                <%#: Item.DepartureDateTime.ToString("ddd dd, MMM", CultureInfo.InvariantCulture) %>
                            </span>
                            <span class="price">
                                <%#: Item.Id != 0 ? "\u20AC" + (Item.Price + Item.Aircraft.GetCheapestTravelClassPrice) : "No flight" %>
                            </span>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div id="NoReturnRouteFlightsDiv" class="warningPanel">
                <h5>Sorry, there are no flights available on this day!</h5>
            </div>

            <div id="ReturnRouteSelectedFlightDetailsDiv" class="selectedFlightDetailsDiv">
                <asp:UpdatePanel runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Button ID="ShowReturnFlgihtInfoHiddenButton" ClientIDMode="Static" runat="server"
                            UseSubmitBehavior="false" OnClick="ShowReturnFlgihtInfoHiddenButton_Click" ValidateRequestMode="Disabled" />

                        <asp:FormView ID="ReturnFlightDetailsFormView" runat="server" ItemType="BalkanAir.Data.Models.LegInstance">
                            <ItemTemplate>
                                <div class="flightDetailsDiv">
                                    <h4>Flight details</h4>
                                    <h5>
                                        <strong>
                                            <%#: Item.DepartureDateTime.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) %>
                                        </strong>
                                    </h5>
                                    <br />
                                    <div class="flightDepartureDetailsDiv">
                                        <span class="departureFlightNumberSpan"><%#: Item.FlightLeg.Flight.Number %></span>
                                        <span class="departureAirportSpan"><%#: Item.FlightLeg.Route.Origin.Name %></span>
                                        <span class="departureSpan"><%#: Item.DepartureDateTime.ToString("HH:mm", CultureInfo.InvariantCulture) %></span>
                                    </div>
                                    <div class="flightMiddleImage">
                                        <span>
                                            <input type="image" alt="Airplane image" src="../Content/Images/airplane_between_airports.png"
                                                class="airplaneBetweenAirports" />
                                        </span>
                                    </div>
                                    <div class="flightDestinationDetailsDeiv">
                                        <span class="flightDurationSpan"><%#: Item.Duration.Hours %> hr <%#: Item.Duration.Minutes %> min</span>
                                        <span class="destinationAirportSpan"><%#: Item.FlightLeg.Route.Destination.Name %></span>
                                        <span class="arrivalSpan"><%#: Item.ArrivalDateTime.ToString("HH:mm", CultureInfo.InvariantCulture) %></span>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:FormView>

                        <div class="flightTravelClassesDiv returnRouteTravelClasses">
                            <asp:Repeater ID="ReturnFlightTravelClassesRepeater" runat="server" ClientIDMode="Static"
                                ItemType="BalkanAir.Data.Models.TravelClass">
                                <ItemTemplate>
                                    <div class="travelClass <%#: Item.Type %>ClassDiv" title="">
                                        <span class="<%#: Item.Type %>ClassSpan travelClassTypeSpan"><%#: Item.Type %> Class
                                <span class="travelClassTypeDetailsSpan">
                                    <img src="../Content/Images/online-check-in-icon.png" class="availableIcon" alt="Online ckeck-in Icon" />
                                    <img src="../Content/Images/meal-icon.png" class="availableIcon" alt="Meal Icon" />
                                    <img src="../Content/Images/reserved-seat-icon.png" class="availableIcon" alt="Reserved Seat Icon" />
                                    <img src="../Content/Images/priority-boarding-icon.png" class="priorityBoardingIcons" alt="Priority Boarding Icon" />
                                    <img src="../Content/Images/extra-baggage-icon.png" class="extraBaggageIcons" alt="Extra Baggage Icon" />
                                    <img src="../Content/Images/earn-miles-icon.png" class="earnMilesIcons" alt="Earn Miles Icon" />
                                </span>
                                        </span>
                                        <span class="travelClassPriceSpan">
                                            <label>
                                                <input type="radio" required name="returnRoutePrice" value="<%# Item.Id %>"
                                                    class="<%#: Item.NumberOfAvailableSeats == 0 ? "noMoreSeats" : "" %>" />
                                                &#8364; <%# Item.Price + this.LegInstance.Price %>
                                            </label>

                                            <span class="travelClassSeats">
                                                <%#: Item.NumberOfAvailableSeats %> seats remaining at this price
                                            </span>
                                        </span>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <asp:HiddenField ID="ReturnRouteInitialSlideIndexHiddenField" ClientIDMode="Static" runat="server" />
                        <asp:HiddenField ID="ReturnRouteCurrentFlightInfoIdHiddenField" ClientIDMode="Static" runat="server" />
                        <asp:HiddenField ID="ReturnRouteSelectedFlightIdHiddenField" runat="server" />
                        <asp:HiddenField ID="ReturnRouteSelectedTravelClassIdHiddenField" ClientIDMode="Static" runat="server" />

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </asp:Panel>

        <div id="ContinueBookingDiv">
            <asp:Button ID="ContinueBookingBtn" Text="Continue" runat="server" ClientIDMode="Static"
                OnClick="OnContinueBookingBtnClicked" CausesValidation="true" UseSubmitBehavior="true"  />
            
            <span id="BookingHelperSpan"></span>
        </div>

        <asp:Panel ID="SignInRequiredPanel" Visible="false" CssClass="warningPanel" runat="server" ClientIDMode="Static">
            <h5>YOU NEED TO SIGN IN TO CONTINUE!</h5>

            <asp:Button ID="CreateNewAccountToContinueBtn" ClientIDMode="Static" UseSubmitBehavior="false"
                Text="CREATE A NEW ACCOUNT TO CONTINUE" runat="server" PostBackUrl="~/Account/Register.aspx" />

            <asp:Button ID="SignInToContinueBtn" ClientIDMode="Static" Text="SIGN IN" runat="server" UseSubmitBehavior="false"
                PostBackUrl="~/Account/Login.aspx" />
        </asp:Panel>

        <asp:Panel ID="ConfirmEmailPanel" Visible="false" CssClass="warningPanel" runat="server" ClientIDMode="Static">
            <h5>YOU NEED TO CONFIRM YOUR EMAIL TO CONTINUE!</h5>
        </asp:Panel>
    </asp:Panel>
</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="JavaScriptContent" runat="server">
    <script src="../Scripts/Booking/select-flight.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            showSlickSlider($('.oneWayRouteSlider'), $('#OneWayRouteInitialSlideIndexHiddenField'));
            
            if ($('#ReturnRouteFlightsPanel').is(':visible')) {
                showSlickSlider($('.returnRouteSlider'), $('#ReturnRouteInitialSlideIndexHiddenField'));
            }

            function showSlickSlider($sliderClass, $initialSlideIndexHiddenField) {
                $sliderClass.slick({
                    infinite: false,
                    centerMode: true,
                    focusOnSelect: true,
                    variableWidth: true,
                    slidesToShow: 5,
                    slidesToScroll: 1,
                    initialSlide: parseInt($initialSlideIndexHiddenField.val(), 10)
                });
            }
        }());
    </script>
</asp:Content>