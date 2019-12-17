namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> UInt Type </summary>
    [Serializable][ProtoContract]
    public struct ST_UInt32Type {

        [ProtoMember(1)] public uint uint32;

    }

}