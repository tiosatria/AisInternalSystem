using AisInternalSystem.Module;
using AisInternalSystem.UserInterface.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.Pivot.Queryable.Filtering;

namespace AisInternalSystem.Controller
{
    public class Data
    {


        #region Enumeration

        #endregion

        #region Entities
        public static User user = new User();
        public static List<TaskContainer> TaskContainers = new List<TaskContainer>();
        public static List<TaskExpander> taskExpanders = new List<TaskExpander>();
        public static List<Teacher> teachersList = Teacher.GetTeacherList();
        public static List<Grade> grades = Grade.GradeList();
        public static List<Teacher> assistantTeacherList = Teacher.GetAssistantTeacherList();
        public static List<Teacher> AssignTeacherList = Teacher.GetCareTeacherList();
        public static List<Teacher> AssignAssTcList = Teacher.GetAssCareTeacherList();
        public static List<string> Religion = new List<string> { "CHRISTIAN", "MOESLEM", "CATHOLIC", "KONG HU CHU", "BUDDHIST", "HINDU" };
        public static List<string> DocumentTypeStudent = new List<string> { "Report Card", "Birth Certificate", "KITAS", "Photocopy Family Card (KK)", "Photocopy Parents ID (KTP)", "Passport Photo", "Passport", "Transfer Letter", "Other" };
        public static List<string> DocumentTypeEmployee = new List<string> { "Certificate", "Birth Certificate", "KITAS", "Photocopy Family Card (KK)", "ID Card (KTP)", "Passport Photo", "Passport", "Other", "CV" };
        public static List<string> EnglishProficiency = new List<string> { "VERY WELL", "WELL", "NOT WELL", "NOT AT ALL" };
        public static List<string> StudentStatus = new List<string> { "ACTIVE", "SUSPEND", "ON LEAVE", "DEFER", "TRANSFER", "GRADUATE" };
        public static AutoCompleteStringCollection LanguageSpoken = Query.GetAutoCompleteCollection("GetLangSpoken");
        public static AutoCompleteStringCollection AustralianResidence = new AutoCompleteStringCollection() {"Australian", "New Zealand", "Not Stated" };
        public static AutoCompleteStringCollection AustralianAborigine = new AutoCompleteStringCollection() { "Aboriginal", "Aboriginal and Torres Strait Islander", "Torres Strait Islander", "Neither", "Not Stated" };
        public static AutoCompleteStringCollection SchoolEducation = new AutoCompleteStringCollection() { "Not Stated", "YEAR 9", "YEAR 10", "YEAR 11", "YEAR 12"};
        public static AutoCompleteStringCollection NonSchoolEducation = new AutoCompleteStringCollection() {"Not Stated", "BACHELOR", "CERT I-IV", "MASTER", "DIPLOMA/ADVANCED DIPLOMA", "PhD", "Non school education" };
        public static AutoCompleteStringCollection Country = Query.GetAutoCompleteCollection("GetCountryOfBirth");
        public static AutoCompleteStringCollection State = Query.GetAutoCompleteCollection("GetHomeState");
        public static AutoCompleteStringCollection Nationality = Query.GetAutoCompleteCollection("GetNationality");
        public static AutoCompleteStringCollection PlaceOfBirth = Query.GetAutoCompleteCollection("GetPlaceOfBirth");
        public static AutoCompleteStringCollection Occupation = Query.GetAutoCompleteCollection("GetOccupation");
        #endregion

        #region Variable

        #endregion
        public Data()
        {
            
        }

    }
}
