using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNS_Reklamacije.Services
{
    public static class StampanjeService
    {
        public static void Stampaj(Reklamacija reklamacija, string Stampac, int BrojKopija)
        {
            if (!File.Exists(@$"{Directory.GetCurrentDirectory()}\TEMPLATE.docx"))
            {
                Byte[] bytes = Properties.Resources.TEMPLATE;

                using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(@$"{Directory.GetCurrentDirectory()}\TEMPLATE.docx", FileMode.Create)))
                {
                    binaryWriter.Write(bytes);
                }
            }

                try
            {
                Podesavanja sts = PodesavanjaServices.ProcitajFajl();

                var aplication = new Microsoft.Office.Interop.Word.Application();
                var doc = new Microsoft.Office.Interop.Word.Document();

                try
                {
                    doc = aplication.Documents.Add(Template: @$"{Directory.GetCurrentDirectory()}\TEMPLATE.docx");
                    //doc = aplication.Documents.Add(Template: Properties.Resources.TEMPLATE);
                    doc.Fields.Update();
                }
                catch (Exception)
                {
                    MessageBox.Show("Template.docx fajl nije pronađen !");
                    return;
                }

                foreach (Microsoft.Office.Interop.Word.Section section in doc.Sections)
                {
                    doc.Fields.Update();

                    HeadersFooters headers = section.Headers;
                    foreach (HeaderFooter header in headers)
                    {
                        Fields fields = header.Range.Fields;
                        popuni(fields, reklamacija, sts, aplication);
                    }

                    HeadersFooters footers = section.Footers;
                    foreach (HeaderFooter footer in footers)
                    {
                        Fields fields = footer.Range.Fields;
                        popuni(fields, reklamacija, sts, aplication);
                    }
                }

                doc.Fields.Update();
                Fields docFields = doc.Fields;
                popuni(docFields, reklamacija, sts, aplication);
                

                try
                {
                    aplication.ActivePrinter = Stampac;
                    aplication.PrintOut(Copies: BrojKopija);
                }
                catch (Exception)
                {
                    MessageBox.Show("Došlo je do greške pri komunikaciji sa štampačem !");
                    return;
                }

                object saveOptions = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
                object originalFormat = Microsoft.Office.Interop.Word.WdOriginalFormat.wdOriginalDocumentFormat;
                object routeDocument = false;

                doc.Close(ref saveOptions, ref originalFormat, ref routeDocument);
                aplication.Quit();
            }
            catch (Exception)
            {
                MessageBox.Show("Došlo je do greške pri štampanju !");
            }
        }


        private static string tekst(string s)
        {
            return s == string.Empty ? "N/A" : s;
        }
        private static void popuni(Fields fields, Reklamacija rek, Podesavanja sts, Microsoft.Office.Interop.Word.Application aplication)
        {
            foreach (Microsoft.Office.Interop.Word.Field field in fields)
            {
                if (field.Code.Text.Contains("Kupac"))
                {
                    field.Select();
                    aplication.Selection.TypeText(tekst(rek.Kupac));
                }
                else if (field.Code.Text.Contains("ID"))
                {
                    field.Select();
                    aplication.Selection.TypeText(tekst(rek.ID.ToString()));
                }
                else if (field.Code.Text.Contains("PostanskiBroj"))
                {
                    field.Select();
                    aplication.Selection.TypeText(tekst(sts.Postanskibroj));
                }
                else if (field.Code.Text.Contains("DanasnjiDatum"))
                {
                    field.Select();
                    aplication.Selection.TypeText(tekst(DateTime.Now.ToString("dd.MM.yyyy")));
                }
                else if (field.Code.Text.Contains("MaticniBroj"))
                {
                    field.Select();
                    aplication.Selection.TypeText(tekst(sts.MaticniBroj));
                }
                else if (field.Code.Text.Contains("OpisKvara"))
                {
                    field.Select();
                    aplication.Selection.TypeText(tekst(rek.Napomena));
                }
                else if (field.Code.Text.Contains("Dobavljac"))
                {
                    field.Select();
                    aplication.Selection.TypeText(tekst(rek.Dobavljac));
                }
                else if (field.Code.Text.Contains("DatumKupovine"))
                {
                    field.Select();
                    aplication.Selection.TypeText(tekst(rek.KupDatum is null ? "" : rek.KupDatum.Value.ToString("dd.MM.yyyy")));
                }
                else if (field.Code.Text.Contains("Razresenje"))
                {
                    field.Select();
                    aplication.Selection.TypeText(tekst(rek.Razresenje));
                }
                else if (field.Code.Text.Contains("Proizvodjac"))
                {
                    field.Select();
                    aplication.Selection.TypeText(tekst(rek.Proizvodjac));
                }
                else if (field.Code.Text.Contains("NazivModela"))
                {
                    field.Select();
                    aplication.Selection.TypeText(tekst(rek.NazivModela));
                }
                else if (field.Code.Text.Contains("OpisModela"))
                {
                    field.Select();
                    aplication.Selection.TypeText(tekst(rek.OpisModela));
                }
                else if (field.Code.Text.Contains("Serijski"))
                {
                    field.Select();
                    aplication.Selection.TypeText(tekst(rek.Serijski));
                }
                else if (field.Code.Text.Contains("ImeFirme"))
                {
                    field.Select();
                    aplication.Selection.TypeText(tekst(sts.ImeFirme));
                }
                else if (field.Code.Text.Contains("PIB"))
                {
                    field.Select();
                    aplication.Selection.TypeText(tekst(sts.PIB));
                }
                else if (field.Code.Text.Contains("Adresa"))
                {
                    field.Select();
                    aplication.Selection.TypeText(tekst(sts.Adresa));
                }
                else if (field.Code.Text.Contains("BrojTelefona"))
                {
                    field.Select();
                    aplication.Selection.TypeText(tekst(sts.BrojTelefona));
                }
                else if (field.Code.Text.Contains("EMail"))
                {
                    field.Select();
                    aplication.Selection.TypeText(tekst(sts.EMail));
                }
            }
        }
    }
}
