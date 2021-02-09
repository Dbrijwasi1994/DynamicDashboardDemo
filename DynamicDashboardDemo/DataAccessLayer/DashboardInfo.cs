using DynamicDashboardDemo.Models;
using DynamicDashboardDemo.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DynamicDashboardDemo.DataAccessLayer
{
    public class DashboardInfo : IDashboardInfoRepo, IDisposable
    {
        private readonly DynamicDashboardDemoContext _context;
        private readonly ILogger<DashboardInfo> _logger;

        public DashboardInfo(DynamicDashboardDemoContext context, ILogger<DashboardInfo> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Dispose()
        {
            ((IDisposable)_context).Dispose();
        }

        public List<DashboardsInfo> GetDashboardInfo()
        {
            return _context.DashboardsInfos.ToList();
        }

        public DashboardsInfo GetDashboardInfo(int Id)
        {
            return _context.DashboardsInfos.Where(x => x.Id == Id).FirstOrDefault();
        }

        public int? GetWidgetsCount(int templateId)
        {
            try
            {
                return _context.Templates.Where(x => x.Id == templateId).Select(y => y.WidgetsCount).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public List<DashboardLinkedWidgets> GetLinkedWidgets(int Id)
        {
            return _context.DashboardLinkedWidgets.Where(x => x.DashboardId == Id).ToList();
        }

        public string CreateDashboard(DashboardsInfo dashboard)
        {
            string query = @"insert into Dashboards_Info(Name, TemplateId) values(@Name, @TemplateId)";
            try
            {
                var sqlParamList = new object[] { new SqlParameter("@Name", dashboard.Name), new SqlParameter("@TemplateId", dashboard.TemplateId) };
                _context.Database.ExecuteSqlRaw(query, sqlParamList);
                return dashboard.Id.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return "False";
            }
        }

        public bool AddWidget(DashboardLinkedWidgets widget)
        {
            string query = @"insert into DashboardLinkedWidgets(DashboardId, WidgetId, Placement) values(@DashboardId, @WidgetId, @Placement)";
            try
            {
                var sqlParamList = new object[] { new SqlParameter("@DashboardId", widget.DashboardId), new SqlParameter("@WidgetId", widget.WidgetId), new SqlParameter("@Placement", widget.Placement) };
                return _context.Database.ExecuteSqlRaw(query, sqlParamList) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public bool RemoveWidgets(DashboardLinkedWidgets widget)
        {
            try
            {
                var oldWidgets = _context.DashboardLinkedWidgets.Where(x => x.DashboardId == widget.DashboardId && x.Placement == widget.Placement).ToList();
                if (oldWidgets != null)
                {
                    oldWidgets.ForEach(x => _context.DashboardLinkedWidgets.Remove(x));
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
