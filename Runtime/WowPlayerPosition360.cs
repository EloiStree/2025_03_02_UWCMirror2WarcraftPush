using UnityEngine;

[System.Serializable]
public class WowPlayerPosition360 { 

    public Vector2 m_mapPositionLRTD;
    public Vector3 m_worldPositionRLDT;
    public float m_playerAngle360 = 0f;

    public WowPlayerPosition360(Vector2 mapPositionLRTD, Vector3 worldPositionRLDT, float angle)
    {
        m_mapPositionLRTD = mapPositionLRTD;
        m_worldPositionRLDT = worldPositionRLDT;
        m_playerAngle360 = angle;
    }
}
