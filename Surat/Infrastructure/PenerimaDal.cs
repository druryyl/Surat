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
    public class PenerimaDal
    {
        private readonly string _connString;

        public PenerimaDal()
        {
            _connString = ConnStringHelper.Get();
        }
        public int Insert(PenerimaModel model)
        {
            const string sql = @"
                INSERT INTO tb_penerima (
                    id_user, id_dokumen,
                    namaPenerima, createdAt)
                VALUES (
                    @id_user, @id_dokumen,
                    @namaPenerima, @createdAt); 
                SELECT CAST(SCOPE_IDENTITY() AS INT)";

            var dp = new DynamicParameters(sql);
            dp.AddParam("@id_user", model.id_user, SqlDbType.Int);
            dp.AddParam("@id_dokumen", model.id_dokumen, SqlDbType.Int);
            dp.AddParam("@namaPenerima", model.namaPenerima, SqlDbType.VarChar);
            dp.AddParam("@createdAt", model.createdAt, SqlDbType.DateTime);

            using var conn = new SqlConnection(_connString);
            var newId = conn.ExecuteScalar<int>(sql, dp);
            return newId;
        }

        public void Update(PenerimaModel model)
        {
            const string sql = @"
                UPDATE 
                    tb_penerima 
                SET
                    id_penerima = @id_penerima,
                    id_user = @id_user,
                    id_dokumen = @id_dokumen,
                    namaPenerima = @namaPenerima,
                    createdAt = @createdAt
                WHERE
                    id_penerima = @id_penerima ";

            var dp = new DynamicParameters(sql);
            dp.AddParam("@id_penerima", model.id_penerima, SqlDbType.Int);
            dp.AddParam("@id_user", model.id_user, SqlDbType.Int);
            dp.AddParam("@id_dokumen", model.id_dokumen, SqlDbType.Int);
            dp.AddParam("@namaPenerima", model.namaPenerima, SqlDbType.VarChar);
            dp.AddParam("@createdAt", model.createdAt, SqlDbType.DateTime);

            using var conn = new SqlConnection(_connString);
            conn.Execute(sql, dp);
        }
        public void Delete(string id)
        {
            const string sql = @"
                DELETE FROM
                    tb_penerima 
                WHERE
                    id_penerima = @id_penerima ";

            var dp = new DynamicParameters(sql);
            dp.AddParam("@id_penerima", id, SqlDbType.Int);

            using var conn = new SqlConnection(_connString);
            conn.Execute(sql, dp);
        }

        public PenerimaModel GetData(string id)
        {
            const string sql = @"
                SELECT
                    id_penerima, id_user, id_dokumen,
                    namaPenerima, createdAt
                FROM
                    tb_penerima
                WHERE
                    id_penerima = @id_penerima ";

            var dp = new DynamicParameters(sql);
            dp.AddParam("@id_penerima", id, SqlDbType.VarChar);

            using var conn = new SqlConnection(_connString);
            return conn.ReadSingle<PenerimaModel>(sql, dp);
        }
        public IEnumerable<PenerimaModel> ListData(int id_dokumen)
        {
            const string sql = @"
                SELECT
                    id_penerima, id_user, id_dokumen,
                    namaPenerima, createdAt
                FROM
                    tb_penerima
                WHERE
                    id_dokumen = @id_dokumen ";

            var dp = new DynamicParameters(sql);
            dp.AddParam("@id_dokumen", id_dokumen, SqlDbType.Int);

            using var conn = new SqlConnection(_connString);
            return conn.Read<PenerimaModel>(sql, dp);
        }
    }
}
