<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

    <%= Html.Telerik().Chart()
            .Name("chart")
            .Theme(Html.GetCurrentTheme())
            .Title("Hybrid car mileage report")
            .Legend(legend => legend
                .Position(ChartLegendPosition.Top)
            )
            .Series(series =>
            {
                series
                    .Column(new int[] { 20, 40, 45, 30, 50 })
                    .Stack(true)
                    .Color("#13558E")
                    .Name("on battery");
                
                series
                    .Column(new int[] { 20, 30, 35, 35, 40 })
                    .Stack(true)
                    .Color("#E21219")
                    .Name("on gas");
                
                series
                    .Line(new double[] { 30, 38, 40, 32, 42 })
                    .Name("MPG")
                    .Color("#DE7E42")
                    .Axis("mpg");
                
                series
                    .Line(new double[] { 7.8, 6.2, 5.9, 7.4, 5.6 })
                    .Name("l/100 km")
                    .Color("#057589")
                    .Axis("l100km");
            })
            .CategoryAxis(axis => axis
                .Categories("Mon", "Tue", "Wed", "Thu", "Fri")
                // Align the first two value axes to the left
                // and the last two to the right.
                //
                // Right alignment is done by specifying a
                // crossing value greater than or equal to
                // the number of categories.
                .AxisCrossingValue(0, 0, 100, 100)
            )
            .ValueAxis(axis => axis
                .Numeric()
                    .Title("miles")
                    .Min(0).Max(100)
            )
            .ValueAxis(axis => axis
                .Numeric("km")
                    .Title("km")
                    .Min(0).Max(161).MajorUnit(32)
            )
            .ValueAxis(axis => axis
                .Numeric("mpg")
                    .Title("miles per gallon")
                    .Color("#DE7E42")
            )
            .ValueAxis(axis => axis
                .Numeric("l100km")
                    .Title("liters per 100km")
                    .Color("#057589")
            )
            .HtmlAttributes(new { style = "width: 670px; height: 400px; margin: auto;" })
    %>

</asp:Content>
