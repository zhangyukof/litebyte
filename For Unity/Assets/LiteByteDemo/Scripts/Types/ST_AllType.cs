namespace LiteByte.Demo {

    using System;
    using ProtoBuf;

    /// <summary>
    /// 所有类型测试 | All types test
    /// ZhangYu 2019-11-21
    /// </summary>
    [Serializable][ProtoContract]
    public class ST_AllType {

        // Bit
        [ProtoMember(1)] public bool bit1;
	    [ProtoMember(2)] public byte bit2;
	    [ProtoMember(3)] public byte bit3;
	    [ProtoMember(4)] public byte bit4;
	    [ProtoMember(5)] public byte bit5;
	    [ProtoMember(6)] public byte bit6;
	    [ProtoMember(7)] public byte bit7;
	
	    // Int
	    [ProtoMember(8)] public sbyte int8;
	    [ProtoMember(9)] public short int16;
	    [ProtoMember(10)] public int int24;
	    [ProtoMember(11)] public int int32;
	    [ProtoMember(12)] public long int40;
	    [ProtoMember(13)] public long int48;
	    [ProtoMember(14)] public long int56;
	    [ProtoMember(15)] public long int64;
	
	    // UInt
	    [ProtoMember(16)] public byte uint8;
	    [ProtoMember(17)] public ushort uint16;
	    [ProtoMember(18)] public uint uint24;
	    [ProtoMember(19)] public uint uint32;
	    [ProtoMember(20)] public ulong uint40;
	    [ProtoMember(21)] public ulong uint48;
	    [ProtoMember(22)] public ulong uint56;
	    [ProtoMember(23)] public ulong uint64;

        // Float
        [ProtoMember(24)] public float float8;
        [ProtoMember(25)] public float float16;
        [ProtoMember(26)] public float float24;
        [ProtoMember(27)] public float float32;
        [ProtoMember(28)] public double float64;

        // VarInt
        [ProtoMember(29)] public short vint16;
	    [ProtoMember(30)] public int vint32;
	    [ProtoMember(31)] public long vint64;
        [ProtoMember(32)] public int vlength;

        // VarUInt
        [ProtoMember(33)] public ushort vuint16;
	    [ProtoMember(34)] public uint vuint32;
	    [ProtoMember(35)] public ulong vuint64;

        // string
        [ProtoMember(36)] public string utf8;
        [ProtoMember(37)] public string unicode;
        [ProtoMember(38)] public string ascii;

        // CustomType
        [ProtoMember(39)] public ST_Vector3 position;

        // NestedType
        [ProtoMember(40)] public ST_PlayerInfo playerInfo;

        // BitArray
        [ProtoMember(41)] public bool[] bit1Array;
        [ProtoMember(42)] public byte[] bit2Array;
        [ProtoMember(43)] public byte[] bit3Array;
        [ProtoMember(44)] public byte[] bit4Array;
        [ProtoMember(45)] public byte[] bit5Array;
        [ProtoMember(46)] public byte[] bit6Array;
        [ProtoMember(47)] public byte[] bit7Array;

        // IntArray
        [ProtoMember(48)] public sbyte[] int8Array;
        [ProtoMember(49)] public short[] int16Array;
        [ProtoMember(50)] public int[] int24Array;
        [ProtoMember(51)] public int[] int32Array;
        [ProtoMember(52)] public long[] int40Array;
        [ProtoMember(53)] public long[] int48Array;
        [ProtoMember(54)] public long[] int56Array;
        [ProtoMember(55)] public long[] int64Array;

        // UIntArray
        [ProtoMember(56)] public byte[] uint8Array;
        [ProtoMember(57)] public ushort[] uint16Array;
        [ProtoMember(58)] public uint[] uint24Array;
        [ProtoMember(59)] public uint[] uint32Array;
        [ProtoMember(60)] public ulong[] uint40Array;
        [ProtoMember(61)] public ulong[] uint48Array;
        [ProtoMember(62)] public ulong[] uint56Array;
        [ProtoMember(63)] public ulong[] uint64Array;

        // FloatArray
        [ProtoMember(64)] public float[] float8Array;
        [ProtoMember(65)] public float[] float16Array;
        [ProtoMember(66)] public float[] float24Array;
        [ProtoMember(67)] public float[] float32Array;
        [ProtoMember(68)] public double[] float64Array;

        // VarIntArray
        [ProtoMember(69)] public short[] vint16Array;
        [ProtoMember(70)] public int[] vint32Array;
        [ProtoMember(71)] public long[] vint64Array;
        [ProtoMember(72)] public int[] vlengthArray;
        [ProtoMember(73)] public ushort[] vuint16Array;
        [ProtoMember(74)] public uint[] vuint32Array;
        [ProtoMember(75)] public ulong[] vuint64Array;

        // StringArray
        [ProtoMember(76)] public string[] utf8Array;
        [ProtoMember(77)] public string[] unicodeArray;
        [ProtoMember(78)] public string[] asciiArray;

        // CustomTypeArray
        [ProtoMember(79)] public ST_Vector3[] positions;
        [ProtoMember(80)] public ST_PlayerInfo[] playerInfos;

    }

}