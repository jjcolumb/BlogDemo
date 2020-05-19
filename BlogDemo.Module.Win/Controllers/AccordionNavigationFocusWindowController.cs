using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.XtraBars.Navigation;
using System.Windows.Forms;

namespace BlogDemo.Module.Win.Controllers
{
    public class AccordionNavigationFocusWindowController : WindowController, IMessageFilter
    {
        const int WM_KEYDOWN = 0x0100;
        AccordionControl accordion;
        public AccordionNavigationFocusWindowController()
        {
            TargetWindowType = WindowType.Main;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            System.Windows.Forms.Application.AddMessageFilter(this);
            Frame.GetController<ShowNavigationItemController>().ShowNavigationItemAction.CustomizeControl += ShowNavigationItemAction_CustomizeControl;
        }
        private void ShowNavigationItemAction_CustomizeControl(object sender, CustomizeControlEventArgs e)
        {
            if (e.Control is AccordionControl)
            {
                accordion = (AccordionControl)e.Control;
                ((AccordionSearchControl)accordion.GetFilterControl()).NullValuePrompt = "Search (Ctrl+Q)";
            }
        }
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_KEYDOWN)
            {
                Keys key = (Keys)m.WParam.ToInt32();
                if (key == Keys.Q && Control.ModifierKeys == Keys.Control)
                {
                    ((AccordionSearchControl)accordion.GetFilterControl()).Focus();
                }
            }
            return false;
        }
    }
}
