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
        public static new List<string> Religion = new List<string> { "CHRISTIAN", "MOESLEM", "CATHOLIC", "KONG HU CHU", "BUDDHIST", "HINDU" };
        public static new List<string> DocumentTypeStudent = new List<string> { "Report Card", "Birth Certificate", "KITAS", "Photocopy Family Card (KK)", "Photocopy Parents ID (KTP)", "Passport Photo", "Passport", "Transfer Letter", "Other" };
        public static List<string> EnglishProficiency = new List<string> { "VERY WELL", "WELL", "NOT WELL", "NOT AT ALL" };
        public static List<string> StudentStatus = new List<string> { "ACTIVE", "SUSPEND", "ON LEAVE", "DEFER", "TRANSFER", "GRADUATE" };
        public static AutoCompleteStringCollection LanguageSpoken = Query.GetAutoCompleteCollection("GetLangSpoken");
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
