# revit-mdi-document-scope
It's just an example of using Microsoft.Extensions.DependencyInjection with Autodesk Revit external application. 
The main aim of the project is showing how to create domain model instances in the scope of the Revit document.

Here it's snippet which describes how to get new instance of any object from the scope.

``` CSharp
[Transaction(TransactionMode.Manual)]
public class DoSomeWorkCommand : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        var document = commandData.Application.ActiveUIDocument.Document;

        var scopeFactory = RevitAndMdiApp.ServiceProvider.GetService<IDocumentServiceScopeFactory>();
        var documentScope = scopeFactory?.CreateDocumentScope(document);
        var docDependentEntity = documentScope?
            .ServiceProvider
            .GetService<IFactory<Document, DocDependentEntity>>()
            ?.New(document);

        docDependentEntity?.ShowIdentityInRevit();

        return Result.Succeeded;
    }
}
```
