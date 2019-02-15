using Chinook.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;

namespace Chinook.Data
{
    public class GenreDapperDA : BaseConnection
    {

        public List<Genre> GetGenreWithSP(string filterByName)
        {
            var result = new List<Genre>();
            var sql = "usp_GetGenre";
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                result = cn.Query<Genre>(sql
                    , new { pNombre = filterByName }
                    , commandType: CommandType.StoredProcedure).ToList();

            }

            return result;
        }

        public int InsertGenre(Genre entity)
        {
            //usp_InsertGenre

            var result = 0;
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                result = cn.Query<int>("usp_InsertGenre",
                    new { Name = entity.Name },
                    commandType: CommandType.StoredProcedure).Single();

            }
            return result;
        }

        public int InsertGenreTrans(Genre entity)
        {
            var result = 0;
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {

                cn.Open();
                // local transaction
                var tx = cn.BeginTransaction();
                try
                {

                    result = cn.Query<int>("usp_InsertGenre",
                    new { NAme = entity.Name },
                    commandType: CommandType.StoredProcedure,
                    transaction: tx
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

        public int InsertGenreTransDistribuida(Genre entity)
        {

            var result = 0;
            using (var tx = new TransactionScope())
            {
                try
                {
                    using (IDbConnection cn = new SqlConnection(GetConnection()))
                    {

                        result = cn.Query<int>("usp_InsertGenre",
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

        public bool UpdateGenre(Genre entity)
        {
            var result = false;
            using (IDbConnection cn = new SqlConnection(GetConnection()))
            {
                cn.Query("usp_updateGenre",
                    new { Name = entity.Name },
                    commandType: CommandType.StoredProcedure
                    );
                result = true;
            }
            return result;
        }
    }
}
