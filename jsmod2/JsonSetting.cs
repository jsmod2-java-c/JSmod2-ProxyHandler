using System;

namespace jsmod2
{
    //设置一个协议对象
    public class JsonSetting
    {
        public JsonSetting(int id, object o, IdMapping idMapping)
        {
            this.id = id;
            Object = o;
            this.idMapping = idMapping;
        }

        public int id { get; }
        public Object Object { get; }

        public IdMapping idMapping { get; }


    }
}