using System;
using System.IO;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.XtraPdfViewer;

namespace BlogDemo.Module.Win.Editors
{
    [PropertyEditor(typeof(FileData), false)] // IsDefaultEditor = false  
    public class PdfViewerPropertyEditor : WinPropertyEditor
    {
        public PdfViewerPropertyEditor(Type objectType, IModelMemberViewItem info)
            : base(objectType, info)
        {
        }

        public new PdfViewer Control
        {
            get { return ((PdfViewer)base.Control); }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && Control != null)
                {
                    Control.DocumentChanged -= PdfViewerOnDocumentChanged;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        protected override void ReadValueCore()
        {
            var fileData = PropertyValue as IFileData;
            if (fileData != null && fileData.FileName.ToLower().Contains(".pdf"))
            {
                using (var stream = new MemoryStream())
                {
                    fileData.SaveToStream(stream);
                    Control.LoadDocument(stream);
                }
            }
            else
            {
                Control.CloseDocument();
            }
        }

        protected override object CreateControlCore()
        {
            var pdfViewer = new PdfViewer
            {
                DetachStreamAfterLoadComplete = true
            };
            pdfViewer.DocumentChanged += PdfViewerOnDocumentChanged;
            return pdfViewer;
        }

        private void PdfViewerOnDocumentChanged(object sender, PdfDocumentChangedEventArgs e)
        {
            // if (Control.Parent != null)  
            //    Control.CreateBars();  
        }
    }
}
