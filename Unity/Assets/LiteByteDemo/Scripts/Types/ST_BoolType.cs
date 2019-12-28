namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> BoolType </summary>
    [Serializable][ProtoContract]
    public struct ST_BoolType {

        [ProtoMember(1)] public bool b1;
        [ProtoMember(2)] public bool b2;
        [ProtoMember(3)] public bool b3;
        [ProtoMember(4)] public bool b4;
        [ProtoMember(5)] public bool b5;
        [ProtoMember(6)] public bool b6;
        [ProtoMember(7)] public bool b7;
        [ProtoMember(8)] public bool b8;

    }

}