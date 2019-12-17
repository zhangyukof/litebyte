# LiteByte
LiteByte is a lightweight binary data interchange format

# Explain
**Version**:0.6.10<br/>
**Author**:ZhangYu<br/>
**CreateDate**:2019-05-22<br/>
**LastModifiedDate**:2019-12-16<br/>
**GitHub**:https://github.com/zhangyukof/litebyte

## Intro:
LiteByte is a lightweight binary data interchange format.<br/>
Small size and easy to use are the design goals.

## Features：
1.Compact binary data format with small data volume.<br/>
2.Define your own type like wirte a class, easy to use.

## Implementation approach：
An object is divided into two parts: 
structure, which is defined by a configuration file, 
and value, which is used for network transfer.

## Usage：
(1)Create structure file CustomType.lbs(LiteByte Schema is a text file)<br/>
(2)Define object fields(write structure, type, and name just as you write a class)<br/>
(3)Use api LBUtil.Serialize(typeName, object) Serializes the object into binary data,<br/>
   Use api LBUtil.Deserilize(typeName, bytes) Deserialize binary data into objects.<br/>

## BaseTypes
Support for 38 data types:
### Bit(7 types)
Type:Bit1(Boolean)  Size:1Bit    Value Range:0 ~ 1<br/>
Type:Bit2(Byte)     Size:2Bit    Value Range:0 ~ 3<br/>
Type:Bit3(Byte)     Size:3Bit    Value Range:0 ~ 7<br/>
Type:Bit4(Byte)     Size:4Bit    Value Range:0 ~ 15<br/>
Type:Bit5(Byte)     Size:5Bit    Value Range:0 ~ 31<br/>
Type:Bit6(Byte)     Size:6Bit    Value Range:0 ~ 63<br/>
Type:Bit7(Byte)     Size:7Bit    Value Range:0 ~ 127<br/>

### Integer(16 types)
Type:Int8(sbyte)    Size:1Byte   Value Range:-128 ~ 127<br/>
Type:Int16(short)   Size:2Byte   Value Range:-32768 ~ -32767<br/>
Type:Int24(int)     Size:3Byte   Value Range:-8388608 ~ 8388607<br/>
Type:Int32(int)     Size:4Byte   Value Range:-2147483648 ~ 2147483647<br/>
Type:Int40(long)    Size:5Byte   Value Range:-549755813888 ~ 549755813887<br/>
Type:Int48(long)    Size:6Byte   Value Range:-140737488355328 ~ 140737488355327<br/>
Type:Int56(long)    Size:7Byte   Value Range:-36028797018963968 ~ 36028797018963967<br/>
Type:Int64(long)    Size:8Byte   Value Range:-9223372036854775808 ~ 9223372036854775807<br/>

Type:UInt8(byte)    Size:1Byte   Value Range:0 ~ 255<br/>
Type:UInt16(ushort)	Size:2Byte   Value Range:0 ~ 65535<br/>
Type:UInt24(uint)	Size:3Byte   Value Range:0 ~ 16777215<br/>
Type:UInt32(uint)	Size:4Byte   Value Range:0 ~ 4294967295<br/>
Type:UInt40(ulong)	Size:5Byte   Value Range:0 ~ 1099511627775<br/>
Type:UInt48(ulong)	Size:6Byte   Value Range:0 ~ 281474976710655<br/>
Type:UInt56(ulong)	Size:7Byte   Value Range:0 ~ 72057594037927935<br/>
Type:UInt64(ulong)	Size:8Byte   Value Range:0 ~ 18446744073709551615<br/>

### Float(5 types)
Type:Float8(single)   Size:1  Significant Digits:7Bit   Value Range:0/255 ~ 255/255<br/>
Type:Float16(single)  Size:2  Significant Digits:3Bit   Value Range:±6.55E +4<br/>
Type:Float24(single)  Size:3  Significant Digits:5Bit   Value Range:±1.8447E +19<br/>
Type:Float32(single)  Size:4  Significant Digits:7Bit   Value Range:±3.402823E +38<br/>
Type:Float64(double)  Size:8  Significant Digits:15Bit  Value Range:±1.7976931348623157E +308<br/>

### Variable Integer(7 types)
Type:VarInt16(short)  Size:1Bit + 1~2Byte  Value Range:Same as Int16<br/>
Type:VarInt32(int)    Size:2Bit + 1~4Byte  Value Range:Same as Int32<br/>
Type:VarInt64(int)    Size:3Bit + 1~8Byte  Value Range:Same as Int64<br/>

Type:VarUInt16(short) Size:1Bit + 1~2Byte	Value Range::Same as UInt16<br/>
Type:VarUInt32(int)   Size:2Bit + 1~4Byte	Value Range::Same as UInt32<br/>
Type:VarUInt64(int)   Size:3Bit + 1~8Byte	Value Range::Same as UInt64<br/>

Type:VarLength(uint)  Size:2Bit + 1~4Byte	Value Range:-1 ~ 1073741821(uint.MaxValue / 2 - 1)<br/>

