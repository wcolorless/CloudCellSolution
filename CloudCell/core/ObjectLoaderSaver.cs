using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CloudCell
{
    public class ObjectLoaderSaver
    {
        public static T Load<T>(string Path)
        {
            using (FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter bf = new BinaryFormatter();
                var obj = (T)bf.Deserialize(fs);
                fs.Close();
                return obj;
            }
        }

        public static void Save(string Path, object obj)
        {
            using (FileStream fs = new FileStream(Path, FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);
                fs.Close();
            }
        }
    }
}
