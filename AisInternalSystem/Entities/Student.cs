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

        }
        #region Properties
        public int ID { get; set; }
        public string AisID { get; set; }
        public string NIS { get; set; }
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



        #endregion
        #region Function
        public static bool ReviseStudent(string[] val)
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
                "@_revised",
                "@_current_grade",
                "@_proposedgrade"},
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
                    MySql.Data.MySqlClient.MySqlDbType.Int32,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar },
                    new string[34] {
                    val[0],
                    val[1],
                    val[2],
                    val[3],
                    val[4],
                    val[5],
                    val[6],
                    val[7],
                    val[8],
                    val[9],
                    val[10],
                    val[11],
                    val[12],
                    val[13],
                    val[14],
                    val[15],
                    val[16],
                    val[17],
                    val[18],
                    val[19],
                    val[20],
                    val[21],
                    val[22],
                    val[23],
                    val[24],
                    val[25],
                    val[26],
                    val[27],
                    val[28],
                    val[29],
                    val[30],
                    val[31],
                    val[32],
                    val[33],
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

        public static bool InsertStudent(string[] val)
        {
            if (
             Query.Insert("InsertStudent", new string[34]
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
                "@_maker",
                "@_current_grade",
                "@_proposedgrade"},
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
                    MySql.Data.MySqlClient.MySqlDbType.Int32,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar },
                    new string[34] {
                    val[0],
                    val[1],
                    val[2],
                    val[3],
                    val[4],
                    val[5],
                    val[6],
                    val[7],
                    val[8],
                    val[9],
                    val[10],
                    val[11],
                    val[12],
                    val[13],
                    val[14],
                    val[15],
                    val[16],
                    val[17],
                    val[18],
                    val[19],
                    val[20],
                    val[21],
                    val[22],
                    val[23],
                    val[24],
                    val[25],
                    val[26],
                    val[27],
                    val[28],
                    val[29],
                    val[30],
                    val[31],
                    val[32],
                    val[33],
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

        //public static bool DeleteStudent(int id)
        //{
        //    Query.Delete("", );
        //}

        //public static List<Student> GetAllStudentsList()
        //{
        //    List<Student> students = new List<Student>();
        //    DataTable dt = Query.GetDataTable("", new string[1] { "@noparam" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { "" });
        //    if (dt.Rows.Count >= 1)
        //    {
        //        Student[] student = new Student[dt.Rows.Count];
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            int j = -1;
        //            student[i] = new Student();
        //            student[i].ID = Convert.ToInt32(dt.Rows[i][j++].ToString());
        //            student[i].AisID = dt.Rows[i][j++].ToString();
        //            student[i].NIS = dt.Rows[i][j++].ToString();
        //            student[i].Intake = dt.Rows[i][j++].ToString();
        //            student[i].FamilyName = dt.Rows[i][j++].ToString();
        //            student[i].GivenName = dt.Rows[i][j++].ToString();
        //            student[i].MiddleName = dt.Rows[i][j++].ToString();
        //            student[i].CertificateName = dt.Rows[i][j++].ToString();
        //            student[i].DateofBirth = dt.Rows[i][j++].ToString();
        //            student[i].PlaceOfBirth = dt.Rows[i][j++].ToString();
        //            student[i].CountryOfBirth = dt.Rows[i][j++].ToString();
        //            student[i].Gender = dt.Rows[i][j++].ToString();
        //            student[i].Religion = dt.Rows[i][j++].ToString();
        //            student[i].Nationality = dt.Rows[i][j++].ToString();
        //            student[i].HomeAddress = dt.Rows[i][j++].ToString();
        //            student[i].HomeState = dt.Rows[i][j++].ToString();
        //            student[i].Suburb = dt.Rows[i][j++].ToString();
        //            student[i].PostCode = dt.Rows[i][j++].ToString();
        //            student[i].HomeCountry = dt.Rows[i][j++].ToString();
        //            student[i].PostalAddress = dt.Rows[i][j++].ToString();
        //            student[i].PostalState = dt.Rows[i][j++].ToString();
        //            student[i].PostalSuburb = dt.Rows[i][j++].ToString();
        //            student[i].PostalCode = dt.Rows[i][j++].ToString();
        //            student[i].PostalCountry = dt.Rows[i][j++].ToString();
        //            student[i].HomePhone = dt.Rows[i][j++].ToString();
        //            student[i].FaxNumber = dt.Rows[i][j++].ToString();
        //            student[i].LangSpoken = dt.Rows[i][j++].ToString();
        //            student[i].EnglishProficiency = dt.Rows[i][j++].ToString();
        //            student[i].StudentStatus = dt.Rows[i][j++].ToString();
        //            student[i].PhotoLocation = dt.Rows[i][j++].ToString();
        //            student[i].MobileNumber = dt.Rows[i][j++].ToString();


        //            students.Add(student[i]);
        //        }
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //    return students;
        //}
        #endregion
        #region Variable

        #endregion
    }
}
