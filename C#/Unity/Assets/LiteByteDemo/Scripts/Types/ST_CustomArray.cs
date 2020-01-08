namespace LiteByte.Demo {

    using System;
    using ProtoBuf;
    using UnityEngine;

    /// <summary> 自定义结构数组测试 | custom array test </summary>
    [Serializable][ProtoContract]
    public struct ST_CustomArray {

        [ProtoMember(1)][SerializeField] public Vector3[] positions;
	
    }

}