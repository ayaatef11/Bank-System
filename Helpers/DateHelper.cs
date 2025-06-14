using System;
using System.Globalization;

namespace BankSystem.Helpers
{
    public class DateHelper
    {
        private short _day = 1;
        private short _month = 1;
        private short _year = 1900;

        public short Day
        {
            get => _day;
            set => _day = value;
        }

        public short Month
        {
            get => _month;
            set => _month = value;
        }

        public short Year
        {
            get => _year;
            set => _year = value;
        }

        public void Print()
        {
            Console.WriteLine(DateToString());
        }

        public static DateHelper GetSystemDate()
        {
            DateTime now = DateTime.Now;
            return new DateHelper
            {
                Day = (short)now.Day,
                Month = (short)now.Month,
                Year = (short)now.Year
            };
        }

        public static bool IsValidDate(DateHelper date)
        {
            if (date.Day < 1 || date.Day > 31)
                return false;

            if (date.Month < 1 || date.Month > 12)
                return false;

            if (date.Month == 2)
            {
                if (IsLeapYear(date.Year))
                {
                    if (date.Day > 29)
                        return false;
                }
                else
                {
                    if (date.Day > 28)
                        return false;
                }
            }

            short daysInMonth = NumberOfDaysInAMonth(date.Month, date.Year);
            return date.Day <= daysInMonth;
        }

        public bool IsValid()
        {
            return IsValidDate(this);
        }

        public static string DateToString(DateHelper date)
        {
            return $"{date.Day}/{date.Month}/{date.Year}";
        }

        public string DateToString()
        {
            return DateToString(this);
        }

        public static bool IsLeapYear(short year)
        {
            return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        }

        public bool IsLeapYear()
        {
            return IsLeapYear(_year);
        }

        public static short NumberOfDaysInAYear(short year)
        {
            return IsLeapYear(year) ? (short)366 : (short)365;
        }

        public short NumberOfDaysInAYear()
        {
            return NumberOfDaysInAYear(_year);
        }

        public static short NumberOfHoursInAYear(short year)
        {
            return (short)(NumberOfDaysInAYear(year) * 24);
        }

        public short NumberOfHoursInAYear()
        {
            return NumberOfHoursInAYear(_year);
        }

        public static int NumberOfMinutesInAYear(short year)
        {
            return NumberOfHoursInAYear(year) * 60;
        }

        public int NumberOfMinutesInAYear()
        {
            return NumberOfMinutesInAYear(_year);
        }

        public static int NumberOfSecondsInAYear(short year)
        {
            return NumberOfMinutesInAYear(year) * 60;
        }

        public int NumberOfSecondsInAYear()
        {
            return NumberOfSecondsInAYear(_year);
        }

        public static short NumberOfDaysInAMonth(short month, short year)
        {
            if (month < 1 || month > 12)
                return 0;

            return month == 2
                ? (IsLeapYear(year) ? (short)29 : (short)28)
                : (short)DateTime.DaysInMonth(year, month);
        }

        public short NumberOfDaysInAMonth()
        {
            return NumberOfDaysInAMonth(_month, _year);
        }

        public static short NumberOfHoursInAMonth(short month, short year)
        {
            return (short)(NumberOfDaysInAMonth(month, year) * 24);
        }

        public short NumberOfHoursInAMonth()
        {
            return NumberOfHoursInAMonth(_month, _year);
        }

        public static int NumberOfMinutesInAMonth(short month, short year)
        {
            return NumberOfHoursInAMonth(month, year) * 60;
        }

        public int NumberOfMinutesInAMonth()
        {
            return NumberOfMinutesInAMonth(_month, _year);
        }

        public static int NumberOfSecondsInAMonth(short month, short year)
        {
            return NumberOfMinutesInAMonth(month, year) * 60;
        }

        public int NumberOfSecondsInAMonth()
        {
            return NumberOfSecondsInAMonth(_month, _year);
        }

        public static short DayOfWeekOrder(short day, short month, short year)
        {
            DateTime date = new DateTime(year, month, day);
            return (short)date.DayOfWeek;
        }

        public short DayOfWeekOrder()
        {
            return DayOfWeekOrder(_day, _month, _year);
        }

        public static string DayShortName(short dayOfWeekOrder)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName((DayOfWeek)dayOfWeekOrder);
        }

        public static string DayShortName(short day, short month, short year)
        {
            return DayShortName(DayOfWeekOrder(day, month, year));
        }

        public string DayShortName()
        {
            return DayShortName(_day, _month, _year);
        }

