namespace jsmod2
{
    //对于协议的lib
    public class Lib
    {
        public const string ID = "playerName";
        public const string PLAYER_SCPDATA_ID = "scp079Data-"+ID;
        //关于playerEvent类型
        public const string PLAYER_EVENT_SCPDATA_ID = "player-"+PLAYER_SCPDATA_ID;
        
        //关于adminEvent类型
        public const string ADMIN_EVENT_SCPDATA_ID = "admin-"+PLAYER_SCPDATA_ID;
        
        //关于itemEvent类型
        public const string ITEM_EVENT_ID = "item-"+ID;
        
        //关于authCheck类型
        public const string AUTH_CHECK_EVENT_REQUESTER_SCPDATA_ID = "requester-"+PLAYER_SCPDATA_ID;

    }
}