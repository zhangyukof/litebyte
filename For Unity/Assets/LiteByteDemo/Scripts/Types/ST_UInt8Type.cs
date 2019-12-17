namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> UInt Type </summary>
    [Serializable][ProtoContract]
    public struct ST_UInt8Type {

        [ProtoMember(1)] public byte uint8;

    }

}