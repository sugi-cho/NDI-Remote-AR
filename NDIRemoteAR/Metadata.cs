using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace rAR
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Metadata
    {
        public Vector3 position;
        public Quaternion rotation;
        public Matrix4x4 prjMatrix;

        public string Serialize()
        {
            ReadOnlySpan<Metadata> data = stackalloc[] { this };
            var bytes = MemoryMarshal.AsBytes(data).ToArray();
            return $"<![CDATA[{Convert.ToBase64String(bytes)}]] >";
        }
        public static Metadata Deserialize(string xml)
        {
            var base64 = xml.Substring(9, xml.Length - 9 - 4);
            var data = Convert.FromBase64String(base64);
            return MemoryMarshal.Read<Metadata>(new Span<byte>(data));
        }
    }
}