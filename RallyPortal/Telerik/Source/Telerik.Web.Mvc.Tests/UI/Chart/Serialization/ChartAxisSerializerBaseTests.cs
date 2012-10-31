﻿namespace Telerik.Web.Mvc.UI.Tests
{
    using Moq;
    using Xunit;
    using System.Collections.Generic;

    public class ChartAxisSerializerBaseTests
    {
        private readonly Mock<IChartAxis> axisMock;
        private readonly ChartAxisSerializerBase serializer;

        public ChartAxisSerializerBaseTests()
        {
            axisMock = new Mock<IChartAxis>();
            axisMock.SetupGet(axis => axis.MajorGridLines).Returns(new ChartLine());
            axisMock.SetupGet(axis => axis.MinorGridLines).Returns(new ChartLine());
            axisMock.SetupGet(a => a.Line).Returns(new ChartLine());
            axisMock.SetupGet(a => a.Title).Returns(new ChartAxisTitle());
            axisMock.SetupGet(a => a.Labels).Returns(new ChartAxisLabels());
            axisMock.SetupGet(a => a.PlotBands).Returns(new List<ChartPlotBand>());
            serializer = new ChartAxisSerializerBase(axisMock.Object);
        }

        [Fact]
        public void Should_serialize_AxisCrossingValues()
        {
            axisMock.SetupGet(a => a.AxisCrossingValues).Returns(new double[10]);
            serializer.Serialize()["axisCrossingValue"].ShouldEqual(new double[10]);
        }

        [Fact]
        public void Should_not_serialize_AxisCrossingValue_if_not_set()
        {
            axisMock.SetupGet(a => a.AxisCrossingValues).Returns(new double[] { });
            serializer.Serialize().ContainsKey("axisCrossingValues").ShouldBeFalse();
        }

        [Fact]
        public void Should_serialize_MinorTickSize()
        {
            axisMock.SetupGet(a => a.MinorTickSize).Returns(1);
            serializer.Serialize()["minorTickSize"].ShouldEqual(1);
        }

        [Fact]
        public void Should_not_serialize_default_MinorTickSize()
        {
            axisMock.SetupGet(a => a.MinorTickSize).Returns((int?)null);
            serializer.Serialize().ContainsKey("minorTickSize").ShouldBeFalse();
        }

        [Fact]
        public void Should_serialize_MajorTickSize()
        {
            axisMock.SetupGet(a => a.MajorTickSize).Returns(1);
            serializer.Serialize()["majorTickSize"].ShouldEqual(1);
        }

        [Fact]
        public void Should_not_serialize_default_MajorTickSize()
        {
            axisMock.SetupGet(a => a.MajorTickSize).Returns((int?)null);
            serializer.Serialize().ContainsKey("majorTickSize").ShouldBeFalse();
        }

        [Fact]
        public void Should_not_serialize_default_MajorTickType()
        {
            axisMock.SetupGet(a => a.MajorTickType).Returns((ChartAxisTickType?)null);
            serializer.Serialize().ContainsKey("majorTickType").ShouldBeFalse();
        }

        [Fact]
        public void Should_not_serialize_default_MinorTickType()
        {
            axisMock.SetupGet(a => a.MinorTickType).Returns((ChartAxisTickType?)null);
            serializer.Serialize().ContainsKey("minorTickType").ShouldBeFalse();
        }

        [Fact]
        public void Should_serialize_Line()
        {
            axisMock.SetupGet(a => a.Line).Returns(new ChartLine(2, "green", ChartDashType.Dot, true));
            serializer.Serialize().ContainsKey("line").ShouldBeTrue();
        }

        [Fact]
        public void Should_not_serialize_default_Line()
        {
            serializer.Serialize().ContainsKey("line").ShouldBeFalse();
        }

        [Fact]
        public void Should_not_serialize_majorGridLines_if_not_set()
        {
            serializer.Serialize().ContainsKey("majorGridLines").ShouldBeFalse();
        }

        [Fact]
        public void Should_serialize_majorGridLines_if_set()
        {
            axisMock.SetupGet(a => a.MajorGridLines).Returns(
                new ChartLine(1, "white", ChartDashType.Dot, true)
            );

            serializer.Serialize().ContainsKey("majorGridLines").ShouldBeTrue();
        }

        [Fact]
        public void Should_serialize_majorGridLines_if_hidden()
        {
            axisMock.SetupGet(a => a.MajorGridLines).Returns(
                new ChartLine { Visible = false }
            );

            serializer.Serialize().ContainsKey("majorGridLines").ShouldBeTrue();
        }

        [Fact]
        public void Should_not_serialize_minorGridLines_if_not_set()
        {
            serializer.Serialize().ContainsKey("minorGridLines").ShouldBeFalse();
        }

        [Fact]
        public void Should_serialize_minorGridLines_if_set()
        {
            axisMock.SetupGet(a => a.MinorGridLines).Returns(
                new ChartLine(1, "white", ChartDashType.Dot, true)
            );

            serializer.Serialize().ContainsKey("minorGridLines").ShouldBeTrue();
        }

        [Fact]
        public void Should_serialize_minorGridLines_if_hidden()
        {
            axisMock.SetupGet(a => a.MinorGridLines).Returns(
                new ChartLine { Visible = false }
            );

            serializer.Serialize().ContainsKey("minorGridLines").ShouldBeTrue();
        }

        [Fact]
        public void Should_not_serialize_PlotBands_if_not_set()
        {
            serializer.Serialize().ContainsKey("plotBands").ShouldBeFalse();
        }

        [Fact]
        public void Should_serialize_PlotBands_if_set()
        {
            var data = new List<ChartPlotBand>();
            data.Add(new ChartPlotBand() { Color = "red" });
            axisMock.SetupGet(a => a.PlotBands).Returns(data);

            serializer.Serialize().ContainsKey("plotBands").ShouldBeTrue();
        }

        [Fact]
        public void Should_not_serialize_Title_if_not_set()
        {
            serializer.Serialize().ContainsKey("title").ShouldBeFalse();
        }

        [Fact]
        public void Should_serialize_Title_if_set()
        {
            axisMock.SetupGet(a => a.Title).Returns(new ChartAxisTitle() { Color = "Red" });

            serializer.Serialize().ContainsKey("title").ShouldBeTrue();
        }

        [Fact]
        public void Should_not_serialize_Name_if_not_set()
        {
            serializer.Serialize().ContainsKey("name").ShouldBeFalse();
        }

        [Fact]
        public void Should_serialize_Name_if_set()
        {
            axisMock.SetupGet(a => a.Name).Returns("Axis");

            serializer.Serialize().ContainsKey("name").ShouldBeTrue();
        }

        [Fact]
        public void Should_not_serialize_Color_if_not_set()
        {
            serializer.Serialize().ContainsKey("color").ShouldBeFalse();
        }

        [Fact]
        public void Should_serialize_Color_if_set()
        {
            axisMock.SetupGet(a => a.Color).Returns("#f00");

            serializer.Serialize().ContainsKey("color").ShouldBeTrue();
        }
    }
}