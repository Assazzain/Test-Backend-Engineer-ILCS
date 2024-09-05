using SoalPemrogramanDasar.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoalPemrogramanDasar.Data
{
    public class DataHasil
    {
        public List<HasilModel> LoadHasil()
        {
            List<HasilModel> listResult = new List<HasilModel>();

            HasilModel Andi = new HasilModel
            {
                Index = 1,
                Nama = "Andi",
                Soal1 = "BENAR",
                Soal2 = "SALAH",
                Soal3 = "BENAR",
                Soal4 = "BENAR",
                Soal5 = "SALAH"
            };

            HasilModel Maya = new HasilModel
            {
                Index = 2,
                Nama = "Maya",
                Soal1 = "SALAH",
                Soal2 = "SALAH",
                Soal3 = "SALAH",
                Soal4 = "BENAR",
                Soal5 = "BENAR"
            };

            HasilModel Budi = new HasilModel
            {
                Index = 3,
                Nama = "Budi",
                Soal1 = "BENAR",
                Soal2 = "SALAH",
                Soal3 = "BENAR",
                Soal4 = "SALAH",
                Soal5 = "BENAR"
            };

            HasilModel Asih = new HasilModel
            {
                Index = 4,
                Nama = "Asih",
                Soal1 = "BENAR",
                Soal2 = "BENAR",
                Soal3 = "BENAR",
                Soal4 = "BENAR",
                Soal5 = "SALAH"
            };

            HasilModel Raja = new HasilModel
            {
                Index = 5,
                Nama = "Raja",
                Soal1 = "SALAH",
                Soal2 = "SALAH",
                Soal3 = "BENAR",
                Soal4 = "BENAR",
                Soal5 = "BENAR"
            };

            listResult.Add(Andi);
            listResult.Add(Maya);
            listResult.Add(Budi);
            listResult.Add(Asih);
            listResult.Add(Raja);

            return listResult;
        }
    }
}
