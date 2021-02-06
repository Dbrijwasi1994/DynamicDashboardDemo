using System;
using System.Collections.Generic;

#nullable disable

namespace DynamicDashboardDemo.Models
{
    public partial class Template
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WidgetsCount { get; set; }
    }
}