### String(3 Encodings)
Type:UTF8		Size:1~4Byte	MaxSize:VarLength(1~4) + (0 ~ 1073741822) Bytes<br/>
Type:Unicode	Size:2Byte	    MaxSize:VarLength(1~4) + (0 ~ 1073741822) x 2 Bytes<br/>
Type:ASCII		Size:1Byte	    MaxSize:VarLength(1~4) + (0 ~ 1073741822) Bytes<br/>

### Complex Type(2 types)
Type:Array		 Expression:{TypeName}[]<br/>
// (Unimplemented)Type:Dictionary xpression:Dictionary<{BaseType}, {TypeName}><br/>
Type:CustomType  Expression:Different name with baseType, is CustomType<br/>

### Others
It is recommended that fields be defined with as small a data type as possible，<br/>
This reduces the size of the serialized data.<br/>

You can give nicknames to basic data types, Define your own style for writing.<br/>

### Custom Type Config(LiteByte Schema) Samples
The base types in the following samples apply the following config by default.<br/>
Bit1      = bool<br/>
Int8      = sbyte<br/>
UInt8     = byte<br/>
VarInt32  = int<br/>
VarUnt32  = uint<br/>
VarInt64  = long<br/>
VarUInt64 = ulong<br/>
UTF8      = string<br/>

#### BaseType Sample 1:
```cs
struct BaseTypeST {

	// Bit
	bool boolValue;

	// Int
	sbyte sbyteValue;
	short shortValue;
	int intValue;
	long longValue;

	// UInt
	byte byteValue;
	ushort ushortValue;
	uint uintValue;
	ulong ulongValue;

	// Float
	float floatValue;
	double doubleValue;

	// String(UTF8)
	string stringValue;

}
```

#### Array sample 1:
```cs
struct ArrayST {
	int[] ids;	
}
```

#### Array sample 2:
```cs
struct ArrayST {
	string[] names;	
}
```
#### Login info Sample:
```cs
struct LoginInfoST {

	string username;
	string password;

}
```

#### User info sample:
```cs
struct UserInfoST {

	uint id;
	string username;
	string nickname;
	int hp;
	int mp;
	long exp;
	long gold;
	byte age;
	bool isVip;

}
```

## Data Type Table
###Bit：
 Type | ByteSize |    C#   |Java(Unimpl)  | C++(Unimpl) | Go(Unimpl)   <br/>
Bit1      1Bit       bool       boolean        char          bool<br/>
Bit2      2Bit       byte         byte         char          uint8<br/>
Bit3      3Bit       byte         byte         char          uint8<br/>
Bit4      4Bit       byte         byte         char          uint8<br/>
Bit5      5Bit       byte         byte         char          uint8<br/>
Bit6      6Bit       byte         byte         char          uint8<br/>
Bit7      7Bit       byte         byte         char          uint8<br/>

###Integer：
Int8     1Byte      byte         byte         char          int8<br/>
Int16    2Byte      short        short        short         int16<br/>
Int24    3Byte      int          int          int           int32<br/>
Int32    4Byte      int          int          int           int32<br/>
Int40    5Byte      long         long      long long        int64<br/>
Int48    6Byte      long         long      long long        int64<br/>
Int56    7Byte      long         long      long long        int64<br/>
Int64    8Byte      long         long      long long        int64<br/>

UInt8    1Byte      byte         byte         char          uint8<br/>
UInt16   2Byte      ushort       ushort   unsigned short    uint16<br/>
UInt24   3Byte      uint         uint     unsigned int      uint32<br/>
UInt32   4Byte      uint         uint     unsigned int      uint32<br/>
UInt40   5Byte      ulong        ulong  unsigned long long  uint64<br/>
UInt48   6Byte      ulong        ulong  unsigned long long  uint64<br/>
UInt56   7Byte      ulong        ulong  unsigned long long  uint64<br/>
UInt64   8Byte      ulong        ulong  unsigned long long  uint64<br/>

###Float：
Float8   1Byte      float       float         float         float32<br/>
Float16  2Byte      float       float         float         float32<br/>
Float24  3Byte      float       float         float         float32<br/>
Float32  4Byte      float       float         float         float32<br/>
Float64  8Byte      double      double        double        float64<br/>

###Variable Integer：
VarInt16 1Bit+1~2Byte short      short         short         int16<br/>
VarInt32 2Bit+1~4Byte int        int           int           int32<br/>
VarInt64 3Bit+1~8Byte long       long          long long     int64<br/>

VarUInt16 1Bit+1~2Byte ushort    ushort  unsigned short      uint16<br/>
VarUInt32 2Bit+1~4Byte uint      uint    unsigned int        uint32<br/>
VarUInt64 3Bit+1~8Byte ulong     ulong   unsigned long long  uint64<br/>

VarLength 2Bit+1~4Byte int        int           int           int32<br/>

###String:
UTF8    1~4Byte     string      String        string        string<br/>
Unicode   2Byte     string      String        string        string<br/>
ASCII     1Byte     string      String        string        string<br/>

