using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {

            //每页条数   
            const int pageSize = 2;
            //页码 0也就是第一条 
            int pageNum = 5;

            //源数据   
            string[] names = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            var query = names.Skip((pageNum - 1) * pageSize).Take(pageSize);
            foreach (var q in query)
            {
                Console.WriteLine(q);
            }
            //while ((pageNum-1) * pageSize < names.Length)
            //{
            //    //分页   
            //    var query = names.Skip((pageNum-1) * pageSize).Take(pageSize);
            //    Console.WriteLine("输出第{0}页记录", pageNum);
            //    //输出每页内容   
            //    foreach (var q in query)
            //    {
            //        Console.WriteLine(q);
            //    }
            //    pageNum++;
            //}
            Console.ReadLine();
        }
    }
}
