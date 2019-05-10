<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GradeAverages._Default" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Grading Averages in Schools</h1>
        <p class="lead">Grading averagse in schools can vary depending on different impacts the students fact on a daily basis. <br /> Click any of the buttons to query our data for specifics of how certain criteria corresponds to the final grade.</p>
    </div>

    <div class="container">
        <div class="row">
            <h2>
                Data Query
            </h2>
            <p>
                By selecting any of the options and hitting submit you can see how these correlate to the overall grade of a student. 
            </p>
            <div class="col-md-3">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnCheckedChanged="Internet_CheckedChanged" Width="173px" >
                    <asp:ListItem>Internet Availability</asp:ListItem>
                    <asp:ListItem>Past Failures</asp:ListItem>
                    <asp:ListItem>Study Time</asp:ListItem>
                    <asp:ListItem>Number of Absences</asp:ListItem>
                </asp:RadioButtonList>
                <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" OnClientClick="return false"/>
            </div>
            <div>
                <asp:TextBox ID="TextBox1" runat="server" Height="120px" TextMode="MultiLine" Width="450px" Font-Size="Small"></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="container">
        <div class="row">
            <h2>
                Travel Time
            </h2>
            <p>
                This graph shows the correlation of travel time of the student to their overall grade.
            </p>
            <div class="col-md-4">
                <asp:Chart ID="Chart1" runat="server">
                    <Series>
                        <asp:Series Name="Series2">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </div>
        </div>
    </div>

</asp:Content>
