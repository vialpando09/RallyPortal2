﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    /// <summary>
    /// Represents default chart series settings
    /// </summary>
    public interface IChartSeriesDefaults : IChartSeries
    {
        /// <summary>
        /// The default settings for all bar series
        /// </summary>
        IChartBarSeries Bar
        {
            get;
        }

        /// <summary>
        /// The default settings for all column series
        /// </summary>
        IChartBarSeries Column
        {
            get;
        }

        /// <summary>
        /// The default settings for all line series
        /// </summary>
        IChartLineSeries Line
        {
            get;
        }

        /// <summary>
        /// The default settings for all vertical line series
        /// </summary>
        IChartLineSeries VerticalLine
        {
            get;
        }

        /// <summary>
        /// The default settings for all area series
        /// </summary>
        IChartAreaSeries Area
        {
            get;
        }

        /// <summary>
        /// The default settings for all vertical area series
        /// </summary>
        IChartAreaSeries VerticalArea
        {
            get;
        }

        /// <summary>
        /// The default settings for all pie series
        /// </summary>
        IChartPieSeries Pie
        {
            get;
        }

        /// <summary>
        /// The default settings for all scatter series
        /// </summary>
        IChartScatterSeries Scatter
        {
            get;
        }

        /// <summary>
        /// The default settings for all scatter line series
        /// </summary>
        IChartScatterLineSeries ScatterLine
        {
            get;
        }
    }
}