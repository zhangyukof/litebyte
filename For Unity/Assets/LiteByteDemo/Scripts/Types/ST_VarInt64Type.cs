namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> VarInt Type </summary>
    [Serializable][ProtoContract]
    public struct ST_VarInt64Type {

        [ProtoMember(1)] public long vint64;

    }

}