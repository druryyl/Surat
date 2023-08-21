using Dapper;
using Nuna.Lib.DataAccessHelper;
using System.Data;
using System.Data.SqlClient;
using Surat.Domain;
using Surat.Helper;

namespace Surat.Infrastructure
{
    public class UserDal
    {
        private readonly string _connString;

        public UserDal()
        {
            _connString = ConnStringHelper.Get();
        }
        public int Insert(UserModel model)
        {
            const string sql = @"
                INSERT INTO tb_User (
                    name, no_identitas, id_identitas,
                    alamat, phoneNumber, password, verify,
                    level, createdAt)
                VALUES (
                    @name, @no_identitas, @id_identitas,
                    @alamat, @phoneNumber, @password, @verify,
                    @level, @createdAt) ";

            var dp = new DynamicParameters(sql);
            dp.AddParam("@name", model.name, SqlDbType.VarChar);
            dp.AddParam("@no_identitas", model.no_identitas, SqlDbType.VarChar);
            dp.AddParam("@id_identitas", model.id_identitas, SqlDbType.Int);
            dp.AddParam("@alamat", model.alamat, SqlDbType.VarChar);
            dp.AddParam("@phoneNumber", model.phoneNumber, SqlDbType.VarChar);
            dp.AddParam("@password", model.password, SqlDbType.VarChar);
            dp.AddParam("@verify", model.verify, SqlDbType.Int);
            dp.AddParam("@level", model.level, SqlDbType.Int);
            dp.AddParam("@createdAt", model.createdAt, SqlDbType.DateTime);

            using var conn = new SqlConnection(_connString);
            var newId = conn.ExecuteScalar<int>(sql, dp);
            return newId;
        }

        public void Update(UserModel model)
        {
            const string sql = @"
                UPDATE 
                    User 
                SET
                    name = @name,
                    no_identitas = @no_identitas,
                    id_identitas = @id_identitas,
                    alamat = @alamat,
                    phoneNumber = @phoneNumber,
                    password = @password,
                    verify = @verify,
                    level = @level,
                    createdAt = @createdAt
                WHERE
                    id_user = @id_user ";

            var dp = new DynamicParameters(sql);
            dp.AddParam("@id_user", model.id_user, SqlDbType.Int);
            dp.AddParam("@name", model.name, SqlDbType.VarChar);
            dp.AddParam("@no_identitas", model.no_identitas, SqlDbType.VarChar);
            dp.AddParam("@id_identitas", model.id_identitas, SqlDbType.Int);
            dp.AddParam("@alamat", model.alamat, SqlDbType.VarChar);
            dp.AddParam("@phoneNumber", model.phoneNumber, SqlDbType.VarChar);
            dp.AddParam("@password", model.password, SqlDbType.VarChar);
            dp.AddParam("@verify", model.verify, SqlDbType.Int);
            dp.AddParam("@level", model.level, SqlDbType.Int);
            dp.AddParam("@createdAt", model.createdAt, SqlDbType.DateTime);

            using var conn = new SqlConnection(_connString);
            conn.Execute(sql, dp);
        }
        public void Delete(string id)
        {
            const string sql = @"
                DELETE FROM
                    tb_User 
                WHERE
                    id_user = @id_user ";

            var dp = new DynamicParameters(sql);
            dp.AddParam("@id_user", id, SqlDbType.Int);

            using var conn = new SqlConnection(_connString);
            conn.Execute(sql, dp);
        }

        public UserModel GetData(int id)
        {
            const string sql = @"
                SELECT
                    id_user, name, no_identitas, id_identitas,
                    alamat, phoneNumber, password, verify,
                    level, createdAt
                FROM
                    tb_user
                WHERE
                    id_user = @id_user";

            var dp = new DynamicParameters(sql);
            dp.AddParam("@id_user", id, SqlDbType.Int);

            using var conn = new SqlConnection(_connString);
            return conn.ReadSingle<UserModel>(sql, dp);
        }
        public IEnumerable<UserModel> ListData()
        {
            const string sql = @"
                SELECT
                    id_user, name, no_identitas, id_identitas,
                    alamat, phoneNumber, password, verify,
                    level, createdAt
                FROM
                    tb_user ";

            using var conn = new SqlConnection(_connString);
            return conn.Read<UserModel>(sql);
        }
    }
}
