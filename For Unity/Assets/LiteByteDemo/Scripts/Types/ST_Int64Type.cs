namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> Int Type </summary>
    [Serializable][ProtoContract]
    public struct ST_Int64Type {

        [ProtoMember(1)] public long int64;

    }

}