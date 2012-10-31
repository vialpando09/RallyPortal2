﻿namespace Telerik.Web.Mvc.UI.Tests
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.UI.Tests.Chart;
    using Xunit;

    public class ChartBarSeriesSerializerTests
        : ChartBoundSeriesSerializerTests<ChartBarSeries<SalesData, decimal>, SalesData, decimal>
    {
        public ChartBarSeriesSerializerTests()
        {
            var chart = ChartTestHelper.CreateChart<SalesData>();
            chart.DataSource = SalesDataBuilder.GetCollection();
            series = new ChartBarSeries<SalesData, decimal>(chart, s => s.RepSales);
        }

        [Fact]
        public void Bar_serializes_type()
        {
            GetJson(series)["type"].ShouldEqual("bar");
        }

        [Fact]
        public void Bar_serializes_type_for_vertical_orientation()
        {
            series.Orientation = ChartSeriesOrientation.Vertical;
            GetJson(series)["type"].ShouldEqual("column");
        }

        [Fact]
        public void Bar_serializes_stack()
        {
            series.Stacked = true;
            GetJson(series)["stack"].ShouldEqual(true);
        }

        [Fact]
        public void Bar_should_not_seriale_default_stack()
        {
            GetJson(series).ContainsKey("stack").ShouldBeFalse();
        }

        [Fact]
        public void Bar_serializes_gap()
        {
            series.Gap = 1;
            GetJson(series)["gap"].ShouldEqual(1.0);
        }

        [Fact]
        public void Bar_should_not_seriale_default_gap()
        {
            GetJson(series).ContainsKey("gap").ShouldBeFalse();
        }

        [Fact]
        public void Spacing_serializes_spacing()
        {
            series.Spacing = 1;
            GetJson(series)["spacing"].ShouldEqual(1.0);
        }

        [Fact]
        public void Spacing_should_not_seriale_default_spacing()
        {
            GetJson(series).ContainsKey("spacing").ShouldBeFalse();
        }

        [Fact]
        public void Bar_should_serialize_label_settings()
        {
            series.Labels.Visible = true;
            GetJson(series).ContainsKey("labels").ShouldEqual(true);
        }

        [Fact]
        public void Bar_should_not_serialize_label_settings_by_default()
        {
            GetJson(series).ContainsKey("labels").ShouldEqual(false);
        }

        [Fact]
        public void Serializes_border()
        {
            series.Border.Color = "red";
            series.Border.Width = 1;
            series.Border.DashType = ChartDashType.Dot;
            ((Dictionary<string, object>)GetJson(series)["border"])["width"].ShouldEqual(1);
            ((Dictionary<string, object>)GetJson(series)["border"])["color"].ShouldEqual("red");
            ((Dictionary<string, object>)GetJson(series)["border"])["dashType"].ShouldEqual("dot");
        }

        [Fact]
        public void Does_not_serialize_default_border()
        {
            GetJson(series).ContainsKey("border").ShouldBeFalse();
        }

        [Fact]
        public void Serializes_color()
        {
            series.Color = "Blue";
            GetJson(series)["color"].ShouldEqual("Blue");
        }

        [Fact]
        public void Does_not_serialize_default_color()
        {
            GetJson(series).ContainsKey("color").ShouldBeFalse();
        }

        [Fact]
        public void Bar_serializes_overlay()
        {
            series.Overlay = ChartBarSeriesOverlay.None;
            GetJson(series).ContainsKey("overlay").ShouldBeTrue();
        }

        [Fact]
        public void Bar_should_not_serialize_default_overlay()
        {
            series.Overlay = null;
            GetJson(series).ContainsKey("overlay").ShouldBeFalse();
        }
    }
}