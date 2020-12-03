using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AisInternalSystem.Entities
{
    public class Employee
    {
        public Employee()
        {
            
        }
        #region Properties
        public DateTime CreatedON { get; set; }
        public Int64 EmployeeID { get; set; }
        public int EmployeeIdentifier { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
        public DateTime JoinDate { get; set; }
        public string Fullname { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string HometownAddress { get; set; }
        public string HometownNumber { get; set; }
        public string Mobile { get; set; }
        public string Whatsapp { get; set; }
        public string PersonalEmail { get; set; }
        public string NPWP { get; set; }
        public string NIK { get; set; }
        public string BPJSKES { get; set; }
        public string BPJSTK { get; set; }
        public string MaritalStatus { get; set; }
        public string SpouseName { get; set; }
        public string SpousePhone { get; set; }
        public int NoOfDependant { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactRelationship { get; set; }
        public string EmergencyContactAddress { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string PhotoLocation { get; set; }
        public string PassportNumber { get; set; }
        public string EmployeeStatus { get; set; }
        public int Maker { get; set; }
        public List<Education> Educations { get; set; }
        #endregion
        public static Employee CurrentEmployee = null;
        #region Function
        public static DataTable GetDataSource()
        {
            DataTable dt = Controller.Query.GetDataTable("GetEmployeeDataSource", new string[1] { "@noparam" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { "" });
            if (dt.Rows.Count>=1)
            {
                return dt;
            }
            else
            {
                dt = null;
                return dt;
            }
        }
        public static DataTable GetSearchResult(string search, string query)
        {
            DataTable dt = Controller.Query.GetDataTable("GetSearchResultEmployee", new string[2] { "@_search", "@_query" }, new MySql.Data.MySqlClient.MySqlDbType[2] { MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[2] { search, query });
            if (dt.Rows.Count >= 1)
            {
                return dt;
            }
            else
            {
                dt = null;
                return dt;
            }
        }
        public static Employee GetEmployeeInfo(int employeeid)
        {
            Employee e = new Employee();
            DataTable dt = Controller.Query.GetDataTable("GetEmployeeData", new string[1] { "@_employeeid" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { employeeid.ToString() });
            if (dt.Rows.Count >=1)
            {
                e.EmployeeID = Convert.ToInt64(dt.Rows[0][1].ToString());
                e.EmployeeIdentifier = Convert.ToInt32(dt.Rows[0][2].ToString());
                e.Department = dt.Rows[0][3].ToString();
                e.Role = dt.Rows[0][4].ToString();
                try
                {
                    e.JoinDate = Convert.ToDateTime(dt.Rows[0][5].ToString());

                }
                catch (Exception)
                {
                    e.JoinDate = DateTime.Now;
                }
                e.Fullname = dt.Rows[0][6].ToString();
                e.Gender = dt.Rows[0][7].ToString();
                e.Religion = dt.Rows[0][8].ToString();
                e.PlaceOfBirth = dt.Rows[0][9].ToString();
                e.Nationality = dt.Rows[0][10].ToString();
                e.DateOfBirth = Convert.ToDateTime(dt.Rows[0][11].ToString());
                e.Address = dt.Rows[0][12].ToString();
                e.HomePhone = dt.Rows[0][13].ToString();
                e.HometownAddress = dt.Rows[0][14].ToString();
                e.HometownNumber = dt.Rows[0][15].ToString();
                e.Mobile = dt.Rows[0][16].ToString();
                e.Whatsapp = dt.Rows[0][17].ToString();
                e.PersonalEmail = dt.Rows[0][18].ToString();
                e.NPWP = dt.Rows[0][19].ToString();
                e.NIK = dt.Rows[0][20].ToString();
                e.BPJSKES = dt.Rows[0][21].ToString();
                e.BPJSTK = dt.Rows[0][22].ToString();
                e.MaritalStatus = dt.Rows[0][23].ToString();
                e.SpouseName = dt.Rows[0][24].ToString();
                e.SpousePhone = dt.Rows[0][25].ToString();
                try
                {
                    e.NoOfDependant = Convert.ToInt32(dt.Rows[0][26].ToString());
                }
                catch (Exception)
                {
                    e.NoOfDependant = 0;
                }
                e.EmergencyContactName = dt.Rows[0][27].ToString();
                e.EmergencyContactRelationship = dt.Rows[0][28].ToString();
                e.EmergencyContactAddress = dt.Rows[0][29].ToString();
                e.EmergencyContactPhone = dt.Rows[0][30].ToString();
                e.PhotoLocation = dt.Rows[0][31].ToString();
                e.PassportNumber = dt.Rows[0][32].ToString();
                e.EmployeeStatus = dt.Rows[0][33].ToString();
                e.Educations = Education.GetEducation(e.EmployeeIdentifier);
                return e;
            }
            else
            {
                return null;
            }
        }
        public static bool DeleteEmployee(Employee employee)
        {
            if (Controller.Query.Delete("DeleteEmployee", new string[1] { "@_employeeid" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { employee.EmployeeIdentifier.ToString() }))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool UpdatePhoto(int employeeid, string location)
        {
            if (Controller.Query.Insert("UpdatePhotoEmployee", new string[2] { "@_employeeid", "@_photolocation" }, new MySql.Data.MySqlClient.MySqlDbType[2] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[2] { employeeid.ToString(), location }))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool SaveEmployee(bool isSaved, Employee e)
        {
            if (!isSaved)
            {
                //insert data
                if (Controller.Query.Insert
                    (
                    "InsertEmployee", new string[33]
                    {
                    "@_emp_id",
                    "@_emp_department",
                    "@_emp_roles",
                    "@_emp_joindate",
                    "@_emp_fullname",
                    "@_emp_gender",
                    "@_emp_religion",
                    "@_emp_pob",
                    "@_emp_nationality",
                    "@_emp_dob",
                    "@_emp_address",
                    "@_emp_homephone",
                    "@_emp_hometown_address",
                    "@_emp_hometown_number",
                    "@_emp_mobile",
                    "@_emp_whatsapp",
                    "@_emp_personal_email",
                    "@_emp_npwp",
                    "@_emp_nik",
                    "@_emp_bpjs_kes",
                    "@_emp_bpjs_tk",
                    "@_emp_marital_stat",
                    "@_emp_spouse_name",
                    "@_emp_spouse_phone",
                    "@_emp_no_dependant",
                    "@_emp_emergencycontactname",
                    "@_emp_emergencyrelationship",
                    "@_emp_emergencycontactaddress",
                    "@_emp_emergencycontactphone",
                    "@_employee_pic",
                    "@_emp_passport_no",
                    "@_emp_status",
                    "@_created_by"
                    }, new MySql.Data.MySqlClient.MySqlDbType[33]
                    {
                     MySql.Data.MySqlClient.MySqlDbType.Int32,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.Date,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.Text,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.Text,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.Int32,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.Text,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.Int32
                    }, new string[33]
                    {
                     e.EmployeeID.ToString(),
                     e.Department,
                     e.Role,
                     e.JoinDate.ToString(Controller.PublicProperties.DateFormat),
                     e.Fullname,
                     e.Gender,
                     e.Religion,
                     e.PlaceOfBirth,
                     e.Nationality,
                     e.DateOfBirth.ToString(Controller.PublicProperties.DateFormat),
                     e.Address,
                     e.HomePhone,
                     e.HometownAddress,
                     e.HometownNumber,
                     e.Mobile,
                     e.Whatsapp,
                     e.PersonalEmail,
                     e.NPWP,
                     e.NIK,
                     e.BPJSKES,
                     e.BPJSTK,
                     e.MaritalStatus,
                     e.SpouseName,
                     e.SpousePhone,
                     e.NoOfDependant.ToString(),
                     e.EmergencyContactName,
                     e.EmergencyContactRelationship,
                     e.EmergencyContactAddress,
                     e.EmergencyContactPhone,
                     e.PhotoLocation,
                     e.PassportNumber,
                     e.EmployeeStatus,
                     e.Maker.ToString()
                    }
                    ))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                //update data
                if (Controller.Query.Insert
                    (
                    "UpdateEmployee", new string[34]
                    {
                    "@_employeeid",
                    "@_emp_id",
                    "@_emp_department",
                    "@_emp_roles",
                    "@_emp_joindate",
                    "@_emp_fullname",
                    "@_emp_gender",
                    "@_emp_religion",
                    "@_emp_pob",
                    "@_emp_nationality",
                    "@_emp_dob",
                    "@_emp_address",
                    "@_emp_homephone",
                    "@_emp_hometown_address",
                    "@_emp_hometown_number",
                    "@_emp_mobile",
                    "@_emp_whatsapp",
                    "@_emp_personal_email",
                    "@_emp_npwp",
                    "@_emp_nik",
                    "@_emp_bpjs_kes",
                    "@_emp_bpjs_tk",
                    "@_emp_marital_stat",
                    "@_emp_spouse_name",
                    "@_emp_spouse_phone",
                    "@_emp_no_dependant",
                    "@_emp_emergencycontactname",
                    "@_emp_emergencyrelationship",
                    "@_emp_emergencycontactaddress",
                    "@_emp_emergencycontactphone",
                    "@_employee_pic",
                    "@_emp_passport_no",
                    "@_emp_status",
                    "@_created_by"
                    }, new MySql.Data.MySqlClient.MySqlDbType[34]
                    {
                     MySql.Data.MySqlClient.MySqlDbType.Int32,
                     MySql.Data.MySqlClient.MySqlDbType.Int32,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.Date,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.Text,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.Text,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.Int32,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.Text,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.VarChar,
                     MySql.Data.MySqlClient.MySqlDbType.Int32
                    }, new string[34]
                    {
                     e.EmployeeIdentifier.ToString(),
                     e.EmployeeID.ToString(),
                     e.Department,
                     e.Role,
                     e.JoinDate.ToString(Controller.PublicProperties.DateFormat),
                     e.Fullname,
                     e.Gender,
                     e.Religion,
                     e.PlaceOfBirth,
                     e.Nationality,
                     e.DateOfBirth.ToString(Controller.PublicProperties.DateFormat),
                     e.Address,
                     e.HomePhone,
                     e.HometownAddress,
                     e.HometownNumber,
                     e.Mobile,
                     e.Whatsapp,
                     e.PersonalEmail,
                     e.NPWP,
                     e.NIK,
                     e.BPJSKES,
                     e.BPJSTK,
                     e.MaritalStatus,
                     e.SpouseName,
                     e.SpousePhone,
                     e.NoOfDependant.ToString(),
                     e.EmergencyContactName,
                     e.EmergencyContactRelationship,
                     e.EmergencyContactAddress,
                     e.EmergencyContactPhone,
                     e.PhotoLocation,
                     e.PassportNumber,
                     e.EmployeeStatus,
                     e.Maker.ToString()
                    }
                    ))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion
    }
}
