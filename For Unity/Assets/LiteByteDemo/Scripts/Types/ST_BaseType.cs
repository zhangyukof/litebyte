namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> 基本类型测试 | Base type test </summary>
    [Serializable][ProtoContract]
    public struct ST_BaseType {

        [ProtoMember(1)] public bool tBool;
        [ProtoMember(2)] public sbyte tSByte;
        [ProtoMember(3)] public byte tByte;
        [ProtoMember(4)] public short tShort;
        [ProtoMember(5)] public int tInt;
        [ProtoMember(6)] public long tLong;
        [ProtoMember(7)] public ushort tUShort;
        [ProtoMember(8)] public uint tUInt;
        [ProtoMember(9)] public ulong tULong;
        [ProtoMember(10)] public float tFloat;
        [ProtoMember(11)] public double tDouble;
        [ProtoMember(12)] public string tString;
	
    }

}