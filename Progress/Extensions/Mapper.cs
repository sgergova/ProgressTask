using Client;
using Progress.Models;
using System.ComponentModel.DataAnnotations;

namespace Progress.Extensions
{
    public static class Mapper
    {
        public static ClientCredentials GetClient(this ModelVM model)
        {
            var client = new ClientCredentials
            {
                Email = model.Email
            };

            return client;
        }
    }
}