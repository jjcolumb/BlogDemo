using BlogDemo.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDemo.Module.Controllers
{
    public class MyViewController : ViewController
    {
        public MyViewController()
        {
            SimpleAction myAction = new SimpleAction(this, "MyAction", "View");
            myAction.ImageName = "Action_SimpleAction";
        }
    }
    public class EmptyListViewController : ViewController
    {
        public EmptyListViewController()
        {
            TargetObjectType = typeof(Customer);
            TargetViewType = ViewType.ListView;

        }
        protected override void OnActivated()
        {
            base.OnActivated();


           ((ListView)View).CollectionSource.Criteria["FullTextSearchCriteria"] = CriteriaOperator.Parse("StartsWith([Name], 'A233241dsaasfdascsa')");
        }
    }
}
