==============================˵��==============================
LiteByte
�汾:0.6.13
����:ZhangYu
��������:2019-05-22
�޸�����:2020-01-06
GitHub:https://github.com/zhangyukof/litebyte

���:
LiteByte��һ���������Ķ��������ݽ�����ʽ��
���С�ɡ������������Ŀ�ꡣ

�ص㣺
1.���յĶ��������ݸ�ʽ, ������С��
2.�ý��ƴ��붨����ķ�ʽ�������ṹ, ʹ�÷��㡣

ʵ��˼·��
��һ�������Ϊ�������֣��ṹ��ֵ��
�ṹ�������ļ����壬Խ����Խ�á�
ֵ�������紫�䣬ԽСԽ�á�

ʹ�÷�����
(1)�����Զ���ṹ�ļ�CustomType.lbs ��LiteByte Schema �ı��ļ���
(2)�����������(��д��һ��д�ṹ�����ͺ�����)
(3)����LBUtil.Serialize(object) �Ѷ������л��ɶ��������ݣ�
   ����LBUtil.Deserilize(bytes) �Ѷ��������ݷ����л��ɶ���

===========================�����������ͣ�===========================
֧��38��:
------------------ ������ 7�� ------------------
����:Bit1(Boolean)	����:1λ		ֵ��Χ:0 ~ 1
����:Bit2(Byte)		����:2λ		ֵ��Χ:0 ~ 3
����:Bit3(Byte)		����:3λ		ֵ��Χ:0 ~ 7
����:Bit4(Byte)		����:4λ		ֵ��Χ:0 ~ 15
����:Bit5(Byte)		����:5λ		ֵ��Χ:0 ~ 31
����:Bit6(Byte)		����:6λ		ֵ��Χ:0 ~ 63
����:Bit7(Byte)		����:7λ		ֵ��Χ:0 ~ 127

------------------ ������ 16�� ------------------
����:Int8(sbyte)		����:1�ֽ�	ֵ��Χ:-128 ~ 127
����:Int16(short)	����:2�ֽ�	ֵ��Χ:-32768 ~ -32767
����:Int24(int)		����:3�ֽ�	ֵ��Χ:-8388608 ~ 8388607
����:Int32(int)		����:4�ֽ�	ֵ��Χ:-2147483648 ~ 2147483647
����:Int40(long)		����:5�ֽ�	ֵ��Χ:-549755813888 ~ 549755813887
����:Int48(long)		����:6�ֽ�	ֵ��Χ:-140737488355328 ~ 140737488355327
����:Int56(long)		����:7�ֽ�	ֵ��Χ:-36028797018963968 ~ 36028797018963967
����:Int64(long)		����:8�ֽ�	ֵ��Χ:-9223372036854775808 ~ 9223372036854775807

����:UInt8(byte)		����:1�ֽ�	ֵ��Χ:0 ~ 255
����:UInt16(ushort)	����:2�ֽ�	ֵ��Χ:0 ~ 65535
����:UInt24(uint)	����:3�ֽ�	ֵ��Χ:0 ~ 16777215
����:UInt32(uint)	����:4�ֽ�	ֵ��Χ:0 ~ 4294967295
����:UInt40(ulong)	����:5�ֽ�	ֵ��Χ:0 ~ 1099511627775
����:UInt48(ulong)	����:6�ֽ�	ֵ��Χ:0 ~ 281474976710655
����:UInt56(ulong)	����:7�ֽ�	ֵ��Χ:0 ~ 72057594037927935
����:UInt64(ulong)	����:8�ֽ�	ֵ��Χ:0 ~ 18446744073709551615

------------------ ������ 5�� ------------------
����:Float8(single)	�ֽڳ���:1	��Ч����:xλ		ֵ��Χ:0/255 ~ 255/255
����:Float16(single)�ֽڳ���:2	��Ч����:3λ		ֵ��Χ:��6.55E +4
����:Float24(single)		�ֽڳ���:3	��Ч����:5λ		ֵ��Χ:��1.8447E +19
����:Float32(single)		�ֽڳ���:4	��Ч����:7λ		ֵ��Χ:��3.402823E +38
����:Float64(double)		�ֽڳ���:8	��Ч����:15λ	ֵ��Χ:��1.7976931348623157E +308

------------------ �ɱ䳤������ 7�� ------------------
����:VarInt16(short)		����:1λ + 1~2�ֽ�	ֵ��Χ:ͬInt16
����:VarInt32(int)		����:2λ + 1~4�ֽ�	ֵ��Χ:ͬInt32
����:VarInt64(int)		����:3λ + 1~8�ֽ�	ֵ��Χ:ͬInt64

