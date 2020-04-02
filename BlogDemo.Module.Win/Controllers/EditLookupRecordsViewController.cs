using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp.Win.SystemModule;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDemo.Module.Win.Controllers
{
    public class EditLookupRecordsViewController : ViewController<DetailView>
    {
        Dictionary<LookupPropertyEditor, EventHandler> lookupPropertyEditorEventHandlers;
        Dictionary<LookupEdit, ButtonPressedEventHandler> lookupEditEventHandlers;

        protected override void OnActivated()
        {
            base.OnActivated();

            lookupPropertyEditorEventHandlers = new Dictionary<LookupPropertyEditor, EventHandler>();
            lookupEditEventHandlers = new Dictionary<LookupEdit, ButtonPressedEventHandler>();



            foreach (var item in View.Items.OfType<LookupPropertyEditor>())
            {

                var theItem = item;
                if (item.Control == null)
                {
                    EventHandler<EventArgs> controlCreated = null;

                    controlCreated = (s, e) =>
                    {
                        theItem.ControlCreated -= controlCreated;
                        AddOpenObjectButton(theItem);
                    };

                    item.ControlCreated += controlCreated;
                }
                else
                {
                    AddOpenObjectButton(item);
                }

            }
        }

        private void AddOpenObjectButton(LookupPropertyEditor item)
        {

            Image img = ImageLoader.Instance.GetImageInfo("Editor_Edit").Image;
            var ima = (Image)(new Bitmap(img, new Size(16, 16)));
            var openObjectButton = new EditorButton(ButtonPredefines.Glyph, ima, new SuperToolTip());



            if (item.PropertyValue == null)
            {
                openObjectButton.Enabled = false;
            }

            openObjectButton.Enabled = openObjectButton.Enabled && Frame.GetController<OpenObjectController>().Active.ResultValue;

            item.Control.Properties.Buttons.Insert(0, openObjectButton);
            item.Control.Properties.ActionButtonIndex = item.Control.Properties.Buttons.Count - 1;


            EventHandler controlValueChanged = (s, e) =>
            {
                var openObjectController = this.Frame.GetController<OpenObjectController>();
                openObjectButton.Enabled = openObjectController.OpenObjectAction.Active.ResultValue && item.ControlValue != null;
            };

            item.ControlValueChanged += controlValueChanged;
            lookupPropertyEditorEventHandlers[item] = controlValueChanged;

            ButtonPressedEventHandler buttonClick = (s, e) =>
            {
                if (e.Button == openObjectButton)
                {
                    var openObjectController = this.Frame.GetController<OpenObjectController>();
                    openObjectController.OpenObjectAction.DoExecute();
                }
            };

            item.Control.ButtonClick += buttonClick;
            lookupEditEventHandlers[item.Control] = buttonClick;
        }

        protected override void OnDeactivated()
        {
            foreach (var pair in lookupPropertyEditorEventHandlers)
                pair.Key.ControlValueChanged -= pair.Value;

            lookupPropertyEditorEventHandlers.Clear();


            foreach (var pair in lookupEditEventHandlers)
                pair.Key.ButtonClick -= pair.Value;

            lookupEditEventHandlers.Clear();

            base.OnDeactivated();
        }
    }
}
