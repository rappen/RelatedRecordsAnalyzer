using System.ComponentModel.Composition;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Rappen.XTB.RRA
{
    // Do not forget to update version number and author (company attribute) in AssemblyInfo.cs class
    // To generate Base64 string for Images below, you can use https://www.base64-image.de/
    [Export(typeof(IXrmToolBoxPlugin)),
        ExportMetadata("Name", "Related Records Analyzer"),
        ExportMetadata("Description", "Select a record and find all related records."),
        // Please specify the base64 content of a 32x32 pixels image
        ExportMetadata("SmallImageBase64", null),
        // Please specify the base64 content of a 80x80 pixels image
        ExportMetadata("BigImageBase64", null),
        ExportMetadata("BackgroundColor", "#FFFFC0"),
        ExportMetadata("PrimaryFontColor", "#0000C0"),
        ExportMetadata("SecondaryFontColor", "#0000FF")]
    public class RRADescription : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new RRA();
        }
    }
}