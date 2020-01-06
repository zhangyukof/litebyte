==============================说明==============================
LiteByte
版本:0.6.13
作者:ZhangYu
创建日期:2019-05-22
修改日期:2020-01-06
GitHub:https://github.com/zhangyukof/litebyte

简介:
LiteByte是一种轻量级的二进制数据交换格式。
体积小巧、简单易用是设计目标。

特点：
1.紧凑的二进制数据格式, 数据量小。
2.用近似代码定义类的方式定义对象结构, 使用方便。

实现思路：
把一个对象分为两个部分：结构和值。
结构用配置文件定义，越方便越好。
值用于网络传输，越小越好。

使用方法：
(1)创建自定义结构文件CustomType.lbs （LiteByte Schema 文本文件）
(2)定义对象属性(像写类一样写结构、类型和名称)
(3)调用LBUtil.Serialize(object) 把对象序列化成二进制数据，
   调用LBUtil.Deserilize(bytes) 把二进制数据反序列化成对象

===========================基本数据类型：===========================
支持38种:
------------------ 比特型 7种 ------------------
类型:Bit1(Boolean)	长度:1位		值范围:0 ~ 1
类型:Bit2(Byte)		长度:2位		值范围:0 ~ 3
类型:Bit3(Byte)		长度:3位		值范围:0 ~ 7
类型:Bit4(Byte)		长度:4位		值范围:0 ~ 15
类型:Bit5(Byte)		长度:5位		值范围:0 ~ 31
类型:Bit6(Byte)		长度:6位		值范围:0 ~ 63
类型:Bit7(Byte)		长度:7位		值范围:0 ~ 127

------------------ 整数型 16种 ------------------
类型:Int8(sbyte)		长度:1字节	值范围:-128 ~ 127
类型:Int16(short)	长度:2字节	值范围:-32768 ~ -32767
类型:Int24(int)		长度:3字节	值范围:-8388608 ~ 8388607
类型:Int32(int)		长度:4字节	值范围:-2147483648 ~ 2147483647
类型:Int40(long)		长度:5字节	值范围:-549755813888 ~ 549755813887
类型:Int48(long)		长度:6字节	值范围:-140737488355328 ~ 140737488355327
类型:Int56(long)		长度:7字节	值范围:-36028797018963968 ~ 36028797018963967
类型:Int64(long)		长度:8字节	值范围:-9223372036854775808 ~ 9223372036854775807

类型:UInt8(byte)		长度:1字节	值范围:0 ~ 255
类型:UInt16(ushort)	长度:2字节	值范围:0 ~ 65535
类型:UInt24(uint)	长度:3字节	值范围:0 ~ 16777215
类型:UInt32(uint)	长度:4字节	值范围:0 ~ 4294967295
类型:UInt40(ulong)	长度:5字节	值范围:0 ~ 1099511627775
类型:UInt48(ulong)	长度:6字节	值范围:0 ~ 281474976710655
类型:UInt56(ulong)	长度:7字节	值范围:0 ~ 72057594037927935
类型:UInt64(ulong)	长度:8字节	值范围:0 ~ 18446744073709551615

------------------ 浮点型 5种 ------------------
类型:Float8(single)	字节长度:1	有效数字:x位		值范围:0/255 ~ 255/255
类型:Float16(single)字节长度:2	有效数字:3位		值范围:±6.55E +4
类型:Float24(single)		字节长度:3	有效数字:5位		值范围:±1.8447E +19
类型:Float32(single)		字节长度:4	有效数字:7位		值范围:±3.402823E +38
类型:Float64(double)		字节长度:8	有效数字:15位	值范围:±1.7976931348623157E +308

------------------ 可变长度整型 7种 ------------------
类型:VarInt16(short)		长度:1位 + 1~2字节	值范围:同Int16
类型:VarInt32(int)		长度:2位 + 1~4字节	值范围:同Int32
类型:VarInt64(int)		长度:3位 + 1~8字节	值范围:同Int64

