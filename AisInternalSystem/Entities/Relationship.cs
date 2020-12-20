using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AisInternalSystem.Entities
{
    public class Relationship
    {
        public Relationship()
        {

        }
        #region Properties
        public int RelatinshipID { get; set; }
        public string RelationshipType { get; set; }
        public string RelationshipName { get; set; }
        public string Nationality { get; set; }
        public string AustralianResidence { get; set; }
        public string AustralianAborigin { get; set; }
        public string SchoolEducation { get; set; }
        public string NonSchoolEducation { get; set; }
        public string Occupation { get; set; }
        public string Homeaddress { get; set; }
        public string Homestate { get; set; }
        public string HomeCountry { get; set; }
        public string Suburb { get; set; }
        public string PostCode { get; set; }
        public string PostalAddress { get; set; }
        public string PostalState { get; set; }
        public string PostalSuburb { get; set; }
        public string PostalCode { get; set; }
        public string PostalCountry { get; set; }
        public string HomephoneNo { get; set; }
        public string MobileNumb { get; set; }
        public string FaxNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Whatsapp { get; set; }
        public string MainLang { get; set; }
        public string OtherThanEnglish { get; set; }
        public DateTime DOC { get; set; }
        public string Photolocation { get; set; }
        public int Maker { get; set; }
        #endregion
        public static Relationship CurrentRelationship = null;
        #region Function
        public static Relationship GetRelationship(Relationship relationship)
        {
            CurrentRelationship = new Relationship();
            DataTable dt = Controller.Query.GetDataTable("GetRelationship", new string[1] { "@_id" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { relationship.RelatinshipID.ToString() });
            if (dt.Rows.Count >=1)
            {
                CurrentRelationship.RelationshipType = dt.Rows[0][1].ToString();
                CurrentRelationship.RelationshipName = dt.Rows[0][2].ToString();
                CurrentRelationship.Nationality = dt.Rows[0][3].ToString();
                CurrentRelationship.AustralianResidence = dt.Rows[0][4].ToString();
                CurrentRelationship.AustralianAborigin = dt.Rows[0][5].ToString();
                CurrentRelationship.SchoolEducation = dt.Rows[0][6].ToString();
                CurrentRelationship.NonSchoolEducation = dt.Rows[0][7].ToString();
                CurrentRelationship.Occupation = dt.Rows[0][8].ToString();
                CurrentRelationship.Homeaddress = dt.Rows[0][9].ToString();
                CurrentRelationship.Homestate = dt.Rows[0][10].ToString();
                CurrentRelationship.HomeCountry = dt.Rows[0][11].ToString();
                CurrentRelationship.Suburb = dt.Rows[0][12].ToString();
                CurrentRelationship.PostCode = dt.Rows[0][13].ToString();
                CurrentRelationship.PostalAddress = dt.Rows[0][14].ToString();
                CurrentRelationship.PostalState = dt.Rows[0][15].ToString();
                CurrentRelationship.PostalSuburb = dt.Rows[0][16].ToString();
                CurrentRelationship.PostalCode = dt.Rows[0][17].ToString();
                CurrentRelationship.PostalCountry = dt.Rows[0][18].ToString();
                CurrentRelationship.HomephoneNo = dt.Rows[0][19].ToString();
                CurrentRelationship.MobileNumb = dt.Rows[0][20].ToString();
                CurrentRelationship.FaxNumber = dt.Rows[0][21].ToString();
                CurrentRelationship.EmailAddress = dt.Rows[0][22].ToString();
                CurrentRelationship.Whatsapp = dt.Rows[0][23].ToString();
                CurrentRelationship.MainLang = dt.Rows[0][24].ToString();
                CurrentRelationship.OtherThanEnglish = dt.Rows[0][25].ToString();
                CurrentRelationship.Photolocation = dt.Rows[0][27].ToString();
                CurrentRelationship.RelatinshipID = relationship.RelatinshipID;
                return CurrentRelationship;
            }
            else
            {
                CurrentRelationship = null;
                return CurrentRelationship;
            }
        }
        public static List<Relationship> GetRelationshipGroupByAISID(int aisid)
        {
            int j = 1;
            List<Relationship> ListOfRelationship = new List<Relationship>();
            DataTable dt = Controller.Query.GetDataTable("GetRelationshipGroup", new string[1] { "@_studaisid" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { aisid.ToString() });
            if (dt.Rows.Count >=1)
            {
                Relationship[] relationships = new Relationship[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    relationships[i] = new Relationship();
                    relationships[i].RelatinshipID = Convert.ToInt32(dt.Rows[0][j].ToString());
                    relationships[i].RelationshipType = dt.Rows[0][j++].ToString();
                    relationships[i].RelationshipName = dt.Rows[0][j++].ToString();
                    relationships[i].Nationality = dt.Rows[0][j++].ToString();
                    relationships[i].AustralianResidence = dt.Rows[0][j++].ToString();
                    relationships[i].AustralianAborigin = dt.Rows[0][j++].ToString();
                    relationships[i].SchoolEducation = dt.Rows[0][j++].ToString();
                    relationships[i].NonSchoolEducation = dt.Rows[0][j++].ToString();
                    relationships[i].Occupation = dt.Rows[0][j++].ToString();
                    relationships[i].Homeaddress = dt.Rows[0][j++].ToString();
                    relationships[i].Homestate = dt.Rows[0][j++].ToString();
                    relationships[i].HomeCountry = dt.Rows[0][j++].ToString();
                    relationships[i].Suburb = dt.Rows[0][j++].ToString();
                    relationships[i].PostCode = dt.Rows[0][j++].ToString();
                    relationships[i].PostalAddress = dt.Rows[0][j++].ToString();
                    relationships[i].PostalState = dt.Rows[0][j++].ToString();
                    relationships[i].PostalSuburb = dt.Rows[0][j++].ToString();
                    relationships[i].PostalCode = dt.Rows[0][j++].ToString();
                    relationships[i].PostalCountry = dt.Rows[0][j++].ToString();
                    relationships[i].HomephoneNo = dt.Rows[0][j++].ToString();
                    relationships[i].MobileNumb = dt.Rows[0][j++].ToString();
                    relationships[i].FaxNumber = dt.Rows[0][j++].ToString();
                    relationships[i].EmailAddress = dt.Rows[0][j++].ToString();
                    relationships[i].Whatsapp = dt.Rows[0][j++].ToString();
                    relationships[i].MainLang = dt.Rows[0][j++].ToString();
                    relationships[i].OtherThanEnglish = dt.Rows[0][j++].ToString();
                    relationships[i].DOC = Convert.ToDateTime(dt.Rows[0][j++].ToString());
                    relationships[i].Photolocation = dt.Rows[0][j++].ToString();
                    relationships[i].Maker = Convert.ToInt32(dt.Rows[0][j++].ToString());
                    ListOfRelationship.Add(relationships[i]);
                }
                return ListOfRelationship;
            }
            else
            {
                ListOfRelationship = null;
                return ListOfRelationship;
            }
        }
        public static bool SaveRelationship(bool isSaved, Relationship relationship)
        {
            if (!isSaved)
            {
                //insert
                if (Controller.Query.Insert("InsertRelationship", new string[27] {
                    "@_relationship",
                    "@_relationshipname",
                    "@_nationality",
                    "@_australianresidence",
                    "@_auabortorres",
                    "@_schooleducation",
                    "@_nonschooleducation",
                    "@_occupation",
                    "@_homeaddress",
                    "@_homestate",
                    "@_homecountry",
                    "@_suburb",
                    "@_postcode",
                    "@_postaladdress",
                    "@_postalstate",
                    "@_postalsuburb",
                    "@_postalcode",
                    "@_postalcountry",
                    "@_homephoneno",
                    "@_mobilenumb",
                    "@_faxnumb",
                    "@_emailaddress",
                    "@_whatsapp",
                    "@_mainlang",
                    "@_otherthanenglish",
                    "@_photo",
                    "@_maker"
                     },
                    new MySql.Data.MySqlClient.MySqlDbType[27] { MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.Text, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Int32},
                    new string[27]
                    {
                    relationship.RelationshipType, relationship.RelationshipName, relationship.Nationality, relationship.AustralianResidence, relationship.AustralianAborigin, relationship.SchoolEducation,
                    relationship.NonSchoolEducation, relationship.Occupation, relationship.Homeaddress, relationship.Homestate, relationship.HomeCountry, relationship.Suburb, relationship.PostCode,
                    relationship.PostalAddress, relationship.PostalState, relationship.PostalSuburb, relationship.PostalCode, relationship.PostalCountry, relationship.HomephoneNo, relationship.MobileNumb,
                    relationship.FaxNumber, relationship.EmailAddress, relationship.Whatsapp, relationship.MainLang, relationship.OtherThanEnglish, relationship.Photolocation, relationship.Maker.ToString()
                    }))
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
                //revise
                if (Controller.Query.Insert("UpdateRelationship", new string[28] {
                    "@_id",
                    "@_relationship",
                    "@_relationshipname",
                    "@_nationality",
                    "@_australianresidence",
                    "@_auabortorres",
                    "@_schooleducation",
                    "@_nonschooleducation",
                    "@_occupation",
                    "@_homeaddress",
                    "@_homestate",
                    "@_homecountry",
                    "@_suburb",
                    "@_postcode",
                    "@_postaladdress",
                    "@_postalstate",
                    "@_postalsuburb",
                    "@_postalcode",
                    "@_postalcountry",
                    "@_homephoneno",
                    "@_mobilenumb",
                    "@_faxnumb",
                    "@_emailaddress",
                    "@_whatsapp",
                    "@_mainlang",
                    "@_otherthanenglish",
                    "@_photo",
                    "@_maker"
                     },
                    new MySql.Data.MySqlClient.MySqlDbType[28] { MySql.Data.MySqlClient.MySqlDbType.Int32 ,MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.Text, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar,
                    MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.Int32},
                    new string[28]
                    {
                    relationship.RelatinshipID.ToString() ,relationship.RelationshipType, relationship.RelationshipName, relationship.Nationality, relationship.AustralianResidence, relationship.AustralianAborigin, relationship.SchoolEducation,
                    relationship.NonSchoolEducation, relationship.Occupation, relationship.Homeaddress, relationship.Homestate, relationship.HomeCountry, relationship.Suburb, relationship.PostCode,
                    relationship.PostalAddress, relationship.PostalState, relationship.PostalSuburb, relationship.PostalCode, relationship.PostalCountry, relationship.HomephoneNo, relationship.MobileNumb,
                    relationship.FaxNumber, relationship.EmailAddress, relationship.Whatsapp, relationship.MainLang, relationship.OtherThanEnglish, relationship.Photolocation, relationship.Maker.ToString()
                    }))
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }
        public static bool DeleteRelationship(Relationship relationship)
        {
            if (Controller.Query.Delete("DeleteRelationship", new string[1] { "@_id" }, new MySql.Data.MySqlClient.MySqlDbType[1] { MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[1] { relationship.RelatinshipID.ToString() }))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool UnlinkRelationshipFromStudent(Relationship relationship, Student student)
        {
            if (Controller.Query.Delete("UnlinkRelationshipFromStudent", new string[2] { "@_studaisid", "@_parrelatid" }, new MySql.Data.MySqlClient.MySqlDbType[2] { MySql.Data.MySqlClient.MySqlDbType.Int32, MySql.Data.MySqlClient.MySqlDbType.Int32 }, new string[2] { relationship.RelatinshipID.ToString(), student.AisID.ToString() }))
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        public static DataTable GetDataSource(string relationship, string name)
        {
            DataTable dt = Controller.Query.GetDataTable("GetParentList", new string[2] { "@_relationship", "@_names" }, new MySql.Data.MySqlClient.MySqlDbType[2] { MySql.Data.MySqlClient.MySqlDbType.VarChar, MySql.Data.MySqlClient.MySqlDbType.VarChar }, new string[2] { relationship, name });
            if (dt.Rows.Count >=1)
            {
                return dt;
            }
            else
            {
                dt = null;
                return dt;
            }
        }
        #endregion


    }
}
