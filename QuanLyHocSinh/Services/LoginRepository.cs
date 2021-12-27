using System;
using System.Threading.Tasks;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using QuanLyHocSinh.Models;
using MailKit.Net.Smtp;
using QuanLyHocSinh.Services.Interface;

namespace QuanLyHocSinh.Services
{
    public class LoginRepository : BaseRepository<Account>, ILoginRepository
    {
        private IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
        public LoginRepository(IDbContextFactory<QuanLyHocSinhContext> context) : base(context)
        {
            _contextFactory = context;
        }

        public async Task<Account> GetUser(string username, string password)
        {
            var context = _contextFactory.CreateDbContext();
            return await context.Accounts.FirstOrDefaultAsync(x => x.UserName == username && x.Password == password);
        }

        public async Task<Account> GetByUserName(string userName)
        {
            var context = _contextFactory.CreateDbContext();
            return await context.Accounts.FirstOrDefaultAsync(x => x.UserName.Equals(userName));
          
        }

        public async Task<(Student, Teacher)> GetUserOrTeacher(Guid id)
        {
            var context = _contextFactory.CreateDbContext(); 
            var student =await context.Students.FirstOrDefaultAsync(x=>x.Id== id);
            var teacher = await context.Teachers.FirstOrDefaultAsync(x => x.Id == id);

            return (student, teacher);
        }

        public async Task<string> ChangePassword(Guid id, string oldPass, string newPass)
        {
            var context = _contextFactory.CreateDbContext();
            var std = await context.Accounts.FirstOrDefaultAsync(x => x.Password == oldPass && x.Id == id);
            if (std == null)
            {
                return "Password Incorect";
            }

            std.Password = newPass;
            context.Accounts.Update(std);
            await context.SaveChangesAsync();

            var student = await context.Students.FirstOrDefaultAsync(x => x.Id == id);
            var teacher = await context.Teachers.FirstOrDefaultAsync(x => x.Id == id);
            string name = student!=null?student.Name:teacher.Name;
            string mail = student != null ? student.Email : teacher.Email;

            
            SendMail(name, mail);
            return "Change Success";
        }
        void SendMail(string name, string email)
        {
            MimeMessage message = new MimeMessage();
            using (var client = new SmtpClient())
            {
                message.From.Add(new MailboxAddress("Thông báo", "ntanh.hutech@gmail.com"));
                message.To.Add(new MailboxAddress(name, email));
                message.Subject = "PASSWORD CHANGED";

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "<p>Your password has been changed on " + DateTime.UtcNow.AddHours(7).ToString("dddd, MMMM d, yyyy hh:mm:ss") + "</p>";
                message.Body = bodyBuilder.ToMessageBody();

                client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);

                client.Authenticate("ntanh.hutech@gmail.com", "olmwycchdvjacger");

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
