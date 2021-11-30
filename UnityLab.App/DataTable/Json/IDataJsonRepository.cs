using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using UnityLab.Entity;

namespace UnityLab.DataTable
{
    public interface IDataJsonRepository<T> where T : BaseEntity, new()
    {
        void Insert(params T[] t);
        List<T> Load();
        int Update(params T[] t);
        void Delete(params T[] t);
    }

    public class DataJsonRepository<T> : IDataJsonRepository<T> where T : BaseEntity, new()
    {
        private Dictionary<int, T> data;
        private string FileNmae;
        public DataJsonRepository()
        {
            if (!Directory.Exists("JsonData"))
            {
                Directory.CreateDirectory("JsonData");
            }
            FileNmae = Path.Combine("JsonData", typeof(T).Name+"data.json");
            if (!File.Exists(FileNmae))
            {
                File.Create(FileNmae);
                data = new Dictionary<int, T>(); 
            }
            else
            {
                data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, T>>(File.ReadAllText(FileNmae, Encoding.UTF8));
                if (data==null)
                {
                    data = new Dictionary<int, T>();
                }
                
            }

        }

        public void Delete(params T[] t)
        {
            foreach (var item in t)
            {
                data.Remove(item.Id);
            }
            File.WriteAllText(FileNmae, JsonConvert.SerializeObject(data), Encoding.UTF8);
            
        }

        public void Insert(params T[] t)
        {
            int id = 0;
            if (data.Count != 0)
            {
                id = data.Max(a => a.Key);
                id++;
            }
            else {
                id = 1;
            }

            foreach (var item in t)
            {
                item.Id = id;
                data.Add(id, item);
                id++;
            }
            File.WriteAllText(FileNmae, JsonConvert.SerializeObject(data),Encoding.UTF8);
        }

        public List<T> Load()
        {
            return data.Values.ToList();
        }

        public int Update(params T[] t)
        {
            foreach (var item in t)
            {
                data[item.Id] = item;
            }
            File.WriteAllText(FileNmae, JsonConvert.SerializeObject(data), Encoding.UTF8);
            return t.Length;
        }
    }
}
