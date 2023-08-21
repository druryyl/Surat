using System.Reflection.Emit;

namespace Surat.Domain
{
    public class UserModel
    {
        public int id_user {get;set;}
        public string name {get;set;}
        public string no_identitas {get;set;}
        public string id_identitas {get;set;}
        public string alamat {get;set;}
        public string phoneNumber {get;set;}
        public string password {get;set;}
        public string verify {get;set;}
        public string level {get;set;}
        public DateTime createdAt { get; set; }
    }
}
