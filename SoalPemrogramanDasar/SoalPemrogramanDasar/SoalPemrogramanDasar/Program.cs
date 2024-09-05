using SoalPemrogramanDasar.Service;
using System;

namespace SoalPemrogramanDasar
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Soal Pemrograman Dasar");
            Console.WriteLine("");

            DisplayService displayService = new DisplayService();

            displayService.DisplayDataHasil();
            Console.WriteLine("");

            displayService.DisplayDataSkor();
        }
    }
}
