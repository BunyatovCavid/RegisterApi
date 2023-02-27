using RegisterApi.Crud;
using System.Collections.Generic;

namespace RegisterApi.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string E_mail { get; set; }
        public string Password { get; set; }
        public string RPassword { get; set; }
        public string Repost { get; set; }

    }
}
