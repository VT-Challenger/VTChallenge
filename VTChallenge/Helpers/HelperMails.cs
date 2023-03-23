using System.Net.Mail;
using System.Net;

namespace VTChallenge.Helpers {
    public class HelperMails {
        private IConfiguration configuration;

        public HelperMails(IConfiguration configuration) {
            this.configuration = configuration;
        }

        private MailMessage ConfigureMailMessage(string para, string asunto, string mensaje) {
            MailMessage mailMessage = new MailMessage();
            string email = this.configuration.GetValue<string>("MailSettings:Credentials:User");
            mailMessage.From = new MailAddress(email);
            mailMessage.To.Add(new MailAddress(para));
            mailMessage.Subject = asunto;
            mailMessage.Body = mensaje;
            mailMessage.IsBodyHtml = true;

            return mailMessage;
        }

        private SmtpClient ConfigureSmtpClient() {
            string user = this.configuration.GetValue<string>("MailSettings:Credentials:User");
            string password = this.configuration.GetValue<string>("MailSettings:Credentials:Password");
            string host = this.configuration.GetValue<string>("MailSettings:Smtp:Host");
            int port = this.configuration.GetValue<int>("MailSettings:Smtp:Port");
            bool enableSSL = this.configuration.GetValue<bool>("MailSettings:Smtp:EnableSSL");
            bool defaultCredentials = this.configuration.GetValue<bool>("MailSettings:Smtp:DefaultCredentials");

            SmtpClient client = new SmtpClient();
            client.Host = host;
            client.Port = port;
            client.EnableSsl = enableSSL;
            client.UseDefaultCredentials = defaultCredentials;

            NetworkCredential credentials = new NetworkCredential(user, password);
            client.Credentials = credentials;
            return client;
        }


        public async Task SendMailAsync(string para, string asunto, string mensaje) {
            MailMessage mail = this.ConfigureMailMessage(para, asunto, mensaje);
            SmtpClient client = this.ConfigureSmtpClient();
            await client.SendMailAsync(mail);
        }

        public string PlantillaInscriptionPlayer(string name, string nombreTorneo, string fecha, string server, string organizador) {
            string html = "<p>Estimado" + name  +",</p>" +
                           "<p>¡Gracias por unirse a nuestro torneo! Nos complace informarle que su registro ha sido confirmado y ahora forma parte del grupo de participantes del torneo <b>" + nombreTorneo + "</b></p>" +
                            "<p>El torneo se llevará a cabo el día" + fecha  + " y tendrá lugar en el siguiente link: " + server +" .Por favor, asegúrese de estar al menos 30 minutos antes de que comience para completar el proceso de registro.</p>" +
                            "<p>Le deseamos la mejor de las suertes en el torneo y esperamos que tenga una experiencia emocionante y satisfactoria. Si tiene alguna pregunta o inquietud, no dude en ponerse en contacto con nosotros.</p>" +
                            "<p>¡Gracias de nuevo por unirse a nosotros!</p>" +
                            "<p>Atentamente,<br>" + organizador + "</p>";
            return html;
        }

        public string PlantillaInscriptionOrg(string playerName, int inscripciones) {
            string html = "<p>El jugador: " + playerName + " se ha unido al torneo</p>" + 
                          "<p>Quedan " + inscripciones + " inscripciones</p>"; 
            return html;
        }

        public string PlantillaRemoveUserTournament() {
            string html = "Por decisión del organizador fuiste expulsado";
            return html;
        }
    }
}
