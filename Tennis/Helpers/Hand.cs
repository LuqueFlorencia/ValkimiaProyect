using System.ComponentModel;

namespace Tennis.Helpers
{
    public enum Hand
    {
        [Description("Right")]
        Right = 1,
        [Description("Left")]
        Left = 2
    }
    public static class HandExtension
    {
        public static string GetDescription(this Hand hand)
        {
            var type = hand.GetType();
            var memberInfo = type.GetMember(hand.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                var attrib = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrib != null && attrib.Length > 0)
                    return ((DescriptionAttribute)attrib[0]).Description;
            }
            return hand.ToString();
        }
    }
}
