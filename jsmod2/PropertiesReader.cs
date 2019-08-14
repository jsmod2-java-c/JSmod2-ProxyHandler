using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace jsmod2
{
    //读取配置文件
    public class PropertiesReader
    {
        private string file;

        private bool getA = false;
        
        private Dictionary<string, string> conf = new Dictionary<string, string>();

        public PropertiesReader append(string key, string value)
        {
            conf.Add(key,value);
            return this;
        }
        
        public void create(string file)
        {
            this.file = file;
            if (!File.Exists(file))
            {
                FileStream stream = File.Create(file);
                StringBuilder builder = new StringBuilder();
                foreach (var str in conf)
                {
                    builder.Append(str.Key + "=" + str.Value+";");
                }
                byte[] bytes = System.Text.Encoding.Default.GetBytes(builder.ToString());
                stream.Write(bytes,0,bytes.Length);
                stream.Close();
            }
            
        }

        public string get(string key)
        {
            if (!getA)
            {
                FileStream stream = new FileStream(file, FileMode.Open);
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                string[] things = System.Text.Encoding.Default.GetString(bytes, 0, bytes.Length).Split(';');
                foreach (var thing in things)
                {
                    if (!thing.Equals(""))
                    {
                        string[] kv = thing.Split('=');
                        if (!conf.ContainsKey(kv[0]))
                        {
                            conf.Add(kv[0], kv[1]); 
                        }
                    }
                }
                stream.Close();
                getA = true;
            }

            return conf[key];
        }
    }
}