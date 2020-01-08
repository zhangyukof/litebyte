namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    [Serializable][ProtoContract]
    public class ClassType {

        [ProtoMember(1)]
        public ClassType2 a;
        [ProtoMember(2)]
        public ClassType2 b;
        [ProtoMember(3)]
        public ClassType2 c;

        public ClassType() {}

    }

}