﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI.Fluent
{
    using System;
    using Telerik.Web.Mvc.Infrastructure;
    using Telerik.Web.Mvc.UI;

    /// <summary>
    /// Defines the fluent interface for configuring area series.
    /// </summary>
    /// <typeparam name="T">The type of the data item</typeparam>
    public class ChartAreaSeriesBuilder<T> : ChartSeriesBuilderBase<IChartAreaSeries, ChartAreaSeriesBuilder<T>>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartAreaSeriesBuilder{T}"/> class.
        /// </summary>
        /// <param name="series">The series.</param>
        public ChartAreaSeriesBuilder(IChartAreaSeries series)
            : base(series)
        {
        }

        /// <summary>
        /// Sets a value indicating if the areas should be stacked.
        /// </summary>
        /// <param name="stacked">A value indicating if the areas should be stacked.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart(Model)
        ///             .Name("Chart")
        ///             .Series(series => series.Area(s => s.Sales).Stack(true))
        /// %&gt;
        /// </code>
        /// </example>
        public ChartAreaSeriesBuilder<T> Stack(bool stacked)
        {
            Series.Stacked = stacked;

            return this;
        }

        /// <summary>
        /// Configures the area chart labels.
        /// </summary>
        /// <param name="configurator">The configuration action.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart()
        ///             .Name("Chart")
        ///             .Series(series => series
        ///                 .Area(s => s.Sales)
        ///                 .Labels(labels => labels
        ///                     .Position(ChartBarLabelsPosition.Above)
        ///                     .Visible(true)
        ///                 );
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public ChartAreaSeriesBuilder<T> Labels(Action<ChartPointLabelsBuilder> configurator)
        {
            Guard.IsNotNull(configurator, "configurator");

            configurator(new ChartPointLabelsBuilder(Series.Labels));

            return this;
        }

        /// <summary>
        /// Sets the visibility of area chart labels.
        /// </summary>
        /// <param name="visible">The visibility. The default value is false.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart()
        ///             .Name("Chart")
        ///             .Series(series => series
        ///                 .Area(s => s.Sales)
        ///                 .Labels(true);
        ///              )
        /// %&gt;
        /// </code>
        /// </example>
        public ChartAreaSeriesBuilder<T> Labels(bool visible)
        {
            Series.Labels.Visible = visible;

            return this;
        }

        /// <summary>
        /// Configures the area chart line.
        /// </summary>
        /// <param name="configurator">The configuration action.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Telerik().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///               .Area(s => s.Sales)        
        ///               .Line(2, "red", ChartDashType.Dot)
        ///           )
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartAreaSeriesBuilder<T> Line(int width, string color, ChartDashType dashType)
        {
            Series.Line.Width = width;
            Series.Line.Color = color;
            Series.Line.DashType = dashType;

            return this;
        }

        /// <summary>
        /// Configures the area chart line.
        /// </summary>
        /// <param name="configurator">The configuration action.</param>
        /// <example>
        /// <code lang="CS">
        /// &lt;% Html.Telerik().Chart()
        ///           .Name("Chart")
        ///           .Series(series => series
        ///               .Area(s => s.Sales)        
        ///               .Line(line => line.Opacity(0.2))
        ///           )
        ///           .Render();
        /// %&gt;
        /// </code>
        /// </example>        
        public ChartAreaSeriesBuilder<T> Line(Action<ChartAreaLineBuilder> configurator)
        {
            Guard.IsNotNull(configurator, "configurator");

            configurator(new ChartAreaLineBuilder(Series.Line));

            return this;
        }

        /// <summary>
        /// Configures the area chart markers.
        /// </summary>
        /// <param name="configurator">The configuration action.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart()
        ///             .Name("Chart")
        ///             .Series(series => series
        ///                 .Area(s => s.Sales)
        ///                 .Markers(markers => markers
        ///                     .Type(ChartMarkerShape.Triangle)
        ///                 );
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public ChartAreaSeriesBuilder<T> Markers(Action<ChartMarkersBuilder> configurator)
        {
            Guard.IsNotNull(configurator, "configurator");

            configurator(new ChartMarkersBuilder(Series.Markers));

            return this;
        }

        /// <summary>
        /// Sets the visibility of area chart markers.
        /// </summary>
        /// <param name="visible">The visibility. The default value is true.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart()
        ///             .Name("Chart")
        ///             .Series(series => series
        ///                 .Area(s => s.Sales)
        ///                 .Markers(true);
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public ChartAreaSeriesBuilder<T> Markers(bool visible)
        {
            Series.Markers.Visible = visible;

            return this;
        }

        /// <summary>
        /// Configures the behavior for handling missing values in area series.
        /// </summary>
        /// <param name="missingValues">The missing values behavior. The default is to leave gaps.</param>
        /// <example>
        /// <code lang="CS">
        ///  &lt;%= Html.Telerik().Chart()
        ///             .Name("Chart")
        ///             .Series(series => series
        ///                 .Area(s => s.Sales)
        ///                 .MissingValues(ChartAreaMissingValues.Interpolate);
        ///             )
        /// %&gt;
        /// </code>
        /// </example>
        public ChartAreaSeriesBuilder<T> MissingValues(ChartAreaMissingValues missingValues)
        {
            Series.MissingValues = missingValues;

            return this;
        }
    }
}