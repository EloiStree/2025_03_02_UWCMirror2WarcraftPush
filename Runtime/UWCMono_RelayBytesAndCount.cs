using System;
using UnityEngine;
using UnityEngine.Events;

public class UWCMono_RelayBytesAndCount : MonoBehaviour {

    public int m_defaultIndex = -40;
    public ulong m_bytesCount;
    public double m_kiloBytesCount;
    public double m_megaBytesCount;
    public double m_gigaBytesCount;

    public UnityEvent<byte[]> m_onPushBytes;


    public void PushIntegerIndexValue(int index, int value)
    {
        byte[] bytes = new byte[8];
        BitConverter.GetBytes(index).CopyTo(bytes, 0);
        BitConverter.GetBytes(value).CopyTo(bytes, 4);
        PushBytes(bytes);
    }

    public void PushDefaultIndexValue(int value)
    {
        PushIntegerIndexValue(m_defaultIndex, value);
    }
    public void PushBytesAsLittleEndian(int value)
    {
        byte[] bytes = BitConverter.GetBytes(value);

        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes);
        }
        PushBytes(bytes);
    }
    public void PushBytes(byte[] bytes)
    {
        if (bytes == null) return;

        m_bytesCount += (ulong)bytes.Length;

        m_kiloBytesCount = (double)m_bytesCount / 1024.0;
        m_megaBytesCount = (double)m_kiloBytesCount / 1024.0;
        m_gigaBytesCount = (double)m_megaBytesCount / 1024.0;
        m_onPushBytes?.Invoke(bytes);
    }
}
