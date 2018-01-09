using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverFlow_API
{
    public class ListQuestionRequest
    {
        public List<Soru> SoruList = new List<Soru>();
        public List<Cevap> CevapList = new List<Cevap>();

    }
}