using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using CommonUtilities;

namespace ModelLayer
{
    public class User : INotifyPropertyChanged
    {
        private string strUserId;
        private string strPassword;
        private string strConPassword;
        private string strUserType;
        private EncodeDecode objPwd = new EncodeDecode();
        private SqlConnection con;

        public string UserId
        {
            get
            {
                return strUserId;
            }
            set
            {
                strUserId = value;
                OnPropertyChanged("UserId");
            }
        }
        public string Password
        {
            get
            {
                return strPassword;
            }
            set
            {
                strPassword = value;
                OnPropertyChanged("Password");
            }
        }
        public string ConfirmPassword
        {
            get
            {
                return strConPassword;
            }
            set
            {
                strConPassword = value;
                OnPropertyChanged("ConfirmPassword");
            }
        }
        public string UserType
        {
            get
            {
                return strUserType;
            }
            set
            {
                strUserType = value;
                OnPropertyChanged("UserType");
            }
        }

        //public string PasswordMatch
        //{
        //    get
        //    {
        //        return strPwdMatch;
        //    }
        //    set
        //    {
        //        strPwdMatch = value;
        //        OnPropertyChanged("PasswordMatch");
        //    }
        //}
        //public string UserIdValid
        //{
        //    get
        //    {
        //        return strUserIdValid;
        //    }
        //    set
        //    {
        //        strUserIdValid = value;
        //        OnPropertyChanged("UserIdValid");
        //    }
        //}
        //public string PwdValid
        //{
        //    get
        //    {
        //        return strPwdValid;
        //    }
        //    set
        //    {
        //        strPwdValid = value;
        //        OnPropertyChanged("PwdValid");
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
        public void InsertUser()
        {
            SetConnection();
            string encdPwd = "";
            if (this.Password != null)
            {
                encdPwd = objPwd.EncodePassword(this.Password);
            }
            if (this.UserId.Length != 0 && encdPwd.Length != 0)
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                try
                {
                    SqlCommand cmd = new SqlCommand("InsertUser", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", this.UserId);
                    cmd.Parameters.AddWithValue("@Password", encdPwd);
                    cmd.Parameters.AddWithValue("@UserType", this.UserType);
                    cmd.ExecuteNonQuery();
                    CloseConnection(con);
                }
                catch (Exception ex)
                {

                }
            }
        }
        public bool SelectUser(string UserId, string Password)
        {
            SetConnection();
            string pwd = string.Empty;
            SqlDataAdapter adptr = new SqlDataAdapter();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                SqlCommand cmd = new SqlCommand("SelectUser", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserId);
                //SqlDataReader rdr = cmd.ExecuteReader();
                //while(rdr.Read())
                //{
                //    pwd = (string)rdr["Password"];
                //}
                cmd.ExecuteNonQuery();
                adptr.SelectCommand = cmd;
                adptr.Fill(ds);
                dt = ds.Tables[0];
                CloseConnection(con);
            }
            catch (Exception ex)
            {

            }
            if (dt != null)
            {
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    if (dt.Rows[i]["UserID"].ToString() == UserId &&
                        objPwd.DecodePassword(dt.Rows[i]["Password"].ToString()) == Password)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public string SelectUserType(string UserId)
        {
            SetConnection();
            string usrType = string.Empty;
            SqlDataAdapter adptr = new SqlDataAdapter();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            try
            {
                SqlCommand cmd = new SqlCommand("SelectUser", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserId);
                cmd.ExecuteNonQuery();
                adptr.SelectCommand = cmd;
                adptr.Fill(ds);
                dt = ds.Tables[0];
                CloseConnection(con);
            }
            catch (Exception ex)
            {

            }
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["UserID"].ToString() == UserId)
                    {
                        usrType = dt.Rows[i]["UserType"].ToString();
                    }
                }
            }
            return usrType;
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
