#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace MessagePack.Resolvers
{
    using System;
    using MessagePack;

    public class GeneratedResolver : global::MessagePack.IFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new GeneratedResolver();

        GeneratedResolver()
        {

        }

        public global::MessagePack.Formatters.IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly global::MessagePack.Formatters.IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                var f = GeneratedResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::MessagePack.Formatters.IMessagePackFormatter<T>)f;
                }
            }
        }
    }

    internal static class GeneratedResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static GeneratedResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(15)
            {
                {typeof(global::System.Collections.Generic.List<string>), 0 },
                {typeof(global::System.Collections.Generic.List<global::UnityEngine.Vector3>), 1 },
                {typeof(global::System.Collections.Generic.List<global::UnityEngine.Quaternion>), 2 },
                {typeof(global::System.Collections.Generic.List<int>), 3 },
                {typeof(global::Synchro.ISynchroCommand), 4 },
                {typeof(global::Synchro.SpatialStatus), 5 },
                {typeof(global::Synchro.TransformsStatusUpdate), 6 },
                {typeof(global::Synchro.Register), 7 },
                {typeof(global::Synchro.UpdatePresence), 8 },
                {typeof(global::Synchro.SpawnObject), 9 },
                {typeof(global::Synchro.DeleteObject), 10 },
                {typeof(global::Synchro.ChangePermission), 11 },
                {typeof(global::Synchro.ReCalibrate), 12 },
                {typeof(global::Synchro.Ping), 13 },
                {typeof(global::Synchro.Test.TestCommand), 14 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                case 0: return new global::MessagePack.Formatters.ListFormatter<string>();
                case 1: return new global::MessagePack.Formatters.ListFormatter<global::UnityEngine.Vector3>();
                case 2: return new global::MessagePack.Formatters.ListFormatter<global::UnityEngine.Quaternion>();
                case 3: return new global::MessagePack.Formatters.ListFormatter<int>();
                case 4: return new MessagePack.Formatters.Synchro.ISynchroCommandFormatter();
                case 5: return new MessagePack.Formatters.Synchro.SpatialStatusFormatter();
                case 6: return new MessagePack.Formatters.Synchro.TransformsStatusUpdateFormatter();
                case 7: return new MessagePack.Formatters.Synchro.RegisterFormatter();
                case 8: return new MessagePack.Formatters.Synchro.UpdatePresenceFormatter();
                case 9: return new MessagePack.Formatters.Synchro.SpawnObjectFormatter();
                case 10: return new MessagePack.Formatters.Synchro.DeleteObjectFormatter();
                case 11: return new MessagePack.Formatters.Synchro.ChangePermissionFormatter();
                case 12: return new MessagePack.Formatters.Synchro.ReCalibrateFormatter();
                case 13: return new MessagePack.Formatters.Synchro.PingFormatter();
                case 14: return new MessagePack.Formatters.Synchro.Test.TestCommandFormatter();
                default: return null;
            }
        }
    }
}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612


