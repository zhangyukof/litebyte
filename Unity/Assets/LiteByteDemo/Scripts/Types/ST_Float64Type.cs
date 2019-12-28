namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> Float Type </summary>
    [Serializable][ProtoContract]
    public struct ST_Float64Type {

        [ProtoMember(1)] public double float64;

    }

}