using BlogDemo.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace BlogDemo.Module.Win.Controllers
{
    public class AccessGridController : ViewController
    {
        ImageCollection imageCollection1;
        public AccessGridController()
        {
            TargetViewType =  ViewType.ListView;
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            imageCollection1 = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();

            imageCollection1.ImageSize = new Size(100, 100);
            IEnumerable<Customer> CustomerCollection = ObjectSpace.GetObjects(typeof(Customer)).Cast<Customer>();
            foreach (Customer c in CustomerCollection)
            {
                imageCollection1.AddImage(c.Image, c.Name);
            }

            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();

        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.

            GridListEditor listEditor = ((ListView)View).Editor as GridListEditor;
            if (listEditor != null)
            {

                GridView gridView = listEditor.GridView;
              //  gridView.GroupRowHeight = 200;
              //  gridView.HtmlImages = imageCollection1;
                // gridView.GroupFormat = "[#image]{1} {2}";
              //  gridView.GroupFormat = "[#image]{1}";          
               //  gridView.CustomColumnDisplayText += GridView_CustomColumnDisplayText;

            }
        }

        private void GridView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.Caption == "Customer")
            {
                var cust =  e.Value as Customer;
                e.DisplayText = GetCustomColumnDisplayText(cust?.Name);
            }
               
        }

        private string GetCustomColumnDisplayText(string groupValueText)
        {
            string imgName = String.Empty;
            string testToDisplay = "Placeholder";
            if (!string.IsNullOrWhiteSpace(groupValueText))
            {
                testToDisplay = groupValueText;
            }

            try
            {
                //imgName = imageCollection1.Images.InnerImages[imgIndex].Name;
                imgName = imageCollection1.Images.InnerImages.Where(x=>x.Name == groupValueText).FirstOrDefault()?.Name;
            }
            catch(Exception ex)
            {
                var message = ex.Message;
            }
            var groupText = string.Format("<size=20><image={0}> {1}", imgName, testToDisplay); //<size=12><color=red><b>Web </b><color=0,255,0><i>Page </i><color=#0000FF><u>Address</u></color></size>   <image={0};size=100,100;align=bottom;height=100;width=100>
            return groupText;
        }


        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
