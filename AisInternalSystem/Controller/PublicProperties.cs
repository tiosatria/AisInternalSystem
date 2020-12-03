using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace AisInternalSystem.Controller
{
    public class PublicProperties
    {
        public PublicProperties()
        {

        }

        public static Color NavNormalColorButton = Color.White;
        public static Color NavNormalForeColor = Color.Black;
        public static Color NavFocusedColorButton = Color.Black;
        public static Color NavFocusedForeColor = Color.White;
        public static string DateFormat = "yyyy-MM-dd";
        public static string TimeStampFormat = "yyyy-MM-dd HH:mm:ss";
        public readonly static List<string> Gender = new List<string> {"MALE", "FEMALE" };
        public readonly static List<string> Origin = new List<string> { "LOCAL", "EXPAT" };
        public static List<string> Revise = new List<string> { "REVISED", "NOT REVISED" };
        public readonly static string YearNow = DateTime.Now.ToString("yyyy");
        public static List<int> DropYearPickFirst(int yearstart)
        {
            List<int> years = new List<int>();
                years.Add(yearstart);
                //init all 12 year
                for (int i = 0; i < 12; i++)
                {
                    yearstart++;
                    years.Add(yearstart);
                }
                return years;
        }
    }
}
