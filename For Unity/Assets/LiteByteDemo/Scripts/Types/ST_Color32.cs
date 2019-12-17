namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> Color32 </summary>
    [Serializable][ProtoContract]
    public struct ST_Color32 {

        [ProtoMember(1)] public float r;
        [ProtoMember(2)] public float g;
        [ProtoMember(3)] public float b;
        [ProtoMember(4)] public float a;

    }

}