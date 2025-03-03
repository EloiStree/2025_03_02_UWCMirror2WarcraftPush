using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class SleepyExportUDP: MonoBehaviour {



    public List<UWCMono_ChampionBasicColorPicking> m_debug;

    public string [] m_addresses = new string[] { "127.0.0.1:1234" };
    public string m_text;
    public void Update()
    {
        m_debug = UWCMono_ChampionBasicColorPicking.m_inScene;
        StringBuilder sb = new StringBuilder();
        foreach (UWCMono_ChampionBasicColorPicking c in m_debug)
        {
            sb.AppendLine("Life: " + c.m_lifePercent + "| XP: " + c.m_xpPercent + "| Level: " + c.m_playerLevel + "| MapX: " + c.m_mapX + "| MapY: " + c.m_mapY + "| Rotation: " + c.m_playerRotation + "| WorldX: " + c.m_worldX + "| WorldY: " + c.m_worldY);
        }
        m_text = sb.ToString();
        PushTextAsUdp(m_text);

    }

    private void PushTextAsUdp(string m_text)
    {
        UdpClient client = new UdpClient();
        byte[] data = Encoding.UTF8.GetBytes(m_text);
        foreach (string address in m_addresses)
        {
            string[] parts = address.Split(':');
            string ip = parts[0];
            int port = int.Parse(parts[1]);
            client.Send(data, data.Length, ip, port);
        }
    }
}

