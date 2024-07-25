using System;
using Microsoft.Win32;

namespace RegistryExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("write a Registry");
            WriteRegistry();
            Console.WriteLine("Read a Registry");
            ReadRegistry();
        }

        static void WriteRegistry()
        {
            // 레지스트리 키 경로
            string subKey = @"Software\MyApp";
            // 레지스트리 키를 생성하거나 엽니다.
            RegistryKey key = Registry.CurrentUser.CreateSubKey(subKey);

            if (key != null)
            {
                // 레지스트리에 값 쓰기
                key.SetValue("Username", "JohnDoe");
                key.SetValue("UserAge", 30);

                // 키를 닫습니다.
                key.Close();

                Console.WriteLine("Registry values written successfully.");
            }
        }

        static void ReadRegistry()
        {
            // 레지스트리 키 경로
            string subKey = @"Software\MyApp";
            // 레지스트리 키를 엽니다.
            RegistryKey key = Registry.CurrentUser.OpenSubKey(subKey);

            if (key != null)
            {
                // 레지스트리에서 값 읽기
                object username = key.GetValue("Username");
                object userAge = key.GetValue("UserAge");

                Console.WriteLine($"Username: {username}");
                Console.WriteLine($"UserAge: {userAge}");

                // 키를 닫습니다.
                key.Close();
            }
            else
            {
                Console.WriteLine("Registry key not found.");
            }
        }

    }
}
