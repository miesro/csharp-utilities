using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Calculates current age
        /// </summary>
        public static int GetAge(this DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