#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace MessagePack.Formatters.Synchro
{
    using System;
    using System.Collections.Generic;
    using MessagePack;

    public sealed class ISynchroCommandFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Synchro.ISynchroCommand>
    {
        readonly Dictionary<RuntimeTypeHandle, KeyValuePair<int, int>> typeToKeyAndJumpMap;
        readonly Dictionary<int, int> keyToJumpMap;

        public ISynchroCommandFormatter()
        {
            this.typeToKeyAndJumpMap = new Dictionary<RuntimeTypeHandle, KeyValuePair<int, int>>(10, global::MessagePack.Internal.RuntimeTypeHandleEqualityComparer.Default)
            {
                { typeof(global::Synchro.Test.TestCommand).TypeHandle, new KeyValuePair<int, int>(0, 0) },
                { typeof(global::Synchro.SpatialStatus).TypeHandle, new KeyValuePair<int, int>(1, 1) },
                { typeof(global::Synchro.TransformsStatusUpdate).TypeHandle, new KeyValuePair<int, int>(2, 2) },
                { typeof(global::Synchro.Register).TypeHandle, new KeyValuePair<int, int>(3, 3) },
                { typeof(global::Synchro.UpdatePresence).TypeHandle, new KeyValuePair<int, int>(4, 4) },
                { typeof(global::Synchro.SpawnObject).TypeHandle, new KeyValuePair<int, int>(5, 5) },
                { typeof(global::Synchro.DeleteObject).TypeHandle, new KeyValuePair<int, int>(6, 6) },
                { typeof(global::Synchro.ChangePermission).TypeHandle, new KeyValuePair<int, int>(7, 7) },
                { typeof(global::Synchro.ReCalibrate).TypeHandle, new KeyValuePair<int, int>(8, 8) },
                { typeof(global::Synchro.Ping).TypeHandle, new KeyValuePair<int, int>(9, 9) },
            };
            this.keyToJumpMap = new Dictionary<int, int>(10)
            {
                { 0, 0 },
                { 1, 1 },
                { 2, 2 },
                { 3, 3 },
                { 4, 4 },
                { 5, 5 },
                { 6, 6 },
                { 7, 7 },
                { 8, 8 },
                { 9, 9 },
            };
        }

        public int Serialize(ref byte[] bytes, int offset, global::Synchro.ISynchroCommand value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            KeyValuePair<int, int> keyValuePair;
            if (value != null && this.typeToKeyAndJumpMap.TryGetValue(value.GetType().TypeHandle, out keyValuePair))
            {
                var startOffset = offset;
                offset += MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 2);
                offset += MessagePackBinary.WriteInt32(ref bytes, offset, keyValuePair.Key);
                switch (keyValuePair.Value)
                {
                    case 0:
                        offset += formatterResolver.GetFormatterWithVerify<global::Synchro.Test.TestCommand>().Serialize(ref bytes, offset, (global::Synchro.Test.TestCommand)value, formatterResolver);
                        break;
                    case 1:
                        offset += formatterResolver.GetFormatterWithVerify<global::Synchro.SpatialStatus>().Serialize(ref bytes, offset, (global::Synchro.SpatialStatus)value, formatterResolver);
                        break;
                    case 2:
                        offset += formatterResolver.GetFormatterWithVerify<global::Synchro.TransformsStatusUpdate>().Serialize(ref bytes, offset, (global::Synchro.TransformsStatusUpdate)value, formatterResolver);
                        break;
                    case 3:
                        offset += formatterResolver.GetFormatterWithVerify<global::Synchro.Register>().Serialize(ref bytes, offset, (global::Synchro.Register)value, formatterResolver);
                        break;
                    case 4:
                        offset += formatterResolver.GetFormatterWithVerify<global::Synchro.UpdatePresence>().Serialize(ref bytes, offset, (global::Synchro.UpdatePresence)value, formatterResolver);
                        break;
                    case 5:
                        offset += formatterResolver.GetFormatterWithVerify<global::Synchro.SpawnObject>().Serialize(ref bytes, offset, (global::Synchro.SpawnObject)value, formatterResolver);
                        break;
                    case 6:
                        offset += formatterResolver.GetFormatterWithVerify<global::Synchro.DeleteObject>().Serialize(ref bytes, offset, (global::Synchro.DeleteObject)value, formatterResolver);
                        break;
                    case 7:
                        offset += formatterResolver.GetFormatterWithVerify<global::Synchro.ChangePermission>().Serialize(ref bytes, offset, (global::Synchro.ChangePermission)value, formatterResolver);
                        break;
                    case 8:
                        offset += formatterResolver.GetFormatterWithVerify<global::Synchro.ReCalibrate>().Serialize(ref bytes, offset, (global::Synchro.ReCalibrate)value, formatterResolver);
                        break;
                    case 9:
                        offset += formatterResolver.GetFormatterWithVerify<global::Synchro.Ping>().Serialize(ref bytes, offset, (global::Synchro.Ping)value, formatterResolver);
                        break;
                    default:
                        break;
                }

                return offset - startOffset;
            }

            return MessagePackBinary.WriteNil(ref bytes, offset);
        }
        
        public global::Synchro.ISynchroCommand Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            
            if (MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize) != 2)
            {
                throw new InvalidOperationException("Invalid Union data was detected. Type:global::Synchro.ISynchroCommand");
            }
            offset += readSize;

            var key = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;

            if (!this.keyToJumpMap.TryGetValue(key, out key))
            {
                key = -1;
            }

            global::Synchro.ISynchroCommand result = null;
            switch (key)
            {
                case 0:
                    result = (global::Synchro.ISynchroCommand)formatterResolver.GetFormatterWithVerify<global::Synchro.Test.TestCommand>().Deserialize(bytes, offset, formatterResolver, out readSize);
                    offset += readSize;
                    break;
                case 1:
                    result = (global::Synchro.ISynchroCommand)formatterResolver.GetFormatterWithVerify<global::Synchro.SpatialStatus>().Deserialize(bytes, offset, formatterResolver, out readSize);
                    offset += readSize;
                    break;
                case 2:
                    result = (global::Synchro.ISynchroCommand)formatterResolver.GetFormatterWithVerify<global::Synchro.TransformsStatusUpdate>().Deserialize(bytes, offset, formatterResolver, out readSize);
                    offset += readSize;
                    break;
                case 3:
                    result = (global::Synchro.ISynchroCommand)formatterResolver.GetFormatterWithVerify<global::Synchro.Register>().Deserialize(bytes, offset, formatterResolver, out readSize);
                    offset += readSize;
                    break;
                case 4:
                    result = (global::Synchro.ISynchroCommand)formatterResolver.GetFormatterWithVerify<global::Synchro.UpdatePresence>().Deserialize(bytes, offset, formatterResolver, out readSize);
                    offset += readSize;
                    break;
                case 5:
                    result = (global::Synchro.ISynchroCommand)formatterResolver.GetFormatterWithVerify<global::Synchro.SpawnObject>().Deserialize(bytes, offset, formatterResolver, out readSize);
                    offset += readSize;
                    break;
                case 6:
                    result = (global::Synchro.ISynchroCommand)formatterResolver.GetFormatterWithVerify<global::Synchro.DeleteObject>().Deserialize(bytes, offset, formatterResolver, out readSize);
                    offset += readSize;
                    break;
                case 7:
                    result = (global::Synchro.ISynchroCommand)formatterResolver.GetFormatterWithVerify<global::Synchro.ChangePermission>().Deserialize(bytes, offset, formatterResolver, out readSize);
                    offset += readSize;
                    break;
                case 8:
                    result = (global::Synchro.ISynchroCommand)formatterResolver.GetFormatterWithVerify<global::Synchro.ReCalibrate>().Deserialize(bytes, offset, formatterResolver, out readSize);
                    offset += readSize;
                    break;
                case 9:
                    result = (global::Synchro.ISynchroCommand)formatterResolver.GetFormatterWithVerify<global::Synchro.Ping>().Deserialize(bytes, offset, formatterResolver, out readSize);
                    offset += readSize;
                    break;
                default:
                    offset += MessagePackBinary.ReadNextBlock(bytes, offset);
                    break;
            }
            
            readSize = offset - startOffset;
            
            return result;
        }
    }


}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace MessagePack.Formatters.Synchro
{
    using System;
    using MessagePack;


    public sealed class SpatialStatusFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Synchro.SpatialStatus>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public SpatialStatusFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "name", 0},
                { "pos", 1},
                { "rot", 2},
                { "scale", 3},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("name"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("pos"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("rot"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("scale"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Synchro.SpatialStatus value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 4);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Serialize(ref bytes, offset, value.pos, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += formatterResolver.GetFormatterWithVerify<global::UnityEngine.Quaternion>().Serialize(ref bytes, offset, value.rot, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Serialize(ref bytes, offset, value.scale, formatterResolver);
            return offset - startOffset;
        }

        public global::Synchro.SpatialStatus Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __name__ = default(string);
            var __pos__ = default(global::UnityEngine.Vector3);
            var __rot__ = default(global::UnityEngine.Quaternion);
            var __scale__ = default(global::UnityEngine.Vector3);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
                offset += readSize;
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __name__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __pos__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __rot__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.Quaternion>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 3:
                        __scale__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Synchro.SpatialStatus();
            ____result.name = __name__;
            ____result.pos = __pos__;
            ____result.rot = __rot__;
            ____result.scale = __scale__;
            return ____result;
        }
    }


    public sealed class TransformsStatusUpdateFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Synchro.TransformsStatusUpdate>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TransformsStatusUpdateFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "names", 0},
                { "poses", 1},
                { "rots", 2},
                { "scales", 3},
                { "owner", 4},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("names"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("poses"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("rots"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("scales"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("owner"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Synchro.TransformsStatusUpdate value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 5);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<string>>().Serialize(ref bytes, offset, value.names, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::UnityEngine.Vector3>>().Serialize(ref bytes, offset, value.poses, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::UnityEngine.Quaternion>>().Serialize(ref bytes, offset, value.rots, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::UnityEngine.Vector3>>().Serialize(ref bytes, offset, value.scales, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.owner, formatterResolver);
            return offset - startOffset;
        }

        public global::Synchro.TransformsStatusUpdate Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __names__ = default(global::System.Collections.Generic.List<string>);
            var __poses__ = default(global::System.Collections.Generic.List<global::UnityEngine.Vector3>);
            var __rots__ = default(global::System.Collections.Generic.List<global::UnityEngine.Quaternion>);
            var __scales__ = default(global::System.Collections.Generic.List<global::UnityEngine.Vector3>);
            var __owner__ = default(string);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
                offset += readSize;
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __names__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<string>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __poses__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::UnityEngine.Vector3>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __rots__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::UnityEngine.Quaternion>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 3:
                        __scales__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::UnityEngine.Vector3>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 4:
                        __owner__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Synchro.TransformsStatusUpdate();
            ____result.names = __names__;
            ____result.poses = __poses__;
            ____result.rots = __rots__;
            ____result.scales = __scales__;
            ____result.owner = __owner__;
            return ____result;
        }
    }


    public sealed class RegisterFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Synchro.Register>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public RegisterFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "name", 0},
                { "owner", 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("name"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("owner"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Synchro.Register value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.owner, formatterResolver);
            return offset - startOffset;
        }

        public global::Synchro.Register Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __name__ = default(string);
            var __owner__ = default(string);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
                offset += readSize;
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __name__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __owner__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Synchro.Register();
            ____result.name = __name__;
            ____result.owner = __owner__;
            return ____result;
        }
    }


    public sealed class UpdatePresenceFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Synchro.UpdatePresence>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public UpdatePresenceFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "name", 0},
                { "owner", 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("name"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("owner"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Synchro.UpdatePresence value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<string>>().Serialize(ref bytes, offset, value.name, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.owner, formatterResolver);
            return offset - startOffset;
        }

        public global::Synchro.UpdatePresence Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __name__ = default(global::System.Collections.Generic.List<string>);
            var __owner__ = default(string);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
                offset += readSize;
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __name__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<string>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __owner__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Synchro.UpdatePresence();
            ____result.name = __name__;
            ____result.owner = __owner__;
            return ____result;
        }
    }


    public sealed class SpawnObjectFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Synchro.SpawnObject>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public SpawnObjectFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "name", 0},
                { "prefabName", 1},
                { "parentName", 2},
                { "startPos", 3},
                { "startRot", 4},
                { "startScale", 5},
                { "owner", 6},
                { "privacy", 7},
                { "owners", 8},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("name"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("prefabName"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("parentName"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("startPos"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("startRot"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("startScale"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("owner"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("privacy"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("owners"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Synchro.SpawnObject value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 9);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.prefabName, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.parentName, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Serialize(ref bytes, offset, value.startPos, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[4]);
            offset += formatterResolver.GetFormatterWithVerify<global::UnityEngine.Quaternion>().Serialize(ref bytes, offset, value.startRot, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[5]);
            offset += formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Serialize(ref bytes, offset, value.startScale, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[6]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.owner, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[7]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.privacy);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[8]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<int>>().Serialize(ref bytes, offset, value.owners, formatterResolver);
            return offset - startOffset;
        }

        public global::Synchro.SpawnObject Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __name__ = default(string);
            var __prefabName__ = default(string);
            var __parentName__ = default(string);
            var __startPos__ = default(global::UnityEngine.Vector3);
            var __startRot__ = default(global::UnityEngine.Quaternion);
            var __startScale__ = default(global::UnityEngine.Vector3);
            var __owner__ = default(string);
            var __privacy__ = default(int);
            var __owners__ = default(global::System.Collections.Generic.List<int>);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
                offset += readSize;
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __name__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __prefabName__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __parentName__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 3:
                        __startPos__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 4:
                        __startRot__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.Quaternion>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 5:
                        __startScale__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 6:
                        __owner__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 7:
                        __privacy__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 8:
                        __owners__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<int>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Synchro.SpawnObject();
            ____result.name = __name__;
            ____result.prefabName = __prefabName__;
            ____result.parentName = __parentName__;
            ____result.startPos = __startPos__;
            ____result.startRot = __startRot__;
            ____result.startScale = __startScale__;
            ____result.owner = __owner__;
            ____result.privacy = __privacy__;
            ____result.owners = __owners__;
            return ____result;
        }
    }


    public sealed class DeleteObjectFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Synchro.DeleteObject>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public DeleteObjectFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "owner", 0},
                { "name", 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("owner"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("name"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Synchro.DeleteObject value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.owner, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.name, formatterResolver);
            return offset - startOffset;
        }

        public global::Synchro.DeleteObject Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __owner__ = default(string);
            var __name__ = default(string);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
                offset += readSize;
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __owner__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __name__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Synchro.DeleteObject();
            ____result.owner = __owner__;
            ____result.name = __name__;
            return ____result;
        }
    }


    public sealed class ChangePermissionFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Synchro.ChangePermission>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public ChangePermissionFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "owner", 0},
                { "objectName", 1},
                { "permissionState", 2},
                { "owners", 3},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("owner"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("objectName"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("permissionState"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("owners"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Synchro.ChangePermission value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 4);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.owner, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.objectName, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[2]);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.permissionState);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[3]);
            offset += formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<int>>().Serialize(ref bytes, offset, value.owners, formatterResolver);
            return offset - startOffset;
        }

        public global::Synchro.ChangePermission Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __owner__ = default(string);
            var __objectName__ = default(string);
            var __permissionState__ = default(int);
            var __owners__ = default(global::System.Collections.Generic.List<int>);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
                offset += readSize;
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __owner__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __objectName__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __permissionState__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 3:
                        __owners__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<int>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Synchro.ChangePermission();
            ____result.owner = __owner__;
            ____result.objectName = __objectName__;
            ____result.permissionState = __permissionState__;
            ____result.owners = __owners__;
            return ____result;
        }
    }


    public sealed class ReCalibrateFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Synchro.ReCalibrate>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public ReCalibrateFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "owner", 0},
                { "target", 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("owner"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("target"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Synchro.ReCalibrate value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.owner, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.target, formatterResolver);
            return offset - startOffset;
        }

        public global::Synchro.ReCalibrate Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __owner__ = default(string);
            var __target__ = default(string);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
                offset += readSize;
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __owner__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __target__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Synchro.ReCalibrate();
            ____result.owner = __owner__;
            ____result.target = __target__;
            return ____result;
        }
    }


    public sealed class PingFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Synchro.Ping>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public PingFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "owner", 0},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("owner"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Synchro.Ping value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 1);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.owner, formatterResolver);
            return offset - startOffset;
        }

        public global::Synchro.Ping Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __owner__ = default(string);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
                offset += readSize;
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __owner__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Synchro.Ping();
            ____result.owner = __owner__;
            return ____result;
        }
    }

}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace MessagePack.Formatters.Synchro.Test
{
    using System;
    using MessagePack;


    public sealed class TestCommandFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::Synchro.Test.TestCommand>
    {

        readonly global::MessagePack.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public TestCommandFormatter()
        {
            this.____keyMapping = new global::MessagePack.Internal.AutomataDictionary()
            {
                { "MyParameter", 0},
                { "MyPosition", 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("MyParameter"),
                global::MessagePack.MessagePackBinary.GetEncodedStringBytes("MyPosition"),
                
            };
        }


        public int Serialize(ref byte[] bytes, int offset, global::Synchro.Test.TestCommand value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedMapHeaderUnsafe(ref bytes, offset, 2);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[0]);
            offset += formatterResolver.GetFormatterWithVerify<string>().Serialize(ref bytes, offset, value.MyParameter, formatterResolver);
            offset += global::MessagePack.MessagePackBinary.WriteRaw(ref bytes, offset, this.____stringByteKeys[1]);
            offset += formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Serialize(ref bytes, offset, value.MyPosition, formatterResolver);
            return offset - startOffset;
        }

        public global::Synchro.Test.TestCommand Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadMapHeader(bytes, offset, out readSize);
            offset += readSize;

            var __MyParameter__ = default(string);
            var __MyPosition__ = default(global::UnityEngine.Vector3);

            for (int i = 0; i < length; i++)
            {
                var stringKey = global::MessagePack.MessagePackBinary.ReadStringSegment(bytes, offset, out readSize);
                offset += readSize;
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __MyParameter__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __MyPosition__ = formatterResolver.GetFormatterWithVerify<global::UnityEngine.Vector3>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                
                NEXT_LOOP:
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new global::Synchro.Test.TestCommand();
            ____result.MyParameter = __MyParameter__;
            ____result.MyPosition = __MyPosition__;
            return ____result;
        }
    }

}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
