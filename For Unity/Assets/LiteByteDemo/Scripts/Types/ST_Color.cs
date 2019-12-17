namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> Color </summary>
    [Serializable][ProtoContract]
    public struct ST_Color {

        [ProtoMember(1)] public float r;
        [ProtoMember(2)] public float g;
        [ProtoMember(3)] public float b;

        public ST_Color(float r, float g, float b) {
            this.r = r;
            this.g = g;
            this.b = b;
        }

    }

}