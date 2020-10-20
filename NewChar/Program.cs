using System;
using System.Globalization;
namespace NewChar
{
    class Program
    {
        static void Main(string[] args)
        {

        Console.Write("-----------------------\nIngrese su usuario:");
        string A = func_nombre();
        Console.WriteLine("\nEl usuario ingresado fue: " + A);

        Console.Write("\n----------------------\nIngrese su apellido: ");
        string B = func_nombre();
        Console.WriteLine("\nEl apellido ingresado fue: " + B);

        Console.Write("\n-----------------------\nIngrese su edad: ");
        string C = func_edad();
        Console.WriteLine("\nLa edad ingresada fue: " + C);

        try
        {
            NumberFormatInfo CO = new NumberFormatInfo();
            CO.NumberDecimalDigits = 2;
            Console.Write("\n-----------------------------------\nIngrese sus ahorros: ");
            decimal DE = decimal.Parse(func_ahorros());
            Console.WriteLine("");
            Console.WriteLine(DE.ToString("N", CO));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        Console.Write("\n-----------------------------------------\nPassword: ");
        string vPass = func_password();

        Console.Write("\nPassword Confirm: ");
        string vPassConfirm = func_password();

        if (vPass == vPassConfirm)
        {
            Console.WriteLine("\nLas contraseñas son iguales\n------------------");
        }
        else
        {
            Console.WriteLine("\nLas contraseñas son diferentes\n---------------");
        }
    }

    static string func_nombre()
    {

        char[] lista = new char[48];
        char captura;

        for (int x = 0; ;)
        {
            captura = Console.ReadKey(true).KeyChar;
            if (captura >= 65 && captura <= 90 || captura >= 97 && captura <= 122 || captura >= 48 && captura <= 57)
            {
                lista[x] = captura;
                ++x;
                Console.Write(captura);
            }
            if (captura == 13)
            {
                lista[x] = '\0';
                break;
            }
            if (captura == 8 && x >= 1)
            {
                Console.Write("\b \b");
                --x;
            }
        }
        return new string(lista);
    }

    static string func_edad()
    {
        char[] lista = new char[48];
        char captura;

        for (int x = 0; ;)
        {
            captura = Console.ReadKey(true).KeyChar;
            if (captura >= 48 && captura <= 57)
            {
                lista[x] = captura;
                ++x;
                Console.Write(captura);
            }
            if (captura == 13)
            {
                lista[x] = '\0';
                break;
            }
            if (captura == 8 && x >= 1)
            {
                Console.Write("\b \b");
                --x;
            }
        }
        return new string(lista);
    }

    static string func_ahorros()
    {

        char[] lista = new char[48];

        char captura;

        for (int x = 0; ;)
        {
            captura = Console.ReadKey(true).KeyChar;

            if (captura >= 48 && captura <= 57 || captura == 46)
            {
                lista[x] = captura;
                ++x;
                Console.Write(captura);
            }
            if (captura == 13)
            {
                lista[x] = '\0';
                break;
            }
            if (captura == 8 && x >= 1)
            {
                Console.Write("\b \b");
                --x;
            }
        }
        return new string(lista);
    }

    static string func_password()
    {

        char[] lista = new char[48];
        char captura;

        for (int x = 0; ;)
        {
            captura = Console.ReadKey(true).KeyChar;
            if (captura >= 65 && captura <= 90 || captura >= 97 && captura <= 122 || captura >= 48 && captura <= 57 || captura >= 42 && captura <= 46)
            {
                lista[x] = captura;
                ++x;
                Console.Write("*");
            }
            if (captura == 13)
            {
                lista[x] = '\0';
                break;
            }
            if (captura == 8 && x >= 1)
            {
                Console.Write("\b \b");
                --x;
            }
        }
        return new string(lista);
    }
        }
    
}
