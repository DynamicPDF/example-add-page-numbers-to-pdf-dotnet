using ceTe.DynamicPDF;
using ceTe.DynamicPDF.Merger;
using ceTe.DynamicPDF.PageElements;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace example_add_page_numbers_to_pdf_dotnet
{
    // This example shows how to add page numbers to a new and an existing PDF document.
    // It references the ceTe.DynamicPDF.CoreSuite.NET NuGet package.
    class Program
    {
        static void Main(string[] args)
        {
            AddPageNumbersToExistingPDF();

            CreatePDFWithPageNumbers();
        }

        // This examples uses the DynamicPDF Merger for .NET Product
        // Use the ceTe.DynamicPDF namespace for the Template class
        // Use the ceTe.DynamicPDF.PageElements namespace for the PageNumberingLabel element.
        // Use the ceTe.DynamicPDF.Merger namespace for the MergeDocument class.
        private static void AddPageNumbersToExistingPDF()
        {
            //Create a MergeDocument object using the existing PDF
            MergeDocument document = new MergeDocument(GetResourcePath("doc-a.pdf"));

            //Create a template bject
            Template template = new Template();

            //Create a PageNumberingLabel and add it to the template
            PageNumberingLabel pageLabels = new PageNumberingLabel("%%CP%% of %%TP%%", 0, 0, 200, 20);
            template.Elements.Add(pageLabels);

            //Set template to the document
            document.Template = template;

            //Save document.
            document.Draw("output-existing-pdf.pdf");
        }

        // This example uses the DynamicPDF Generator for .NET product.
        // Use the ceTe.DynamicPDF namespace for the Document, Page and Template classes.
        // Use the ceTe.DynamicPDF.PageElements namespace for the PageNumberingLabel class.
        private static void CreatePDFWithPageNumbers()
        {
            //Create document object
            Document document = new Document();

            //Create a Template object and set it to the document
            Template documentTemplate = new Template();
            document.Template = documentTemplate;

            //Add page numbering label to the template
            documentTemplate.Elements.Add(new PageNumberingLabel("%%PR%%%%SP%% of %%ST%%", 0, 680, 512, 12, Font.Helvetica, 12, TextAlign.Center));

            //Start a section with pagenumbering style in the document and add pages to it
            document.Sections.Begin(NumberingStyle.RomanLowerCase);
            document.Pages.Add(new Page()); //Page 1
            document.Pages.Add(new Page()); //Page 2
            document.Pages.Add(new Page()); //Page 3

            //Start a section with pagenumbering style in the document and add pages to it
            document.Sections.Begin(NumberingStyle.Numeric);
            document.Pages.Add(new Page()); //Page 4
            document.Pages.Add(new Page()); //page 5
            document.Pages.Add(new Page()); //page 6
            document.Pages.Add(new Page()); //page 7

            //Start a section with pagenumbering style in the document and add pages to it
            document.Sections.Begin(NumberingStyle.RomanLowerCase, "Appendix A - ");
            document.Pages.Add(new Page()); //page 8
            document.Pages.Add(new Page()); //page 9

            document.Draw("output-new-pdf.pdf");
        }

        // This is a helper function to get a full path to a file in the Resources folder.
        public static string GetResourcePath(string inputFileName)
        {
            var exePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return System.IO.Path.Combine(appRoot, "Resources", inputFileName);
        }
    }
}