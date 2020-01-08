namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> Float Type </summary>
    [Serializable][ProtoContract]
    public struct ST_Float32Type {

        [ProtoMember(1)] public float float32;

    }

}