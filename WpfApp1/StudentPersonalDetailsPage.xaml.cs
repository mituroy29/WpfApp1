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
    /// Interaction logic for StudentPersonalDetailsPage.xaml
    /// </summary>
    public partial class StudentPersonalDetailsPage : System.Windows.Controls.Page
    {
        public AddStudent obj = new AddStudent();
        System.Data.DataTable dtStudentList = new System.Data.DataTable();
        //DataGridRow dRw = null;
        ObservableCollection<AddStudent> lst = new ObservableCollection<AddStudent>();
        public StudentPersonalDetailsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dtStudentList = obj.ViewStudentDetails();

            for (int i = 0; i < dtStudentList.Rows.Count; i++)
            {
                AddStudent st = new AddStudent();
                st.ID = Convert.ToInt32(dtStudentList.Rows[i]["ID"]);
                st.Name = Convert.ToString(dtStudentList.Rows[i]["Name"]);
                st.Department = Convert.ToString(dtStudentList.Rows[i]["Department"]);
                st.IsActive = Convert.ToBoolean(dtStudentList.Rows[i]["IsActive"]);
                st.Address = Convert.ToString(dtStudentList.Rows[i]["Address"]);
                st.ContactNumber = Convert.ToString(dtStudentList.Rows[i]["ContactNumber"]);
                lst.Add(st);
            }
            this.grdStudent.ItemsSource = lst;
        }

        private void AddStud_Click(object sender, RoutedEventArgs e)
        {
            if (studID.Text.Length == 0 || studName.Text.Length == 0 || studDep.Text.Length == 0
                || studAddrss.Text.Length == 0 || studCntctNmbr.Text.Length == 0)
            {
                MessageBox.Show("Please enter all mandatory fields.", "Validation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (studID.Text.Length != 0 && studName.Text.Length != 0 && studDep.Text.Length != 0
                && studAddrss.Text.Length != 0 && studCntctNmbr.Text.Length != 0)
            {
                AddStudent st = new AddStudent();

                obj.ID = Convert.ToInt32(studID.Text);
                obj.Name = studName.Text.ToString();
                obj.Department = studDep.Text.ToString();
                obj.IsActive = studIsActv.IsChecked.Value;
                obj.Address = studAddrss.Text.ToString();
                obj.ContactNumber = studCntctNmbr.Text.ToString();

                st = obj;

                lst.Add(st);
                this.grdStudent.ItemsSource = lst;

                obj.AddNewStudent(st);
                studID.Clear();
                studName.Clear();
                studDep.Clear();
                studIsActv.IsChecked = false;
                studAddrss.Clear();
                studCntctNmbr.Clear();
            }
        }
        private void GrdStudent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid rw = sender as DataGrid;
                if (rw != null && rw.SelectedItems != null && rw.SelectedItems.Count == 1)
                {
                    DataGridRow dRw = rw.ItemContainerGenerator.ContainerFromItem(rw.SelectedItem) as DataGridRow;

                    obj = dRw.Item as AddStudent;
                    studID.Text = Convert.ToString(obj.ID);
                    studName.Text = Convert.ToString(obj.Name);
                    studDep.Text = Convert.ToString(obj.Department);
                    studIsActv.IsChecked = Convert.ToBoolean(obj.IsActive);
                    studAddrss.Text = Convert.ToString(obj.Address);
                    studCntctNmbr.Text = Convert.ToString(obj.ContactNumber);

                    studID.IsEnabled = false;
                }
            }
        }
        private void UpdtStud_Click(object sender, RoutedEventArgs e)
        {

            if (studID.Text.Length == 0 || studName.Text.Length == 0 || studDep.Text.Length == 0
                || studAddrss.Text.Length == 0 || studCntctNmbr.Text.Length == 0)
            {
                MessageBox.Show("Please select a record to update.", "Validation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (studID.Text.Length != 0 && studName.Text.Length != 0 && studDep.Text.Length != 0
                && studAddrss.Text.Length != 0 && studCntctNmbr.Text.Length != 0)
            {
                obj.ID = Convert.ToInt32(studID.Text);
                obj.Name = studName.Text.ToString();
                obj.Department = studDep.Text.ToString();
                obj.IsActive = studIsActv.IsChecked.Value;
                obj.Address = studAddrss.Text.ToString();
                obj.ContactNumber = studCntctNmbr.Text.ToString();

                obj.UpdateStudent(obj.ID);
                studID.IsEnabled = true;

                grdStudent.Items.Refresh();
            }
        }

        private void DelStud_Click(object sender, RoutedEventArgs e)
        {
            if (studID.Text.Length == 0 || studName.Text.Length == 0 || studDep.Text.Length == 0
                || studAddrss.Text.Length == 0 || studCntctNmbr.Text.Length == 0)
            {
                MessageBox.Show("Please select a record to delete.", "Validation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (studID.Text.Length != 0 && studName.Text.Length != 0 && studDep.Text.Length != 0
                && studAddrss.Text.Length != 0 && studCntctNmbr.Text.Length != 0)
            {
                obj.ID = Convert.ToInt32(studID.Text);

                obj.DeleteStudent(obj.ID);

                lst.Remove(obj);
                this.grdStudent.ItemsSource = lst;

                studID.Clear();
                studName.Clear();
                studDep.Clear();
                studIsActv.IsChecked = false;
                studAddrss.Clear();
                studCntctNmbr.Clear();
                studID.IsEnabled = true;

                grdStudent.Items.Refresh();
            }
        }
        private void Export_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnExcel_Click(object sender, RoutedEventArgs e)
        {
            System.Data.DataTable dtCpy1 = dtStudentList.Copy();
            dtCpy1.TableName = "Personal Details";
            DataSet ds = new DataSet();
            ds.Tables.Add(dtCpy1);
            //SaveFileDialog dlg = new SaveFileDialog();
            //dlg.FileName = "Excel_Student_Personal_Details.xlsx";
            //dlg.Filter = "All Excel Files .xlsx|*.xlsx|.xls|*.xls";
            //dlg.InitialDirectory = @"C:\New folder\";
            //string strPath = dlg.FileName;
            //dlg.ShowDialog();            

            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            string strPth = @"C:\New Folder\Excel_Student_Personal_Details.xlsx";

            Microsoft.Office.Interop.Excel.Workbook excelWorkBook = excelApp.Workbooks.Open(strPth);

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
            dlg.FileName = "XML_Student_Personal_Details" + ".xml";
            dlg.Filter = "XML Files .xml|*.xml";
            dlg.InitialDirectory = @"C:\New folder\";
            dlg.ShowDialog();
            XmlWriterSettings stngs = new XmlWriterSettings();
            stngs.Indent = true;
            stngs.IndentChars = ("\t");
            using (XmlWriter xml = XmlWriter.Create(dlg.FileName, stngs))
            {

                xml.WriteStartDocument();
                xml.WriteStartElement("StudentPersonalDetails");

                for (int i = 0; i < dtStudentList.Rows.Count; i++)
                {
                    xml.WriteStartElement("Student");
                    xml.WriteAttributeString("ID", Convert.ToString(dtStudentList.Rows[i]["ID"]));
                    xml.WriteStartElement("PersonalDetails");
                    xml.WriteStartElement("Name");
                    xml.WriteString(Convert.ToString(dtStudentList.Rows[i]["Name"]));
                    xml.WriteEndElement();
                    xml.WriteStartElement("Department");
                    xml.WriteString(Convert.ToString(dtStudentList.Rows[i]["Department"]));
                    xml.WriteEndElement();
                    xml.WriteStartElement("IsActive");
                    xml.WriteString(Convert.ToString(dtStudentList.Rows[i]["IsActive"]));
                    xml.WriteEndElement();
                    xml.WriteStartElement("Address");
                    xml.WriteString(Convert.ToString(dtStudentList.Rows[i]["Address"]));
                    xml.WriteEndElement();
                    xml.WriteStartElement("ContactNumber");
                    xml.WriteString(Convert.ToString(dtStudentList.Rows[i]["ContactNumber"]));
                    xml.WriteEndElement();
                    xml.WriteEndElement();
                    xml.WriteEndElement();
                }
                xml.WriteEndElement();
                xml.WriteEndDocument();
            }
        }
        private void BtnCSV_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "CSV_Student_Personal_Details" + ".csv";
            dlg.Filter = "CSV Files .csv|*.csv";
            dlg.InitialDirectory = @"C:\New folder\";
            dlg.ShowDialog();
            string sprts = ",";
            FileStream fstrm = new FileStream(dlg.FileName, FileMode.OpenOrCreate);
            StreamWriter strmWrtr = new StreamWriter(fstrm);
            strmWrtr.WriteLine();
            StringBuilder strCsv = new StringBuilder();

            strCsv.AppendLine("ID,Name,Department,IsActive,Address,ContactNumber,");

            for (int i = 0; i < dtStudentList.Rows.Count; i++)
            {
                string strCntnt1 = Convert.ToString(dtStudentList.Rows[i]["ID"]) + sprts +
                                Convert.ToString(dtStudentList.Rows[i]["Name"]) + sprts +
                                Convert.ToString(dtStudentList.Rows[i]["Department"]) + sprts +
                                Convert.ToString(dtStudentList.Rows[i]["IsActive"]) + sprts +
                                Convert.ToString(dtStudentList.Rows[i]["Address"]) + sprts +
                                Convert.ToString(dtStudentList.Rows[i]["ContactNumber"]) + sprts;
                strCsv.AppendLine(strCntnt1);
            }
            strmWrtr.WriteLine(strCsv);
            strmWrtr.Close();
            fstrm.Close();
        }
    }
}
