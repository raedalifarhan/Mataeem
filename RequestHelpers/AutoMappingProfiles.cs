using AutoMapper;
using Mataeem.DTOs;
using Mataeem.Models;

namespace Mataeem.RequestHelpers
{
    public class AutoMappingProfiles : Profile
    {
        public AutoMappingProfiles()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(dest => dest.IsOpen, opt => opt.MapFrom(src => IsRestaurantOpenNow(src.OpeningHours)));

            CreateMap<RestaurantSaveDto, Restaurant>();
        }

        private static bool IsRestaurantOpenNow(IList<BusinessHours>? openingHours)
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
                if (hours.DayOfWeek == currentDay)
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