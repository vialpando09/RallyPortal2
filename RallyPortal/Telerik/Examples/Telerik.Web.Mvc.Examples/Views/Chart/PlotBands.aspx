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
                series.Column(s => s.RepSales).Name("Representative Sales");
                series.Column(s => s.TotalSales).Name("Total Sales");
            })
            .CategoryAxis(axis => axis
                .Categories(s => s.DateString)
            )
            .ValueAxis(axis => axis
                .Numeric().Labels(labels => labels.Format("${0:#,##0}"))
                .MajorGridLines(majorGridLines => majorGridLines.Visible(false))
                .PlotBands(plotBands =>
                {
                    plotBands.Add().Color("#cccccc").From(0).To(12000).Opacity(0.3);
                })
            )
            .HtmlAttributes(new { style = "width: 600px; height: 400px; margin: auto;" })
    %>

</asp:Content>