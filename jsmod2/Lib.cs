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
        
        public const string ROOM_ID = "room-"+ID;

        public const string TESLAGATE_ID = "teslaGate-" + ID;//TeslaGate

        public const string VICTIM_ID = "victim-" + ID;

        public const string OWNER_ID = "owner-" + ID;

        public const string CONNECTION_ID = "connection-" + ID;

        public const string ROUND_ID = "round-" + ID;

        public const string STATS_ID = "stats-" + ID;

        public const string ROUND_STATS_ID = "round-" + STATS_ID;

        //关于authCheck类型
        public const string AUTH_CHECK_EVENT_REQUESTER_SCPDATA_ID = "requester-"+PLAYER_SCPDATA_ID;

        public const string EVENT_GENERATOR_ID = "generator-" + ID;

        public const string EVENT_GENERATOR_ROOM_ID = "generator-"+ROOM_ID;

        public const string EVENT_USER_ID = "user-" + ID;

        public const string EVENT_USER_SCPDATA_ID = "user-" + PLAYER_SCPDATA_ID;

        public const string EVENT_USER_TEAMROLE_ID = "user-" + PLAYER_TEAM_ROLE_ID;

        public const string EVENT_DEAD_ID = "deadPlayer-" + ID;

        public const string EVENT_DEAD_SCPDATA_ID = "deadPlayer-" + PLAYER_SCPDATA_ID;

        public const string EVENT_DEAD_TEAMROLE_ID = "deadPlayer-" + PLAYER_TEAM_ROLE_ID;

        public const string EVENT_ACTIVATOR_ID = "activator-" + ID;

        public const string EVENT_ACTIVATOR_SCPDATA_ID = "activator-" + PLAYER_SCPDATA_ID;

        public const string EVENT_ACTIVATOR_TEAMROLE_ID = "activator-" + PLAYER_TEAM_ROLE_ID;

        public const string EVENT_DOOR_ID = "door-" + ID;

        public const string EVENT_ELEVATOR_ID = "elevator-" + ID;
        
        public const string EVENT_KILLER_ID = "killer-" + ID;

        public const string EVENT_KILLER_SCPDATA_ID = "killer-" + PLAYER_SCPDATA_ID;

        public const string EVENT_KILLER_TEAMROLE_ID = "killer-" + PLAYER_TEAM_ROLE_ID;
        
        public const string EVENT_VICTIM_SCPDATA_ID = "victim-" + PLAYER_SCPDATA_ID;

        public const string EVENT_VICTIM_TEAMROLE_ID = "victim-" + PLAYER_TEAM_ROLE_ID;
        
        public const string EVENT_OWNER_SCPDATA_ID = "owner-" + PLAYER_SCPDATA_ID;

        public const string EVENT_OWNER_TEAMROLE_ID = "owner-" + PLAYER_TEAM_ROLE_ID;


        public const string TARGET_ID = "target-" + ID;
        
        public const string EVENT_TARGET_SCPDATA_ID = "target-"+ PLAYER_SCPDATA_ID;

        public const string EVENT_TARGET_TEAMROLE_ID = "target-" + PLAYER_TEAM_ROLE_ID;
        
        //Attacker

        public const string ATTACKER_ID = "attacker-" + ID;
        
        public const string EVENT_ATTACKE_SCPDATA_ID = "attacker-" + PLAYER_SCPDATA_ID;

        public const string EVENT_ATTACKER_TEAMROLE_ID = "attacker-" + PLAYER_TEAM_ROLE_ID;

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

            try
            {
                s = s.Replace("(", "").Replace(")", "");
                string[] xyz = s.Split('-');
                float x = getDouble(xyz[0]);
                float y = getDouble(xyz[1]);
                float z = getDouble(xyz[2]);
                return new Vector(x,y,z);
            }
            catch (Exception e)
            {
                return null;
            }
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