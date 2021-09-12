using API.DbManager.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DAL
{
    public interface IDataLayer<T> where T :class
    {
        IEnumerable<T> GetDetails();
        IEnumerable<T> GetDetailsWithCondition(Func<T,bool> func);
        T GetSingelDetailWithCondition(Func<T, bool> func);
        IEnumerable<T> ProceduresGetData(string ProcedureName, Dictionary<string, string> Param);
        string ProcedureOutput(string ProcedureName, Dictionary<string, string> Param);
        string InsertRecord(T Item);

        string InsertRecordList(List<T> Item);
        string Update(T Item);
        string DeleteRecord(T Item);
        string DeleteRecordConditional(Func<T, bool> func);
    }
}
