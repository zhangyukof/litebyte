namespace LiteByte.Demo {

    #pragma warning disable 0219
    using UnityEngine;
    using System.Text;
    using System.Reflection;
    using System.Collections;
    using LiteByte;
    using LiteByte.Common;
    using System.Diagnostics;
    using UnityEngine.UI;
    using UnityEngine.Profiling;

    /// <summary>
    /// LiteByte Demo
    /// ZhangYu 2019-11-21
    /// </summary>
    public class LBDemo : MonoBehaviour {

        public LBInfoUI jsonUI;
        public LBInfoUI pbUI;
        public LBInfoUI lbUI;
        public InputField timesInput;
        public GameObject testBtnsObj;
        private int testTimes = 1000;

        private LBConfigLoader configLoader;
        private Stopwatch watch = new Stopwatch();

        private void Start() {
            ShowBtns(false);
            configLoader = new GameObject("LBConfigLoader").AddComponent<LBConfigLoader>();
            string url = Application.streamingAssetsPath + "/LBConfig.json";
            configLoader.LoadConfig(url, OnComplete, OnError);
            timesInput.text = testTimes.ToString();
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Application.Quit();
            }

            /*
            int ts = 100;

            LBObject obj = new LBObject();
            obj.SetFloat("x", 1.12345f);
            obj.SetFloat("y", 2.12345f);
            obj.SetFloat("z", 3.12345f);

            Vector3 p = new Vector3(1.12345f, 2.12345f, 3.12345f);

            string typeName = "Vector3";

            byte[] bytes = LBUtil.Serialize(typeName, obj);
            Profiler.BeginSample("LBObject");
            watch.Reset();
            watch.Start();
            for (int i = 0; i < ts; i++) {
                LBObject vo = LBUtil.Deserialize(typeName, bytes);
            }
            watch.Stop();
            Profiler.EndSample();
            //print("LBObject:" + watch.Elapsed.TotalMilliseconds);

            byte[] bytes2 = LBUtil.Serialize(typeName, p);
            Profiler.BeginSample("Sysytem.Object");
            watch.Reset();
            watch.Start();
            for (int i = 0; i < ts; i++) {
                Vector3 vo2 = LBUtil.Deserialize<Vector3>(typeName, bytes2);
            }
            watch.Stop();
            Profiler.EndSample();
            //print("Sysytem.Object:" + watch.Elapsed.TotalMilliseconds);
            */
        }

        private void OnComplete() {
            configLoader.Dispose();
            ShowBtns(true);
        }

        private void OnError() {
            configLoader.Dispose();
            //print("OnError");
        }

        #region BoolType Test
        public void BoolTypeTest() {
            ST_BoolType st = new ST_BoolType();
            st.b1 = true;
            st.b2 = true;
            st.b3 = true;
            st.b4 = true;
            st.b5 = true;
            st.b6 = true;
            st.b7 = true;
            st.b8 = true;

            LBObject lb = new LBObject();
            lb.SetBool("b1", st.b1);
            lb.SetBool("b2", st.b2);
            lb.SetBool("b3", st.b3);
            lb.SetBool("b4", st.b4);
            lb.SetBool("b5", st.b5);
            lb.SetBool("b6", st.b6);
            lb.SetBool("b7", st.b7);
            lb.SetBool("b8", st.b8);

            ClassTest("BoolType", st, lb);
        }
        #endregion

        #region IntType Test
        public void Int8TypeTest() {
            ST_Int8Type st = new ST_Int8Type();
            st.int8 = 127;

            LBObject lb = new LBObject();
            lb.SetSByte("int8", st.int8);

            ClassTest("Int8Type", st, lb);
        }

        public void Int16TypeTest() {
            ST_Int16Type st = new ST_Int16Type();
            st.int16 = 12345;

            LBObject lb = new LBObject();
            lb.SetShort("int16", st.int16);

            ClassTest("Int16Type", st, lb);
        }

        public void Int32TypeTest() {
            ST_Int32Type st = new ST_Int32Type();
            st.int32 = 123456;

            LBObject lb = new LBObject();
            lb.SetInt("int32", st.int32);

            ClassTest("Int32Type", st, lb);
        }

        public void Int64TypeTest() {
            ST_Int64Type st = new ST_Int64Type();
            st.int64 = 12345678;

            LBObject lb = new LBObject();
            lb.SetLong("int64", st.int64);

            ClassTest("Int64Type", st, lb);
        }

        public void UInt8TypeTest() {
            ST_UInt8Type st = new ST_UInt8Type();
            st.uint8 = 255;

            LBObject lb = new LBObject();
            lb.SetByte("uint8", st.uint8);

            ClassTest("UInt8Type", st, lb);
        }

        public void UInt16TypeTest() {
            ST_UInt16Type st = new ST_UInt16Type();
            st.uint16 = 255;

            LBObject lb = new LBObject();
            lb.SetUShort("uint16", st.uint16);

            ClassTest("UInt16Type", st, lb);
        }

        public void UInt32TypeTest() {
            ST_UInt32Type st = new ST_UInt32Type();
            st.uint32 = 123456789;

            LBObject lb = new LBObject();
            lb.SetUInt("uint32", st.uint32);

            ClassTest("UInt32Type", st, lb);
        }

        public void UInt64TypeTest() {
            ST_UInt64Type st = new ST_UInt64Type();
            st.uint64 = 1234567890;

            LBObject lb = new LBObject();
            lb.SetULong("uint64", st.uint64);

            ClassTest("UInt64Type", st, lb);
        }
        #endregion

        #region Float Type Test
        public void Float32TypeTest() {
            ST_Float32Type st = new ST_Float32Type();
            st.float32 = 1.234567f;

            LBObject lb = new LBObject();
            lb.SetFloat("float32", st.float32);

            ClassTest("Float32Type", st, lb);
        }

        public void Float64TypeTest() {
            ST_Float64Type st = new ST_Float64Type();
            st.float64 = 1.123456789f;

            LBObject lb = new LBObject();
            lb.SetDouble("float64", st.float64);

            ClassTest("Float64Type", st, lb);
        }
        #endregion

        #region VarInt Type Test
        public void VarInt16TypeTest() {
            ST_VarInt16Type st = new ST_VarInt16Type();
            st.vint16 = 1234;

            LBObject lb = new LBObject();
            lb.SetShort("vint16", st.vint16);

            ClassTest("VarInt16Type", st, lb);
        }

        public void VarInt32TypeTest() {
            ST_VarInt32Type st = new ST_VarInt32Type();
            st.vint32 = 12345;

            LBObject lb = new LBObject();
            lb.SetInt("vint32", st.vint32);

            ClassTest("VarInt32Type", st, lb);
        }

        public void VarInt64TypeTest() {
            ST_VarInt64Type st = new ST_VarInt64Type();
            st.vint64 = 123456;

            LBObject lb = new LBObject();
            lb.SetLong("vint64", st.vint64);

            ClassTest("VarInt64Type", st, lb);
        }

        public void VarUInt16TypeTest() {
            ST_VarUInt16Type st = new ST_VarUInt16Type();
            st.vuint16 = 1234;

            LBObject lb = new LBObject();
            lb.SetUShort("vuint16", st.vuint16);

            ClassTest("VarUInt16Type", st, lb);
        }

        public void VarUInt32TypeTest() {
            ST_VarUInt32Type st = new ST_VarUInt32Type();
            st.vuint32 = 12345;

            LBObject lb = new LBObject();
            lb.SetUInt("vuint32", st.vuint32);

            ClassTest("VarUInt32Type", st, lb);
        }

        public void VarUInt64TypeTest() {
            ST_VarUInt64Type st = new ST_VarUInt64Type();
            st.vuint64 = 123456;

            LBObject lb = new LBObject();
            lb.SetULong("vuint64", st.vuint64);

            ClassTest("VarUInt64Type", st, lb);
        }
        #endregion

        #region String Type Test
        public void StringUTF8TypeTest() {
            ST_UTF8Type st = new ST_UTF8Type();
            st.str = "UTF8";

            LBObject lb = new LBObject();
            lb.SetString("str", st.str);

            ClassTest("UTF8Type", st, lb);
        }

        public void StringUnicodeTypeTest() {
            ST_UnicodeType st = new ST_UnicodeType();
            st.str = "Unicode";

            LBObject lb = new LBObject();
            lb.SetString("str", st.str);

            ClassTest("UnicodeType", st, lb);
        }

        public void StringASCIITypeTest() {
            ST_ASCIIType st = new ST_ASCIIType();
            st.str = "ASCII";

            LBObject lb = new LBObject();
            lb.SetString("str", st.str);

            ClassTest("ASCIIType", st, lb);
        }
        #endregion

        #region Vector3 Test
        public void Vector3Test() {
            ST_Vector3 st = new ST_Vector3(1.123456f, 2.123456f, 3.123456f);

            LBObject lb = new LBObject();
            lb.SetFloat("x", st.x);
            lb.SetFloat("y", st.y);
            lb.SetFloat("z", st.z);

            ClassTest("Vector3", st, lb);
        }
        #endregion

        #region Color Test
        public void ColorTest() {
            ST_Color st = new ST_Color(1 / 255f, 2 / 255f, 3 / 255f);

            LBObject lb = new LBObject();
            lb.SetFloat("r", st.r);
            lb.SetFloat("g", st.g);
            lb.SetFloat("b", st.b);

            ClassTest("Color", st, lb);
        }
        #endregion

        #region Base Type Test
        public void BaseTypeTest() {
            // Class
            ST_BaseType st = new ST_BaseType();
            st.tBool = true;
            st.tSByte = sbyte.MaxValue;
            st.tShort = short.MaxValue;
            st.tInt = int.MaxValue;
            st.tLong = long.MaxValue;
            st.tByte = byte.MaxValue;
            st.tUShort = ushort.MaxValue;
            st.tUInt = uint.MaxValue;
            st.tULong = ulong.MaxValue;
            st.tFloat = 123.4567f;
            st.tDouble = 12345.6789d;
            st.tString = "Hello World";

            // LBObject
            LBObject lb = new LBObject();
            lb.SetBool("tBool", st.tBool);
            lb.SetSByte("tSByte", st.tSByte);
            lb.SetShort("tShort", st.tShort);
            lb.SetInt("tInt", st.tInt);
            lb.SetLong("tLong", st.tLong);
            lb.SetByte("tByte", st.tByte);
            lb.SetUShort("tUShort", st.tUShort);
            lb.SetUInt("tUInt", st.tUInt);
            lb.SetULong("tULong", st.tULong);
            lb.SetFloat("tFloat", st.tFloat);
            lb.SetDouble("tDouble", st.tDouble);
            lb.SetString("tString", st.tString);

            ClassTest("BaseType", st, lb);
        }
        #endregion

        #region Array Test
        public void ArrayTest() {
            // Class
            ST_Array st = new ST_Array();
            st.bools = new bool[] { true, false, true, false, true, false, true, false };
            st.sbytes = new sbyte[] { sbyte.MinValue, sbyte.MaxValue };
            st.shorts = new short[] { short.MinValue, short.MaxValue };
            st.ints = new int[] { int.MaxValue, int.MaxValue };
            st.longs = new long[] { long.MinValue, long.MaxValue };
            st.bytes = new byte[] { byte.MinValue, byte.MaxValue };
            st.ushorts = new ushort[] { ushort.MinValue, ushort.MaxValue };
            st.uints = new uint[] { uint.MaxValue, uint.MaxValue };
            st.ulongs = new ulong[] { ulong.MinValue, ulong.MaxValue };
            st.floats = new float[] { 0.1234567f, float.MinValue, float.MaxValue };
            st.doubles = new double[] { 0.12345678987654321d, double.MinValue, double.MaxValue };
            st.strings = new string[] { "AAA", "BBB", "CCC" };

            // LBObject
            LBObject lb = new LBObject();
            lb.SetBoolArray("bools", st.bools);
            lb.SetSByteArray("sbytes", st.sbytes);
            lb.SetShortArray("shorts", st.shorts);
            lb.SetIntArray("ints", st.ints);
            lb.SetLongArray("longs", st.longs);
            lb.SetByteArray("bytes", st.bytes);
            lb.SetUShortArray("ushorts", st.ushorts);
            lb.SetUIntArray("uints", st.uints);
            lb.SetULongArray("ulongs", st.ulongs);
            lb.SetFloatArray("floats", st.floats);
            lb.SetDoubleArray("doubles", st.doubles);
            lb.SetStringArray("strings", st.strings);

            ClassTest("Array", st, lb);
        }
        #endregion

        #region Custom Array Test
        public void CustomArrayTest() {
            // Class
            ST_CustomArray st = new ST_CustomArray();
            Vector3[] positions = new Vector3[] {
                new Vector3(1.1f, 1.2f, 1.3f),
                new Vector3(2.1f, 2.2f, 2.3f),
                new Vector3(3.1f, 3.2f, 3.3f),
                new Vector3(4.1f, 4.2f, 4.3f),
                new Vector3(5.1f, 5.2f, 5.3f)
            };
            st.positions = positions;

            // LBObject
            LBObject lb = new LBObject();
            LBObject[] ps = new LBObject[positions.Length];
            for (int i = 0; i < ps.Length; i++) {
                Vector3 position = positions[i];
                LBObject p = new LBObject();
                p.SetFloat("x", position.x);
                p.SetFloat("y", position.y);
                p.SetFloat("z", position.z);
                ps[i] = p;
            }
            lb.SetObjectArray("positions", ps);
            ClassTest("CustomArray", st, lb);
        }
        #endregion

        #region NestedTypeTest
        public void NestedTypeTest() {
            // Class
            ST_PlayerInfo playerInfo = new ST_PlayerInfo();
            playerInfo.id = 100001;
            playerInfo.nickname = "Zero";
            playerInfo.gender = 1;
            playerInfo.isVip = true;
            playerInfo.lv = 999;
            playerInfo.hp = 99999;
            playerInfo.mp = 99999;
            playerInfo.exp = 99999999;
            playerInfo.speed = 2.5f;
            ST_Vector3 position = new ST_Vector3(1.123456f, 2.123456f, 3.123456f);
            ST_Color color = new ST_Color(1 / 255f, 2 / 255f, 3 / 255f);
            ST_NestedType st = new ST_NestedType();
            st.playerInfo = playerInfo;
            st.position = position;
            st.color = color;

            // LBObject
            LBObject lb = new LBObject();
            LBObject lbPlayerInfo = new LBObject();
            LBObject lbPosition = new LBObject();
            LBObject lbColor = new LBObject();

            lbPlayerInfo.SetUInt("id", playerInfo.id);
            lbPlayerInfo.SetString("nickname", playerInfo.nickname);
            lbPlayerInfo.SetByte("gender", playerInfo.gender);
            lbPlayerInfo.SetBool("isVip", playerInfo.isVip);
            lbPlayerInfo.SetInt("lv", playerInfo.lv);
            lbPlayerInfo.SetInt("hp", playerInfo.hp);
            lbPlayerInfo.SetInt("mp", playerInfo.mp);
            lbPlayerInfo.SetInt("exp", playerInfo.exp);
            lbPlayerInfo.SetFloat("speed", playerInfo.speed);

            lbPosition.SetFloat("x", position.x);
            lbPosition.SetFloat("y", position.y);
            lbPosition.SetFloat("z", position.z);

            lbColor.SetFloat("r", color.r);
            lbColor.SetFloat("g", color.g);
            lbColor.SetFloat("b", color.b);

            lb.SetObject("playerInfo", lbPlayerInfo);
            lb.SetObject("position", lbPosition);
            lb.SetObject("color", lbColor);

            ClassTest("NestedType", st, lb);

            /*
            // Convert
            string structName = "NestedType";
            byte[] bytes = LB.ToBytes(structName, st);
            //print("==================== NestedType Test ====================");

            // Parse
            ST_NestedType vo = LB.ToObject<ST_NestedType>(structName, bytes);

            // Print
            //print("LB ByteSize: " + bytes.Length);
            ST_PlayerInfo pvo = vo.playerInfo;
            //print("vo.player.id: " + pvo.id);
            //print("vo.player.nickname: " + pvo.nickname);
            //print("vo.player.gender: " + pvo.gender);
            //print("vo.player.isVip: " + pvo.isVip);
            //print("vo.player.lv: " + pvo.lv);
            //print("vo.player.hp: " + pvo.hp);
            //print("vo.player.mp: " + pvo.mp);
            //print("vo.player.exp: " + pvo.exp);
            //print("vo.player.speed: " + pvo.speed);
            PrintVector3("vo.position: ", vo.position);
            PrintColor("vo.color: ", vo.color);
            */
        }
        #endregion

        #region All Types Test
        public void AllTypeTest() {
            // ========================== Class ===============================
            ST_AllType st = new ST_AllType();
            // Bit
            st.bit1 = true;
            st.bit2 = 2;
            st.bit3 = 3;
            st.bit4 = 4;
            st.bit5 = 5;
            st.bit6 = 6;
            st.bit7 = 7;

            // Int
            st.int8 = 8;
            st.int16 = 16;
            st.int24 = 24;
            st.int32 = 32;
            st.int40 = 40;
            st.int48 = 48;
            st.int56 = 56;
            st.int64 = 64;

            // UInt
            st.uint8 = 8;
            st.uint16 = 16;
            st.uint24 = 24;
            st.uint32 = 32;
            st.uint40 = 40;
            st.uint48 = 48;
            st.uint56 = 56;
            st.uint64 = 64;

            // Float
            st.float8 = 1 / 255f;
            st.float16 = 0.12345f;
            st.float24 = 0.1234567f;
            st.float32 = float.MaxValue;
            st.float64 = double.MaxValue;

            // VarInt
            st.vint16 = short.MinValue;
            st.vint32 = int.MinValue;
            st.vint64 = long.MinValue;

            // VarUInt
            st.vuint16 = ushort.MaxValue;
            st.vuint32 = uint.MaxValue;
            st.vuint64 = ulong.MaxValue;

            // String
            st.utf8 = "UTF8 String";
            st.unicode = "Unicode String";
            st.ascii = "ASCII String";

            // CustomType
            st.position = new ST_Vector3(1.12345f, 2.12345f, 3.12345f);

            // BitArray
            bool[] bit1Array = new bool[] { true, false, true, false, true, false, true, false };
            byte[] bit2Array = new byte[4];
            byte[] bit3Array = new byte[8];
            byte[] bit4Array = new byte[16];
            byte[] bit5Array = new byte[32];
            byte[] bit6Array = new byte[64];
            byte[] bit7Array = new byte[128];
            for (int i = 0; i < bit2Array.Length; i++) {
                bit2Array[i] = (byte)i;
            }
            for (int i = 0; i < bit3Array.Length; i++) {
                bit3Array[i] = (byte)i;
            }
            for (int i = 0; i < bit4Array.Length; i++) {
                bit4Array[i] = (byte)i;
            }
            for (int i = 0; i < bit5Array.Length; i++) {
                bit5Array[i] = (byte)i;
            }
            for (int i = 0; i < bit6Array.Length; i++) {
                bit6Array[i] = (byte)i;
            }
            for (int i = 0; i < bit7Array.Length; i++) {
                bit7Array[i] = (byte)i;
            }
            st.bit1Array = bit1Array;
            st.bit2Array = bit2Array;
            st.bit3Array = bit3Array;
            st.bit4Array = bit4Array;
            st.bit5Array = bit5Array;
            st.bit6Array = bit6Array;
            st.bit7Array = bit7Array;

            // IntArray
            sbyte[] int8Array = new sbyte[] { 0, 1, 2, sbyte.MinValue, sbyte.MaxValue };
            short[] int16Array = new short[] { 0, 1, 2, short.MinValue, short.MaxValue };
            int[] int24Array = new int[] { 0, 1, 2, LBInt24.MinValue, LBInt24.MaxValue };
            int[] int32Array = new int[] { 0, 1, 2, int.MinValue, int.MaxValue };
            long[] int40Array = new long[] { 0, 1, 2, LBInt40.MinValue, LBInt40.MaxValue };
            long[] int48Array = new long[] { 0, 1, 2, LBInt48.MinValue, LBInt48.MaxValue };
            long[] int56Array = new long[] { 0, 1, 2, LBInt56.MinValue, LBInt56.MaxValue };
            long[] int64Array = new long[] { 0, 1, 2, long.MinValue, long.MaxValue };
            st.int8Array = int8Array;
            st.int16Array = int16Array;
            st.int24Array = int24Array;
            st.int32Array = int32Array;
            st.int40Array = int40Array;
            st.int48Array = int48Array;
            st.int56Array = int56Array;
            st.int64Array = int64Array;

            // UIntArray
            byte[] uint8Array = new byte[] { 0, 1, 2, byte.MinValue, byte.MaxValue };
            ushort[] uint16Array = new ushort[] { 0, 1, 2, ushort.MinValue, ushort.MaxValue };
            uint[] uint24Array = new uint[] { 0, 1, 2, LBUInt24.MinValue, LBUInt24.MaxValue };
            uint[] uint32Array = new uint[] { 0, 1, 2, uint.MinValue, uint.MaxValue };
            ulong[] uint40Array = new ulong[] { 0, 1, 2, LBUInt40.MinValue, LBUInt40.MaxValue };
            ulong[] uint48Array = new ulong[] { 0, 1, 2, LBUInt48.MinValue, LBUInt48.MaxValue };
            ulong[] uint56Array = new ulong[] { 0, 1, 2, LBUInt56.MinValue, LBUInt56.MaxValue };
            ulong[] uint64Array = new ulong[] { 0, 1, 2, ulong.MinValue, ulong.MaxValue };
            st.uint8Array = uint8Array;
            st.uint16Array = uint16Array;
            st.uint24Array = uint24Array;
            st.uint32Array = uint32Array;
            st.uint40Array = uint40Array;
            st.uint48Array = uint48Array;
            st.uint56Array = uint56Array;
            st.uint64Array = uint64Array;

            // FloatArray
            float[] float8Array = new float[] { 1/255F, 2/255F, LBFloat8.MinValue, LBFloat8.MaxValue };
            float[] float16Array = new float[] { 1 / 255F, -1 / 255F, LBFloat16.MinValue, LBFloat16.MaxValue };
            float[] float24Array = new float[] { 1 / 255F, -1 / 255F, LBFloat24.MinValue, LBFloat24.MaxValue };
            float[] float32Array = new float[] { 1 / 255F, -1 / 255F, LBFloat32.MinValue, LBFloat32.MaxValue };
            double[] float64Array = new double[] { 1 / 255d, -1 / 255F, LBFloat64.MinValue, LBFloat64.MaxValue };
            st.float8Array = float8Array;
            st.float16Array = float16Array;
            st.float24Array = float24Array;
            st.float32Array = float32Array;
            st.float64Array = float64Array;

            // Variable Integer Array
            short[] varInt16Array = new short[] { 1, -1, short.MinValue, short.MaxValue };
            int[] varInt32Array = new int[] { 1, -1, int.MinValue, int.MaxValue };
            long[] varInt64Array = new long[] { 1, -1, long.MaxValue, long.MinValue };
            ushort[] varUInt16Array = new ushort[] { 1, 2, ushort.MinValue, ushort.MaxValue };
            uint[] varUInt32Array = new uint[] { 1, 2, uint.MinValue, uint.MaxValue };
            ulong[] varUInt64Array = new ulong[] { 1, 2, ulong.MinValue, ulong.MaxValue };
            st.vint16Array = varInt16Array;
            st.vint32Array = varInt32Array;
            st.vint64Array = varInt64Array;
            st.vuint16Array = varUInt16Array;
            st.vuint32Array = varUInt32Array;
            st.vuint64Array = varUInt64Array;

            // StringArray
            st.utf8Array = new string[] { "utf8_1", "utf8_2", "utf8_3" };
            st.unicodeArray = new string[] { "unicode_1", "unicode_2", "unicode_3" };
            st.asciiArray = new string[] { "ascii_1", "ascii_2", "ascii_3" };

            // ========================== LBObject ===============================
            LBObject lb = new LBObject();

            // Bit
            lb.SetBool("bit1", st.bit1);
            lb.SetByte("bit2", st.bit2);
            lb.SetByte("bit3", st.bit3);
            lb.SetByte("bit4", st.bit4);
            lb.SetByte("bit5", st.bit5);
            lb.SetByte("bit6", st.bit6);
            lb.SetByte("bit7", st.bit7);

            // Int
            lb.SetSByte("int8", st.int8);
            lb.SetShort("int16", st.int16);
            lb.SetInt("int24", st.int24);
            lb.SetInt("int32", st.int32);
            lb.SetLong("int40", st.int40);
            lb.SetLong("int48", st.int48);
            lb.SetLong("int56", st.int56);
            lb.SetLong("int64", st.int64);

            // UInt
            lb.SetByte("uint8", st.uint8);
            lb.SetUShort("uint16", st.uint16);
            lb.SetUInt("uint24", st.uint24);
            lb.SetUInt("uint32", st.uint32);
            lb.SetULong("uint40", st.uint40);
            lb.SetULong("uint48", st.uint48);
            lb.SetULong("uint56", st.uint56);
            lb.SetULong("uint64", st.uint64);

            // Float
            lb.SetFloat("float8", st.float8);
            lb.SetFloat("float16", st.float16);
            lb.SetFloat("float24", st.float24);
            lb.SetFloat("float32", st.float32);
            lb.SetDouble("float64", st.float64);

            // VarInt
            lb.SetShort("vint16", st.vint16);
            lb.SetInt("vint32", st.vint32);
            lb.SetLong("vint64", st.vint64);

            // VarUInt
            lb.SetUShort("vuint16", st.vuint16);
            lb.SetUInt("vuint32", st.vuint32);
            lb.SetULong("vuint64", st.vuint64);

            // String
            lb.SetString("utf8", st.utf8);
            lb.SetString("unicode", st.unicode);
            lb.SetString("ascii", st.ascii);

            // CustomType
            LBObject lbPosition = new LBObject();
            lbPosition.SetFloat("x", st.position.x);
            lbPosition.SetFloat("y", st.position.y);
            lbPosition.SetFloat("z", st.position.z);
            lb.SetObject("position", lbPosition);

            // BitArray
            lb.SetBoolArray("bit1Array", st.bit1Array);
            lb.SetByteArray("bit2Array", st.bit2Array);
            lb.SetByteArray("bit3Array", st.bit3Array);
            lb.SetByteArray("bit4Array", st.bit4Array);
            lb.SetByteArray("bit5Array", st.bit5Array);
            lb.SetByteArray("bit6Array", st.bit6Array);
            lb.SetByteArray("bit7Array", st.bit7Array);

            // IntArray
            lb.SetSByteArray("int8Array", st.int8Array);
            lb.SetShortArray("int16Array", st.int16Array);
            lb.SetIntArray("int24Array", st.int24Array);
            lb.SetIntArray("int32Array", st.int32Array);
            lb.SetLongArray("int40Array", st.int40Array);
            lb.SetLongArray("int48Array", st.int48Array);
            lb.SetLongArray("int56Array", st.int56Array);
            lb.SetLongArray("int64Array", st.int64Array);

            // UIntArray
            lb.SetByteArray("uint8Array", st.uint8Array);
            lb.SetUShortArray("uint16Array", st.uint16Array);
            lb.SetUIntArray("uint24Array", st.uint24Array);
            lb.SetUIntArray("uint32Array", st.uint32Array);
            lb.SetULongArray("uint40Array", st.uint40Array);
            lb.SetULongArray("uint48Array", st.uint48Array);
            lb.SetULongArray("uint56Array", st.uint56Array);
            lb.SetULongArray("uint64Array", st.uint64Array);

            // FloatArray
            lb.SetFloatArray("float8Array", st.float8Array);
            lb.SetFloatArray("float16Array", st.float16Array);
            lb.SetFloatArray("float24Array", st.float24Array);
            lb.SetFloatArray("float32Array", st.float32Array);
            lb.SetDoubleArray("float64Array", st.float64Array);

            // Variable Integer Array
            lb.SetShortArray("vint16Array", st.vint16Array);
            lb.SetIntArray("vint32Array", st.vint32Array);
            lb.SetLongArray("vint64Array", st.vint64Array);
            lb.SetUShortArray("vuint16Array", st.vuint16Array);
            lb.SetUIntArray("vuint32Array", st.vuint32Array);
            lb.SetULongArray("vuint64Array", st.vuint64Array);

            // StringArray
            lb.SetStringArray("utf8Array", st.utf8Array);
            lb.SetStringArray("unicodeArray", st.unicodeArray);
            lb.SetStringArray("asciiArray", st.asciiArray);

            ClassTest("AllType", st, lb);

            #if UNITY_EDITOR
            // Convert
            //print("==================== AllType Test ====================");
            //string structName = "AllType";
            //byte[] bytes = LBUtil.Serialize(structName, st);
            //print("ByteSize: " + bytes.Length);

            // Parse
            //ST_AllType vo = LBUtil.Deserialize<ST_AllType>(structName, bytes);
            //print("LB.ToObject:vo");

            // Bit
            //print("vo.bit1: " + vo.bit1);
            //print("vo.bit2: " + vo.bit2);
            //print("vo.bit3: " + vo.bit3);
            //print("vo.bit4: " + vo.bit4);
            //print("vo.bit5: " + vo.bit5);
            //print("vo.bit6: " + vo.bit6);
            //print("vo.bit7: " + vo.bit7);

            // Int
            //print("vo.int8: " + vo.int8);
            //print("vo.int16: " + vo.int16);
            //print("vo.int24: " + vo.int24);
            //print("vo.int32: " + vo.int32);
            //print("vo.int40: " + vo.int40);
            //print("vo.int48: " + vo.int48);
            //print("vo.int56: " + vo.int56);
            //print("vo.int64: " + vo.int64);

            // UInt
            //print("vo.uint8: " + vo.uint8);
            //print("vo.uint16: " + vo.uint16);
            //print("vo.uint24: " + vo.uint24);
            //print("vo.uint32: " + vo.uint32);
            //print("vo.uint40: " + vo.uint40);
            //print("vo.uint48: " + vo.uint48);
            //print("vo.uint56: " + vo.uint56);
            //print("vo.uint64: " + vo.uint64);

            // Float
            //print("vo.float8: " + vo.float8);
            //print("vo.float16: " + vo.float16);
            //print("vo.float24: " + vo.float24);
            //print("vo.float32: " + vo.float32);
            //print("vo.float64: " + vo.float64);

            // VarInt
            //print("vo.vint16: " + vo.vint16);
            //print("vo.vint32: " + vo.vint32);
            //print("vo.vint64: " + vo.vint64);

            // VarUInt
            //print("vo.vunt16: " + vo.vint16);
            //print("vo.vuint32: " + vo.vint32);
            //print("vo.vuint64: " + vo.vint64);

            // BitArray
            //PrintArray("vo.bit1Array: ", vo.bit1Array);
            //PrintArray("vo.bit2Array: ", vo.bit2Array);
            //PrintArray("vo.bit3Array: ", vo.bit3Array);
            //PrintArray("vo.bit4Array: ", vo.bit4Array);
            //PrintArray("vo.bit5Array: ", vo.bit5Array);
            //PrintArray("vo.bit6Array: ", vo.bit6Array);
            //PrintArray("vo.bit7Array: ", vo.bit7Array);

            // IntArray
            //PrintArray("vo.int8Array: ", vo.int8Array);
            //PrintArray("vo.int16Array: ", vo.int16Array);
            //PrintArray("vo.int23Array: ", vo.int24Array);
            //PrintArray("vo.int32Array: ", vo.int32Array);
            //PrintArray("vo.int40Array: ", vo.int40Array);
            //PrintArray("vo.int48Array: ", vo.int48Array);
            //PrintArray("vo.int56Array: ", vo.int56Array);
            //PrintArray("vo.int64Array: ", vo.int64Array);

            // UIntArray
            //PrintArray("vo.uint8Array: ", vo.uint8Array);
            //PrintArray("vo.uint16Array: ", vo.uint16Array);
            //PrintArray("vo.uint24Array: ", vo.uint24Array);
            //PrintArray("vo.uint32Array: ", vo.uint32Array);
            //PrintArray("vo.uint40Array: ", vo.uint40Array);
            //PrintArray("vo.uint48Array: ", vo.uint48Array);
            //PrintArray("vo.uint56Array: ", vo.uint56Array);
            //PrintArray("vo.uint64Array: ", vo.uint64Array);

            // FloatArray
            //PrintArray("vo.float8Array: ", vo.float8Array);
            //PrintArray("vo.float16Array: ", vo.float16Array);
            //PrintArray("vo.float24Array: ", vo.float24Array);
            //PrintArray("vo.float32Array: ", vo.float32Array);
            //PrintArray("vo.float64Array: ", vo.float64Array);

            // VarIntArray
            //PrintArray("vo.vint16Array: ", vo.vint16Array);
            //PrintArray("vo.vint32Array: ", vo.vint32Array);
            //PrintArray("vo.vint64Array: ", vo.vint64Array);
            //PrintArray("vo.vlengthArray: ", vo.vlengthArray);

            //PrintArray("vo.vuint16Array: ", vo.vuint16Array);
            //PrintArray("vo.vuint32Array: ", vo.vuint32Array);
            //PrintArray("vo.vuint64Array: ", vo.vuint64Array);

            // StringArray
            //PrintArray("vo.utf8Array: ", vo.utf8Array);
            //PrintArray("vo.unicodeArray: ", vo.unicodeArray);
            //PrintArray("vo.asciiArray: ", vo.asciiArray);
            #endif
        }
        #endregion

        #region ClassType Test
        public void ClassTypeTest() {
            // Class
            ClassType cls = new ClassType();
            ClassType2 cls2 = new ClassType2(1, 2);
            cls.c = cls2;

            // LBObject
            LBObject lb = new LBObject();
            LBObject lb2 = new LBObject();
            lb2.SetInt("x", cls2.x);
            lb2.SetInt("y", cls2.y);
            lb.SetObject("c", lb2);

            ClassTest("ClassType", cls, lb);
        }
        #endregion

        #region PlayerInfo Test
        public void PlayerInfoTest() {
            ST_PlayerInfo st = new ST_PlayerInfo();
            st.id = 100001;
            st.nickname = "冰封百度";
            st.gender = 1;
            st.isVip = true;
            st.lv = 999;
            st.hp = 999999;
            st.mp = 999999;
            st.exp = 9999999;
            st.speed = 100.5f;

            ClassTest("PlayerInfo", st);
        }
        #endregion

        #region Tools
        private void ClassTest<T>(string typeName, T obj, LBObject lbObj = null) where T : new() {
            JsonTest(obj);
            PBTest(obj);
            LBTest(typeName, obj, lbObj);
        }

        private void JsonTest<T>(T obj) where T : new() {
            // JSON Serialize
            try {
                // JsonUtility.ToJSON()有BUG 会把空的字段赋值 这里用拷贝对象代替原对象
                obj = (T)Copy(obj);
                jsonUI.Clear();
                StartTimer();
                string jsonText = JsonUtility.ToJson(obj);
                StopTimer();
                string jsonByteSize = Encoding.UTF8.GetByteCount(jsonText).ToString();
            
                if (Application.isEditor) Profiler.BeginSample("Json Serialize");
                StartTimer();
                for (int i = 0; i < testTimes; i++) {
                    string s = JsonUtility.ToJson(obj);
                }
                StopTimer();
                if (Application.isEditor) Profiler.EndSample();
                string jsonSerialTime = GetMilliseconds();

                // JSON Deserialize
                if (Application.isEditor) Profiler.BeginSample("Json Deserialize");
                StartTimer();
                for (int i = 0; i < testTimes; i++) {
                    T v = JsonUtility.FromJson<T>(jsonText);
                }
                StopTimer();
                if (Application.isEditor) Profiler.EndSample();
                string jsonDeserialTime = GetMilliseconds();
            
                // JSON Test Info
                jsonUI.text = jsonText;
                jsonUI.byteSize = jsonByteSize;
                jsonUI.serializeTime = jsonSerialTime;
                jsonUI.deserializeTime = jsonDeserialTime;
            } catch (System.Exception e) {
                jsonUI.text = e.ToString();
                jsonUI.byteSize = "-";
                jsonUI.serializeTime = "-";
                jsonUI.deserializeTime = "-";
            }
            jsonUI.Refresh();
        }

        private object Copy(object obj) {
            if (obj == null) return null;
            System.Type type = obj.GetType();
            object copy = System.Activator.CreateInstance(obj.GetType());
            FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            for (int i = 0; i < fieldInfos.Length; i++) {
                FieldInfo fieldInfo = fieldInfos[i];
                fieldInfo.SetValue(copy, fieldInfo.GetValue(obj));
            }
            return copy;
        }

        private void PBTest<T>(T obj) where T : new() {
            try {
                // Protobuf Serialize
                pbUI.Clear();
                StartTimer();
                byte[] pbBytes = ProtobufUtil.To(obj);
                StopTimer();
                string pbText = GetString(pbBytes);
                string pbByteSize = pbBytes.Length.ToString();

                if (Application.isEditor) Profiler.BeginSample("Protobuf Serialize");
                StartTimer();
                for (int i = 0; i < testTimes; i++) {
                    byte[] bs = ProtobufUtil.To(obj);
                }
                StopTimer();
                if (Application.isEditor) Profiler.EndSample();
                string pbSerialTime = GetMilliseconds();

                // Protobuf Deserialize
                if (Application.isEditor) Profiler.BeginSample("Protobuf Deserialize");
                StartTimer();
                for (int i = 0; i < testTimes; i++) {
                    T v = ProtobufUtil.From<T>(pbBytes);
                }
                StopTimer();
                if (Application.isEditor) Profiler.EndSample();
                string pbDeserialTime = GetMilliseconds();

                // Protobuf Test Info
                pbUI.text = pbText;
                pbUI.byteSize = pbByteSize;
                pbUI.serializeTime = pbSerialTime;
                pbUI.deserializeTime = pbDeserialTime;
            } catch (System.Exception e) {
                pbUI.text = e.ToString();
                pbUI.byteSize = "-";
                pbUI.serializeTime = "-";
                pbUI.deserializeTime = "-";
            }
            pbUI.Refresh();
        }

        private void LBTest<T>(string typeName, T obj, LBObject lbObj = null) where T : new() {
            try {
                // LiteByte Serialize
                lbUI.Clear();
                StartTimer();
                byte[] lbBytes = LBUtil.Serialize(typeName, obj);
                StopTimer();
                string lbText = GetString(lbBytes);
                string lbByteSize = lbBytes.Length.ToString();

                if (Application.isEditor) Profiler.BeginSample("LiteByte Serialize(System.Object)");
                StartTimer();
                for (int i = 0; i < testTimes; i++) {
                    byte[] bs = LBUtil.Serialize(typeName, obj);
                }
                StopTimer();
                if (Application.isEditor) Profiler.EndSample();
                string lbSerialTime = GetMilliseconds();

                // Serialize LBObject
                if (lbObj != null) {
                    if (Application.isEditor) Profiler.BeginSample("LiteByte Serialize(LBObject)");
                    StartTimer();
                    for (int i = 0; i < testTimes; i++) {
                        byte[] bs = LBUtil.Serialize(typeName, lbObj);
                    }
                    StopTimer();
                    if (Application.isEditor) Profiler.EndSample();
                    lbSerialTime += "|" + GetMilliseconds();
                }

                // LiteByte Deserialize
                if (Application.isEditor) Profiler.BeginSample("LiteByte Deserialize(System.Object)");
                StartTimer();
                for (int i = 0; i < testTimes; i++) {
                    T v = LBUtil.Deserialize<T>(typeName, lbBytes);
                }
                StopTimer();
                if (Application.isEditor) Profiler.EndSample();
                string lbDeserialTime = GetMilliseconds();

                // Deserialize LBObject
                if (lbObj != null) {
                    byte[] lbBytes2 = LBUtil.Serialize(typeName, lbObj);
                    lbByteSize += "|" + lbBytes2.Length;
                    if (Application.isEditor) Profiler.BeginSample("LiteByte Deserialize(LBObject)");
                    StartTimer();
                    for (int i = 0; i < testTimes; i++) {
                        LBObject lb = LBUtil.Deserialize(typeName, lbBytes2);
                    }
                    StopTimer();
                    if (Application.isEditor) Profiler.EndSample();
                    lbDeserialTime += "|" + GetMilliseconds();
                }

                lbUI.text = lbText;
                lbUI.byteSize = lbByteSize;
                lbUI.serializeTime = lbSerialTime;
                lbUI.deserializeTime = lbDeserialTime;
            } catch (System.Exception e) {
                // LiteByte Test Info
                lbUI.text = e.ToString();
                lbUI.byteSize = "-";
                lbUI.serializeTime = "-";
                lbUI.deserializeTime = "-";
            }
            lbUI.Refresh();
        }

        private string GetString(byte[] bytes) {
            return System.BitConverter.ToString(bytes);
        }

        private void PrintArray(string prefix, IList array) {
            StringBuilder sb = new StringBuilder();
            string type = array.ToString().Replace("System.", "").Replace("[]", "[" + array.Count + "]");
            sb.Append(prefix);
            sb.Append(type + " {").Replace("System.", "").Replace("[]", "");
            for (int i = 0; i < array.Count; i++) {
                if (i > 0) sb.Append(", ");
                sb.Append(array[i]);
            }
            sb.Append('}');
            //print(sb);
        }

        private void PrintVector3(string prefix, ST_Vector3 vector3) {
            //print(prefix + "(" + vector3.x + ", " + vector3.y + ", " + vector3.z + ")");
        }

        private void PrintColor(string prefix, ST_Color color) {
            //print(prefix + "(" + color.r + ", " + color.g + ", " + color.b + ")");
        }

        private void StartTimer() {            
            watch.Reset();
            watch.Start();
        }

        private void StopTimer() {
            watch.Stop();
            //System.Threading.Thread.Sleep(1);
        }

        private string GetMilliseconds() {
            return watch.Elapsed.TotalMilliseconds.ToString();
        }

        private void UpdateUI() {
            jsonUI.Refresh();
            pbUI.Refresh();
            lbUI.Refresh();
        }

        public void UpdateTimes() {
            testTimes = int.Parse(timesInput.text);
        }

        private void ShowBtns(bool isActive) {
            testBtnsObj.SetActive(isActive);
        }
        #endregion

    }

}
