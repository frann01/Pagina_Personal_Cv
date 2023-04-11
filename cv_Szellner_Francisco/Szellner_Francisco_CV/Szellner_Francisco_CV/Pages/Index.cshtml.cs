using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System.Xml.Linq;

namespace Szellner_Francisco_CV.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string company { get; set; }
        public string message { get; set; }


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            
        }

        public void OnPost()
        {
            enviarForm();
        }


        public void enviarForm() 
        {
            String Servidor = "smtp.gmail.com";
            int Puerto = 587;
            String GmailUser = "franszellner@gmail.com";
            String GmailPassword = "zfmnbcnkngnbijop";

            MimeMessage mensaje = new();
            mensaje.From.Add(new MailboxAddress("Contacto Curriculum " + company, GmailUser));
            mensaje.To.Add(new MailboxAddress("Contacto Curriculum", GmailUser));
            mensaje.Subject = "Contacto Curriculum " + company;

            BodyBuilder CuerpoMensaje = new();
            CuerpoMensaje.TextBody = string.Format("Nombre: {0}" + Environment.NewLine +
                "Empresa: {1}"  + Environment.NewLine +
                "Telefono: {2}" + Environment.NewLine +
                "Email: {3}" + Environment.NewLine +
                "{4}", name, company, phone, email, message);

            mensaje.Body = CuerpoMensaje.ToMessageBody();

            SmtpClient clienteSmtp = new();
            clienteSmtp.CheckCertificateRevocation = false;
            clienteSmtp.Connect(Servidor, Puerto, MailKit.Security.SecureSocketOptions.StartTls);

            clienteSmtp.Authenticate(GmailUser, GmailPassword);
            clienteSmtp.Send(mensaje);
            clienteSmtp.Disconnect(true);

        }
    }
}