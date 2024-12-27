namespace Restoran_Otomasyonu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RestoranOtomasyonu restoran = new RestoranOtomasyonu();
            restoran.Calistir();
        }
    }
    class RestoranOtomasyonu
    {
        private List<List<string>> masalar = new List<List<string>>();


        private Dictionary<string, double> menu = new Dictionary<string, double>
    {
            {"Mercimek",50 },
            {"Ezogelin",45 },
            {"Domates",55.5},
            {"Tavuk Suyu",50 }
    };
        private List<string> siparisler = new List<string>();

        public RestoranOtomasyonu()
        {
            for (int i = 0; i < 5; i++)
            {
                masalar.Add(new List<string>());
            }
        }

        private void MenuListele()
        {
            Console.WriteLine("\nMenü:");
            foreach (var item in menu)
            {
                Console.WriteLine($"{item.Key}:{item.Value} TL");

            }
        }

        private void MasaDurumu()
        {
            Console.WriteLine("\nMasa Durumu:");
            for (int i = 0; i < masalar.Count; i++)
            {
                Console.WriteLine($"Masa {i + 1}: {(masalar[i].Count > 0 ? "Dolu" : "Boş")}");
            }
        }

        private int YeniMusteri()
        {
            MasaDurumu();
            for (int i = 0; i < masalar.Count; i++)
            {
                if (masalar[i].Count == 0)
                {
                    Console.WriteLine($"Müşteriler Masa {i + 1}'e yerleşti.");
                    return i;
                }
            }
            Console.WriteLine("Tüm masalar dolu!");
            return -1;
        }

        private void SiparisAl(int masaNo)
        {
            Console.WriteLine($"Masa {masaNo + 1} için sipariş alınıyor...");
            while (true)
            {
                MenuListele();
                Console.Write("Sipariş seçin:" +
                    "");
                string siparis = Console.ReadLine();

                if (menu.ContainsKey(siparis))
                {
                    masalar[masaNo].Add(siparis);
                    siparisler.Add(siparis);
                    Console.WriteLine($"{siparis} siparişi alındı.");
                }
                else
                {
                    Console.WriteLine("Hatalı seçim, tekrar deneyin.");
                    continue;
                }

                Console.Write("Başka arzunuz var mı? (Evet/Hayır): ");
                string devam = Console.ReadLine().ToLower();
                if (devam != "evet")
                {
                    break;
                }
            }
        }

        private void HesapAl(int masaNo)
        {
            double toplam = 0;
            foreach (string siparis in masalar[masaNo])
            {
                toplam += menu[siparis];
            }
            Console.WriteLine($"Masa {masaNo + 1} için hesap: {toplam} TL");
            masalar[masaNo].Clear();
        }

        private void ZRaporu()
        {
            Console.WriteLine("\n******** Z RAPORU ********");
            Dictionary<string, int> urunSayaci = new Dictionary<string, int>();
            foreach (string siparis in siparisler)
            {
                if (urunSayaci.ContainsKey(siparis))
                {
                    urunSayaci[siparis]++;
                }
                else
                {
                    urunSayaci[siparis] = 1;
                }
            }

            double toplamGelir = 0;
            foreach (string siparis in siparisler)
            {
                toplamGelir += menu[siparis];
            }
            Console.WriteLine($"Toplam Gelir: {toplamGelir} TL");
        }

        public void Calistir()
        {
            Console.WriteLine("******** RESTAURANT OTOMASYONU ********");
            while (true)
            {
                Console.Write("\nYeni müşteri var mı? (Evet/Hesap Al/Z Raporu/Çıkış): ");
                string secim = Console.ReadLine().ToLower();
                if (secim == "evet")
                {
                    int masaNo = YeniMusteri();
                    if (masaNo != -1)
                    {
                        SiparisAl(masaNo);
                    }
                }
                else if (secim == "hesap al")
                {
                    MasaDurumu();
                    Console.Write("Hangi masanın hesabı alınacak? (1-5): ");
                    int masaNo = int.Parse(Console.ReadLine()) - 1;
                    if (masaNo >= 0 && masaNo < masalar.Count && masalar[masaNo].Count > 0)
                    {
                        HesapAl(masaNo);
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz masa numarası veya masa boş.");
                    }
                }
                else if (secim == "z raporu")
                {
                    ZRaporu();
                }
                else if (secim == "çıkış")
                {
                    Console.WriteLine("Çıkış yapılıyor...");
                    break;
                }
                else
                {
                    Console.WriteLine("Geçersiz seçim, tekrar deneyin.");
                }
            }
        }


    }
}
