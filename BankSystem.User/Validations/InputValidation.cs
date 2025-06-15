using System.Globalization;

namespace BankSystem.Validations;
public static class InputValidation
{
    public static bool IsNumberBetween(short number, short from, short to)
    {
        return number >= from && number <= to;
    }

    public static bool IsNumberBetween(int number, int from, int to)
    {
        return number >= from && number <= to;
    }

    public static bool IsNumberBetween(double number, double from, double to)
    {
        return number >= from && number <= to;
    }

    public static bool IsDateBetween(DateTime date, DateTime from, DateTime to)
    {
        if ((date > from || date == from) && (date < to || date == to))
        {
            return true;
        }

        if ((date > to || date == to) && (date < from || date == from))
        {
            return true;
        }

        return false;
    }

    public static int ReadIntNumber(string errorMessage = "Invalid Number, Enter again\n")
    {
        int number;
        while (!int.TryParse(Console.ReadLine(), out number))
        {
            Console.Write(errorMessage);
        }
        return number;
    }

    public static int ReadIntNumberBetween(int from, int to, string errorMessage = "Number is not within range, Enter again:\n")
    {
        int number = ReadIntNumber();
        while (!IsNumberBetween(number, from, to))
        {
            Console.Write(errorMessage);
            number = ReadIntNumber();
        }
        return number;
    }

    public static float ReadFloatNumber(string errorMessage = "Invalid Number, Enter again\n")
    {
        float number;
        while (!float.TryParse(Console.ReadLine(), out number))
        {
            Console.Write(errorMessage);
        }
        return number;
    }

    public static float ReadFloatNumberBetween(float from, float to, string errorMessage = "Number is not within range, Enter again:\n")
    {
        float number = ReadFloatNumber();
        while (!IsNumberBetween(number, from, to))
        {
            Console.Write(errorMessage);
            number = ReadFloatNumber();
        }
        return number;
    }

    public static double ReadDoubleNumber(string errorMessage = "Invalid Number, Enter again\n")
    {
        double number;
        while (!double.TryParse(Console.ReadLine(), out number))
        {
            Console.Write(errorMessage);
        }
        return number;
    }

    public static double ReadDoubleNumberBetween(double from, double to, string errorMessage = "Number is not within range, Enter again:\n")
    {
        double number = ReadDoubleNumber();
        while (!IsNumberBetween(number, from, to))
        {
            Console.Write(errorMessage);
            number = ReadDoubleNumber();
        }
        return number;
    }

    public static bool IsValidDate(DateTime date)
    {
        try
        {
            var testDate = new DateTime(date.Year, date.Month, date.Day);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static string ReadString()
    {
        return Console.ReadLine() ?? string.Empty;
    }

    public static DateTime ReadDate(string prompt = "Enter a date (MM/dd/yyyy): ",
                                  string errorMessage = "Invalid date format. Please try again.")
    {
        DateTime date;
        Console.Write(prompt);
        while (!DateTime.TryParseExact(Console.ReadLine(),
                                     "MM/dd/yyyy",
                                     CultureInfo.InvariantCulture,
                                     DateTimeStyles.None,
                                     out date))
        {
            Console.Write(errorMessage + " " + prompt);
        }
        return date;
    }
}
