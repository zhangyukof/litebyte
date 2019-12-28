namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> Int Type </summary>
    [Serializable][ProtoContract]
    public struct ST_Int32Type {

        [ProtoMember(1)] public int int32;

    }

}