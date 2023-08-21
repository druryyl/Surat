namespace Surat.Domain
{
    public class PenerimaModel
    {
        public int id_penerima {get;set;}
        public int id_user {get;set;}
        public int id_dokumen {get;set;}
        public string namaPenerima {get;set;}
        public DateTime createdAt { get; set; }
    }
}
