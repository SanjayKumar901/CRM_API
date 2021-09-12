using API.DbManager.DbModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DAL
{
    public class DataLayer<T> : IDataLayer<T> where T : class
    {
        object deadlock = new object();
        DbManager.DbManager manager;
        DbSet<T> Modelset;
        public DataLayer()
        {
            manager = new DbManager.DbManager();
            Modelset = manager.Set<T>();
        }
        public IEnumerable<T> GetDetails()
        {
            try
            {
                return Modelset.ToList();
            }
            catch (Exception ex) { return null; }
        }

        public IEnumerable<T> GetDetailsWithCondition(Func<T, bool> func)
        {
            try
            {
                return Modelset.Where(func).ToList();
            }
            catch (Exception ex) { return null; }
        }

        public T GetSingelDetailWithCondition(Func<T, bool> func)
        {
            try
            {
                return Modelset.Where(func).FirstOrDefault();
            }
            catch (Exception ex)
            {
                try { System.IO.File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + @"\isAvailable.txt", ex.Message); } catch { }
                return null;
            }
        }

        public string InsertRecord(T Item)
        {

            string Response = "";
            try
            {
                using (var Trans = manager.Database.BeginTransaction())
                {
                    try
                    {
                        manager.Add(Item);
                        manager.SaveChanges();
                        Response = "Data Save Successfully.";
                        Trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        Response = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                        Trans.Rollback(); 
                    }
                }
            }
            catch (Exception ex)
            {
                Response = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                try { System.IO.File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + @"\InsertRecException.txt", ex.StackTrace.ToString()); } catch { }
            }
            return Response;
        }

        public string InsertRecordList(List<T> Item)
        {
            string Response = "";
            try
            {
                manager.AddRange(Item);
                manager.SaveChanges();
                Response = "Data Save Successfully.";
            }
            catch (Exception ex) { Response = ex.InnerException == null ? ex.Message : ex.InnerException.Message; }
            return Response;
        }
        public string ProcedureOutput(string ProcedureName, Dictionary<string, string> Param)
        {
            dynamic data = null;
            try
            {
                lock (deadlock)
                {
                    string qry = "";
                    List<MySqlParameter> param = new List<MySqlParameter>();
                    StringBuilder stringParam = new StringBuilder();
                    foreach (var item in Param)
                    {
                        stringParam.Append(item.Key);
                        //stringParam.Append(item.Key + "='" + item.Value + "'");
                        qry += "'" + item.Value + "'";
                        param.Add(new MySqlParameter(item.Key, item.Value));
                        stringParam.Append(",");
                    }
                    stringParam.Remove(stringParam.Length - 1, 1);
                    string temp = stringParam.ToString();
                    ProcedureName = "call " + ProcedureName;
                    data = manager.OutputTypeProc(ProcedureName + " (" + temp + ")", param);
                }
            }
            catch (Exception ex) { data = "Exception Dal :" + ex.Message; }
            return data;
        }

        public IEnumerable<T> ProceduresGetData(string ProcedureName, Dictionary<string, string> Param)
        {
            dynamic data = null;
            try
            {
                List<MySqlParameter> param = new List<MySqlParameter>();
                StringBuilder stringParam = new StringBuilder();
                string temp = "";
                foreach (var item in Param)
                {
                    stringParam.Append(item.Key);
                    //stringParam.Append(item.Key + "='" + item.Value + "'");
                    param.Add(new MySqlParameter(item.Key, item.Value));
                    stringParam.Append(",");
                }
                stringParam.Remove(stringParam.Length - 1, 1);
                temp = stringParam.ToString();
                ProcedureName = "call " + ProcedureName;
                data = manager.GetProcedureData<T>(ProcedureName + " (" + temp + ")", param);
            }
            catch (Exception ex) { }
            return data;
        }

        public string Update(T Item)
        {
            string Response = "";
            try
            {
                using (var Trans = manager.Database.BeginTransaction())
                {
                    try
                    {
                        Modelset.Attach(Item);
                        var entry = manager.Entry(Item);
                        entry.State = EntityState.Modified;
                        manager.SaveChanges();
                        Response = "Data Updated Successfully.";
                        Trans.Commit();
                    }
                    catch (Exception ex) { Trans.Rollback(); }
                }
            }
            catch (Exception ex)
            {
                Response = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                try { System.IO.File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + @"\UPdateRecException.txt", ex.StackTrace.ToString()); } catch { }
            }
            return Response;
        }
        public string DeleteRecord(T item)
        {
            string Response = "";
            try
            {
                Modelset.Attach(item);
                var entry = manager.Entry(item);
                entry.State = EntityState.Deleted;
                manager.SaveChanges();
                Response = "Data Deleted Successfully.";
            }
            catch (Exception ex)
            {
                Response = ex.Message;
                try { System.IO.File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + @"\DeleteRecException.txt", ex.StackTrace.ToString()); } catch { }
            }
            return Response;
        }
        public string DeleteRecordConditional(Func<T, bool> func)
        {
            string Response = "";
            try
            {
                var items = Modelset.Where(func).ToList();
                Modelset.AttachRange(items);
                manager.RemoveRange(items);
                manager.SaveChanges();
                Response = "Data Deleted Successfully.";
            }
            catch (Exception ex)
            {
                Response = ex.Message;
                try { System.IO.File.WriteAllText(System.IO.Directory.GetCurrentDirectory() + @"\DeleteRecordConditional.txt", ex.StackTrace.ToString()); } catch { }
            }

            return Response;
        }
    }
}
