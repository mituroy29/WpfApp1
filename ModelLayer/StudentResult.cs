using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;

namespace ModelLayer
{
    public class StudentResult:INotifyPropertyChanged
    {
        private int nStudentId;
        private string strName;
        private int nSub1;
        private int nSub2;
        private int nSub3;
        private int nSub4;
        private int nSub5;
        private int nSub6;
        private int nSub7;
        private string strExam;
        private string strMonth;
        private int nAtdnce;
        private SqlConnection con;

        public int ID
        {
            get
            {
                return nStudentId;
            }
            set
            {
                nStudentId = value;
                OnPropertyChanged("ID");
            }
        }
        public string Name
        {
            get
            {
                return strName;
            }
            set
            {
                strName = value;
                OnPropertyChanged("Name");
            }
        }
        public int Subject1
        {
            get
            {
                return nSub1;
            }
            set
            {
                nSub1 = value;
                OnPropertyChanged("Subject1");
            }
        }
        public int Subject2
        {
            get
            {
                return nSub2;
            }
            set
            {
                nSub2 = value;
                OnPropertyChanged("Subject2");
            }
        }
        public int Subject3
        {
            get
            {
                return nSub3;
            }
            set
            {
                nSub3 = value;
                OnPropertyChanged("Subject3");
            }
        }
        public int Subject4
        {
            get
            {
                return nSub4;
            }
            set
            {
                nSub4 = value;
                OnPropertyChanged("Subject4");
            }
        }
        public int Subject5
        {
            get
            {
                return nSub5;
            }
            set
            {
                nSub5 = value;
                OnPropertyChanged("Subject5");
            }
        }
        public int Subject6
        {
            get
            {
                return nSub6;
            }
            set
            {
                nSub6 = value;
                OnPropertyChanged("Subject6");
            }
        }
        public int Subject7
        {
            get
            {
                return nSub7;
            }
            set
            {
                nSub7 = value;
                OnPropertyChanged("Subject7");
            }
        }
        public string ExamName
        {
            get
            {
                return strExam;
            }
            set
            {
                strExam = value;
                OnPropertyChanged("ExamName");
            }
        }
        public string ExamMonth
        {
            get
            {
                return strMonth;
            }
            set
            {
                strMonth = value;
                OnPropertyChanged("ExamMonth");
            }
        }
        public int Attendance
        {
            get
            {
                return nAtdnce;
            }
            set
            {
                nAtdnce = value;
                OnPropertyChanged("Attendance");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string Property)
        {
            if(PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Property));
            }
        }

        public void SetConnection()
        {
            con = new SqlConnection("Data Source=MININT-74Q67AA\\SQLEXPRESS;Initial Catalog=DemoDB;Integrated Security=true");
        }
        public void AddStudentMarks()
        {
            SetConnection();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                SqlCommand cmd = new SqlCommand("InsertStudentMarks", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", this.ID);
                cmd.Parameters.AddWithValue("@Name", this.Name);
                cmd.Parameters.AddWithValue("@Subject1", this.Subject1);
                cmd.Parameters.AddWithValue("@Subject2", this.Subject2);
                cmd.Parameters.AddWithValue("@Subject3", this.Subject3);
                cmd.Parameters.AddWithValue("@Subject4", this.Subject4);
                cmd.Parameters.AddWithValue("@Subject5", this.Subject5);
                cmd.Parameters.AddWithValue("@Subject6", this.Subject6);
                cmd.Parameters.AddWithValue("@Subject7", this.Subject7);
                cmd.Parameters.AddWithValue("@ExamName", this.ExamName);
                cmd.Parameters.AddWithValue("@ExamMonth", this.ExamMonth);
                cmd.Parameters.AddWithValue("@Attendance", this.Attendance);
                cmd.ExecuteNonQuery();
                CloseConnection(con);
            }
            catch (Exception ex)
            {

            }
        }
        public void UpdateStudentMarks(int ID)
        {
            SetConnection();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                SqlCommand cmd = new SqlCommand("UpdateStudentMarks", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Name", this.Name);
                cmd.Parameters.AddWithValue("@Subject1", this.Subject1);
                cmd.Parameters.AddWithValue("@Subject2", this.Subject2);
                cmd.Parameters.AddWithValue("@Subject3", this.Subject3);
                cmd.Parameters.AddWithValue("@Subject4", this.Subject4);
                cmd.Parameters.AddWithValue("@Subject5", this.Subject5);
                cmd.Parameters.AddWithValue("@Subject6", this.Subject6);
                cmd.Parameters.AddWithValue("@Subject7", this.Subject7);
                cmd.Parameters.AddWithValue("@ExamName", this.ExamName);
                cmd.Parameters.AddWithValue("@ExamMonth", this.ExamMonth);
                cmd.Parameters.AddWithValue("@Attendance", this.Attendance);
                cmd.ExecuteNonQuery();
                CloseConnection(con);
            }
            catch (Exception ex)
            {

            }
        }
        public void DeleteStudentMarks(int ID)
        {
            SetConnection();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteStudentMarks", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.ExecuteNonQuery();
                CloseConnection(con);
            }
            catch (Exception ex)
            {

            }
        }
        public DataTable SelectStudentMarksList()
        {
            SetConnection();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlDataAdapter adptr = new SqlDataAdapter();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SelectStudentMarksList", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                adptr.SelectCommand = cmd;
                adptr.Fill(ds);
                dt = ds.Tables[0];
                //this.dtStudentList = ds.Tables[0];

                CloseConnection(con);
            }
            catch (Exception ex)
            {

            }
            return dt;
        }
        public DataTable SelectStudentMarksListByID(int ID)
        {
            SetConnection();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlDataAdapter adptr = new SqlDataAdapter();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SelectStudentMarksListByID", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.ExecuteNonQuery();
                adptr.SelectCommand = cmd;
                adptr.Fill(ds);
                dt = ds.Tables[0];
                //this.dtStudentList = ds.Tables[0];

                CloseConnection(con);
            }
            catch (Exception ex)
            {

            }
            return dt;
        }
        public DataTable SelectStudentMarksListByName(string Name)
        {
            SetConnection();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            SqlDataAdapter adptr = new SqlDataAdapter();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand("SelectStudentMarksListByName", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.ExecuteNonQuery();
                adptr.SelectCommand = cmd;
                adptr.Fill(ds);
                dt = ds.Tables[0];
                //this.dtStudentList = ds.Tables[0];

                CloseConnection(con);
            }
            catch (Exception ex)
            {

            }
            return dt;
        }
        public void CloseConnection(SqlConnection con)
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }
    }
}
