using DynamicDashboardDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicDashboardDemo.Repositories
{
    public interface IDashboardInfoRepo
    {
        List<DashboardsInfo> GetDashboardInfo();
        DashboardsInfo GetDashboardInfo(int Id);
        int? GetWidgetsCount(int templateId);
        List<DashboardLinkedWidgets> GetLinkedWidgets(int Id);
        string CreateDashboard(DashboardsInfo dashboard);
        bool AddWidget(DashboardLinkedWidgets widget);
        bool RemoveWidgets(DashboardLinkedWidgets widget);
    }
}
