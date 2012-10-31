<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<SalesData>>" %>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

    <%= Html.Telerik().Chart(Model)
            .Name("chart")
            .Theme(Html.GetCurrentTheme())
            .Title("Representative Sales vs. Total Sales")
            .Legend(legend => legend
                .Position(ChartLegendPosition.Bottom)
            )
            .Series(series => {
                series.Area(s => s.TotalSales).Name("Total Sales");
                series.Area(s => s.RepSales).Name("Representative Sales");
            })
            .CategoryAxis(axis => axis
                .Categories(s => s.DateString)
            )
            .ValueAxis(axis => axis
                .Numeric().Labels(labels => labels.Format("${0:#,##0}"))
            )
            .Tooltip(tooltip => tooltip.Visible(true))
            .HtmlAttributes(new { style = "width: 600px; height: 400px; margin: auto;" })
    %>

</asp:Content>