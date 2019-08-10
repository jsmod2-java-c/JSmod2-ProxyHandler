using System;

namespace jsmod2
{
    //设置一个协议对象
    public class JsonSetting
    {
        public JsonSetting(int id, object responseValue, IdMapping idMapping)
        {
            this.id = id;
            this.responseValue = responseValue == null?"{}":responseValue;
            this.idMapping = idMapping;
        }

        public int id { get; }
        public Object responseValue { get; }

        public IdMapping idMapping { get; }


    }
}