using Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ISendEmail
    {
         Task<HttpResponseMessage> SendEmailAsync(ClientCredentials model);
    }
}
