namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> VarInt Type </summary>
    [Serializable][ProtoContract]
    public struct ST_VarUInt16Type {

        [ProtoMember(1)] public ushort vuint16;

    }

}