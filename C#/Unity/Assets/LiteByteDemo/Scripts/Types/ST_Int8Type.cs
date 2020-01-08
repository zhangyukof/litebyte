namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> Int Type </summary>
    [Serializable][ProtoContract]
    public struct ST_Int8Type {

        [ProtoMember(1)] public sbyte int8;

    }

}