        public static string MonthShortName(short monthNumber)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(monthNumber);
        }

        public string MonthShortName()
        {
            return MonthShortName(_month);
        }

        public static short DaysFromTheBeginningOfTheYear(short day, short month, short year)
        {
            short totalDays = 0;
            for (short i = 1; i <= month - 1; i++)
            {
                totalDays += NumberOfDaysInAMonth(i, year);
            }
            return (short)(totalDays + day);
        }

        public short DaysFromTheBeginningOfTheYear()
        {
            return DaysFromTheBeginningOfTheYear(_day, _month, _year);
        }

        public static DateHelper GetDateFromDayOrderInYear(short dateOrderInYear, short year)
        {
            DateHelper date = new DateHelper { Year = year, Month = 1 };
            short remainingDays = dateOrderInYear;
            short monthDays;

            while (true)
            {
                monthDays = NumberOfDaysInAMonth(date.Month, year);
                if (remainingDays > monthDays)
                {
                    remainingDays -= monthDays;
                    date.Month++;
                }
                else
                {
                    date.Day = remainingDays;
                    break;
                }
            }
            return date;
        }

        public void AddDays(short days)
        {
            DateTime date = new DateTime(_year, _month, _day).AddDays(days);
            _day = (short)date.Day;
            _month = (short)date.Month;
            _year = (short)date.Year;
        }

        public static bool IsDate1BeforeDate2(DateHelper date1, DateHelper date2)
        {
            return new DateTime(date1.Year, date1.Month, date1.Day) <
                   new DateTime(date2.Year, date2.Month, date2.Day);
        }

        public bool IsDateBeforeDate2(DateHelper date2)
        {
            return IsDate1BeforeDate2(this, date2);
        }

        public static bool IsDate1EqualDate2(DateHelper date1, DateHelper date2)
        {
            return date1.Day == date2.Day &&
                   date1.Month == date2.Month &&
                   date1.Year == date2.Year;
        }

        public bool IsDateEqualDate2(DateHelper date2)
        {
            return IsDate1EqualDate2(this, date2);
        }

        public static bool IsLastDayInMonth(DateHelper date)
        {
            return date.Day == NumberOfDaysInAMonth(date.Month, date.Year);
        }

        public bool IsLastDayInMonth()
        {
            return IsLastDayInMonth(this);
        }

        public static bool IsLastMonthInYear(short month)
        {
            return month == 12;
        }

        public static DateHelper AddOneDay(DateHelper date)
        {
            DateTime dt = new DateTime(date.Year, date.Month, date.Day).AddDays(1);
            return new DateHelper
            {
                Day = (short)dt.Day,
                Month = (short)dt.Month,
                Year = (short)dt.Year
            };
        }

        public void AddOneDay()
        {
            DateTime dt = new DateTime(_year, _month, _day).AddDays(1);
            _day = (short)dt.Day;
            _month = (short)dt.Month;
            _year = (short)dt.Year;
        }

        public static void SwapDates(ref DateHelper date1, ref DateHelper date2)
        {
            DateHelper temp = date1;
            date1 = date2;
            date2 = temp;
        }

        public static int GetDifferenceInDays(DateHelper date1, DateHelper date2, bool includeEndDay = false)
        {
            DateTime dt1 = new DateTime(date1.Year, date1.Month, date1.Day);
            DateTime dt2 = new DateTime(date2.Year, date2.Month, date2.Day);

            TimeSpan difference = dt2 - dt1;
            return includeEndDay ? difference.Days + 1 : difference.Days;
        }

        public int GetDifferenceInDays(DateHelper date2, bool includeEndDay = false)
        {
            return GetDifferenceInDays(this, date2, includeEndDay);
        }

        public static short CalculateMyAgeInDays(DateHelper dateOfBirth)
        {
            return (short)GetDifferenceInDays(dateOfBirth, GetSystemDate(), true);
        }

        // Additional methods would follow the same pattern of conversion
        // Using DateTime where possible for reliability and simplicity

        public enum DateCompare { Before = -1, Equal = 0, After = 1 };

        public static DateCompare CompareDates(DateHelper date1, DateHelper date2)
        {
            if (IsDate1BeforeDate2(date1, date2))
                return DateCompare.Before;

            if (IsDate1EqualDate2(date1, date2))
                return DateCompare.Equal;

            return DateCompare.After;
        }

        public DateCompare CompareDates(DateHelper date2)
        {
            return CompareDates(this, date2);
        }
    }
}