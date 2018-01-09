using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverFlow_API
{
    public class QuestionRequest
    {
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public string KullaniciID { get; set; }
    }
}

