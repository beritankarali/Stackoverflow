using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverFlow_API.BE
{
    public class AnswerResponse
    {
        public int SoruID { get; set; }
        public int CevapID { get; set; }
        public string CevapIcerik { get; set; }
        public Nullable<int> Count { get; set; }

        public static AnswerResponse Map(Cevap c)
        {
            AnswerResponse ar = new AnswerResponse
            {
                CevapID = c.CevapID,
                CevapIcerik = c.CevapIcerik,
                Count = c.Count,
                SoruID = c.SoruID
            };
            return ar;
        }
    }
}