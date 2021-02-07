using DynamicDashboardDemo.Models;
using DynamicDashboardDemo.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            try
            {
                _context.DashboardsInfos.Add(dashboard);
                _context.SaveChanges();
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
            try
            {
                _context.DashboardLinkedWidgets.Add(widget);
                return _context.SaveChanges() > 0 ? true : false;
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