类型:VarUInt16(short)	长度:1位 + 1~2字节	值范围:同UInt16
类型:VarUInt32(int)		长度:2位 + 1~4字节	值范围:同UInt32
类型:VarUInt64(int)		长度:3位 + 1~8字节	值范围:同UInt64
------------------ 字符串类型 1种(3种编码) ------------------
类型:UTF8		字符长度:1~4字节	总字节长度:(1~5) + (0 ~ 1073741822)
类型:Unicode	    字符长度:2字节	总字节长度:(1~5) + (0 ~ 1073741822) x 2
类型:ASCII		字符长度:1字节	总字节长度:(1~5) + (0 ~ 1073741822)

------------------ 复合类型 2种 ------------------
支持2种:
类型:数组		表达式:{类型名称}[]
// (未实现)类型:字典		表达式:Dictionary<{基本类型}, {类型}>
类型:自定义类型  表达式:只要不和基本类型和数组重名 即被当作自定义类型

------------------ 其他 ------------------
建议用尽量小的数据类型定义字段，这样可以减少序列化后的数据大小
可以对基本数据类型 Bit,Int,Float,String 定义自己喜欢的简称，方便编写

------------------ 自定义类型结构配置(LiteByte Schema)样例 ------------------
以下样例中 基本类型默认应用以下简称配置
Bit1      = bool
Int8      = sbyte
UInt8     = byte
VarInt32  = int
VarUnt32  = uint
VarInt64  = long
VarUInt64 = ulong
UTF8      = string

基本数据类型 结构:
struct BaseTypeST {

	// 比特型
	bool boolValue;

	// 有符号整型
	sbyte sbyteValue;
	short shortValue;
	int intValue;
	long longValue;

	// 无符号整型
	byte byteValue;
	ushort ushortValue;
	uint uintValue;
	ulong ulongValue;

	// 有符号浮点型
	float floatValue;
	double doubleValue;

	// 字符型(UTF8)
	string stringValue;

}

数组 结构1:
struct ArrayST {
	int[] ids;	
}

数组 结构2:
struct ArrayST {
	string[] names;	
}

登录信息 结构:
struct LoginInfoST {

	string username;
	string password;

}

用户信息 结构2:
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

============================== 各语言类型对照表 ==============================
比特型：
 类型    | 长度 |     C#    | Java(未实现) | C++(未实现) | Go(未实现)   
Bit1      1位       bool       boolean        char          bool
Bit2      2位       byte         byte         char          uint8
Bit3      3位       byte         byte         char          uint8
Bit4      4位       byte         byte         char          uint8
Bit5      5位       byte         byte         char          uint8
Bit6      6位       byte         byte         char          uint8
Bit7      7位       byte         byte         char          uint8

整型：
Int8     1字节      sbyte        sbyte        char          int8
Int16    2字节      short        short        short         int16
Int24    3字节      int          int          int           int32
Int32    4字节      int          int          int           int32
Int40    5字节      long         long      long long        int64
Int48    6字节      long         long      long long        int64
Int56    7字节      long         long      long long        int64
Int64    8字节      long         long      long long        int64

UInt8    1字节      byte         byte         char          uint8
UInt16   2字节      ushort       ushort   unsigned short    uint16
UInt24   3字节      uint         uint     unsigned int      uint32
UInt32   4字节      uint         uint     unsigned int      uint32
UInt40   5字节      ulong        ulong  unsigned long long  uint64
UInt48   6字节      ulong        ulong  unsigned long long  uint64
UInt56   7字节      ulong        ulong  unsigned long long  uint64
UInt64   8字节      ulong        ulong  unsigned long long  uint64

浮点型：
Float8   1字节      float       float         float         float32
Float16  2字节      float       float         float         float32
Float24  3字节      float       float         float         float32
Float32  4字节      float       float         float         float32
Float64  8字节      double      double        double        float64

变长整型：
VarInt16 1位+1~2字节 short      short         short         int16
VarInt32 2位+1~4字节 int        int           int           int32
VarInt64 3位+1~8字节 long       long          long long     int64

VarUInt16 1位+1~2字节 ushort    ushort  unsigned short      uint16
VarUInt32 2位+1~4字节 uint      uint    unsigned int        uint32
VarUInt64 3位+1~8字节 ulong     ulong   unsigned long long  uint64

字符串型:
UTF8    1~4字节     string      String        string        string
Unicode   2字节     string      String        string        string
ASCII     1字节     string      String        string        string
