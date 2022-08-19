using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitAndMicrosoftDi.Model
{
    public class DocDependentEntity
    {
        protected Document Document;

        protected Guid Guid { get; }

        public DocDependentEntity(Document document)
        {
            Document = document;
            Guid = Guid.NewGuid();
        }

        public string GetIdentity() => $"Object GUID: {{{Guid}}} Document Title: {{{Document.Title}}} Project ID: {{{Document.ProjectInformation.Id.IntegerValue}}}";

        public void ShowIdentityInRevit() => TaskDialog.Show(@"DocDependentEntity", GetIdentity());

    }
}
