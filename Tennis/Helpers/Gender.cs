using System.ComponentModel;

namespace Tennis.Helpers
{
    public enum Gender
    {
        [Description("Male")]
        Male = 1,
        [Description("Female")]
        Female = 2
    }
    public static class GenderExtension
    {
        public static string GetDescription(this Gender gender)
        {
            var type = gender.GetType();
            var memberInfo = type.GetMember(gender.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                var attrib = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrib != null && attrib.Length > 0)
                    return ((DescriptionAttribute)attrib[0]).Description;
            }
            return gender.ToString();
        }
    }
}
    