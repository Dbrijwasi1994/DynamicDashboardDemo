using System;
using System.Collections.Generic;

#nullable disable

namespace DynamicDashboardDemo.Models
{
    public partial class DashboardLinkedWidgets
    {
        public int Id { get; set; }
        public int DashboardId { get; set; }
        public int WidgetId { get; set; }
        public string Placement { get; set; }
    }
}
