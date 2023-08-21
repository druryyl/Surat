using Dapper;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.ValidationHelper;
using Surat.Domain;
using Surat.Helper;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Surat.Infrastructure
{
    public class DokumenDal
    {
        private readonly string _connString;

        public DokumenDal()
        {
            _connString = ConnStringHelper.Get();
        }
        public int Insert(DokumenModel model)
        {
            const string sql = @"
                INSERT INTO tb_dokumen (
                    nameDokumen, pengirimDokumen,
                    id_pemilik, pemilikDokumen, id_penerima,
                    penerimaDokumen, uraianDokumen,
                    tgl_diterima, tgl_dokumen, tgl_agendaAwal,
                    tgl_agendaAkhir, tgl_createdAt)
                VALUES (
                    @nameDokumen, @pengirimDokumen,
                    @id_pemilik, @pemilikDokumen, @id_penerima,
                    @penerimaDokumen, @uraianDokumen,
                    @tgl_diterima, @tgl_dokumen, @tgl_agendaAwal,
                    @tgl_agendaAkhir, @tgl_createdAt); 
                SELECT CAST(SCOPE_IDENTITY() AS INT)";

            var dp = new DynamicParameters(sql);
            dp.AddParam("@nameDokumen", model.nameDokumen, SqlDbType.VarChar);
            dp.AddParam("@pengirimDokumen", model.pengirimDokumen, SqlDbType.VarChar);
            dp.AddParam("@id_pemilik", model.id_pemilik, SqlDbType.VarChar);
            dp.AddParam("@pemilikDokumen", model.pemilikDokumen, SqlDbType.VarChar);
            dp.AddParam("@id_penerima", model.id_penerima, SqlDbType.VarChar);
            dp.AddParam("@penerimaDokumen", model.penerimaDokumen, SqlDbType.VarChar);
            dp.AddParam("@uraianDokumen", model.uraianDokumen, SqlDbType.VarChar);
            dp.AddParam("@tgl_diterima", model.tgl_diterima, SqlDbType.DateTime);
            dp.AddParam("@tgl_dokumen", model.tgl_dokumen, SqlDbType.DateTime);
            dp.AddParam("@tgl_agendaAwal", model.tgl_agendaAwal, SqlDbType.DateTime);
            dp.AddParam("@tgl_agendaAkhir", model.tgl_agendaAkhir, SqlDbType.DateTime);
            dp.AddParam("@tgl_createdAt", model.tgl_createdAt, SqlDbType.DateTime);

            using var conn = new SqlConnection(_connString);
            var newId = conn.ExecuteScalar<int>(sql, dp);
            return newId;
        }

        public void Update(DokumenModel model)
        {
            const string sql = @"
                UPDATE 
                    tb_dokumen 
                SET
                    id_dokumen = @id_dokumen,
                    nameDokumen = @nameDokumen,
                    pengirimDokumen = @pengirimDokumen,
                    id_pemilik = @id_pemilik,
                    pemilikDokumen = @pemilikDokumen,
                    id_penerima = @id_penerima,
                    penerimaDokumen = @penerimaDokumen,
                    uraianDokumen = @uraianDokumen,
                    tgl_diterima = @tgl_diterima,
                    tgl_dokumen = @tgl_dokumen,
                    tgl_agendaAwal = @tgl_agendaAwal,
                    tgl_agendaAkhir = @tgl_agendaAkhir,
                    tgl_createdAt = @tgl_createdAt
                WHERE
                    id_dokumen = @id_dokumen ";

            var dp = new DynamicParameters(sql);
            dp.AddParam("@id_dokumen", model.id_dokumen, SqlDbType.Int);
            dp.AddParam("@nameDokumen", model.nameDokumen, SqlDbType.VarChar);
            dp.AddParam("@pengirimDokumen", model.pengirimDokumen, SqlDbType.VarChar);
            dp.AddParam("@id_pemilik", model.id_pemilik, SqlDbType.VarChar);
            dp.AddParam("@pemilikDokumen", model.pemilikDokumen, SqlDbType.VarChar);
            dp.AddParam("@id_penerima", model.id_penerima, SqlDbType.VarChar);
            dp.AddParam("@penerimaDokumen", model.penerimaDokumen, SqlDbType.VarChar);
            dp.AddParam("@uraianDokumen", model.uraianDokumen, SqlDbType.VarChar);
            dp.AddParam("@tgl_diterima", model.tgl_diterima, SqlDbType.DateTime);
            dp.AddParam("@tgl_dokumen", model.tgl_dokumen, SqlDbType.DateTime);
            dp.AddParam("@tgl_agendaAwal", model.tgl_agendaAwal, SqlDbType.DateTime);
            dp.AddParam("@tgl_agendaAkhir", model.tgl_agendaAkhir, SqlDbType.DateTime);
            dp.AddParam("@tgl_createdAt", model.tgl_createdAt, SqlDbType.DateTime);

            using var conn = new SqlConnection(_connString);
            conn.Execute(sql, dp);
        }
        public void Delete(string dokumenId)
        {
            const string sql = @"
                DELETE FROM
                    tb_dokumen 
                WHERE
                    id_dokumen = @id_dokumen ";

            var dp = new DynamicParameters(sql);
            dp.AddParam("@id_dokumen", dokumenId, SqlDbType.VarChar);

            using var conn = new SqlConnection(_connString);
            conn.Execute(sql, dp);
        }

        public DokumenModel GetData(string dokumenId)
        {
            const string sql = @"
                SELECT
                    id_dokumen, nameDokumen, pengirimDokumen,
                    id_pemilik, pemilikDokumen, id_penerima,
                    penerimaDokumen, uraianDokumen,
                    tgl_diterima, tgl_dokumen, tgl_agendaAwal,
                    tgl_agendaAkhir, tgl_createdAt
                FROM
                    tb_dokumen
                WHERE
                    id_dokumen = @id_dokumen ";

            var dp = new DynamicParameters(sql);
            dp.AddParam("@id_dokumen", dokumenId, SqlDbType.VarChar);

            using var conn = new SqlConnection(_connString);
            return conn.ReadSingle<DokumenModel>(sql, dp);
        }
        public IEnumerable<DokumenModel> ListData(Periode periode)
        {
            const string sql = @"
                SELECT
                    id_dokumen, nameDokumen, pengirimDokumen,
                    id_pemilik, pemilikDokumen, id_penerima,
                    penerimaDokumen, uraianDokumen,
                    tgl_diterima, tgl_dokumen, tgl_agendaAwal,
                    tgl_agendaAkhir, tgl_createdAt
                FROM
                    tb_dokumen
                WHERE
                    tgl_createdAt BETWEEN @tgl1 AND @tgl2 ";

            var dp = new DynamicParameters(sql);
            dp.AddParam("@tgl1", periode.Tgl1, SqlDbType.DateTime);
            dp.AddParam("@tgl2", periode.Tgl2, SqlDbType.DateTime);

            using var conn = new SqlConnection(_connString);
            return conn.Read<DokumenModel>(sql, dp);
        }
    }
}
