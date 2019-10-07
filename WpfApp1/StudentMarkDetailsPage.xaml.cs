using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using ViewModelLayer;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for StudentMarkDetailsPage.xaml
    /// </summary>
    public partial class StudentMarkDetailsPage : System.Windows.Controls.Page
    {
        public StudentResultDetails obj = new StudentResultDetails();
        System.Data.DataTable dtStudentMarksList = new System.Data.DataTable();
        //DataGridRow dRw = null;
        ObservableCollection<StudentResultDetails> lst = new ObservableCollection<StudentResultDetails>();
        public StudentMarkDetailsPage()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dtStudentMarksList = obj.ViewStudentMarksDetails();

            for (int i = 0; i < dtStudentMarksList.Rows.Count; i++)
            {
                StudentResultDetails st = new StudentResultDetails();
                st.ID = Convert.ToInt32(dtStudentMarksList.Rows[i]["ID"]);
                st.Name = Convert.ToString(dtStudentMarksList.Rows[i]["Name"]);
                st.Subject1 = Convert.ToInt32(dtStudentMarksList.Rows[i]["Subject1"]);
                st.Subject2 = Convert.ToInt32(dtStudentMarksList.Rows[i]["Subject2"]);
                st.Subject3 = Convert.ToInt32(dtStudentMarksList.Rows[i]["Subject3"]);
                st.Subject4 = Convert.ToInt32(dtStudentMarksList.Rows[i]["Subject4"]);
                st.Subject5 = Convert.ToInt32(dtStudentMarksList.Rows[i]["Subject5"]);
                st.Subject6 = Convert.ToInt32(dtStudentMarksList.Rows[i]["Subject6"]);
                st.Subject7 = Convert.ToInt32(dtStudentMarksList.Rows[i]["Subject7"]);
                st.ExamName = Convert.ToString(dtStudentMarksList.Rows[i]["ExamName"]);
                st.ExamMonth = Convert.ToString(dtStudentMarksList.Rows[i]["ExamMonth"]);
                st.Attendance = Convert.ToInt32(dtStudentMarksList.Rows[i]["Attendance"]);
                lst.Add(st);
            }
            this.grdStudentMark.ItemsSource = lst;
        }
        private void GrdStudentMark_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid rw = sender as DataGrid;
                if (rw != null && rw.SelectedItems != null && rw.SelectedItems.Count == 1)
                {
                    DataGridRow dRw = rw.ItemContainerGenerator.ContainerFromItem(rw.SelectedItem) as DataGridRow;

                    obj = dRw.Item as StudentResultDetails;
                    studID.Text = Convert.ToString(obj.ID);
                    studName.Text = Convert.ToString(obj.Name);
                    studSub1.Text = Convert.ToString(obj.Subject1);
                    studSub2.Text = Convert.ToString(obj.Subject2);
                    studSub3.Text = Convert.ToString(obj.Subject3);
                    studSub4.Text = Convert.ToString(obj.Subject4);
                    studSub5.Text = Convert.ToString(obj.Subject5);
                    studSub6.Text = Convert.ToString(obj.Subject6);
                    studSub7.Text = Convert.ToString(obj.Subject7);
                    studExmName.Text = Convert.ToString(obj.ExamName);
                    studExmMnth.Text = Convert.ToString(obj.ExamMonth);
                    studAtndnce.Text = Convert.ToString(obj.Attendance);

                    studID.IsEnabled = false;
                    studName.IsEnabled = false;
                }
            }
        }
        private void AddStudMark_Click(object sender, RoutedEventArgs e)
        {
            if (studID.Text.Length == 0 || studName.Text.Length == 0 || studSub1.Text.Length == 0 || studSub2.Text.Length == 0 ||
                studSub3.Text.Length == 0 || studSub4.Text.Length == 0 || studSub5.Text.Length == 0 || studSub6.Text.Length == 0 ||
                studSub7.Text.Length == 0 || studExmName.Text.Length == 0 || studExmMnth.Text.Length == 0 || studAtndnce.Text.Length == 0)
            {
                MessageBox.Show("Please enter all mandatory fields.", "Validation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (studID.Text.Length != 0 || studName.Text.Length != 0 || studSub1.Text.Length != 0 || studSub2.Text.Length != 0 ||
                studSub3.Text.Length != 0 || studSub4.Text.Length != 0 || studSub5.Text.Length != 0 || studSub6.Text.Length != 0 ||
                studSub7.Text.Length != 0 || studExmName.Text.Length != 0 || studExmMnth.Text.Length != 0 || studAtndnce.Text.Length != 0)
            {
                StudentResultDetails st = new StudentResultDetails();

                obj.ID = Convert.ToInt32(studID.Text);
                obj.Name = Convert.ToString(studName.Text);
                obj.Subject1 = Convert.ToInt32(studSub1.Text);
                obj.Subject2 = Convert.ToInt32(studSub2.Text);
                obj.Subject3 = Convert.ToInt32(studSub3.Text);
                obj.Subject4 = Convert.ToInt32(studSub4.Text);
                obj.Subject5 = Convert.ToInt32(studSub5.Text);
                obj.Subject6 = Convert.ToInt32(studSub6.Text);
                obj.Subject7 = Convert.ToInt32(studSub7.Text);
                obj.ExamName = Convert.ToString(studExmName.Text);
                obj.ExamMonth = Convert.ToString(studExmMnth.Text);
                obj.Attendance = Convert.ToInt32(studAtndnce.Text);

                st = obj;

                lst.Add(st);
                this.grdStudentMark.ItemsSource = lst;

                obj.AddStudentMarks(st);
                studID.Clear();
                studName.Clear();
                studSub1.Clear();
                studSub2.Clear();
                studSub3.Clear();
                studSub4.Clear();
                studSub5.Clear();
                studSub6.Clear();
                studSub7.Clear();
                studExmName.Clear();
                studExmMnth.Clear();
                studAtndnce.Clear();
            }
        }

        private void UpdtStudMark_Click(object sender, RoutedEventArgs e)
        {
            if (studID.Text.Length == 0 || studName.Text.Length == 0 || studSub1.Text.Length == 0 || studSub2.Text.Length == 0 ||
                studSub3.Text.Length == 0 || studSub4.Text.Length == 0 || studSub5.Text.Length == 0 || studSub6.Text.Length == 0 ||
                studSub7.Text.Length == 0 || studExmName.Text.Length == 0 || studExmMnth.Text.Length == 0 || studAtndnce.Text.Length == 0)
            {
                MessageBox.Show("Please enter all mandatory fields.", "Validation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (studID.Text.Length != 0 || studName.Text.Length != 0 || studSub1.Text.Length != 0 || studSub2.Text.Length != 0 ||
                studSub3.Text.Length != 0 || studSub4.Text.Length != 0 || studSub5.Text.Length != 0 || studSub6.Text.Length != 0 ||
                studSub7.Text.Length != 0 || studExmName.Text.Length != 0 || studExmMnth.Text.Length != 0 || studAtndnce.Text.Length != 0)
            {
                obj.ID = Convert.ToInt32(studID.Text);
                obj.Subject1 = Convert.ToInt32(studSub1.Text);
                obj.Subject2 = Convert.ToInt32(studSub2.Text);
                obj.Subject3 = Convert.ToInt32(studSub3.Text);
                obj.Subject4 = Convert.ToInt32(studSub4.Text);
                obj.Subject5 = Convert.ToInt32(studSub5.Text);
                obj.Subject6 = Convert.ToInt32(studSub6.Text);
                obj.Subject7 = Convert.ToInt32(studSub7.Text);
                obj.ExamName = Convert.ToString(studExmName.Text);
                obj.ExamMonth = Convert.ToString(studExmMnth.Text);
                obj.Attendance = Convert.ToInt32(studAtndnce.Text);

                obj.UpdateStudentMarks(obj.ID);
                studID.IsEnabled = true;
                studName.IsEnabled = true;

                this.grdStudentMark.Items.Refresh();
            }
        }

        private void DelStudMark_Click(object sender, RoutedEventArgs e)
        {
            if (studID.Text.Length == 0 || studName.Text.Length == 0 || studSub1.Text.Length == 0 || studSub2.Text.Length == 0 ||
                studSub3.Text.Length == 0 || studSub4.Text.Length == 0 || studSub5.Text.Length == 0 || studSub6.Text.Length == 0 ||
                studSub7.Text.Length == 0 || studExmName.Text.Length == 0 || studExmMnth.Text.Length == 0 || studAtndnce.Text.Length == 0)
            {
                MessageBox.Show("Please enter all mandatory fields.", "Validation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (studID.Text.Length != 0 || studName.Text.Length != 0 || studSub1.Text.Length != 0 || studSub2.Text.Length != 0 ||
                studSub3.Text.Length != 0 || studSub4.Text.Length != 0 || studSub5.Text.Length != 0 || studSub6.Text.Length != 0 ||
                studSub7.Text.Length != 0 || studExmName.Text.Length != 0 || studExmMnth.Text.Length != 0 || studAtndnce.Text.Length != 0)
            {
                obj.ID = Convert.ToInt32(studID.Text);

                obj.DeleteStudentMarks(obj.ID);

                lst.Remove(obj);
                this.grdStudentMark.ItemsSource = lst;

                studID.Clear();
                studName.Clear();
                studSub1.Clear();
                studSub2.Clear();
                studSub3.Clear();
                studSub4.Clear();
                studSub5.Clear();
                studSub6.Clear();
                studSub7.Clear();
                studExmName.Clear();
                studExmMnth.Clear();
                studAtndnce.Clear();
                studID.IsEnabled = true;
                studName.IsEnabled = true;

                this.grdStudentMark.Items.Refresh();
            }
        }

        private void ExportStudMark_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnExcel_Click(object sender, RoutedEventArgs e)
        {
            System.Data.DataTable dtCpy1 = dtStudentMarksList.Copy();
            dtCpy1.TableName = "Semester";
            DataSet ds = new DataSet();
            ds.Tables.Add(dtCpy1);

            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            string strPath = @"C:\New Folder\Semester_Details.xlsx";

            Microsoft.Office.Interop.Excel.Workbook excelWorkBook = excelApp.Workbooks.Open(strPath);


            foreach (System.Data.DataTable table in ds.Tables)
            {
                if (excelWorkBook.Sheets.Count != 0)
                {
                    Worksheet sht = (Worksheet)excelWorkBook.Worksheets[table.TableName];
                    sht.Name = "RemoveSheet";
                }
                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();

                excelWorkSheet.Name = table.TableName;

                for (int i = 1; i < table.Columns.Count + 1; i++)
                {
                    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                }

                for (int j = 0; j < table.Rows.Count; j++)
                {
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                    }
                }
            }
            excelWorkBook.Worksheets[2].Delete();
            excelWorkBook.Save();
            excelWorkBook.Close();
            excelApp.Quit();
        }

        private void BtnXML_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "Student_Marks_Details" + ".xml";
            dlg.Filter = "XML Files .xml|*.xml";
            dlg.InitialDirectory = @"C:\New folder\";
            dlg.ShowDialog();
            XmlWriterSettings stngs = new XmlWriterSettings();
            stngs.Indent = true;
            stngs.IndentChars = ("\t");
            Dictionary<string, string> lst = new Dictionary<string, string>();

            for (int i = 0; i < dtStudentMarksList.Rows.Count; i++)
            {
                string strID = Convert.ToString(dtStudentMarksList.Rows[i]["ID"]);
                string strName = Convert.ToString(dtStudentMarksList.Rows[i]["Name"]);

                if (!lst.ContainsKey(strID))
                {
                    lst.Add(strID, strName);
                }
            }

            using (XmlWriter xml = XmlWriter.Create(dlg.FileName, stngs))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("StudentMarksDetails");
                for (int j = 0; j < lst.Count; j++)
                {
                    xml.WriteStartElement("Student");
                    xml.WriteAttributeString("ID", lst.Keys.ElementAt(j));
                    xml.WriteAttributeString("StudentName", lst.Values.ElementAt(j));

                    for (int i = 0; i < dtStudentMarksList.Rows.Count; i++)
                    {
                        if (Convert.ToString(dtStudentMarksList.Rows[i]["ID"]) == lst.Keys.ElementAt(j))
                        {
                            xml.WriteStartElement(Convert.ToString(dtStudentMarksList.Rows[i]["ExamName"]));
                            xml.WriteStartElement("ExamMonth");
                            xml.WriteString(Convert.ToString(dtStudentMarksList.Rows[i]["ExamMonth"]));
                            xml.WriteEndElement();
                            xml.WriteStartElement("Subject1");
                            xml.WriteString(Convert.ToString(dtStudentMarksList.Rows[i]["Subject1"]));
                            xml.WriteEndElement();
                            xml.WriteStartElement("Subject2");
                            xml.WriteString(Convert.ToString(dtStudentMarksList.Rows[i]["Subject2"]));
                            xml.WriteEndElement();
                            xml.WriteStartElement("Subject3");
                            xml.WriteString(Convert.ToString(dtStudentMarksList.Rows[i]["Subject3"]));
                            xml.WriteEndElement();
                            xml.WriteStartElement("Subject4");
                            xml.WriteString(Convert.ToString(dtStudentMarksList.Rows[i]["Subject4"]));
                            xml.WriteEndElement();
                            xml.WriteStartElement("Subject5");
                            xml.WriteString(Convert.ToString(dtStudentMarksList.Rows[i]["Subject5"]));
                            xml.WriteEndElement();
                            xml.WriteStartElement("Subject6");
                            xml.WriteString(Convert.ToString(dtStudentMarksList.Rows[i]["Subject6"]));
                            xml.WriteEndElement();
                            xml.WriteStartElement("Subject7");
                            xml.WriteString(Convert.ToString(dtStudentMarksList.Rows[i]["Subject7"]));
                            xml.WriteEndElement();
                            xml.WriteStartElement("Attendance");
                            xml.WriteString(Convert.ToString(dtStudentMarksList.Rows[i]["Attendance"]));
                            xml.WriteEndElement();
                            xml.WriteEndElement();
                        }
                    }
                    xml.WriteEndElement();
                }
                xml.WriteEndElement();
                xml.WriteEndDocument();
            }
        }

        private void BtnCSV_Click(object sender, RoutedEventArgs e)
        {
            string sprts = ",";
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "Student_Marks_Details.csv";
            dlg.Filter = "CSV Files (*.csv)|*.csv";
            dlg.InitialDirectory = @"C:\New folder\";
            dlg.ShowDialog();
            FileStream fstrm = new FileStream(dlg.FileName, FileMode.OpenOrCreate);
            StreamWriter strmWrtr = new StreamWriter(fstrm);
            strmWrtr.WriteLine();
            StringBuilder strCsv = new StringBuilder();
            strCsv.AppendLine("ID, StdentName, ExamName, ExamMonth, Subject1, Subject2, Subject3, Subject4, Subject5, Subject6, Subject7, Attendance");
            for (int i = 0; i < dtStudentMarksList.Rows.Count; i++)
            {
                string strCntnt2 = Convert.ToString(dtStudentMarksList.Rows[i]["ID"]) + sprts +
                                    Convert.ToString(dtStudentMarksList.Rows[i]["Name"]) + sprts +
                                    Convert.ToString(dtStudentMarksList.Rows[i]["ExamName"]) + sprts +
                                    Convert.ToString(dtStudentMarksList.Rows[i]["ExamMonth"]) + sprts +
                                    Convert.ToString(dtStudentMarksList.Rows[i]["Subject1"]) + sprts +
                                    Convert.ToString(dtStudentMarksList.Rows[i]["Subject2"]) + sprts +
                                    Convert.ToString(dtStudentMarksList.Rows[i]["Subject3"]) + sprts +
                                    Convert.ToString(dtStudentMarksList.Rows[i]["Subject4"]) + sprts +
                                    Convert.ToString(dtStudentMarksList.Rows[i]["Subject5"]) + sprts +
                                    Convert.ToString(dtStudentMarksList.Rows[i]["Subject6"]) + sprts +
                                    Convert.ToString(dtStudentMarksList.Rows[i]["Subject7"]) + sprts +
                                    Convert.ToString(dtStudentMarksList.Rows[i]["Attendance"]) + sprts;
                strCsv.AppendLine(strCntnt2);
            }
            strmWrtr.WriteLine(strCsv);
            strmWrtr.Close();
            fstrm.Close();
        }
    }
}
