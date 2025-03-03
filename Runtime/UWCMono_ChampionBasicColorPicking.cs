using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
public class UWCMono_ChampionBasicColorPicking : MonoBehaviour {

    public static List<UWCMono_ChampionBasicColorPicking> m_inScene = new List<UWCMono_ChampionBasicColorPicking>();

    public float m_lifePercent;
    public float m_xpPercent;
    public int m_playerLevel;
    public float m_mapX;
    public float m_mapY;
    public float m_playerRotation;
    public float m_worldX;
    public float m_worldY;

    public void OnEnable()
    {
        m_inScene.Add(this);
    }

    public void OnDisable()
    {
        m_inScene.Remove(this);
    }
    public void Update()
    {
        m_lifePercent = m_healXp.m_gPercentLife;
        m_xpPercent = m_healXp.m_bPercentXp;
        m_playerLevel = m_healXp.m_rPlayerLevel;
        m_mapX = m_mapPosition.m_rMapX;
        m_mapY = m_mapPosition.m_gMapY;
        m_playerRotation = m_mapPosition.m_bRotationAngle;
        m_worldX = m_worldPosition.m_worldPositionX;
        m_worldY = m_worldPosition.m_worldPositionY;
    }
    public UWCMono_ImportMap2DPosition m_mapPosition;
    public UWCMono_ImportWorldPosition m_worldPosition;
    public UWCMono_ImportHealXp m_healXp;
}

