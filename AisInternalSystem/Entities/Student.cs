using AisInternalSystem.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI;

namespace AisInternalSystem.Entities
{

    public class Student
    {
        public Student()
        {
            UserInterface.Student.UCStudDirectory.CurrentSelectedStudentChanged += UCStudDirectory_CurrentSelectedStudentChanged;
        }

        private void UCStudDirectory_CurrentSelectedStudentChanged(object sender, Student e)
        {
            CurrentStudent = e;

        }

        #region Properties
        public int ID { get; set; }
        public int AisID { get; set; }
        public string NIS { get; set; }
        public string ASN { get; set; }
        public DateTime Intake { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string CertificateName { get; set; }
        public DateTime DateofBirth { get; set; }
        private int _age;

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }
        public string PlaceOfBirth { get; set; }
        public string CountryOfBirth { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public string HomeAddress { get; set; }
        public string HomeState { get; set; }
        public string Suburb { get; set; }
        public string PostCode { get; set; }
        public string HomeCountry { get; set; }
        public string PostalCode { get; set; }
        public string PostalAddress { get; set; }
        public string PostalState { get; set; }
        public string PostalSuburb { get; set; }
        public string PostalCountry { get; set; }
        public string HomePhone { get; set; }
        public string MobileNumber { get; set; }
        public string FaxNumber { get; set; }
        public string LangSpoken { get; set; }
        public string EnglishProficiency { get; set; }
        public string StudentStatus { get; set; }
        public string PhotoLocation { get; set; }
        public int CurrClass { get; set; }
        public string CurrClassString { get; set; }
        public DateTime DateOfCreation { get; set; }
        public int Maker { get; set; }
        public string MakerString { get; set; }
        public int CurrentGrade { get; set; }
        public string CurrentGradeString { get; set; }
        public string ProposedGrade { get; set; }
        public int Revised { get; set; }
        public string RevisedString { get; set; }
        public string localForeigner { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string GuardianName { get; set; }
        public string FatherContact { get; set; }
        public string MotherContact { get; set; }
        public string GuardianContact { get; set; }
        public string ClassName { get; set; }


        #endregion
        #region Function
        public static List<Student> GetListOfStudent(int classid)
        {
            List<Student> studentList = new List<Student>();
            DataTable dt = Query.GetDataTable("GetStudentList", new string[1] { "@_currclass" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { classid.ToString() });
            if (dt.Rows.Count >=1)
            {
                int j = 0;
                Student[] students = new Student[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    students[i] = new Student();
                    try
                    {
                        students[i].ID = Convert.ToInt32(dt.Rows[i][j].ToString());

                    }
                    catch (Exception)
                    {
                        students[i].ID = 0;
                        j++;
                    }
                    try
                    {
                        students[i].AisID = Convert.ToInt32(dt.Rows[i][j++].ToString());

                    }
                    catch (Exception)
                    {
                        students[i].AisID = 0;
                        j++;
                    }
                    students[i].NIS = dt.Rows[i][j++].ToString();
                    students[i].NIS = dt.Rows[i][j++].ToString();
                    students[i].ASN = dt.Rows[i][j++].ToString();
                    students[i].Intake = Convert.ToDateTime(dt.Rows[i][j++].ToString());
                    students[i].FamilyName = dt.Rows[i][j++].ToString();
                    students[i].GivenName = dt.Rows[i][j++].ToString();
                    students[i].MiddleName = dt.Rows[i][j++].ToString();
                    students[i].CertificateName = dt.Rows[i][j++].ToString();
                    students[i].DateofBirth = Convert.ToDateTime(dt.Rows[i][j++].ToString());
                    students[i].PlaceOfBirth = dt.Rows[i][j++].ToString();
                    students[i].CountryOfBirth = dt.Rows[i][j++].ToString();
                    students[i].Gender = dt.Rows[i][j++].ToString();
                    students[i].Religion = dt.Rows[i][j++].ToString();
                    students[i].Nationality = dt.Rows[i][j++].ToString();
                    students[i].HomeAddress = dt.Rows[i][j++].ToString();
                    students[i].HomeState = dt.Rows[i][j++].ToString();
                    students[i].Suburb = dt.Rows[i][j++].ToString();
                    students[i].PostCode = dt.Rows[i][j++].ToString();
                    students[i].HomeCountry = dt.Rows[i][j++].ToString();
                    students[i].PostalAddress = dt.Rows[i][j++].ToString();
                    students[i].PostalState = dt.Rows[i][j++].ToString();
                    students[i].PostalSuburb = dt.Rows[i][j++].ToString();
                    students[i].PostalCode = dt.Rows[i][j++].ToString();
                    students[i].PostalCountry = dt.Rows[i][j++].ToString();
                    students[i].HomePhone = dt.Rows[i][j++].ToString();
                    students[i].MobileNumber = dt.Rows[i][j++].ToString();
                    students[i].FaxNumber = dt.Rows[i][j++].ToString();
                    students[i].LangSpoken = dt.Rows[i][j++].ToString();
                    students[i].EnglishProficiency = dt.Rows[i][j++].ToString();
                    students[i].StudentStatus = dt.Rows[i][j++].ToString();
                    students[i].PhotoLocation = dt.Rows[i][j++].ToString();
                    students[i].CurrClass = Convert.ToInt32(dt.Rows[i][j++].ToString());
                    students[i].DateOfCreation = Convert.ToDateTime(dt.Rows[i][j++].ToString());
                    students[i].Maker = Convert.ToInt32(dt.Rows[i][j++].ToString());
                    students[i].CurrentGrade = Convert.ToInt32(dt.Rows[i][j++].ToString());
                    students[i].ProposedGrade = dt.Rows[i][j++].ToString();
                    try
                    {
                        students[i].Revised = Convert.ToInt32(dt.Rows[i][j++].ToString());

                    }
                    catch (Exception)
                    {
                        students[i].Revised = 0;
                        j++;
                    }
                    j = 0;
                    studentList.Add(students[i]);
                }
            }
            else
            {
                studentList = null;
            }
            return studentList;
        }

        

        public static List<Student> GetListOfStudent()
        {
            //possible bug for later
            List<Student> studentList = new List<Student>();
            DataTable dt = Query.GetDataTable("GetStudentList", new string[1] { "@_currclass" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { "9999" });
            if (dt.Rows.Count >= 1)
            {
                int j = 0;
                Student[] students = new Student[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    students[i] = new Student();
                    students[i].ID = Convert.ToInt32(dt.Rows[i][j].ToString());
                    students[i].AisID = Convert.ToInt32(dt.Rows[i][j++].ToString());
                    students[i].NIS = dt.Rows[i][j++].ToString();
                    students[i].NIS = dt.Rows[i][j++].ToString();
                    students[i].ASN = dt.Rows[i][j++].ToString();
                    students[i].Intake = Convert.ToDateTime(dt.Rows[i][j++].ToString());
                    students[i].FamilyName = dt.Rows[i][j++].ToString();
                    students[i].GivenName = dt.Rows[i][j++].ToString();
                    students[i].MiddleName = dt.Rows[i][j++].ToString();
                    students[i].CertificateName = dt.Rows[i][j++].ToString();
                    students[i].DateofBirth = Convert.ToDateTime(dt.Rows[i][j++].ToString());
                    students[i].PlaceOfBirth = dt.Rows[i][j++].ToString();
                    students[i].CountryOfBirth = dt.Rows[i][j++].ToString();
                    students[i].Gender = dt.Rows[i][j++].ToString();
                    students[i].Religion = dt.Rows[i][j++].ToString();
                    students[i].Nationality = dt.Rows[i][j++].ToString();
                    students[i].HomeAddress = dt.Rows[i][j++].ToString();
                    students[i].HomeState = dt.Rows[i][j++].ToString();
                    students[i].Suburb = dt.Rows[i][j++].ToString();
                    students[i].PostCode = dt.Rows[i][j++].ToString();
                    students[i].HomeCountry = dt.Rows[i][j++].ToString();
                    students[i].PostalAddress = dt.Rows[i][j++].ToString();
                    students[i].PostalState = dt.Rows[i][j++].ToString();
                    students[i].PostalSuburb = dt.Rows[i][j++].ToString();
                    students[i].PostalCode = dt.Rows[i][j++].ToString();
                    students[i].PostalCountry = dt.Rows[i][j++].ToString();
                    students[i].HomePhone = dt.Rows[i][j++].ToString();
                    students[i].MobileNumber = dt.Rows[i][j++].ToString();
                    students[i].FaxNumber = dt.Rows[i][j++].ToString();
                    students[i].LangSpoken = dt.Rows[i][j++].ToString();
                    students[i].EnglishProficiency = dt.Rows[i][j++].ToString();
                    students[i].StudentStatus = dt.Rows[i][j++].ToString();
                    students[i].PhotoLocation = dt.Rows[i][j++].ToString();
                    students[i].CurrClass = Convert.ToInt32(dt.Rows[i][j++].ToString());
                    students[i].DateOfCreation = Convert.ToDateTime(dt.Rows[i][j++].ToString());
                    students[i].Maker = Convert.ToInt32(dt.Rows[i][j++].ToString());
                    students[i].CurrentGrade = Convert.ToInt32(dt.Rows[i][j++].ToString());
                    students[i].ProposedGrade = dt.Rows[i][j++].ToString();
                    students[i].Revised = Convert.ToInt32(dt.Rows[i][j++].ToString());
                    students[i].FatherName = dt.Rows[i][j++].ToString();
                    students[i].MotherName = dt.Rows[i][j++].ToString();
                    studentList.Add(students[i]);
                }
            }
            else
            {
                studentList = null;
            }
            return studentList;
        }

        public static DataTable GetDataSource()
        {
            DataTable dt = Query.GetDataTable("FetchAllStudentList", new string[1] { "@noparam" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] {"" });
            return dt;
        }

        public static DataTable GetDataSourceByName(string name)
        {
            DataTable dt = Query.GetDataTable("FetchStudentByName", new string[1] { "@_certificatename" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { name });
            return dt;
        }

        public static DataTable GetDataSourceByID(string id)
        {
            DataTable dt = Query.GetDataTable("FetchStudentByID", new string[1] { "@_aisid" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { id.ToString() });
            return dt;
        }

        public static DataTable GetDataSourceByGender(string gender)
        {
            DataTable dt = Query.GetDataTable("FetchStudentByGender", new string[1] { "@_gender" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { gender });
            return dt;
        }

        public static DataTable GetDataSourceByRevised()
        {
            DataTable dt = Query.GetDataTable("FetchStudentByRevised", new string[1] { "@noparam" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { "" });
            return dt;
        }

        public static DataTable GetDataSourceByNotRevised()
        {
            DataTable dt = Query.GetDataTable("FetchStudentByRevised", new string[1] { "@noparam" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { "" });
            return dt;
        }

        public static DataTable GetDataSourceByLocal()
        {
            DataTable dt = Query.GetDataTable("FetchStudentByLocal", new string[1] { "@noparam" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { "" });
            return dt;
        }
        public static Student CurrentStudent = null;
        public static Student GetStudentInfo(int aisid)
        {
            if (aisid != 0)
            {
                try
                {
                    CurrentStudent = new Student();
                    DataTable dt = Query.GetDataTable("GetStudentInfo", new string[1] { "_aisid" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { aisid.ToString() });
                    CurrentStudent.AisID = aisid;
                    CurrentStudent.CertificateName = dt.Rows[0][0].ToString();
                    CurrentStudent.DateofBirth = Convert.ToDateTime(dt.Rows[0][1].ToString());
                    CurrentStudent.Nationality = dt.Rows[0][2].ToString();
                    CurrentStudent.FatherContact = dt.Rows[0][3].ToString();
                    CurrentStudent.MotherContact = dt.Rows[0][4].ToString();
                    CurrentStudent.HomeAddress = dt.Rows[0][5].ToString();
                    CurrentStudent.ClassName = dt.Rows[0][6].ToString();
                    CurrentStudent.MakerString = dt.Rows[0][7].ToString();
                    CurrentStudent.RevisedString = dt.Rows[0][8].ToString();
                    CurrentStudent.DateOfCreation = Convert.ToDateTime(dt.Rows[0][9].ToString());
                    CurrentStudent.PhotoLocation = dt.Rows[0][10].ToString();
                }
                catch (Exception)
                {
                    PopUp.Alert("Something wrong", frmAlert.AlertType.Warning);
                }

            }
            return CurrentStudent;
        }

        public static DataTable GetDataSourceByExpat()
        {
            DataTable dt = Query.GetDataTable("FetchStudentByExpat", new string[1] { "@noparam" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { "" });
            return dt;
        }

        public static Student GetStudent(int id)
        {
            Student student = CurrentStudent;
            DataTable dt = Query.GetDataTable("GetStudentData", new string[1] { "@_aisid" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { id.ToString() });
            if (dt.Rows.Count >= 1)
            {
                student.AisID = id;
                student.NIS = dt.Rows[0][1].ToString();
                student.ASN = dt.Rows[0][2].ToString();
                student.Intake = Convert.ToDateTime(dt.Rows[0][3].ToString());
                student.FamilyName = dt.Rows[0][4].ToString();
                student.GivenName = dt.Rows[0][5].ToString();
                student.MiddleName = dt.Rows[0][6].ToString();
                student.CertificateName = dt.Rows[0][7].ToString();
                student.DateofBirth= Convert.ToDateTime(dt.Rows[0][8].ToString());
                student.PlaceOfBirth = dt.Rows[0][9].ToString();
                student.CountryOfBirth = dt.Rows[0][10].ToString();
                student.Gender = dt.Rows[0][11].ToString();
                student.Religion = dt.Rows[0][12].ToString();
                student.Nationality = dt.Rows[0][13].ToString();
                student.HomeAddress = dt.Rows[0][14].ToString();
                student.HomeState = dt.Rows[0][15].ToString();
                student.Suburb = dt.Rows[0][16].ToString();
                student.PostCode = dt.Rows[0][17].ToString();
                student.HomeCountry = dt.Rows[0][18].ToString();
                student.PostalAddress = dt.Rows[0][19].ToString();
                student.PostalState = dt.Rows[0][20].ToString();
                student.PostalCode = dt.Rows[0][21].ToString();
                student.PostalCountry = dt.Rows[0][22].ToString();
                student.HomePhone = dt.Rows[0][23].ToString();
                student.MobileNumber = dt.Rows[0][24].ToString();
                student.MobileNumber = dt.Rows[0][25].ToString();
                student.FaxNumber = dt.Rows[0][26].ToString();
                student.LangSpoken = dt.Rows[0][27].ToString();
                student.EnglishProficiency = dt.Rows[0][28].ToString();
                student.StudentStatus = dt.Rows[0][29].ToString();
                student.PhotoLocation = dt.Rows[0][30].ToString();
                student.CurrClass = Convert.ToInt32(dt.Rows[0][31].ToString());
                student.Maker = Convert.ToInt32(dt.Rows[0][33].ToString());
                student.CurrentGrade= Convert.ToInt32(dt.Rows[0][34].ToString());
                student.CurrentGradeString = Module.Grade.GetGradeNameByID(student.CurrentGrade.ToString());
                student.ProposedGrade = dt.Rows[0][35].ToString();

                try
                {
                    student.Revised = Convert.ToInt32(dt.Rows[0][36].ToString());

                }
                catch (Exception)
                
                {
                    student.Revised = 0;
                }
            }
            else
            {
            }
            return student;
        }

        public static DataTable GetStudentOverview(string str)
        {
            DataTable dt = Query.GetDataTable(str, new string[1] {"@noparam" }, new MySql.Data.MySqlClient.MySqlDbType[1] {  MySql.Data.MySqlClient.MySqlDbType.VarChar}, new string[1] {"" });
            return dt;
        }

        public static bool ReviseStudent(Student student)
        {
            if (
             Query.Insert("UpdateStudent", new string[34] 
             { "@_aisid",
                "@_nis",
                "@_ausid",
                "@_intake",
                "@_familyname",
                "@_givenname",
                "@_middlename",
                "@_certificatename",
                "@_dob",
                "@_pob",
                "@_cob",
                "@_gender",
                "@_religion",
                "@_nationality",
                "@_homeaddress",
                "@_homestate",
                "@_suburb",
                "@_postcode",
                "@_homecountry",
                "@_postaladdress",
                "@_postalstate",
                "@_postalsuburb",
                "@_postalcode",
                "@_postalcountry",
                "@_homephone",
                "@_mobilenumb",
                "@_faxnumb",
                "@_langspoken",
                "@_englishproficiency",
                "@_studstat",
                "@_studentimg",
                "@_current_grade",
                "@_proposedgrade", 
                "@_revised" },
                    new MySql.Data.MySqlClient.MySqlDbType[34]
                    {
                    MySql.Data.MySqlClient.MySqlDbType.Int32,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.Date,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.Date,
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
                    MySql.Data.MySqlClient.MySqlDbType.Int32 },
                    new string[34] {
                    student.AisID.ToString(),
                    student.NIS.ToString(),
                    student.ASN,
                    student.Intake.ToString(PublicProperties.DateFormat),
                    student.FamilyName,
                    student.GivenName,
                    student.MiddleName,
                    student.CertificateName,
                    student.DateofBirth.ToString(PublicProperties.DateFormat),
                    student.PlaceOfBirth,
                    student.CountryOfBirth,
                    student.Gender,
                    student.Religion,
                    student.Nationality,
                    student.HomeAddress,
                    student.HomeState,
                    student.Suburb,
                    student.PostCode,
                    student.HomeCountry,
                    student.PostalAddress,
                    student.PostalState,
                    student.PostalCode,
                    student.PostalCountry,
                    student.HomePhone,
                    student.MobileNumber,
                    student.MobileNumber,
                    student.FaxNumber,
                    student.LangSpoken,
                    student.EnglishProficiency,
                    student.StudentStatus,
                    student.PhotoLocation,
                    student.CurrentGrade.ToString(),
                    student.ProposedGrade,
                    student.Revised.ToString()
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

        public static bool InsertStudent(Student student)
        {
            if (
 Query.Insert("InsertStudent", new string[34]
 {              "@_aisid",
                "@_nis",
                "@_ausid",
                "@_intake",
                "@_familyname",
                "@_givenname",
                "@_middlename",
                "@_certificatename",
                "@_dob",
                "@_pob",
                "@_cob",
                "@_gender",
                "@_religion",
                "@_nationality",
                "@_homeaddress",
                "@_homestate",
                "@_suburb",
                "@_postcode",
                "@_homecountry",
                "@_postaladdress",
                "@_postalstate",
                "@_postalsuburb",
                "@_postalcode",
                "@_postalcountry",
                "@_homephone",
                "@_mobilenumb",
                "@_faxnumb",
                "@_langspoken",
                "@_englishproficiency",
                "@_studstat",
                "@_studentimg",
                "@_current_grade",
                "@_proposedgrade",
                "@_revised" },
        new MySql.Data.MySqlClient.MySqlDbType[34]
        {
                    MySql.Data.MySqlClient.MySqlDbType.Int32,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.Date,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.Date,
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
                    MySql.Data.MySqlClient.MySqlDbType.Int32 },
        new string[34] {
                    student.AisID.ToString(),
                    student.NIS.ToString(),
                    student.ASN,
                    student.Intake.ToString(PublicProperties.DateFormat),
                    student.FamilyName,
                    student.GivenName,
                    student.MiddleName,
                    student.CertificateName,
                    student.DateofBirth.ToString(PublicProperties.DateFormat),
                    student.PlaceOfBirth,
                    student.CountryOfBirth,
                    student.Gender,
                    student.Religion,
                    student.Nationality,
                    student.HomeAddress,
                    student.HomeState,
                    student.Suburb,
                    student.PostCode,
                    student.HomeCountry,
                    student.PostalAddress,
                    student.PostalState,
                    student.PostalCode,
                    student.PostalCountry,
                    student.HomePhone,
                    student.MobileNumber,
                    student.MobileNumber,
                    student.FaxNumber,
                    student.LangSpoken,
                    student.EnglishProficiency,
                    student.StudentStatus,
                    student.PhotoLocation,
                    student.CurrentGrade.ToString(),
                    student.ProposedGrade,
                    student.Maker.ToString()
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
        #endregion
        #region Variable

        #endregion
    }
}
