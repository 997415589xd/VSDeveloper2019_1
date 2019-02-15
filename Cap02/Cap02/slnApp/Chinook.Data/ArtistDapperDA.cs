using Chinook.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Chinook.Data
{
   public class ArtistDapperDA : BaseConnection
    {
        public int GetCount()
        {
            var result = 0;

            var sql = "SELECT COUNT(1) FROM Artist";
            /*1: Create una instancia sql DbConnection*/
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                result = cn.Query<int>(sql).Single();
            }

            return result;
        }

        // //procedimeinto pro Select
        public List<Artist> GetArtists()
        {
            var result = new List<Artist>();
            var sql = "SELECT ArtistId,Name FROM Artist";
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                result= cn.Query<Artist>(sql).ToList();
            }

            return result;
        }
        //procedimeinto pro Select


        public List<Artist> GetArtistsWithSP(string filterByName)
        {
            var result = new List<Artist>();
            var sql = "usp_GetArtist";
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                result = cn.Query<Artist>(sql
                    , new { pNombre = filterByName }
                    , commandType: CommandType.StoredProcedure).ToList();
                    
            }

            return result;
        }
        public List<Artist> GetArtists(string filterByName)
        {
            var result = new List<Artist>();
            var sql = "SELECT ArtistId,Name FROM Artist WHERE Name like @name";
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                result = cn.Query<Artist>(sql
                   , new { name = filterByName }).ToList();
            }

            return result;
        }

        public int InsertArtist(Artist entity)
        {
            var result = 0;
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                result = cn.Query<int>("usp_InsertArtits",
                    new { NAme = entity.Name },
                    commandType: CommandType.StoredProcedure).Single();

            }
            return result;
        }

        public int InsertArtistConOutput(Artist entity)
        {
            var result = 0;
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                var param = new DynamicParameters();

                param.Add("Name", entity.Name);
                param.Add("ID", dbType: DbType.Int32
                    , direction: ParameterDirection.Output);
                               

                     cn.Query<int>("usp_InsertArtitsWhitOutPut",
                        param,
                        commandType: CommandType.StoredProcedure);



                result = param.Get<int>("ID");

            }
            return result;
        }

        public int InsertArtistTrans(Artist entity)
        {



            var result = 0;
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {

                cn.Open();
                // local transaction
                var tx = cn.BeginTransaction();
                try
                {

                    result = cn.Query<int>("usp_InsertArtits",
                    new { NAme = entity.Name },
                    commandType: CommandType.StoredProcedure,
                    transaction:tx
                    ).Single();

                    tx.Commit();
                    
                }
                catch (Exception)
                {

                    tx.Rollback();
                   

                }


            }
            return result;
        }

        public int InsertArtistTransDistribuida(Artist entity)
        {

            var result = 0;
            using (var tx = new TransactionScope())
            {
                try
                {
                    using (IDbConnection cn = new SqlConnection(GetConnection()))
                    {

                        result = cn.Query<int>("usp_InsertArtits",
                    new { Name = entity.Name },
                    commandType: CommandType.StoredProcedure).Single();

                    }
                    tx.Complete();

                }
                catch (Exception ex)
                {


                }
            }






            return result;
        }

        public bool UpdateArtis(Artist entity)
        {
            var result = false;
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                cn.Query("usp_updateArtis",
                    new { Name = entity.Name },
                    commandType: CommandType.StoredProcedure
                    );
                result = true;
            }
            return result;
        }

    }

}
