namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> VarInt Type </summary>
    [Serializable][ProtoContract]
    public struct ST_VarUInt64Type {

        [ProtoMember(1)] public ulong vuint64;

    }

}