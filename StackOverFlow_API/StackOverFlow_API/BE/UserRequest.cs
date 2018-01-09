using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackOverFlow_API
{
    public class UserRequest
    {
        public string KullaniciAd { get; set; }
        public string Isim { get; set; }
        public string Soyisim { get; set; }
        public string Sifre { get; set; }
        public string Mail { get; set; }
    }
}