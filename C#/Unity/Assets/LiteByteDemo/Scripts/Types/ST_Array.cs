namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary> 数组测试 | Array test </summary>
    [Serializable][ProtoContract]
    public struct ST_Array {

        [ProtoMember(1)] public bool[] bools;
        [ProtoMember(2)] public sbyte[] sbytes;
        [ProtoMember(3)] public byte[] bytes;
        [ProtoMember(4)] public short[] shorts;
        [ProtoMember(5)] public int[] ints;
        [ProtoMember(6)] public long[] longs;
        [ProtoMember(7)] public ushort[] ushorts;
        [ProtoMember(8)] public uint[] uints;
        [ProtoMember(9)] public ulong[] ulongs;
        [ProtoMember(10)] public float[] floats;
        [ProtoMember(11)] public double[] doubles;
        [ProtoMember(12)] public string[] strings;
	
    }

}