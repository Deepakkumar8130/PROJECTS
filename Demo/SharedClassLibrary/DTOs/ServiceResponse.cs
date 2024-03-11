using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedClassLibrary.DTOs
{
    public class ServiceResponse
    {
        public record class GeneralResposne(bool Flag, string Message);
        public record class LoginResposne(bool Flag,string Token, string Message);
    }
}
