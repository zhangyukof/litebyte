namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> 嵌套类型测试 | Nested type test </summary>
    [Serializable]
    [ProtoContract]
    public struct ST_NestedType {

        [ProtoMember(1)] public ST_PlayerInfo playerInfo;
        [ProtoMember(2)] public ST_Vector3 position;
        [ProtoMember(3)] public ST_Color color;
	
    }

}