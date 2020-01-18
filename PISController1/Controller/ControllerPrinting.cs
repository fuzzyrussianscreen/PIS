using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PISController1.Controller
{
    public class ControllerPrinting
    {
        private PISDbContext context;
        public ControllerPrinting(PISDbContext context)
        {
            this.context = context;
        }

        public void savePDF(string title, DataGridView dataGridView1, string data)
        {
            if (MessageBox.Show("Напечатать отчет?", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "pdf|*.pdf"
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    int countColumn = 0;
                    string FONT_LOCATION = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.TTF"); //определяем В СИСТЕМЕ(чтобы не копировать файл) расположение шрифта arial.ttf
                    BaseFont baseFont = BaseFont.CreateFont(FONT_LOCATION, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED); //создаем шрифт
                    iTextSharp.text.Font fontParagraph = new iTextSharp.text.Font(baseFont, 17, iTextSharp.text.Font.NORMAL); //регистрируем + можно задать параметры для него(17 - размер, последний параметр - стиль)

                    var dataStr = new Phrase(data,
                    new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD));
                    iTextSharp.text.Paragraph paragraphdata = new
                   iTextSharp.text.Paragraph(dataStr)
                    {
                        Alignment = Element.ALIGN_RIGHT - 1,
                        SpacingAfter = 12,
                    };

                    var phraseTitle = new Phrase(title,
                    new iTextSharp.text.Font(baseFont, 18, iTextSharp.text.Font.BOLD));
                    iTextSharp.text.Paragraph paragraph = new
                   iTextSharp.text.Paragraph(phraseTitle)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 12
                    };

                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        if (dataGridView1.Columns[i].Visible == true) countColumn++;
                    }

                    PdfPTable table = new PdfPTable(countColumn);

                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        if (dataGridView1.Columns[i].Visible == true)
                        {
                            table.AddCell(new Phrase(dataGridView1.Columns[i].HeaderCell.Value.ToString(), fontParagraph));
                            countColumn++;
                        }
                    }
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            if (dataGridView1.Columns[j].Visible == true)
                            {
                                table.AddCell(new Phrase(dataGridView1.Rows[i].Cells[j].Value.ToString(), fontParagraph));
                            }
                        }
                    }


                    using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        iTextSharp.text.Document pdfDoc;
                        if (dataGridView1.Columns.Count == 13)
                        {
                            pdfDoc = new iTextSharp.text.Document(PageSize.A2, 10f, 10f, 10f, 0f);
                        }
                        else
                        {
                            pdfDoc = new Document();
                        }
                        PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();
                        pdfDoc.Add(paragraph);
                        pdfDoc.Add(paragraphdata);
                        pdfDoc.Add(table);
                        pdfDoc.Close();
                        stream.Close();
                    }
                }
            }
        }

        public void saveDiagramm(string title, Chart chart)
        {
            if (MessageBox.Show("Напечатать отчет?", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "pdf|*.pdf"
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string FONT_LOCATION = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.TTF"); //определяем В СИСТЕМЕ(чтобы не копировать файл) расположение шрифта arial.ttf
                        BaseFont baseFont = BaseFont.CreateFont(FONT_LOCATION, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED); //создаем шрифт
                        iTextSharp.text.Font fontParagraph = new iTextSharp.text.Font(baseFont, 17, iTextSharp.text.Font.NORMAL); //регистрируем + можно задать параметры для него(17 - размер, последний параметр - стиль)

                        var phraseTitle = new Phrase(title,
                        new iTextSharp.text.Font(baseFont, 18, iTextSharp.text.Font.BOLD));
                        iTextSharp.text.Paragraph paragraph = new
                       iTextSharp.text.Paragraph(phraseTitle)
                        {
                            Alignment = Element.ALIGN_CENTER,
                            SpacingAfter = 12
                        };

                        chart.SaveImage(sfd.FileName + ".png", System.Drawing.Imaging.ImageFormat.Png);


                        Document document = new iTextSharp.text.Document(PageSize.A2, 10f, 10f, 10f, 0f);
                        using (var stream = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write, FileShare.None))

                        {
                            PdfWriter.GetInstance(document, stream);
                            document.Open();
                            using (var imageStream = new FileStream(sfd.FileName + ".png", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                var image = Image.GetInstance(imageStream);
                                document.Add(paragraph);
                                document.Add(image);
                            }
                            document.Close();
                            File.Delete(sfd.FileName + ".png");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("ERROR");
                    }
                }
            }
        }
    }
}

