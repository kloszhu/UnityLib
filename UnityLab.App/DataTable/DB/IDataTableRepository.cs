using ServiceStack.Data;
using System.Collections.Generic;

namespace UnityLab.DataTable
{
    public interface IDataTableRepository<T> where T : BaseEntity, new()
    {
       

        void Insert(params T[] t);
        List<T> Load();
        int Update(params T[] t);
        void Delete(params T[] t);
    }
}