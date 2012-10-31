﻿// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using Telerik.Web.Mvc.Infrastructure;

    internal abstract class ChartSeriesSerializerBase : IChartSerializer
    {
        private readonly IChartSeries series;

        public ChartSeriesSerializerBase(IChartSeries series)
        {
            this.series = series;
        }

        public virtual IDictionary<string, object> Serialize()
        {
            var result = new Dictionary<string, object>();
            FluentDictionary.For(result)
                  .Add("name", series.Name, string.Empty)
                  .Add("opacity", series.Opacity, () => series.Opacity.HasValue)
                  .Add("axis", series.Axis, string.Empty);

            var tooltipData = series.Tooltip.CreateSerializer().Serialize();
            if (tooltipData.Count > 0) {
                  result.Add("tooltip", tooltipData);
            }

            return result;
        }
    }
}
