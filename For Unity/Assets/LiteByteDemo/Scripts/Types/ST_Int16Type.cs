namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> Int Type </summary>
    [Serializable][ProtoContract]
    public struct ST_Int16Type {

        [ProtoMember(1)] public short int16;

    }

}