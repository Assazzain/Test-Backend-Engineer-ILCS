using SoalPemrogramanDasar.Data;
using SoalPemrogramanDasar.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoalPemrogramanDasar.Service
{
    public class DisplayService
    {
        public void DisplayDataHasil()
        {
            Console.WriteLine("Data Hasil Pengerjaan Soal");
            Console.WriteLine("Index\t Nama\t SOAL1\t SOAL2\t SOAL3\t SOAL4\t SOAL5\t");

            DataHasil dataHasil = new DataHasil();

            List<HasilModel> listResult = dataHasil.LoadHasil();

            foreach (var item in listResult)
            {
                Console.WriteLine($"{item.Index}\t {item.Nama}\t {item.Soal1}\t {item.Soal2}\t {item.Soal3}\t {item.Soal4}\t {item.Soal5}\t");
            }
        }

        public void DisplayDataSkor()
        {
            Console.WriteLine("Data Skor");
            Console.WriteLine("Index\t Nama\t Skor\t");

            SkorService skorService= new SkorService();

            List<SkorModel> listSkor = skorService.ListSkor();
            SortDescending(listSkor);

            foreach (var item in listSkor)
            {
                Console.WriteLine($"{item.Index}\t {item.Nama}\t {item.Skor}\t");
            }
        }

        static void SortDescending(List<SkorModel> listSkor)
        {
            int n = listSkor.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (listSkor[j].Skor < listSkor[j + 1].Skor)
                    {
                        SkorModel temp = listSkor[j];
                        listSkor[j] = listSkor[j + 1];
                        listSkor[j + 1] = temp;
                    }
                }
            }
        }
    }
}
