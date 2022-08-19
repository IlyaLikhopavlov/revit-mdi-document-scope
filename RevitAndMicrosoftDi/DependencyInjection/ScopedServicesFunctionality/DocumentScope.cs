using System;
using Autodesk.Revit.DB;
using Microsoft.Extensions.DependencyInjection;

namespace RevitAndMicrosoftDi.DependencyInjection.ScopedServicesFunctionality
{
    public class DocumentScope : IServiceScope
    {
        public IServiceProvider ServiceProvider => _serviceScope.ServiceProvider;

        private readonly IServiceScope _serviceScope;

        public DocumentScope(IServiceScope serviceScope, Document document)
        {
            _serviceScope = serviceScope;
            Document = document;
        }

        public Document Document { get; }

        public void Dispose()
        {
            _serviceScope?.Dispose();
        }
    }
}
