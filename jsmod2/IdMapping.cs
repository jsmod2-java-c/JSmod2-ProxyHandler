using System;
using Newtonsoft.Json;

namespace jsmod2
{
    //对于api的映射id
    public class IdMapping
    {
        private String append = "";

        public IdMapping appendId(String str, object o)
        {
            append = append + "|" + str + ":" + JsonConvert.SerializeObject(o);
            ProxyHandler.handler.apiMapping.Add(str,o);
            return this;
        }

        public String get()
        {
            return append;
        }
    }
}