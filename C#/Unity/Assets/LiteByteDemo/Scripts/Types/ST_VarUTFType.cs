namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> String Type </summary>
    [Serializable][ProtoContract]
    public struct ST_VarUTFType {

        [ProtoMember(1)] public string str;

    }

}