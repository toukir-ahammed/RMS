using System;

using System.Data;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Windows.Forms;
using System.Web;

namespace RMSDataModel
{
    public partial class PDFGenerator : Form
    {

        #region Functions
        String[][] markList;
        String registrationNumber, examRoll, studentName, semester;
        Student student;
        //public PDFGenerator(String[][] markList, String registrationNumber, String examRoll, String studentName, String semester)
        //{
        //    this.markList = markList;
        //    this.registrationNumber = registrationNumber;
        //    this.examRoll = examRoll;
        //    this.studentName = studentName;
        //    this.semester = semester;
        //    //InitializeComponent();
        //    makepdf();
        //    //DemonstrateMergeTable();
        //}
        public PDFGenerator(String[][] markList, Student student)
        {
            this.markList = markList;
            this.student = student;
            makepdf();
        }

        DataTable MakeDataTable()
        {

            //Create friend table object
            DataTable mark = new DataTable();


            //Define columns
            // mark.Columns.Add("Sudent Name");
            //mark.Columns.Add("Course Name");
            // mark.Columns.Add("Exam Roll");
            // mark.Columns.Add("Cgpa");
            for (int i = 0; i < markList[0].Length; i++)
            {
                mark.Columns.Add(markList[0][i]);
            }
            for (int i = 1; i < markList.Length; i++)
            {
                DataRow row = mark.NewRow();
                for (int j = 0; j < markList[0].Length; j++)
                {
                    row[j] = markList[i][j];
                }
                mark.Rows.Add(row);
            }

            //Populate with friends :)
            // mark.Rows.Add("Tushar", "DesignPattern", "818", "2.0");
            // mark.Rows.Add("Toukir", "DesignPattern", "818", "2.0");
            // mark.Tables[0].MergeColumns("name", "lastname", "firstname");





            return mark;

        }
        #endregion

        void makeTable()
        {
            DataTable mark = new DataTable();


        }

        #region Events
        void ExportDataTableToPdf(DataTable dtblTable, String strPdfPath, string strHeader)
        {
            System.IO.FileStream fs = new FileStream(strPdfPath, FileMode.Create, FileAccess.Write, FileShare.None);
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();

            //Report Header
            BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntHead = new Font(bfntHead, 16, 1, Color.BLACK);
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_CENTER;
            document.Add(prgHeading);

            Paragraph para = new Paragraph();

            para.Alignment = Element.ALIGN_CENTER;
            para.Add(new Chunk(strHeader.ToUpper(), fntHead));
            para.Add(new Chunk("\nName: " + student.Name));
            para.Add(new Chunk("\nRegistration No.: " + student.RegistrationNumber));
            para.Add(new Chunk("\nSesssion: " + student.Session));
            para.Add(new Chunk("\nExam Roll: " + student.ExamRoll));
            para.Add(new Chunk("\nDepartment: " + student.Department.Name));
            para.Add(new Chunk("\nSemester: " + student.Semester.ToString()));
       
            document.Add(para);
           

            //Author


            //Add a line seperation
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(p);

            //Add line break
            document.Add(new Chunk("\n", fntHead));

            //Write the table
            PdfPTable table = new PdfPTable(dtblTable.Columns.Count);
            //Table header
            BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntColumnHeader = new Font(btnColumnHeader, 10, 1, Color.WHITE);
            //for (int i = 0; i < dtblTable.Columns.Count; i++)
            //{
            //    PdfPCell cell = new PdfPCell();
            //    cell.BackgroundColor = Color.GRAY;
            //    cell.AddElement(new Chunk(dtblTable.Columns[i].ColumnName.ToUpper(), fntColumnHeader));
            //    table.AddCell(cell);

            //}

            addDataToTable(table, "Course Code", fntColumnHeader, Color.GRAY);
            addDataToTable(table, "Course Title", fntColumnHeader, Color.GRAY);
            addDataToTable(table, "Credits", fntColumnHeader, Color.GRAY);
            addDataToTable(table, "Grade", fntColumnHeader, Color.GRAY);
            addDataToTable(table, "Grade Point", fntColumnHeader, Color.GRAY);

            BaseFont baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font font = new Font(baseFont, 10, 1, Color.BLACK);
            //table Data
            for (int i = 0; i < dtblTable.Rows.Count; i++)
            {
                for (int j = 0; j < dtblTable.Columns.Count; j++)
                {
                    PdfPCell cell = new PdfPCell();
                    cell.AddElement(new Chunk(dtblTable.Rows[i][j].ToString(), font));
                    table.AddCell(cell);
                }
            }

            document.Add(table);

            //document.Add(new Chunk("\n", fntHead));
            //document.Add(new Chunk("\n", fntHead));

            //PdfPTable table2 = new PdfPTable(6);
            //PdfPCell cell1 = new PdfPCell();
            //cell1.Colspan = 6;
            //cell1.AddElement(new Chunk("Total Credits", font));
            //table2.AddCell(cell1);

            //ResultViewModel viewmodel = new ResultViewModel();
            //PdfPCell cell2 = new PdfPCell();
            //cell2.Colspan = 6;
            //cell2.AddElement(new Chunk(viewmodel.Semester, font));
            //table2.AddCell(cell1);




            Paragraph prgAuthor = new Paragraph();
            BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntAuthor = new Font(btnAuthor, 8, 2, Color.GRAY);
            prgAuthor.Alignment = Element.ALIGN_RIGHT;
            prgAuthor.Add(new Chunk("Generated By : Result Management System", fntAuthor));
            prgAuthor.Add(new Chunk("\nGeneration Date : " + DateTime.Now, fntAuthor));
            document.Add(prgAuthor);

            document.Close();
            writer.Close();
            fs.Close();
        }

        private void addDataToTable(PdfPTable table, string element, Font font, Color bgcolor)
        {
            PdfPCell cell = new PdfPCell();
            cell.BackgroundColor = bgcolor;
            cell.AddElement(new Chunk(element, font));
            table.AddCell(cell);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtbl = MakeDataTable();
                ExportDataTableToPdf(dtbl, @"F:\test.pdf", "Tabulation Sheet");
                // if (cbxOpen.Checked)
                // {
                System.Diagnostics.Process.Start(@"E:\test.pdf");
                this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                // }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }

        private void makepdf()
        {
            try
            {
                DataTable dtbl = MakeDataTable();
                string dateTime = DateTime.Now.ToString();
                string path = HttpContext.Current.Server.MapPath("~/Transcripts/");
                path = Path.Combine(path, student.RegistrationNumber+".pdf");
                ExportDataTableToPdf(dtbl, path, "Transcript");
                // if (cbxOpen.Checked)
                // {
                //System.Diagnostics.Process.Start(@"E:\test.pdf");
                this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                // }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }
        #endregion

    }
}
