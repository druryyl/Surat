namespace Surat.Domain
{
    public class DokumenModel
    {
        public int id_dokumen {get;set;}
        public string nameDokumen {get;set;}
        public string pengirimDokumen {get;set;}
        public int id_pemilik {get;set;}
        public string pemilikDokumen {get;set;}
        
        public int id_penerima {get;set;}
        public string penerimaDokumen {get;set;}
        public string uraianDokumen {get;set;}

        public DateTime tgl_diterima {get;set;}
        public DateTime tgl_dokumen {get;set;}
        public DateTime tgl_agendaAwal {get;set;}
        public DateTime tgl_agendaAkhir {get;set;}
        public DateTime tgl_createdAt { get; set; }
        public string  Token { get; set; }
    }
}
