using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverFlow_API.BE
{
    public class QuestionResponse
    {
        public int SoruID { get; set; }
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public Nullable<int> Count { get; set; }
        public int KullaniciID { get; set; }
        public List<string> CevapList { get; set; }

        public static QuestionResponse Map(Soru s)
        {
            QuestionResponse qr = new QuestionResponse {
                Baslik = s.Baslik,
                Count = s.Count,
                Icerik = s.Icerik,
                KullaniciID = s.KullaniciID,
                SoruID = s.SoruID,
                CevapList = s.Cevap.Select(a=> a.CevapIcerik).ToList()
            };
            return qr;
        }
    }
}