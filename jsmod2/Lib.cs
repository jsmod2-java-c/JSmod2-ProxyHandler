using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Smod2.API;

namespace jsmod2
{
    //对于协议的lib
    public class Lib
    {
        public const string ID = "playerName";

        public const string PLAYER_ID = "player-" + ID;

        public const string ADMIN_ID = "admin-" + ID;
        
        public const string PLAYER_SCPDATA_ID = "scp079Data-"+ID;

        public const string PLAYER_TEAM_ROLE_ID = "teamRole-" + ID;
        
        //关于playerEvent类型
        public const string PLAYER_EVENT_SCPDATA_ID = "player-"+PLAYER_SCPDATA_ID;
        
        //关于adminEvent类型
        public const string ADMIN_EVENT_SCPDATA_ID = "admin-"+PLAYER_SCPDATA_ID;

        public const string PLAYER_EVENT_TEAM_ROLE_ID = "player-" + PLAYER_TEAM_ROLE_ID;

        public const string ADMIN_EVENT_TEAM_ROLE_ID = "admin-" + PLAYER_TEAM_ROLE_ID;
        
        //关于itemEvent类型
        public const string ITEM_EVENT_ID = "item-"+ID;

        //关于authCheck类型
        public const string AUTH_CHECK_EVENT_REQUESTER_SCPDATA_ID = "requester-"+PLAYER_SCPDATA_ID;


        public static object getObject(Dictionary<string, string> dic, Type type,string name)
        {
            return JsonConvert.DeserializeObject(dic[name], type);
        }

        public static string[] getArray(string s)
        {
            return s.Split(',');
        }
        
        public static Vector getVector(string s)
        {
            
            string[] xyz = s.Split(',');
            float x = getDouble(xyz[0]);
            float y = getDouble(xyz[1]);
            float z = getDouble(xyz[2]);
            return new Vector(x,y,z);
        }

        public static float getDouble(string s)
        {
            float d;
            float.TryParse(s, out d);
            return d;
        }

        public static int getInt(string s)
        {
            int id;
            int.TryParse(s, out id);
            return id;
        }

        public static bool getBool(string s)
        {
            bool get;
            bool.TryParse(s, out get);
            return get;
        }
    }
}