namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> 玩家信息测试 | PlayerInfo test </summary>
    [Serializable][ProtoContract]
    public class ST_PlayerInfo {

        [ProtoMember(1)] public uint id;
        [ProtoMember(2)] public string nickname;
        [ProtoMember(3)] public byte gender;
        [ProtoMember(4)] public bool isVip;
        [ProtoMember(5)] public int lv;
        [ProtoMember(6)] public int hp;
        [ProtoMember(7)] public int mp;
        [ProtoMember(8)] public int exp;
        [NonSerialized]
        [ProtoMember(9)] public float speed;
	
    }

}