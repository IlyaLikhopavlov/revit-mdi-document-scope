using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Microsoft.Extensions.DependencyInjection;
using RevitAndMicrosoftDi.DependencyInjection;
using RevitAndMicrosoftDi.DependencyInjection.ScopedServicesFunctionality;
using RevitAndMicrosoftDi.Model;
using RevitAndMicrosoftDi.RevitInfrastructure.Commands;
using RevitAndMicrosoftDi.RevitInfrastructure.RibbonPanels;

namespace RevitAndMicrosoftDi
{
    [Transaction(TransactionMode.Manual)]
    public class RevitAndMdiApp : IExternalApplication
    {
        private readonly MainRibbonPanel _mainPanel = new MainRibbonPanel();
        
        public static IServiceProvider ServiceProvider;

        public Result OnStartup(UIControlledApplication application)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IDocumentServiceScopeFactory, DocumentServiceScopeFactory>();
            serviceCollection.AddScoped<DocDependentEntity>();
            serviceCollection.AddFactoryFacility();

            ServiceProvider = serviceCollection.BuildServiceProvider();
            
            _mainPanel.Create(application);

            application.ControlledApplication.ApplicationInitialized += ControlledApplication_ApplicationInitialized;
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            ServiceProvider.GetService<IDocumentServiceScopeFactory>()?.Dispose();
            
            return Result.Succeeded;
        }

        private static bool DocumentWasChanged(Element currentActiveView, Element previousActiveView)
        {
            return !Equals(currentActiveView.Document, previousActiveView?.Document);
        }

        private void ControlledApplication_ApplicationInitialized(object sender, ApplicationInitializedEventArgs e)
        {
            var initCommand = new InitializationCommand();

            switch (sender)
            {
                case UIApplication uiApplication:
                {
                    uiApplication.ViewActivated += UiControlledApplicationOnViewActivated;
                    initCommand.Execute(uiApplication);
                    OnViewActivatedInternal(uiApplication.ActiveUIDocument.Document,
                        uiApplication.ActiveUIDocument.ActiveView, null);
                }
                    break;
                case Application application:
                {
                    var newUiApplication = new UIApplication(application);
                    newUiApplication.ViewActivated += UiControlledApplicationOnViewActivated;
                    initCommand.Execute(newUiApplication);
                    if (newUiApplication.ActiveUIDocument != null)
                    {
                        OnViewActivatedInternal(newUiApplication.ActiveUIDocument.Document,
                            newUiApplication.ActiveUIDocument.ActiveView, null);
                    }
                }
                    break;
            }
        }

        private void UiControlledApplicationOnViewActivated(object sender, ViewActivatedEventArgs e)
        {
            OnViewActivatedInternal(e.Document, e.CurrentActiveView, e.PreviousActiveView);
        }

        private void OnViewActivatedInternal(Document document, Element currentActiveView, Element previousActiveView)
        {
            if (document == null || currentActiveView == null)
            {
                return;
            }

            if (DocumentWasChanged(currentActiveView, previousActiveView))
            {

            }
        }
    }
}
