using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AisInternalSystem.Module
{
    public class Data
    {

        public StringCollection Religion = new StringCollection()
        {"CHRISTIAN",
            "MOESLEM",
            "CATHOLIC",
            "KONG HU CHU",
            "BUDDHIST",
            "HINDU"
        };

        public StringCollection Proficiency = new StringCollection()
        {
            "VERY WELL", "WELL", "NOT WELL", "NOT AT ALL"
        };

        public StringCollection Grade = new StringCollection()
        {
            "NOT ASSIGNED", "NURSERY", "KINDERGARTEN 1", "KINDERGARTEN 2", "KINDERGARTEN 3", "PRIMARY 1", "PRIMARY 2", "PRIMARY 3", "PRIMARY 4", "PRIMARY 5", "PRIMARY 6", "YEAR 7", "YEAR 8", "YEAR 9", "YEAR 10", "YEAR 11", "YEAR 12"
        };

        public StringCollection ClassGrade = new StringCollection()
        {
            "NOT ASSIGNED", "NURSERY", "KINDERGARTEN 1", "KINDERGARTEN 2", "KINDERGARTEN 3", "PRIMARY 1", "PRIMARY 2", "PRIMARY 3", "PRIMARY 4", "PRIMARY 5", "PRIMARY 6", "YEAR 7", "YEAR 8", "YEAR 9", "YEAR 10", "YEAR 11", "YEAR 12"
        };

        public StringCollection ChooseGrade = new StringCollection()
        {
            "NOT ASSIGNED", "NURSERY", "KINDERGARTEN 1", "KINDERGARTEN 2", "KINDERGARTEN 3", "PRIMARY 1", "PRIMARY 2", "PRIMARY 3", "PRIMARY 4", "PRIMARY 5", "PRIMARY 6", "YEAR 7", "YEAR 8", "YEAR 9", "YEAR 10", "YEAR 11", "YEAR 12"
        };

        public StringCollection StudentStatus = new StringCollection()
        {
            "ACTIVE", "SUSPEND", "ON LEAVE", "DEFER", "TRANSFER", "GRADUATE"
        };

        public StringCollection DocumentType = new StringCollection()
        {
            "Report Card", "Birth Certificate", "KITAS", "Photocopy Family Card (KK)", "Photocopy Parents ID (KTP)",
            "Passport Photo", "Passport", "Transfer Letter", "Other"
        };

        //autocomplete string collection
        public AutoCompleteStringCollection placeofbirth = new AutoCompleteStringCollection();
        public AutoCompleteStringCollection countryofbirth = new AutoCompleteStringCollection();
        public AutoCompleteStringCollection nationality = new AutoCompleteStringCollection();
        public AutoCompleteStringCollection HomeState = new AutoCompleteStringCollection();
        public AutoCompleteStringCollection suburb = new AutoCompleteStringCollection();
        public AutoCompleteStringCollection homecountry = new AutoCompleteStringCollection();
        public AutoCompleteStringCollection postalsuburb = new AutoCompleteStringCollection();
        public AutoCompleteStringCollection postalstate = new AutoCompleteStringCollection();
        public AutoCompleteStringCollection postalcode = new AutoCompleteStringCollection();
        public AutoCompleteStringCollection homeaddress = new AutoCompleteStringCollection();
        public AutoCompleteStringCollection schoolcollection = new AutoCompleteStringCollection();
        public AutoCompleteStringCollection langspoken = new AutoCompleteStringCollection();
        public AutoCompleteStringCollection occupation = new AutoCompleteStringCollection();
        public AutoCompleteStringCollection AustralianResidence = new AutoCompleteStringCollection();
        public AutoCompleteStringCollection aborigine = new AutoCompleteStringCollection();
        public AutoCompleteStringCollection nonschooledu = new AutoCompleteStringCollection();
        public AutoCompleteStringCollection schooledu = new AutoCompleteStringCollection();
        public StringCollection EmpidTeacherEmpid = new StringCollection();
        public StringCollection EmpidAssistantTeacherEmpid = new StringCollection();
        public StringCollection EmpTeacherName = new StringCollection();
        public StringCollection EmpAssistantTeacherName = new StringCollection();

        public void AutoCompleteLoad()
        {
            //australianresidence
            AustralianResidence.Add("Australian");
            AustralianResidence.Add("New Zealand");
            AustralianResidence.Add("Other");

            //aborigine
            aborigine.Add("Aboriginal");
            aborigine.Add("Torres Strait Islander");
            aborigine.Add("Aboriginal and Torres Strait Islander");
            aborigine.Add("Neither");
            aborigine.Add("Not Stated");

            //nonschooledu
            nonschooledu.Add("Not Stated");
            nonschooledu.Add("Non School Education");
            nonschooledu.Add("Cert I-IV");
            nonschooledu.Add("Diploma/Advance Diploma");
            nonschooledu.Add("Bachelor");
            nonschooledu.Add("Master");
            nonschooledu.Add("PhD");

            //schooledu
            schooledu.Add("Not Stated");
            schooledu.Add("Year 9");
            schooledu.Add("Year 10");
            schooledu.Add("Year 11");
            schooledu.Add("Year 12");


            //occupation
            occupation.Add("Senior Manager");
            occupation.Add("Other Business Manager");
            occupation.Add("Tradesperson");
            occupation.Add("Clerk");
            occupation.Add("Sales & Service Staff");
            occupation.Add("Machine Operator");
            occupation.Add("Not in Paid Work");
            occupation.Add("Not Stated");
            occupation.Add("Others");
            occupation.Add("Housewife");


            //homestate
            HomeState.Add("Aceh");
            HomeState.Add("Sumatera Utara");
            HomeState.Add("Sumatra Barat");
            HomeState.Add("Riau");
            HomeState.Add("Kepulauan Riau");
            HomeState.Add("Jambi");
            HomeState.Add("Bengkulu");
            HomeState.Add("Sumatera Selatan");
            HomeState.Add("Kepulauan Bangka Belitung");
            HomeState.Add("Lampung");
            HomeState.Add("Banten");
            HomeState.Add("Jawa Barat");
            HomeState.Add("Jakarta");
            HomeState.Add("Jawa Tengah");
            HomeState.Add("Yogyakarta");
            HomeState.Add("Jawa Timur");
            HomeState.Add("Bali");
            HomeState.Add("Nusa Tenggara Barat");
            HomeState.Add("Nusa Tenggara Timur");
            HomeState.Add("Kalimantan Barat");
            HomeState.Add("Kalimantan Selatan");
            HomeState.Add("Kalimantan Tengah");
            HomeState.Add("Kalimantan Timur");
            HomeState.Add("Kalimantan Utara");
            HomeState.Add("Gorontalo");
            HomeState.Add("Sulawesi Barat");
            HomeState.Add("Sulawesi Selatan");
            HomeState.Add("Sulawesi Tengah");
            HomeState.Add("Sulawesi Tenggara");
            HomeState.Add("Sulawesi Utara");
            HomeState.Add("Maluku");
            HomeState.Add("Maluku Utara");
            HomeState.Add("Papua Barat");
            HomeState.Add("Papua");
            //suburb
            suburb.Add("Batam Kota");
            suburb.Add("Batu Aji");
            suburb.Add("Batu Ampar");
            suburb.Add("Belakang Padang");
            suburb.Add("Bengkong");
            suburb.Add("Bulang");
            suburb.Add("Galang");
            suburb.Add("Lubuk Baja");
            suburb.Add("Nongsa");
            suburb.Add("Sagulung");
            suburb.Add("Sei Beduk");
            suburb.Add("Sekupang");
            try
            {
                Db.open_connection();
                MySqlCommand cmd = new MySqlCommand("select * from student_data", Db.get_connection());
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    placeofbirth.Add(reader.GetString("pob"));
                    countryofbirth.Add(reader.GetString("cob"));
                    nationality.Add(reader.GetString("nationality"));
                    langspoken.Add(reader.GetString("langspoken"));
                    homeaddress.Add(reader.GetString("homeaddress"));
                    homecountry.Add(reader.GetString("nationality"));
                }
                reader.Close();
                cmd = new MySqlCommand("SELECT name_of_school FROM aisDb.student_previous_school_info", Db.get_connection());
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    schoolcollection.Add(reader.GetString("name_of_school"));
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Msg.Alert(ex.Message, frmAlert.AlertType.Error);
            }
        }

    }
}
