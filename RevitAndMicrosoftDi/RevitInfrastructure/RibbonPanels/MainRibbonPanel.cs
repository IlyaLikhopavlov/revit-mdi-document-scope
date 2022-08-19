using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using RevitAndMicrosoftDi.RevitInfrastructure.Commands;

namespace RevitAndMicrosoftDi.RevitInfrastructure.RibbonPanels
{
    public class MainRibbonPanel
    {
        private const string RevitAndMdiTabName = @"Revit 'n' MDI";

        private const string ToolButtonsPanelName = @"Tools Button";

        public void Create(UIControlledApplication application)
        {
            application.CreateRibbonTab(RevitAndMdiTabName);

            CreateToolButtonsPanel(application, RevitAndMdiTabName);
        }

        private static void CreateToolButtonsPanel(UIControlledApplication application, string tabName)
        {
            var revitPanel = application.CreateRibbonPanel(tabName, ToolButtonsPanelName);

            var path = Assembly.GetExecutingAssembly().Location;

            var pushButtonCommon =
                new PushButtonData(@"DoWork", @"Do Work", path, typeof(DoSomeWorkCommand).FullName);

            revitPanel.AddItem(pushButtonCommon);
        }
    }
}
