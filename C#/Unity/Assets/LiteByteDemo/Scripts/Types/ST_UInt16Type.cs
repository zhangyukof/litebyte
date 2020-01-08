namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> UInt Type </summary>
    [Serializable][ProtoContract]
    public struct ST_UInt16Type {

        [ProtoMember(1)] public ushort uint16;

    }

}