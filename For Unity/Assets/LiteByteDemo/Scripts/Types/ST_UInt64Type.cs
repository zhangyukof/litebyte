namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> UInt Type </summary>
    [Serializable][ProtoContract]
    public struct ST_UInt64Type {

        [ProtoMember(1)] public ulong uint64;

    }

}