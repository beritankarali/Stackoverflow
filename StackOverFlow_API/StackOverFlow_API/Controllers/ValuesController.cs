using StackOverFlow_API.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace StackOverFlow_API.Controllers
{

    [RoutePrefix("api")]
    public class ValuesController : ApiController
    {
        
        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        StackoverflowEntities dbcontext = new StackoverflowEntities();
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }
        // GET api/<controller>/5
        [Route("hede/{id}")]
        public string Get1(int id)
        {
            return id.ToString();
        }

        // POST api/<controller>C:\Users\Beritan Karali\Desktop\StackOverFlow_API\StackOverFlow_API\Controllers\ValuesController.cs
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        [Route("user/login")] 
        [HttpPost]
        public LoginResponse Login([FromBody]LoginRequest data)
        {
            var result = new LoginResponse() { Status = false };

            var user = dbcontext.Kullanici.Where(x => x.KullaniciAd == data.UserName && x.Sifre == data.Password).FirstOrDefault();


            if (user != null)
            {
                result.Status = true;
                result.Token = Base64Encode(user.KullaniciID.ToString());
            }


            return result;
        }

        [Route("user/add")]
        [HttpPost]
        public UserResponse AddUser([FromBody]UserRequest data)
        {
            // data validation yapılmadı. FE taradından düzgün geleceği varsayıldı.
            var result = new UserResponse() { Status = false };

            var userList = dbcontext.Kullanici.ToList();


            var userNameCheck = userList.Where(a => a.KullaniciAd == data.KullaniciAd).FirstOrDefault();
            var emailCheck = userList.Where(a => a.Mail == data.Mail).FirstOrDefault();

            if (userNameCheck == null && emailCheck == null)
            {
                try
                {
                    Kullanici dbToSave = new Kullanici()
                    {
                        KullaniciAd = data.KullaniciAd,
                        Sifre = data.Sifre,
                        Isim = data.Isim,
                        Mail = data.Mail,
                        Soyisim = data.Soyisim
                    };

                    dbcontext.Kullanici.Add(dbToSave);
                    dbcontext.SaveChanges();
                    result.Status = true;

                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }


            }
            else if (userNameCheck != null)
            {
                result.Message = "Bu username ile daha önce kayıt gerçekleştirilmiş";
            }
            else if (emailCheck != null)
            {
                result.Message = "Bu email ile daha önce kayıt gerçekleştirilmiş";
            }
            return result;
        }

        [Route("user/delete/{token}")]
        [HttpDelete]
        public bool DeleteUser(string token)
        {
            var result = false;
            var kid = Convert.ToInt32(token);
            var user = dbcontext.Kullanici.Where(a => a.KullaniciID == kid).FirstOrDefault();

            if (user != null)
            {
                dbcontext.Kullanici.Remove(user);
                dbcontext.SaveChanges();
                result = true;
            }

            return result;
        }
        [Route("user/addQuestion")]
        [HttpPost]
        public string AddQuestion([FromBody]QuestionRequest data)
        {
            // data validation yapılmadı. FE taradından düzgün geleceği varsayıldı.
            //var result = new UserResponse() { Status = false };

            var questionList = dbcontext.Soru.ToList();


            try
            {
                Soru dbToSave = new Soru()
                {
                    Baslik = data.Baslik,
                    KullaniciID = Convert.ToInt32(Base64Decode(data.KullaniciID)),
                    Icerik = data.Icerik
                };

                dbcontext.Soru.Add(dbToSave);
                dbcontext.SaveChanges();
                //result.Status = true;

                return "Soru Ekleme Basarili";
            }
            catch (Exception ex)
            {
                return "Soru Ekleme Basarisiz" + ex.Message;
            }
        }

        [Route("user/deleteQuestion/{qID}")]
        [HttpDelete]
        public bool DeleteQuestion(string qID)
        {
            var result = false;
            var kid = Convert.ToInt32(qID);
            var question = dbcontext.Soru.Where(a => a.SoruID == kid).FirstOrDefault();

            if (question != null)
            {                
                dbcontext.SoruSil(kid);
                result = true;
            }

            return result;
        }
        [Route("user/addAnswer")]
        [HttpPost]
        public string AddAnswer([FromBody]AnswerRequest data)
        {
            // data validation yapılmadı. FE taradından düzgün geleceği varsayıldı.
            //var result = new UserResponse() { Status = false };


            var r = new Random();

            try
            {
                Cevap dbToSave = new Cevap()
                {
                    CevapIcerik = data.CevapIcerik,
                    SoruID = data.SoruID
                };

                dbcontext.Cevap.Add(dbToSave);
                dbcontext.SaveChanges();
                //result.Status = true;

                return "Cevap Ekleme Basarili";
            }
            catch (Exception ex)
            {
                return "Cevap Ekleme Basarisiz" + ex.Message;
            }
        }
        [Route("user/deleteAnswer/{aID}")]
        [HttpDelete]
        public bool DeleteAnswer(string aID)
        {
            var result = false;
            var kid = Convert.ToInt32(aID);
            var answer = dbcontext.Cevap.Where(a => a.CevapID == kid).FirstOrDefault();

            if (answer != null)
            {
                dbcontext.CevapSil(kid);
                result = true;
            }

            return result;
        }
        [Route("user/ListQuestion")]
        [HttpGet]
        public List<QuestionResponse> ListQuestion()
        {
            List<QuestionResponse> liste = new List<QuestionResponse>();
            var veriler = dbcontext.Soru.Include("Cevap").ToList();
            foreach (var item in veriler)
            {
                liste.Add(QuestionResponse.Map(item));
            }
            return liste;

        }

        [Route("user/ListAnswer")]
        [HttpGet]
        public List<AnswerResponse> ListAnswer()
        {
            List<AnswerResponse> liste = new List<AnswerResponse>();
            var veriler = dbcontext.Cevap.ToList();
            foreach (var item in veriler)
            {
                liste.Add(AnswerResponse.Map(item));
            }
            return liste;

        }


    }
}





