using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazart.EmailService.EmailService.IEmailService
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync(string body, string email);
    }
}