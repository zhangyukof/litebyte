namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> VarInt Type </summary>
    [Serializable][ProtoContract]
    public struct ST_VarUInt32Type {

        [ProtoMember(1)] public uint vuint32;

    }

}