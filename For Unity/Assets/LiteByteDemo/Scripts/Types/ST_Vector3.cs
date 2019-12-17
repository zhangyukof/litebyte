namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> Vector3 </summary>
    [Serializable][ProtoContract]
    public struct ST_Vector3 {

        [ProtoMember(1)]
        public float x;
        [ProtoMember(2)]
        public float y;
        [ProtoMember(3)]
        public float z;

        public ST_Vector3(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

    }

}