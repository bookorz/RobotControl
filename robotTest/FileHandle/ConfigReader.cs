using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace robotTest.FileHandle
{
    class ConfigReader<T>
    {
        ILog logger = LogManager.GetLogger(typeof(ConfigReader<T>));

        public List<T> ReadFile(string FilePath)
        {
            List<T> result = null;
            try
            {
                string t = File.ReadAllText(FilePath,Encoding.UTF8);
                result = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(FilePath,Encoding.UTF8));
            }
            catch (Exception ex)
            {
                logger.Error("ReadFile:" + ex.Message + "\n" + ex.StackTrace);
            }

            return result;
        }

        public void WriteFile(string FilePath,List<T> Obj)
        {
            try
            {
                File.WriteAllText(FilePath, JsonConvert.SerializeObject(Obj));
            }
            catch (Exception ex)
            {
                logger.Error("WriteFile:" + ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
