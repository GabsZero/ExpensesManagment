using ExpenseManagment.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagment.Views.Shared.Components
{
    public class IndexHeader : ViewComponent
    {
        public IViewComponentResult Invoke(string header)
        {
            IndexHeaderViewModel model = new IndexHeaderViewModel
            {
                Header = header
            };
            return View(model);
        }
    }
}
