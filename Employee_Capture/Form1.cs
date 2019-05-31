using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ini;
using System.Data.SqlClient;
using System.IO;
using Connection;


namespace Employee_Capture
{
    

    public partial class Form1 : Form
    {


        public int Age;
        public String Fname;
        public String Lname;
        public String Dept;
        public String DeptType;
        public String EmpType;
        public int managerID;
        

        //SqlDataAdapter da;
        //SqlConnection conn;
        //String con = "Data Source=(Local); Initial Catalog=new_dashboard; Persist Security Info=True;User id= sa; Password=lbc";
        //SqlCommand cmd;

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {


            //--Display Employee Type
            //- Display Department 
            LoadEmployeeType();
            LoadDepartment();

        }

        private void FName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void LName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Age_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cboDept_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           //Checking fields
            if(txtFName.Text == "")
            {
                MessageBox.Show("Please input First Name", "First Name", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtFName.Focus();
                return;
            }

            if (txtLName.Text == "")
            {
                MessageBox.Show("Please input Last Name", "Last Name", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtLName.Focus();
                return;
            }


            if (Convert.ToDouble(txtAge.Text) == 0)
            {
                MessageBox.Show("Please input  Age not  equal to zero.", "Amount", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtAge.Focus();
                return;
            }
            
                 
             if (cmbEmpType.Text.Trim() == "Manager")
                                              
              {

                //managerID = 101;
                EmpType = "1";
                                
              }

             if (cmbEmpType.Text.Trim() == "Employee")

              {
                //EmpType = "2"; 
                managerID = 102;
                
              }

            if (cmbDept.Text.Trim() == "ICT")
            {
                managerID = 101;
            }

             
            if (cmbDept.Text.Trim() == "FIN")
            {
                managerID = 102;
            }

            
            if (cmbDept.Text.Trim() == "HR")
            {
                managerID = 103;
            }

            if (cmbDept.Text.Trim() == "ACCTG")
            {
                managerID = 104;
            }

            

            try
            {
                  SaveData(Convert.ToInt32(txtAge.Text.ToString()), txtFName.Text, txtLName.Text, Convert.ToInt32(managerID.ToString()), cmbDept.Text, cmbDept.SelectedValue.ToString(), EmpType);

            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message, err.Source);
            }




        }
        private void LoadDepartment()
        {
            clsCMScon conDepartment = new clsCMScon();

            try
            {
                conDepartment.Connect();

                SqlDataAdapter daDepartment = new SqlDataAdapter("exec tjm_LoadDeparment", conDepartment.sqlConStr);
                DataTable dtDepartment = new DataTable();

                daDepartment.Fill(dtDepartment);

                cmbDept.DataSource = dtDepartment;
                cmbDept.DisplayMember = "Dept_Type";
                cmbDept.ValueMember = "Dept_Name";

                cmbDept.SelectedValue = 0;
                
                conDepartment.DisConnect();

               }


            catch (Exception err)

            {

                MessageBox.Show(err.Message, err.Source);
                
                StreamWriter sw = new StreamWriter("D:\\PW\\wowRox\\logs" + string.Format("{0:MM-dd-yyyy}", DateTime.Now) + ".log", true);
                sw.WriteLine(err.Message);
                sw.Flush();
                sw.Dispose();

            }

            finally

            {
                conDepartment.DisConnect();

            }
                        
        }
        
        private void SaveData(int Age, String Fname, String Lname, int ManagerID, String Dept, String DeptType, String EmpType )

       {
            // saving data 
            clsCMScon conSaveData = new clsCMScon();

            try
            {
                conSaveData.Connect();

                SqlCommand cmdSaveData = new SqlCommand();

                
                cmdSaveData.CommandText = "InsertEmployee";
                cmdSaveData.CommandType = CommandType.StoredProcedure;
                cmdSaveData.Connection = conSaveData.sqlConStr;
                
                SqlParameter paramAge = cmdSaveData.Parameters.Add("@Age", SqlDbType.Int, 4, "Age");
                SqlParameter paramFname = cmdSaveData.Parameters.Add("@FName", SqlDbType.NVarChar, 30, "Fname"); 
                SqlParameter paramLname = cmdSaveData.Parameters.Add("@Lname", SqlDbType.NVarChar, 30, "Lname");
                SqlParameter paramManagerID = cmdSaveData.Parameters.Add("@ManageriD", SqlDbType.Int, 4, "ManagerID");
                SqlParameter paramDeptName = cmdSaveData.Parameters.Add("@Deptname", SqlDbType.NChar, 30, "Dept");
                SqlParameter paramDeptType = cmdSaveData.Parameters.Add("@DeptType", SqlDbType.NChar, 30, "DeptType");
                SqlParameter paramEmpType = cmdSaveData.Parameters.Add("@Emptype", SqlDbType.NChar, 10, "EmpType");

                
                paramAge.Value = Age;
                paramFname.Value = Fname;
                paramLname.Value = Lname;
                paramManagerID.Value = ManagerID;
                paramDeptName.Value = Dept;
                paramDeptType.Value = DeptType;

                paramEmpType.Value = EmpType;

                                
                cmdSaveData.ExecuteNonQuery();

            
                conSaveData.DisConnect();

                MessageBox.Show("Saving Successful!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, err.Source);
            }
            finally
            {
                conSaveData.DisConnect();
            }

        }

      private void LoadEmployeeType()


        {
            clsCMScon conEmployeeType = new clsCMScon();


            try
            {
                conEmployeeType.Connect();

                SqlDataAdapter daEmployeeType = new SqlDataAdapter("exec tjm_loadEmployeeType", conEmployeeType.sqlConStr);
                DataTable dtEmployeeType = new DataTable();

                daEmployeeType.Fill(dtEmployeeType);

                cmbEmpType.DataSource = dtEmployeeType;
                cmbEmpType.DisplayMember = "Description";
                cmbEmpType.ValueMember = "EmpType_ID";

                cmbEmpType.SelectedValue = 0;

                conEmployeeType.DisConnect();


            }

            catch (Exception err)

            {

                MessageBox.Show(err.Message, err.Source);


                StreamWriter sw = new StreamWriter("D:\\PW\\wowRox\\logs" + string.Format("{0:MM-dd-yyyy}", DateTime.Now) + ".log", true);
                sw.WriteLine(err.Message);
                sw.Flush();
                sw.Dispose();

            }

            finally

            {
                conEmployeeType.DisConnect();

            }

        }

        private void cmbEmpType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
           
       
    }


