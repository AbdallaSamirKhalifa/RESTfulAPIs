using System;
using Word = Microsoft.Office.Interop.Word;

namespace Win32APIs
{
    public class WordDocument
    {
        public static void CreateAWordDocument(string text, string filePath)
        {
            Word.Application wordApp = new Word.Application();

            try
            {
                wordApp.Visible = false;

                Word.Document doc = wordApp.Documents.Add();//creating new documint
                Word.Paragraph para = doc.Paragraphs.Add();
                para.Range.Text = text;

                doc.SaveAs2(filePath);
                doc.Close();
            }catch (Exception ex)
            {
                throw ex;
            }finally
            {
                wordApp.Quit();
            }
        }


    }
}
