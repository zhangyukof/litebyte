namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    [Serializable][ProtoContract]
    public class ClassType2 {

        [ProtoMember(1)]
        public int x;
        [ProtoMember(2)]
        public int y;

        public ClassType2() {}

        public ClassType2(int x, int y) {
            this.x = x;
            this.y = y;
        }

    }

}