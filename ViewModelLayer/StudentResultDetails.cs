using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;

namespace ViewModelLayer
{
    public class StudentResultDetails
    {
        StudentResult obj = new StudentResult();

        public void AddStudentMarks(StudentResultDetails stm)
        {
            obj.AddStudentMarks();
        }
        public void UpdateStudentMarks(int ID)
        {
            obj.UpdateStudentMarks(ID);
        }
        public void DeleteStudentMarks(int ID)
        {
            obj.DeleteStudentMarks(ID);
        }
        public DataTable ViewStudentMarksDetails()
        {
            return obj.SelectStudentMarksList();
        }
        public DataTable SelectStudentMarksListByID(int ID)
        {
            return obj.SelectStudentMarksListByID(ID);
        }
        public DataTable SelectStudentMarksListByName(string Name)
        {
            return obj.SelectStudentMarksListByName(Name);
        }
        public int ID
        {
            get
            {
                return obj.ID;
            }
            set
            {
                obj.ID = value;
            }
        }
        public string Name
        {
            get
            {
                return obj.Name;
            }
            set
            {
                obj.Name = value;
            }
        }
        public int Subject1
        {
            get
            {
                return obj.Subject1;
            }
            set
            {
                obj.Subject1 = value;
            }
        }
        public int Subject2
        {
            get
            {
                return obj.Subject2;
            }
            set
            {
                obj.Subject2 = value;
            }
        }
        public int Subject3
        {
            get
            {
                return obj.Subject3;
            }
            set
            {
                obj.Subject3 = value;
            }
        }
        public int Subject4
        {
            get
            {
                return obj.Subject4;
            }
            set
            {
                obj.Subject4 = value;
            }
        }
        public int Subject5
        {
            get
            {
                return obj.Subject5;
            }
            set
            {
                obj.Subject5 = value;
            }
        }
        public int Subject6
        {
            get
            {
                return obj.Subject6;
            }
            set
            {
                obj.Subject6 = value;
            }
        }
        public int Subject7
        {
            get
            {
                return obj.Subject7;
            }
            set
            {
                obj.Subject7 = value;
            }
        }
        public string ExamName
        {
            get
            {
                return obj.ExamName;
            }
            set
            {
                obj.ExamName = value;
            }
        }
        public string ExamMonth
        {
            get
            {
                return obj.ExamMonth;
            }
            set
            {
                obj.ExamMonth = value;
            }
        }
        public int Attendance
        {
            get
            {
                return obj.Attendance;
            }
            set
            {
                obj.Attendance = value;
            }
        }
    }
}
