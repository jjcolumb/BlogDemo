using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace BlogDemo.Module.Web.Controllers
{
    public class AddStyleController : ViewController
    {

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            LiteralControl style = new LiteralControl();
            style.Text = "<style type='text/css' scoped>.white { background-color: palegreen; }</style>";
            ((Control)View.Control).Controls.Add(style);


        }
    }
}
