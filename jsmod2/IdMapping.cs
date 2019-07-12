using System;
using Newtonsoft.Json;

namespace jsmod2
{
    //对于api的映射id
    public class IdMapping
    {
        private String append = "";

        //str是字段名表 o是id target是设置在apiMapping的对象
        //apiMapping中设置id和api对象的映射
        //发送过去是字段表和id
        //一般target就是str所指的上一级
        public IdMapping appendId(String str, object o,object target)
        {
            append = append + "|" + str + ":" + JsonConvert.SerializeObject(o);
            ProxyHandler.handler.apiMapping.Add(o.ToString(),target);
            return this;
        }

        public String get()
        {
            return append;
        }
    }
}