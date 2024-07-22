using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TransformState : INetworkSerializable
{
    public int Tick;
    public Vector3 position;
    public Quaternion rotation;
    public bool HasStartedMoving;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        if (serializer.IsReader)
        {
            var reader = serializer.GetFastBufferReader();
            reader.ReadValueSafe(out Tick);
            reader.ReadValueSafe(out position);
            reader.ReadValueSafe(out rotation);
            reader.ReadValueSafe(out HasStartedMoving);


        }
        else
        {
            var writer = serializer.GetFastBufferWriter();
            writer.WriteValueSafe(Tick);
            writer.WriteValueSafe(position);
            writer.WriteValueSafe(rotation);
            writer.WriteValueSafe(HasStartedMoving);
        }
    }
}
