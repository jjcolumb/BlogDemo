using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Web.TestScripts;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace BlogDemo.Module.Web.Editors
{

    [PropertyEditor(typeof(string), false)]
    public class ASPxTokenBoxPropertyEditor : ASPxStringPropertyEditor
    {
        private ASPxTokenBox tokenBox;

        public new ASPxTokenBox Editor => (ASPxTokenBox)base.Editor;
        public new ASPxTokenBox InplaceViewModeEditor => (ASPxTokenBox)base.InplaceViewModeEditor;

        public ASPxTokenBoxPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model)
        {
        }

        protected override WebControl CreateEditModeControlCore()
        {
            tokenBox = new ASPxTokenBox();
            tokenBox.IncrementalFilteringMode = IncrementalFilteringMode.None;
            RenderHelper.SetupASPxWebControl(tokenBox);
            SetupTokenBox(tokenBox);
            tokenBox.TextChanged += new EventHandler(EditValueChangedHandler);

            return tokenBox;
        }

        protected override WebControl CreateViewModeControlCore()
        {
            tokenBox = new ASPxTokenBox();
            tokenBox.ClientEnabled = false;
            SetupTokenBox(tokenBox);
            tokenBox.TokenRemoveButtonStyle.Height = 0;
            tokenBox.TokenRemoveButtonStyle.Width = 0;
            tokenBox.Border.BorderWidth = 0;

            return tokenBox;
        }

        protected virtual void SetupTokenBox(ASPxTokenBox tokenBox)
        {
            tokenBox.ShowDropDownOnFocus = ShowDropDownOnFocusMode.Never;
            tokenBox.TokenStyle.BackColor = System.Drawing.Color.DarkGreen;
            tokenBox.TokenRemoveButtonHoverStyle.BackColor = System.Drawing.Color.DarkGreen;
            tokenBox.Width = new Unit("100%");
        }

        protected override void ReadValueCore()
        {
            base.ReadValueCore();
            string value = (string)PropertyValue;

            if (value != null)
            {
                string[] tokens = value.Split(',', ';');

                foreach (string token in tokens)
                {
                    if (!string.IsNullOrEmpty(token) && !string.IsNullOrWhiteSpace(token))
                        tokenBox.Tokens.Add(token);
                }
            }
        }
    }
}
