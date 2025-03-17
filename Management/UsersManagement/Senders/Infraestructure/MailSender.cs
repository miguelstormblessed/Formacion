using System.Net;
using System.Net.Mail;
using UsersManagement.Senders.Domain;
using UsersManagement.Shared.Senders.Domain.Exceptions;

namespace UsersManagement.Senders.Infraestructure;

public class MailSender : ISendRepository
{
    public void Send(Send send)
    {
        string from = "no-reply.jaltest-telematics@cojali.com";
        SmtpClient client = new SmtpClient
        {
            Host = "smtp.office365.com",
            Port = 587,
            UseDefaultCredentials = false,
            Credentials = new System.Net.NetworkCredential("no-reply.jaltest-telematics@cojali.com", "OaJegZm+Z;5>"),
            EnableSsl = true,
        };
        MailMessage email = new MailMessage
        {
            From = new MailAddress("no-reply.jaltest-telematics@cojali.com"), Subject = send.Subject.Subject, Body = send.Message.Message,
            IsBodyHtml = true,
        };
        email.To.Add(send.To.To);

        try
        {
            client.Send(email);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception caught in CreateTestMessage2(): {0}", ex.ToString());
        }
        
    }
}