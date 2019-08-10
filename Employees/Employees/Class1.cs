using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public class Employees
    {
        public Employees(string EmployeesCsv)
        {
            EmployeesValidation(EmployeesCsv);
        }

        private string EmployeesValidation(string EmployeesCsv)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] {
                new DataColumn("EmpId", typeof(int)),
                new DataColumn("ManagerId", typeof(string)),
                new DataColumn("Salary", typeof(string))
            });
            foreach (string row in EmployeesCsv.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    // dt.Rows.Add();
                    int i = 0;

                    //Execute a loop over the columns.  
                    foreach (string cell in row.Split(','))
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell;

                        i++;
                    }
                }
            }
            //Check if all Salaries are integers
            if (ValidateSalary(dt) > 0)
            {
                return "Not all Salaries are integers";
            }
            //check if one employess reports to more than one manager
            if (!EmployeesManager(dt))
            {

            }
            //check if there is more than employee
            if (!ValidateCEO(dt))
            {
                return "There can only be one CEO";
            }

            
            return "null";
        }

        private bool EmployeesManager(DataTable myDataTable)
        {
            int EmployeesmanagersCounter = 0;
            foreach (DataRow row in myDataTable.Rows) //gets current employee
            {
                var EmpId = row["EmpId"].ToString();
                var ManagerId = row["ManagerId"].ToString();
                foreach (DataRow row2 in myDataTable.Rows) //checks if current employee has another manager
                {
                    if(row2["EmpId"].ToString()==EmpId && row["ManagerId"].ToString()!= ManagerId)
                    {
                        EmployeesmanagersCounter++;
                    }
                }
            }
            if (EmployeesmanagersCounter > 0)
            {
                return false;

            }
            return true;
        }
        private bool ManagerIsEmployee(DataTable myDataTable)//checks if manager is also an employee
        {
            int EmployeesmanagersCounter = 0;
            foreach (DataRow row in myDataTable.Rows) //gets current employee
            {
                var ManagerId = row["ManagerId"].ToString();
                foreach (DataRow row2 in myDataTable.Rows) //checks if current employee has another manager
                {
                    if (row2["EmpId"].ToString() == ManagerId)
                    {
                        EmployeesmanagersCounter++;
                    }
                }
            }
            if (EmployeesmanagersCounter > 0)
            {
                return true;

            }
            return false;
        }


        private int ValidateSalary(DataTable myDataTable)
        {
            int NonIntegersCounter = 0;
            foreach (DataRow row in myDataTable.Rows)
            {
                if(row["Salary"].ToString().All(char.IsDigit)==false) //salary is not int
                {
                    NonIntegersCounter++;
                }
            }
            return NonIntegersCounter;
        }
        private bool ValidateCEO(DataTable myDataTable)
        {
            int CEOCounter = 0;
            foreach (DataRow row in myDataTable.Rows)
            {
                if ( row["ManagerId"].ToString()==null || row["ManagerId"].ToString()=="") //This employer is a Ceo
                {
                    CEOCounter++;
                }
            }
            if (CEOCounter > 1)
            {
                return false;

            }
            return true;
        }
    }
}
