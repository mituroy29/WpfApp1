using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using ViewModelLayer;
using System.ComponentModel;
using Microsoft.Win32;
using System.Xml;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class StudentDetailsPage : System.Windows.Controls.Page
    {
        ObservableCollection<string> cmbSrchLst = new ObservableCollection<string>();
        ObservableCollection<string> cmbRprtFrmtLst = new ObservableCollection<string>();
        AddStudent objStud = new AddStudent();
        StudentResultDetails objStudMrks = new StudentResultDetails();
        System.Data.DataTable dt = new System.Data.DataTable();
        System.Data.DataTable dt1 = new System.Data.DataTable();
        List<KeyValuePair<string, int>> lst = new List<KeyValuePair<string, int>>();
        public StudentDetailsPage()
        {
            InitializeComponent();

            cmbSrchLst.Add("ID");
            cmbSrchLst.Add("Name");
            cmbSearch.ItemsSource = cmbSrchLst;

            cmbRprtFrmtLst.Add("CSV");
            cmbRprtFrmtLst.Add("Excel");
            cmbRprtFrmtLst.Add("XML");
            cmbRprtFormat.ItemsSource = cmbRprtFrmtLst;
        }

        private void SelectStud_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSearch.SelectedItem == null && txtSearch.Text.Length == 0)
            {
                MessageBox.Show("Please select a student.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (cmbSearch.SelectedItem != null)
            {
                if (cmbSearch.SelectedItem.ToString() == "ID")
                {
                    dt = objStud.SearchStudentByID(Convert.ToInt32(txtSearch.Text));
                    dt1 = objStudMrks.SelectStudentMarksListByID(Convert.ToInt32(txtSearch.Text));
                    txtId.Text = Convert.ToString(Convert.ToInt32(txtSearch.Text));
                    txtName.Text = Convert.ToString(dt.Rows[0]["Name"]);
                }
                if (cmbSearch.SelectedItem.ToString() == "Name")
                {
                    dt = objStud.SearchStudentByName(txtSearch.Text);
                    dt1 = objStudMrks.SelectStudentMarksListByName(txtSearch.Text);
                    txtId.Text = Convert.ToString(dt.Rows[0]["ID"]);
                    txtName.Text = Convert.ToString(txtSearch.Text);
                }
                txtDprt.Text = Convert.ToString(dt.Rows[0]["Department"]);
                chkAct.IsChecked = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                txtAdrs.Text = Convert.ToString(dt.Rows[0]["Address"]);
                txtCntctNmbr.Text = Convert.ToString(dt.Rows[0]["ContactNumber"]);
            }

            int total;
            int prcntg;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                total = Convert.ToInt32(dt1.Rows[i]["Subject1"])
                   + Convert.ToInt32(dt1.Rows[i]["Subject2"])
                   + Convert.ToInt32(dt1.Rows[i]["Subject3"])
                   + Convert.ToInt32(dt1.Rows[i]["Subject4"])
                   + Convert.ToInt32(dt1.Rows[i]["Subject5"])
                   + Convert.ToInt32(dt1.Rows[i]["Subject6"])
                   + Convert.ToInt32(dt1.Rows[i]["Subject7"]);
                prcntg = (total * 100) / 350;
                lst.Add(new KeyValuePair<string, int>("Test" + Convert.ToString(i + 1), prcntg));
            }
        }
        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSearch.SelectedItem == null && txtSearch.Text.ToString() == string.Empty)
            {
                MessageBox.Show("Please select a record first.", "Validation", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (cmbRprtFormat.SelectedItem == null)
            {
                MessageBox.Show("Please select a report format.", "Validation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (txtSearch.Text.ToString() != string.Empty)
            {
                if (cmbRprtFormat.SelectedItem != null)
                {
                    if (cmbRprtFormat.SelectedItem.ToString() == "CSV")
                    {
                        string sprts = ",";
                        SaveFileDialog dlg = new SaveFileDialog();
                        dlg.FileName = Convert.ToString(dt.Rows[0]["ID"]) + "_"
                            + Convert.ToString(dt.Rows[0]["Name"]) + ".csv";
                        dlg.Filter = "CSV Files (*.csv)|*.csv";
                        dlg.InitialDirectory = @"C:\New folder\";
                        dlg.ShowDialog();
                        FileStream fstrm = new FileStream(dlg.FileName, FileMode.OpenOrCreate);
                        StreamWriter strmWrtr = new StreamWriter(fstrm);
                        strmWrtr.WriteLine();
                        StringBuilder strCsv = new StringBuilder();
                        string strCntnt1 = Convert.ToString(dt.Rows[0]["ID"]) + sprts +
                                            Convert.ToString(dt.Rows[0]["Name"]) + sprts +
                                            Convert.ToString(dt.Rows[0]["Department"]) + sprts +
                                            Convert.ToString(dt.Rows[0]["IsActive"]) + sprts +
                                            Convert.ToString(dt.Rows[0]["Address"]) + sprts +
                                            Convert.ToString(dt.Rows[0]["ContactNumber"]) + sprts;
                        StringBuilder strHdr = new StringBuilder();
                        strHdr.Append("ID,Name,Department,IsActive,Address,ContactNumber,");
                        strCsv.Append(strCntnt1);
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string strCntnt2 = Convert.ToString(dt1.Rows[i]["ExamName"]) + sprts +
                                                Convert.ToString(dt1.Rows[i]["ExamMonth"]) + sprts +
                                                Convert.ToString(dt1.Rows[i]["Subject1"]) + sprts +
                                                Convert.ToString(dt1.Rows[i]["Subject2"]) + sprts +
                                                Convert.ToString(dt1.Rows[i]["Subject3"]) + sprts +
                                                Convert.ToString(dt1.Rows[i]["Subject4"]) + sprts +
                                                Convert.ToString(dt1.Rows[i]["Subject5"]) + sprts +
                                                Convert.ToString(dt1.Rows[i]["Subject6"]) + sprts +
                                                Convert.ToString(dt1.Rows[i]["Subject7"]) + sprts +
                                                Convert.ToString(dt1.Rows[i]["Attendance"]) + sprts;
                            strCsv.Append(strCntnt2);
                            strHdr.Append("ExamName,ExamMonth,Subject1,Subject2,Subject3,Subject4,Subject5,Subject6,Subject7,Attendance,");
                        }
                        strmWrtr.WriteLine(strHdr);
                        strmWrtr.WriteLine(strCsv);
                        strmWrtr.Close();
                        fstrm.Close();
                    }
                    if (cmbRprtFormat.SelectedItem.ToString() == "Excel")
                    {
                        System.Data.DataTable dtCpy1 = dt.Copy();
                        System.Data.DataTable dtCpy2 = dt1.Copy();
                        dtCpy1.TableName = "Personal Details";
                        dtCpy2.TableName = "Semester";
                        DataSet ds = new DataSet();
                        ds.Tables.Add(dtCpy1);
                        ds.Tables.Add(dtCpy2);
                        string strPath = @"C:\Exports\Excel_Student_Details.xlsx";

                        Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

                        Microsoft.Office.Interop.Excel.Workbook excelWorkBook = excelApp.Workbooks.Open(strPath);
                        foreach (System.Data.DataTable table in ds.Tables)
                        {
                            if (excelWorkBook.Sheets.Count != 0)
                            {
                                Worksheet sht = (Worksheet)excelWorkBook.Worksheets[table.TableName];
                                sht.Name = "Sheet" + table.TableName;
                            }
                        }
                        foreach (System.Data.DataTable table in ds.Tables)
                        {
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
                        excelApp.DisplayAlerts = false;
                        excelWorkBook.Worksheets[4].Delete();
                        excelWorkBook.Worksheets[3].Delete();
                        excelApp.DisplayAlerts = true;
                        excelWorkBook.Worksheets[2].Move(excelWorkBook.Worksheets[1]);
                        excelWorkBook.Save();
                        excelWorkBook.Close();
                        excelApp.Quit();
                    }
                    if (cmbRprtFormat.SelectedItem.ToString() == "XML")
                    {
                        SaveFileDialog dlg = new SaveFileDialog();
                        dlg.FileName = Convert.ToString(dt.Rows[0]["ID"]) + "_" + Convert.ToString(dt.Rows[0]["Name"]) + ".xml";
                        dlg.Filter = "XML Files .xml|*.xml";
                        dlg.InitialDirectory = @"C:\New folder\";
                        dlg.ShowDialog();
                        XmlWriterSettings stngs = new XmlWriterSettings();
                        stngs.Indent = true;
                        stngs.IndentChars = ("\t");
                        using (XmlWriter xml = XmlWriter.Create(dlg.FileName, stngs))
                        {

                            xml.WriteStartDocument();
                            xml.WriteStartElement("Student");
                            xml.WriteAttributeString("ID", Convert.ToString(dt.Rows[0]["ID"]));
                            xml.WriteStartElement("PersonalDetails");
                            xml.WriteStartElement("Name");
                            xml.WriteString(Convert.ToString(dt.Rows[0]["Name"]));
                            xml.WriteEndElement();
                            xml.WriteStartElement("Department");
                            xml.WriteString(Convert.ToString(dt.Rows[0]["Department"]));
                            xml.WriteEndElement();
                            xml.WriteStartElement("IsActive");
                            xml.WriteString(Convert.ToString(dt.Rows[0]["IsActive"]));
                            xml.WriteEndElement();
                            xml.WriteStartElement("Address");
                            xml.WriteString(Convert.ToString(dt.Rows[0]["Address"]));
                            xml.WriteEndElement();
                            xml.WriteStartElement("ContactNumber");
                            xml.WriteString(Convert.ToString(dt.Rows[0]["ContactNumber"]));
                            xml.WriteEndElement();
                            xml.WriteEndElement();
                            xml.WriteStartElement("Semester");

                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                xml.WriteStartElement(Convert.ToString(dt1.Rows[i]["ExamName"]));
                                xml.WriteStartElement("ExamMonth");
                                xml.WriteString(Convert.ToString(dt1.Rows[i]["ExamMonth"]));
                                xml.WriteEndElement();
                                xml.WriteStartElement("Subject1");
                                xml.WriteString(Convert.ToString(dt1.Rows[i]["Subject1"]));
                                xml.WriteEndElement();
                                xml.WriteStartElement("Subject2");
                                xml.WriteString(Convert.ToString(dt1.Rows[i]["Subject2"]));
                                xml.WriteEndElement();
                                xml.WriteStartElement("Subject3");
                                xml.WriteString(Convert.ToString(dt1.Rows[i]["Subject3"]));
                                xml.WriteEndElement();
                                xml.WriteStartElement("Subject4");
                                xml.WriteString(Convert.ToString(dt1.Rows[i]["Subject4"]));
                                xml.WriteEndElement();
                                xml.WriteStartElement("Subject5");
                                xml.WriteString(Convert.ToString(dt1.Rows[i]["Subject5"]));
                                xml.WriteEndElement();
                                xml.WriteStartElement("Subject6");
                                xml.WriteString(Convert.ToString(dt1.Rows[i]["Subject6"]));
                                xml.WriteEndElement();
                                xml.WriteStartElement("Subject7");
                                xml.WriteString(Convert.ToString(dt1.Rows[i]["Subject7"]));
                                xml.WriteEndElement();
                                xml.WriteStartElement("Attendance");
                                xml.WriteString(Convert.ToString(dt1.Rows[i]["Attendance"]));
                                xml.WriteEndElement();
                                xml.WriteEndElement();
                            }

                            xml.WriteEndElement();
                            xml.WriteEndElement();
                            xml.WriteEndDocument();
                        }
                    }
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            myChrtBar.DataContext = new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string, int>("1",10),
                new KeyValuePair<string, int>("2",20)
            };
        }

        private void MyTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (dt1.Rows.Count != 0)
            {
                if (((TreeViewItem)e.NewValue).Header.ToString() == "Class Test1")
                {
                    stckCol.Visibility = Visibility.Visible;
                    stckPie.Visibility = Visibility.Hidden;
                    myBar.Title = "Class Test 1";
                    myChrtBar.DataContext = new KeyValuePair<string, int>[]
                     {
                     new KeyValuePair<string, int>("Sub1",Convert.ToInt32(dt1.Rows[0]["Subject1"])),
                     new KeyValuePair<string, int>("Sub2",Convert.ToInt32(dt1.Rows[0]["Subject2"])),
                     new KeyValuePair<string, int>("Sub3",Convert.ToInt32(dt1.Rows[0]["Subject3"])),
                     new KeyValuePair<string, int>("Sub4",Convert.ToInt32(dt1.Rows[0]["Subject4"])),
                     new KeyValuePair<string, int>("Sub5",Convert.ToInt32(dt1.Rows[0]["Subject5"])),
                     new KeyValuePair<string, int>("Sub6",Convert.ToInt32(dt1.Rows[0]["Subject6"])),
                     new KeyValuePair<string, int>("Sub7",Convert.ToInt32(dt1.Rows[0]["Subject7"]))
                     };
                }
                if (((TreeViewItem)e.NewValue).Header.ToString() == "Class Test2")
                {
                    stckPie.Visibility = Visibility.Hidden;
                    stckCol.Visibility = Visibility.Visible;
                    myBar.Title = "Class Test 2";
                    myChrtBar.DataContext = new KeyValuePair<string, int>[]
                     {
                     new KeyValuePair<string, int>("Sub1",Convert.ToInt32(dt1.Rows[1]["Subject1"])),
                     new KeyValuePair<string, int>("Sub2",Convert.ToInt32(dt1.Rows[1]["Subject2"])),
                     new KeyValuePair<string, int>("Sub3",Convert.ToInt32(dt1.Rows[1]["Subject3"])),
                     new KeyValuePair<string, int>("Sub4",Convert.ToInt32(dt1.Rows[1]["Subject4"])),
                     new KeyValuePair<string, int>("Sub5",Convert.ToInt32(dt1.Rows[1]["Subject5"])),
                     new KeyValuePair<string, int>("Sub6",Convert.ToInt32(dt1.Rows[1]["Subject6"])),
                     new KeyValuePair<string, int>("Sub7",Convert.ToInt32(dt1.Rows[1]["Subject7"]))
                     };
                }
                if (((TreeViewItem)e.NewValue).Header.ToString() == "Class Test3")
                {
                    stckCol.Visibility = Visibility.Visible;
                    stckPie.Visibility = Visibility.Hidden;
                    myBar.Title = "Class Test 3";
                    myChrtBar.DataContext = new KeyValuePair<string, int>[]
                     {
                     new KeyValuePair<string, int>("Sub1",Convert.ToInt32(dt1.Rows[2]["Subject1"])),
                     new KeyValuePair<string, int>("Sub2",Convert.ToInt32(dt1.Rows[2]["Subject2"])),
                     new KeyValuePair<string, int>("Sub3",Convert.ToInt32(dt1.Rows[2]["Subject3"])),
                     new KeyValuePair<string, int>("Sub4",Convert.ToInt32(dt1.Rows[2]["Subject4"])),
                     new KeyValuePair<string, int>("Sub5",Convert.ToInt32(dt1.Rows[2]["Subject5"])),
                     new KeyValuePair<string, int>("Sub6",Convert.ToInt32(dt1.Rows[2]["Subject6"])),
                     new KeyValuePair<string, int>("Sub7",Convert.ToInt32(dt1.Rows[2]["Subject7"]))
                     };
                }
                if (((TreeViewItem)e.NewValue).Header.ToString() == "July")
                {
                    myChrtPie1.DataContext = new KeyValuePair<string, int>[]
                    {
                    new KeyValuePair<string,int>("Present",Convert.ToInt32(dt1.Rows[0]["Attendance"])),
                    new KeyValuePair<string,int>("Absent",(20-Convert.ToInt32(dt1.Rows[0]["Attendance"])))
                    };
                    stckPie.Visibility = Visibility.Visible;
                    stckCol.Visibility = Visibility.Hidden;
                    //Collection<ResourceDictionary> palette1 = new Collection<ResourceDictionary>();
                    //for (int i = 0; i < 2; i++)
                    //{
                    //    ResourceDictionary rd = new ResourceDictionary();
                    //    System.Windows.Style style = new System.Windows.Style(typeof(Control));
                    //    SolidColorBrush brush = null;
                    //    //switch (dt1.Rows[0]["ExamMonth"])
                    //    //{
                    //    //    case "Present": brush = new SolidColorBrush(Colors.Green); break;
                    //    //    case "Absent": brush = new SolidColorBrush(Colors.Red); break;
                    //    //}
                    //    if (i == 0)
                    //    {
                    //        brush = new SolidColorBrush(Colors.Green);
                    //    }
                    //    if (i == 1)
                    //    {
                    //        brush = new SolidColorBrush(Colors.Red);
                    //    }
                    //    style.Setters.Add(new Setter() { Property = Control.BackgroundProperty, Value = brush });
                    //    rd.Add("DataPointStyle", style);
                    //    palette1.Add(rd);
                    //}
                    //myChrtPie1.Palette = palette1;
                }
                if (((TreeViewItem)e.NewValue).Header.ToString() == "August")
                {
                    myChrtPie1.DataContext = new KeyValuePair<string, int>[]
                    {
                    new KeyValuePair<string,int>("Present",Convert.ToInt32(dt1.Rows[1]["Attendance"])),
                    new KeyValuePair<string,int>("Absent",(20-Convert.ToInt32(dt1.Rows[1]["Attendance"])))
                    };
                    stckPie.Visibility = Visibility.Visible;
                    stckCol.Visibility = Visibility.Hidden;
                }
                if (((TreeViewItem)e.NewValue).Header.ToString() == "September")
                {
                    myChrtPie1.DataContext = new KeyValuePair<string, int>[]
                    {
                    new KeyValuePair<string,int>("Present",Convert.ToInt32(dt1.Rows[2]["Attendance"])),
                    new KeyValuePair<string,int>("Absent",(20-Convert.ToInt32(dt1.Rows[2]["Attendance"])))
                    };
                    stckPie.Visibility = Visibility.Visible;
                    stckCol.Visibility = Visibility.Hidden;
                }
                if (((TreeViewItem)e.NewValue).Header.ToString() == "Percentage")
                {
                    stckPie.Visibility = Visibility.Hidden;
                    stckCol.Visibility = Visibility.Visible;

                    myChrtBar.DataContext = new KeyValuePair<string, int>[]
                     {
                     lst[0],
                     lst[1],
                     lst[2]
                     };
                }
            }
        }
    }
}

