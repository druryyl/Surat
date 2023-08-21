using Surat.Domain;
using Surat.Helper;
using Surat.Infrastructure;

namespace Surat.Application
{
    public class SerahDokumenCommand
    {
        public string nameDokumen { get; set; }
        public string pengirimDokumen { get; set; }
        public int id_pemilik { get; set; }
        public string uraianDokumen { get; set; }
        public string tgl_dokumen { get; set; }
        public string tgl_agendaAwal { get; set; }
        public string tgl_agendaAkhir { get; set; }
        public int id_penerima { get; set; }
    }

    public class SerahDokumenHandler
    {
        private readonly UserDal _userDal = new();
        private readonly DokumenDal _dokumenDal = new();
        private readonly PenerimaDal _penerimaDal = new();

        public int Handle(SerahDokumenCommand command)
        {
            Guard(command);
            var dokumen = Create(command);
            var dokDb = Simpan(dokumen);

            var penerima = CreatePenerima(dokDb);
            var penerimaDb = SimpanPenerima(penerima);

            return dokDb.id_dokumen;
        }

        private void Guard(SerahDokumenCommand command)
        {
            if (command.nameDokumen.Length == 0)
                throw new ArgumentException("Nama Dokumen kosong");

            if (command.pengirimDokumen.Length == 0)
                throw new ArgumentException("Pengirim Dokumen kosong");

            if (command.uraianDokumen.Length == 0)
                throw new ArgumentException("Uraian Dokumen kosong");

            if (!command.tgl_dokumen.IsValidTgl("yyyy-MM-dd"))
                throw new ArgumentException("Tgl DOkumen invalid");

            if (command.tgl_agendaAkhir.Length > 0)
                if (command.tgl_agendaAkhir.IsValidTgl("yyyy-MM-dd"))
                    throw new ArgumentException("Tgl Agenda AKhir kosong");

            if (command.tgl_agendaAwal.Length > 0)
                if (command.tgl_agendaAwal.IsValidTgl("yyyy-MM-dd"))
                    throw new ArgumentException("Tgl Agenda Awal invalid");
        }

        public DokumenModel Create(SerahDokumenCommand command)
        {
            var userPemilik = _userDal.GetData(command.id_pemilik) 
                ?? throw new KeyNotFoundException("ID Pemilik invalid");

            var userPenerima = _userDal.GetData(command.id_penerima)
                ?? throw new KeyNotFoundException("ID Penerima invalid");

            var awal = command.tgl_agendaAwal.Length == 0
                ? new DateTime(3000, 1, 1)
                : command.tgl_agendaAwal.ToDate();
            var akhir = command.tgl_agendaAkhir.Length == 0
                    ? new DateTime(3000, 1, 1)
                    : command.tgl_agendaAkhir.ToDate();
            var tglDok = command.tgl_dokumen.ToDate();
            var result = new DokumenModel
            {
                nameDokumen = command.nameDokumen,
                pengirimDokumen = command.pengirimDokumen,
                id_pemilik = command.id_pemilik,
                pemilikDokumen = userPemilik.name,
                id_penerima = command.id_penerima,
                penerimaDokumen = userPenerima.name,
                uraianDokumen = command.uraianDokumen,
                tgl_diterima = DateTime.Now,
                tgl_dokumen = tglDok,
                tgl_agendaAwal = awal,
                tgl_agendaAkhir = akhir,
                tgl_createdAt = DateTime.Now,
                Token = "",
            };
            return result;
        }
        public DokumenModel Simpan(DokumenModel model)
        {
            var newId = _dokumenDal.Insert(model);
            model.id_dokumen = newId;
            return model;
        }

        public PenerimaModel CreatePenerima(DokumenModel model)
        {
            var user = _userDal.GetData(model.id_penerima)
                ?? throw new KeyNotFoundException("ID Penerima invalid");

            var penerima = new PenerimaModel
            {
                id_dokumen = model.id_dokumen,
                id_user = model.id_penerima,
                namaPenerima = user.name,
                createdAt = DateTime.Now,
            };

            return penerima;
        }
        public PenerimaModel SimpanPenerima(PenerimaModel model)
        {
            var newId = _penerimaDal.Insert(model);
            model.id_penerima = newId;
            return model;
        }
    }
}
