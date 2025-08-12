using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Orvosi__Idopont
{
    internal class PDF_generator : IDocument
    {
        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Header().Height(80).Background(Colors.Lime.Medium);
                page.Content().Background(Colors.Lime.Lighten5);
               
                page.Footer().Height(50).Background(Colors.Lime.Darken4);
            });
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

    }
}
