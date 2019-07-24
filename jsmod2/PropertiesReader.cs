using System;
using System.Collections.Generic;
using System.IO;

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
            if (!File.Exists(file))
            {
                this.file = file;
                FileStream stream = new FileStream(file,FileMode.Create);
                foreach (var str in conf)
                {
                    ProxyHandler.handler.Info(str.Key+"="+str.Value);
                    byte[] bytes = System.Text.Encoding.Default.GetBytes(str.Key + "=" + str.Value+";");
                    stream.Write(bytes,0,bytes.Length);
                }
                stream.Close();
            }
            
        }

        public string get(string key,bool getOne)
        {
            if ((File.Exists(file) && getOne)||!getA)
            {
                FileStream stream = new FileStream(file, FileMode.Create);
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                string[] things = System.Text.Encoding.Default.GetString(bytes, 0, bytes.Length).Split(';');
                foreach (var thing in things)
                {
                    string[] kv = thing.Split('=');
                    conf.Add(kv[0], kv[1]);
                }

                getA = true;
            }

            return conf[key];
        }
    }
}