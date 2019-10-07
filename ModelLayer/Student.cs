using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace ModelLayer
{
    public class Student : INotifyPropertyChanged
    {
        private int nStudentId;
        private string strName;
        private string strDepartment;
        private bool bIsActive;
        private string strAddress;
        private string strContactNumber;
        //private DataTable dtStudentList = new DataTable();
        private SqlConnection con;

        public Student()
        {
            //dtStudentList = new DataTable();
            //DataColumn c = new DataColumn();
            //c.ColumnName = "ID";
            //this.StudentList.Columns.Add(c);

            //c = new DataColumn();
            //c.ColumnName = "Name";
            //this.StudentList.Columns.Add(c);

            //c = new DataColumn();
            //c.ColumnName = "Department";
            //this.StudentList.Columns.Add(c);

            //c = new DataColumn();
            //c.ColumnName = "IsActive";
            //this.StudentList.Columns.Add(c);

            //c = new DataColumn();
            //c.ColumnName = "Address";
            //this.StudentList.Columns.Add(c);

            //c = new DataColumn();
            //c.ColumnName = "ContactNumber";
            //this.StudentList.Columns.Add(c);
        }

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
        public string Department
        {
            get
            {
                return strDepartment;
            }
            set
            {
                strDepartment = value;
                OnPropertyChanged("Department");
            }
        }
        public bool IsActive
        {
            get
            {
                return bIsActive;
            }
            set
            {
                bIsActive = value;
                OnPropertyChanged("IsActive");
            }
        }
        public string Address
        {
            get
            {
                return strAddress;
            }
            set
            {
                strAddress = value;
                OnPropertyChanged("Address");
            }
        }
        public string ContactNumber
        {
            get
            {
                return strContactNumber;
            }
            set
            {
                strContactNumber = value;
                OnPropertyChanged("ContactNumber");
            }
        }
        //public DataTable StudentList
        //{
        //    get
        //    {
        //        return SelectStudentList();
        //    }
        //    set
        //    {
        //        dtStudentList = value;
        //        OnPropertyChanged("StudentList");
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string Property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Property));
            }
        }
        public void SetConnection()
        {
            con = new SqlConnection("Data Source=MININT-74Q67AA\\SQLEXPRESS;Initial Catalog=DemoDB;Integrated Security=true");
        }
        public void InsertStudent()
        {
            SetConnection();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                SqlCommand cmd = new SqlCommand("AddStudent", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", this.ID);
                cmd.Parameters.AddWithValue("@Name", this.Name);
                cmd.Parameters.AddWithValue("@Department", this.Department);
                cmd.Parameters.AddWithValue("@IsActive", (this.IsActive == true) ? 1 : 0);
                cmd.Parameters.AddWithValue("@Address", this.Address);
                cmd.Parameters.AddWithValue("@ContactNumber", this.ContactNumber);
                cmd.ExecuteNonQuery();
                CloseConnection(con);
            }
            catch (Exception ex)
            {

            }
        }
        public DataTable SearchStudentByID(int ID)
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
                SqlCommand cmd = new SqlCommand("SearchStudentByID", con);
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
        public DataTable SearchStudentByName(string Name)
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
                SqlCommand cmd = new SqlCommand("SearchStudentByName", con);
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
        public DataTable SelectStudentList()
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
                SqlCommand cmd = new SqlCommand("SelectStudentList", con);
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
        public void UpdateStudent(int ID)
        {
            SetConnection();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                SqlCommand cmd = new SqlCommand("UpdateStudent", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Name", this.Name);
                cmd.Parameters.AddWithValue("@Department", this.Department);
                cmd.Parameters.AddWithValue("@IsActive", (this.IsActive == true) ? 1 : 0);
                cmd.Parameters.AddWithValue("@Address", this.Address);
                cmd.Parameters.AddWithValue("@ContactNumber", this.ContactNumber);
                cmd.ExecuteNonQuery();
                CloseConnection(con);
            }
            catch (Exception ex)
            {

            }
        }
        public void DeleteStudent(int ID)
        {
            SetConnection();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteStudent", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);                
                cmd.ExecuteNonQuery();
                CloseConnection(con);
            }
            catch (Exception ex)
            {

            }
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