����:VarUInt16(short)	����:1λ + 1~2�ֽ�	ֵ��Χ:ͬUInt16
����:VarUInt32(int)		����:2λ + 1~4�ֽ�	ֵ��Χ:ͬUInt32
����:VarUInt64(int)		����:3λ + 1~8�ֽ�	ֵ��Χ:ͬUInt64
------------------ �ַ������� 1��(3�ֱ���) ------------------
����:UTF8		�ַ�����:1~4�ֽ�	���ֽڳ���:(1~5) + (0 ~ 1073741822)
����:Unicode	    �ַ�����:2�ֽ�	���ֽڳ���:(1~5) + (0 ~ 1073741822) x 2
����:ASCII		�ַ�����:1�ֽ�	���ֽڳ���:(1~5) + (0 ~ 1073741822)

------------------ �������� 2�� ------------------
֧��2��:
����:����		���ʽ:{��������}[]
// (δʵ��)����:�ֵ�		���ʽ:Dictionary<{��������}, {����}>
����:�Զ�������  ���ʽ:ֻҪ���ͻ������ͺ��������� ���������Զ�������

------------------ ���� ------------------
�����þ���С���������Ͷ����ֶΣ��������Լ������л�������ݴ�С
���ԶԻ����������� Bit,Int,Float,String �����Լ�ϲ���ļ�ƣ������д

------------------ �Զ������ͽṹ����(LiteByte Schema)���� ------------------
���������� ��������Ĭ��Ӧ�����¼������
Bit1      = bool
Int8      = sbyte
UInt8     = byte
VarInt32  = int
VarUnt32  = uint
VarInt64  = long
VarUInt64 = ulong
UTF8      = string

������������ �ṹ:
struct BaseTypeST {

	// ������
	bool boolValue;

	// �з�������
	sbyte sbyteValue;
	short shortValue;
	int intValue;
	long longValue;

	// �޷�������
	byte byteValue;
	ushort ushortValue;
	uint uintValue;
	ulong ulongValue;

	// �з��Ÿ�����
	float floatValue;
	double doubleValue;

	// �ַ���(UTF8)
	string stringValue;

}

���� �ṹ1:
struct ArrayST {
	int[] ids;	
}

���� �ṹ2:
struct ArrayST {
	string[] names;	
}

��¼��Ϣ �ṹ:
struct LoginInfoST {

	string username;
	string password;

}

�û���Ϣ �ṹ2:
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

============================== ���������Ͷ��ձ� ==============================
�����ͣ�
 ����    | ���� |     C#    | Java(δʵ��) | C++(δʵ��) | Go(δʵ��)   
Bit1      1λ       bool       boolean        char          bool
Bit2      2λ       byte         byte         char          uint8
Bit3      3λ       byte         byte         char          uint8
Bit4      4λ       byte         byte         char          uint8
Bit5      5λ       byte         byte         char          uint8
Bit6      6λ       byte         byte         char          uint8
Bit7      7λ       byte         byte         char          uint8

���ͣ�
Int8     1�ֽ�      sbyte        sbyte        char          int8
Int16    2�ֽ�      short        short        short         int16
Int24    3�ֽ�      int          int          int           int32
Int32    4�ֽ�      int          int          int           int32
Int40    5�ֽ�      long         long      long long        int64
Int48    6�ֽ�      long         long      long long        int64
Int56    7�ֽ�      long         long      long long        int64
Int64    8�ֽ�      long         long      long long        int64

UInt8    1�ֽ�      byte         byte         char          uint8
UInt16   2�ֽ�      ushort       ushort   unsigned short    uint16
UInt24   3�ֽ�      uint         uint     unsigned int      uint32
UInt32   4�ֽ�      uint         uint     unsigned int      uint32
UInt40   5�ֽ�      ulong        ulong  unsigned long long  uint64
UInt48   6�ֽ�      ulong        ulong  unsigned long long  uint64
UInt56   7�ֽ�      ulong        ulong  unsigned long long  uint64
UInt64   8�ֽ�      ulong        ulong  unsigned long long  uint64

�����ͣ�
Float8   1�ֽ�      float       float         float         float32
Float16  2�ֽ�      float       float         float         float32
Float24  3�ֽ�      float       float         float         float32
Float32  4�ֽ�      float       float         float         float32
Float64  8�ֽ�      double      double        double        float64

�䳤���ͣ�
VarInt16 1λ+1~2�ֽ� short      short         short         int16
VarInt32 2λ+1~4�ֽ� int        int           int           int32
VarInt64 3λ+1~8�ֽ� long       long          long long     int64

VarUInt16 1λ+1~2�ֽ� ushort    ushort  unsigned short      uint16
VarUInt32 2λ+1~4�ֽ� uint      uint    unsigned int        uint32
VarUInt64 3λ+1~8�ֽ� ulong     ulong   unsigned long long  uint64

�ַ�����:
UTF8    1~4�ֽ�     string      String        string        string
Unicode   2�ֽ�     string      String        string        string
ASCII     1�ֽ�     string      String        string        string
