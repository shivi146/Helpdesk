using HelpdeskMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpdeskMVC.Repository
{
    public class DistrictRepository
    {
        public IEnumerable<SelectListItem> GetDistricts()
        {
            using (var context = new ApplContext())
            {
                try
                {
                    List<SelectListItem> districts = context.Districts.AsNoTracking().OrderBy(n => n.DistrictNames).Select(n =>
                      new SelectListItem
                      {
                          Value = n.Id.ToString(),
                          Text = n.DistrictNames

                      }).ToList();
                    var ToolTip = new SelectListItem()
                    {
                        Value = null,
                        Text = "--Select Districts--"
                    };
                    districts.Insert(0, ToolTip);
                    return new SelectList(districts, "Value", "Text");
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

    }
}