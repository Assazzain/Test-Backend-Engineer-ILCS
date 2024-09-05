using SoalPemrogramanDasar.Data;
using SoalPemrogramanDasar.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoalPemrogramanDasar.Service
{
    public class SkorService
    {
        public List<SkorModel> ListSkor()
        {
            List<SkorModel> listSkor = new List<SkorModel>();

            DataHasil dataHasil = new DataHasil();

            List<HasilModel> listResult = dataHasil.LoadHasil();

            foreach (var item in listResult)
            {
                int totalSkor = 0;
                if (item.Soal1 == "BENAR")
                {
                    totalSkor += 10;
                }
                if (item.Soal2 == "BENAR")
                {
                    totalSkor += 30;
                }
                if (item.Soal3 == "BENAR")
                {
                    totalSkor += 20;
                }
                if (item.Soal4 == "BENAR")
                {
                    totalSkor += 20;
                }
                if (item.Soal5 == "BENAR")
                {
                    totalSkor += 20;
                }

                SkorModel dataSkor = new SkorModel
                {

                    Index = item.Index,
                    Nama = item.Nama,
                    Skor = totalSkor
                };

                listSkor.Add(dataSkor);
            }

            return listSkor;
        }
    }
}
