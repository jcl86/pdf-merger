using PdfSharpCore.Pdf.IO;
using PdfSharpCore.Pdf;
using Microsoft.JSInterop;
using System.Reflection.Metadata;

namespace PdfMerger
{
    public class MergingService
    {
        private readonly IJSRuntime js;

        public MergingService(IJSRuntime js)
        {
            this.js = js;
        }

        public async Task MergePDFs(IEnumerable<byte[]> files)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var targetDoc = new PdfDocument())
                {
                    foreach (var file in files)
                    {
                        using (var stream = new MemoryStream(file))
                        {
                            using (var pdfDoc = PdfReader.Open(stream, PdfDocumentOpenMode.Import))
                            {
                                for (var i = 0; i < pdfDoc.PageCount; i++)
                                    targetDoc.AddPage(pdfDoc.Pages[i]);
                            }
                        }
                    }
                    targetDoc.Save(outputStream);
                }

                var content = outputStream.ToArray();
                var fileName = "output.pdf";
                await js.InvokeAsync<object>("blazorExtensions.saveAsFile", fileName, Convert.ToBase64String(content));
            }
        }
    }
}
