namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> VarInt Type </summary>
    [Serializable][ProtoContract]
    public struct ST_VarInt16Type {

        [ProtoMember(1)] public short vint16;

    }

}