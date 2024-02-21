using Mataeem.Models;

namespace Mataeem.Lib
{
    public class RestaurantHelper
    {
        public static bool IsRestaurantOpenNow(IList<BusinessHours>? openingHours)
        {
            if (openingHours == null)
                return true;

            // حساب الوقت الحالي
            var currentTime = DateTime.Now;

            // حساب اليوم الحالي بناءً على التوقيت المحلي
            var currentDay = currentTime.DayOfWeek;

            // التحقق مما إذا كان المطعم مفتوحًا في الوقت الحالي ويوم الأسبوع الحالي
            foreach (var hours in openingHours)
            {
                if (hours.DayOfWeek.ToString() == currentDay.ToString())
                {
                    // التحقق من الوقت
                    if (currentTime.TimeOfDay >= hours.OpenTime && currentTime.TimeOfDay <= hours.CloseTime)
                    {
                        return true; // المطعم مفتوح في الوقت الحالي
                    }
                }
            }

            return false; // المطعم مغلق في الوقت الحالي
        }
    }
}
