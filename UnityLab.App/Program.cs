using Newtonsoft.Json;
using System;
using UnityLab.DataTable;
using UnityLab.Entity;

namespace UnityLab.App
{
    class Program
    {
        static void Main(string[] args)
        {
             GameEntry.Instance.Register();
            var repository=  GameEntry.Instance.GetService<IDataTableRepository<Student>>();
            var service = GameEntry.Instance.GetService<IDataJsonRepository<Student>>();
            repository.Insert(new Student { Name = "周杰伦的" });
            var data = repository.Load();
            repository.Delete(data.ToArray());
            var json = JsonConvert.SerializeObject(data);
           var p= service.Load();
            service.Insert(new Student { Name="杰瑞" });
            service.Delete(p.ToArray());
            Console.WriteLine(json);  
        }
    }
}
