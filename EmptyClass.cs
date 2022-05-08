using System;
using System.Net;
using System.Net.Mail;

namespace MailTutorial
{
    public class EmptyClass
    {
        private static string _fromMail = "xx@gmail.com"; // почта с которой отправляют мейл
        private static string _password = "pass"; // пароль от почты 

        private static string _toMail = "xx@gmail.com"; // куда отправлять 

        private static int _needCode;
        private static string _title = "Введите код из сообщения";
        private static string _body;

        public static void Main(string[] arg)
        {
           
            GenerateCode();
        }

        public static void GenerateCode()
        {
            Random r = new Random();
            int randomCode = r.Next(100000, 999999);
            _needCode = randomCode;
            _body = "Рандомный код для вставки " + _needCode.ToString();
            SendMessage(_title, _body);
            RequestCode();
        }

        private static void RequestCode()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Впишите код отправленный в сообщении на почту");
                int enterCode = Convert.ToInt32(Console.ReadLine());
                if (enterCode == _needCode) Succeded() ;
                else GenerateCode();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }            
        }

        private static void SendMessage(string title, string message)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(_fromMail);
                    mail.To.Add(_toMail);
                    mail.Subject = title;
                    mail.Body = message;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Port = 587;
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        smtp.Credentials = new NetworkCredential(mail.From.Address, _password);
                        smtp.SendAsync(mail,null);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void Succeded()
        {
            //Создание базы данных, в неё вносится логин/пароль/почта пользователя
            Console.WriteLine("Всё работает");
        }
    }
}
