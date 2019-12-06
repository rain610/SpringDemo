using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Aspose.Words;
using Aspose.Words.Replacing;
using Newtonsoft.Json;
using Rain.BaseRepository;
using Rain.Common;
using Rain.Common.RedisHelper;
using Rain.Entities;

namespace Rain.Repository
{
    public class AddressRepository
    {
        public List<AddressEntity> List()
        {
            //RedisHelper redisHelper = new RedisHelper();
            //var username = "Rain";
            //if (!Cache.Exists("username"))
            //{
            //    Cache.Insert("username", username);
            //}
            int pageIndex = 1;
            int pageSize = 20;
            var context = RainDbContext.GetContext();
            var data = (from t in context.Set<AddressEntity>()
                        select t);
            var aa = data.ToList();
            var totalPage = Math.Ceiling((double)aa.Count / 20);
            pageIndex = (int)totalPage;
            var test = (from t in context.Set<AddressEntity>()
                        orderby t.AddressID
                        select t).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            //if (redisHelper.KeyExists("addressList"))
            //{
            //    var list = JsonConvert.DeserializeObject<List<AddressEntity>>(redisHelper.StringGet("addressList"));
            //    TimeSpan timeSpan = new TimeSpan(0, 1, 0);
            //    redisHelper.StringSet("addressList", JsonConvert.SerializeObject(test), timeSpan);
            //}
            //else 
            //{
            //    TimeSpan timeSpan = new TimeSpan(0,1,0);
            //    redisHelper.StringSet("addressList", JsonConvert.SerializeObject(test), timeSpan);
            //}



            return test.ToList();
        }

        public string Test()
        {
            var baseUrl = System.AppDomain.CurrentDomain.BaseDirectory;
            var resDir = baseUrl + $"Uploads\\ResponsibilityOrgPost\\";
            string operateFile = string.Empty;
            try
            {

                string modulePath = System.AppDomain.CurrentDomain.BaseDirectory + "Download\\Test2.docx";
                if (!File.Exists(modulePath))
                    throw new Exception("岗位安全生产责任制模板不存在！");
                var dir = resDir + "temp\\";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                //拷贝模板文件进行操作，不独占
                operateFile = resDir + "temp\\" + Guid.NewGuid().ToString() + ".doc";
                File.Copy(modulePath, operateFile);
                Document doc = new Document(operateFile);
                DocumentBuilder builder = new DocumentBuilder(doc);


                //String[] fieldNames = new String[] { "UserName", "Gender", "BirthDay", "Address" };
                //Object[] fieldValues = new Object[] { "张三", "男", "1988-09-02", "陕西咸阳" };
                ////合并模版，相当于页面的渲染
                //doc.MailMerge.Execute(fieldNames, fieldValues);
                var findReplaceOptions = new FindReplaceOptions(FindReplaceDirection.Backward);
                findReplaceOptions.ApplyFont.Size = 10.5;
                findReplaceOptions.ApplyFont.Name = "宋体";
                doc.Range.Replace(new Regex("{ClassName}"), "测试" ?? "", findReplaceOptions);
                DataTable table = new DataTable("UserList");
                table.Columns.Add("UserName");
                table.Columns.Add("Gender");
                table.Columns.Add("BirthDay");
                table.Columns.Add("Address");

                DataRow dr1 = table.NewRow();
                dr1["UserName"] = "张三";
                dr1["Gender"] = "男";
                dr1["BirthDay"] = "1980-01-01";
                dr1["Address"] = "asdffgwesfdsdf";
                table.Rows.Add(dr1);
                DataRow dr2 = table.NewRow();
                dr2["UserName"] = "李斯";
                dr2["Gender"] = "男";
                dr2["BirthDay"] = "1980-01-01";
                dr2["Address"] = "asdffgwesfdsdf";
                table.Rows.Add(dr2);
                //table.ImportRow(dr2);
                //table.Rows.Add(new { UserName = "张三", Gender = "男", BirthDay = "1980-01-01", Address = "asdffgwesfdsdf" });
                //table.Rows.Add(new { UserName = "李斯", Gender = "男", BirthDay = "1980-01-01", Address = "asdffgwesfdsdf" });
                doc.MailMerge.ExecuteWithRegions(table);

                String path = System.IO.Path.GetFullPath(resDir);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filePath = path + Guid.NewGuid().ToString() + ".docx";
                doc.Save(filePath);
                filePath = filePath.Replace(baseUrl.TrimEnd('\\'), string.Empty);
                return filePath;
            }
            catch (Exception ex)
            {
                throw new Exception("生成office退出异常：" + ex.Message);
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(operateFile) && File.Exists(operateFile))
                    File.Delete(operateFile);
            }


        }
    }
}
