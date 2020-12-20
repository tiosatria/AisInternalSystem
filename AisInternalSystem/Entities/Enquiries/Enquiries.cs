using System;
using System.Data;
using AisInternalSystem.Controller;

namespace AisInternalSystem.Entities.Enquiries
{
    public class Enquiries
    {
        public Enquiries()
        {

        }
        #region Properties
        public int EnqID { get; set; }
        public int AisID { get; set; }
        public string StudentName { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PresentSchool { get; set; }
        public string Grade { get; set; }
        public string RelationshipType { get; set; }
        public string RelationshipName { get; set; }
        public string RelationshipPhoneNumber { get; set; }
        public string RelationshipWhatsappNumber { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
        public string EnquiryStatus { get; set; }
        public string StudentCondition { get; set; }
        public string AYCODE { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Referral { get; set; }
        public string Survey { get; set; }
        public int Maker { get; set; }
        public StudentTest StudentTest { get; set; }
        public static Enquiries CurrentEnq = null;
        #endregion
        #region Function
        //public int GetLatestAISID(Enquiries enquiries)
        //{

        //}
        public static bool Insert(Enquiries e)
        {
            if (Query.Insert("InsertInquiry",
                new string[17]
                {
                    "@_studentName",
                    "@_placeofbirth",
                    "@_dateofbirth",
                    "@_PresentSchool",
                    "@_grade",
                    "@_relattype",
                    "@_relatname",
                    "@_phonenumber",
                    "@_whatsappnumber",
                    "@_address",
                    "@_remarks",
                    "@_referral",
                    "@_SurveyQuestion",
                    "@_enquiry_status",
                    "@_maker",
                    "@_academicyearcode",
                    "@_student_condition"
                },
                new MySql.Data.MySqlClient.MySqlDbType[17]
                {
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.Date,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.Text,
                    MySql.Data.MySqlClient.MySqlDbType.Text,
                    MySql.Data.MySqlClient.MySqlDbType.Text,
                    MySql.Data.MySqlClient.MySqlDbType.Text,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.Int32,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar
                }, 
                new string[17]
                { 
                e.StudentName,
                e.PlaceOfBirth,
                e.DateOfBirth.ToString(PublicProperties.DateFormat),
                e.PresentSchool,
                e.Grade,
                e.RelationshipType,
                e.RelationshipName,
                e.RelationshipPhoneNumber,
                e.RelationshipWhatsappNumber,
                e.Address,
                e.Remarks,
                e.Referral,
                e.Survey,
                e.EnquiryStatus,
                e.Maker.ToString(),
                e.AYCODE,
                e.StudentCondition
                }))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Update(Enquiries e)
        {
            if (Query.Insert("UpdateEnquiry",
                new string[19]
                {
                    "@_aisid",
                    "@_idenq",
                    "@_studentName",
                    "@_placeofbirth",
                    "@_dateofbirth",
                    "@_PresentSchool",
                    "@_grade",
                    "@_relattype",
                    "@_relatname",
                    "@_phonenumber",
                    "@_whatsappnumber",
                    "@_address",
                    "@_remarks",
                    "@_referral",
                    "@_SurveyQuestion",
                    "@_enquiry_status",
                    "@_maker",
                    "@_academicyearcode",
                    "@_student_condition"
                },
                new MySql.Data.MySqlClient.MySqlDbType[19]
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
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.Text,
                    MySql.Data.MySqlClient.MySqlDbType.Text,
                    MySql.Data.MySqlClient.MySqlDbType.Text,
                    MySql.Data.MySqlClient.MySqlDbType.Text,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.Int32,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar
                },
                new string[19]
                {
                e.AisID.ToString(),
                e.EnqID.ToString(),
                e.StudentName,
                e.PlaceOfBirth,
                e.DateOfBirth.ToString(PublicProperties.DateFormat),
                e.PresentSchool,
                e.Grade,
                e.RelationshipType,
                e.RelationshipName,
                e.RelationshipPhoneNumber,
                e.RelationshipWhatsappNumber,
                e.Address,
                e.Remarks,
                e.Referral,
                e.Survey,
                e.EnquiryStatus,
                e.Maker.ToString(),
                e.AYCODE,
                e.StudentCondition
                }))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Delete(int EnqID)
        {
            if (Query.Delete("DeleteEnquiry", new string[1] { "noparam" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { EnqID.ToString() }))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Enquiries Get(int ID)
        {
            DataTable dt = Query.GetDataTable("GetEnquiry", new string[1] { "@_idEnq" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { ID.ToString() });
            if (dt.Rows.Count >=1)
            {
                Enquiries e = new Enquiries();
                try
                {
                    e.AisID = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
                catch (Exception)
                {
                    e.AisID = -1;
                }
                e.EnqID = Convert.ToInt32(dt.Rows[0][1].ToString());
                e.StudentName = dt.Rows[0][2].ToString();
                e.PlaceOfBirth = dt.Rows[0][3].ToString(); 
                e.DateOfBirth = Convert.ToDateTime(dt.Rows[0][4].ToString());
                e.PresentSchool = dt.Rows[0][5].ToString(); 
                e.Grade = dt.Rows[0][6].ToString();
                e.RelationshipType = dt.Rows[0][7].ToString();
                e.RelationshipName = dt.Rows[0][8].ToString();
                e.RelationshipPhoneNumber = dt.Rows[0][9].ToString();
                e.RelationshipWhatsappNumber = dt.Rows[0][10].ToString();
                e.Address = dt.Rows[0][11].ToString();
                e.Remarks = dt.Rows[0][12].ToString();
                e.Referral = dt.Rows[0][13].ToString();
                e.Survey = dt.Rows[0][14].ToString();
                e.EnquiryStatus = dt.Rows[0][15].ToString();
                e.Maker = Convert.ToInt32(dt.Rows[0][16].ToString());
                e.DateOfCreation = Convert.ToDateTime(dt.Rows[0][17].ToString()); 
                e.AYCODE = dt.Rows[0][18].ToString();
                e.StudentCondition = dt.Rows[0][19].ToString();
                if (dt.Rows[0][20].ToString() != "" && dt.Rows[0][20].ToString() != null)
                {
                    e.StudentTest = new StudentTest();
                    e.StudentTest.Enquiries = e;
                    try
                    {
                        e.StudentTest.TestID = Convert.ToInt32(dt.Rows[0][20].ToString());

                    }
                    catch (Exception)
                    {
                        e.StudentTest.TestID = -1;
                    }
                    e.StudentTest.S1Name = dt.Rows[0][22].ToString();
                    e.StudentTest.S2Name = dt.Rows[0][23].ToString();
                    e.StudentTest.S3Name = dt.Rows[0][24].ToString();
                    try
                    {
                        e.StudentTest.S1Val = Convert.ToInt32(dt.Rows[0][25].ToString());
                        e.StudentTest.S2Val = Convert.ToInt32(dt.Rows[0][26].ToString());
                        e.StudentTest.S3Val = Convert.ToInt32(dt.Rows[0][27].ToString());

                    }
                    catch (Exception)
                    {
                        e.StudentTest.S1Val = -1;
                        e.StudentTest.S2Val = -1;
                        e.StudentTest.S3Val = -1;
                    }
                    try
                    {
                        e.StudentTest.teacherID = Convert.ToInt32(dt.Rows[0][32].ToString());

                    }
                    catch (Exception)
                    {
                        e.StudentTest.teacherID = -1;
                    }
                    e.StudentTest.DocsLocation = dt.Rows[0][28].ToString();
                    e.StudentTest.Maker = dt.Rows[0][29].ToString();
                    try
                    {
                        e.StudentTest.DateOfCreation = Convert.ToDateTime(dt.Rows[0][30].ToString());
                        e.StudentTest.TestTaken = Convert.ToDateTime(dt.Rows[0][31].ToString());

                    }
                    catch (Exception)
                    {
                        e.StudentTest.DateOfCreation = DateTime.Now;
                        e.StudentTest.TestTaken = DateTime.Now;
                    }
                }
                else
                {
                    e.StudentTest = null;
                }
                return e;
            }
            else
            {
                return null;
            }
        }
        public static DataTable GetDataSource()
        {
            DataTable dt = Query.GetDataTable("GetEnquiryDataSource", new string[1] { "noparam" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[1] { "" });
            return dt;
        }
        public static DataTable GetDataSourceBy(string filter, string keyfilter)
        {
            DataTable dt = Query.GetDataTable("InitDGEnq", new string[2] { "@_filter", "@_filterkey" }, new MySql.Data.MySqlClient.MySqlDbType[2] { MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[2] { filter, keyfilter });
            return dt;
        }
        public static DataTable GetDataSourceByFilter(UserInterface.Enquiries.ReviewEnquiriesUC.FilterBy f, string filterkey)
        {
            DataTable dt = null;
            switch (f)
            {
                case UserInterface.Enquiries.ReviewEnquiriesUC.FilterBy.Name:
                    dt = GetDataSourceBy("name", filterkey);
                    break;
                case UserInterface.Enquiries.ReviewEnquiriesUC.FilterBy.Status:
                    dt = GetDataSourceBy("status", filterkey);
                    break;
                case UserInterface.Enquiries.ReviewEnquiriesUC.FilterBy.Procedure:
                    dt = GetDataSourceBy("procedure", filterkey);
                    break;
                case UserInterface.Enquiries.ReviewEnquiriesUC.FilterBy.Grade:
                    dt =GetDataSourceBy("grade", filterkey);
                    break;
                default:
                    dt = null;
                    break;
            }
            return dt;
        }
        #endregion
    }
    }
