using System.IO;
using ProtoBuf;

/// <summary>
/// Protobuf����������
/// ZhangYu 2019-05-24
/// <para>blog:https://segmentfault.com/u/bingfengbaidu</para>
/// </summary>
public class ProtobufUtil {

    /// <summary> ���л� </summary>
    public static byte[] To<T>(T entity) {
        byte[] result = null;
        if (entity != null) {
            using (MemoryStream stream = new MemoryStream()) {
                Serializer.Serialize<T>(stream, entity);
                result = stream.ToArray();
            }
        }
        return result;
    }

    /// <summary> �����л� </summary>
    public static T From<T>(byte[] message) {
        T result = default(T);
        if (message != null) {
            using (MemoryStream stream = new MemoryStream(message)) {
                result = Serializer.Deserialize<T>(stream);
            }
        }
        return result;
    }
}