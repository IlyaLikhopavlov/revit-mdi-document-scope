using System;
using Autodesk.Revit.DB;
using Microsoft.Extensions.DependencyInjection;

namespace RevitAndMicrosoftDi.DependencyInjection.ScopedServicesFunctionality
{
    internal interface IDocumentServiceScopeFactory : IDisposable
    {
        IServiceScope CreateDocumentScope(Document document);
    }
}
