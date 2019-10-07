using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using System.Windows.Input;
using System.Data;
using System.Collections;
using System.Collections.ObjectModel;

namespace ViewModelLayer
{
    public class AddStudent
    {
        private Student obj = new Student();
        public ICommand AddStudentCmnd { get; set; }

        public AddStudent()
        {
            AddStudentCmnd = new MyCommandBase(Execute, CanExecute);
        }
        private bool CanExecute(object parameter)
        {
            return true;
        }
        private void Execute(object parameter)
        {
            //obj.InsertStudent();
            //obj.SearchStudent(1234);
        }

        public DataTable SearchStudentByID(int ID)
        {
            return obj.SearchStudentByID(ID);
        }
        public DataTable SearchStudentByName(string Name)
        {
            return obj.SearchStudentByName(Name);
        }
        public DataTable ViewStudentDetails()
        {
            return obj.SelectStudentList();     
        }
        public void AddNewStudent(AddStudent stud)
        {
            obj.InsertStudent();
        }
        public void UpdateStudent(int ID)
        {
            obj.UpdateStudent(ID);
        }
        public void DeleteStudent(int ID)
        {
            obj.DeleteStudent(ID);
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
        public string Department
        {
            get
            {
                return obj.Department;
            }
            set
            {
                obj.Department = value;
            }
        }
        public bool IsActive
        {
            get
            {
                return obj.IsActive;
            }
            set
            {
                obj.IsActive = value;
            }
        }
        public string Address
        {
            get
            {
                return obj.Address;
            }
            set
            {
                obj.Address = value;
            }
        }
        public string ContactNumber
        {
            get
            {
                return obj.ContactNumber;
            }
            set
            {
                obj.ContactNumber = value;
            }
        }
        //public DataTable StudentList
        //{
        //    get
        //    {
        //        return obj.StudentList;
        //    }
        //    set
        //    {
        //        obj.StudentList = value;
        //    }
        //}
    }
}
