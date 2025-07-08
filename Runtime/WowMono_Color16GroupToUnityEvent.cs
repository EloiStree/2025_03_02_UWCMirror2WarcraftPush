using UnityEngine;
using static WowMono_Pixels16ToWowBasicInfo;

public class WowMono_Color16GroupToUnityEvent : MonoBehaviour
{

    public Wow_PlayerInfo m_playerInfo = new Wow_PlayerInfo();
    public Wow_PlayerInfoEvent m_playerInfoEvent = new Wow_PlayerInfoEvent();


    public void SetSource(Color16Group colorInfo)
    {

        if (colorInfo == null)
        {
            return;
        }

        m_playerInfo.m_mapPositionLRTD = colorInfo.m_cr0_mapCoordinate.GetMapPositionRaw();
        m_playerInfoEvent.m_mapPositionLRTD.Invoke(m_playerInfo.m_mapPositionLRTD);
        m_playerInfo.m_worldPositionRLDT = colorInfo.m_cr1cr2_toWorldXY.GetWorldPosition();
        m_playerInfoEvent.m_worldPositionRLDT.Invoke(m_playerInfo.m_worldPositionRLDT);

        bool gathering = colorInfo.m_cl3_playerBinaryInfo.m_isGatheringHerbs || colorInfo.m_cl3_playerBinaryInfo.m_isGatheringMining;
        bool discoveringZone = colorInfo.m_cl3_playerBinaryInfo.m_hasDiscoveredZoneLastSeconds;
        if (m_playerInfo.m_discoveringZone != discoveringZone)
        {
            m_playerInfo.m_discoveringZone = discoveringZone;
            if (discoveringZone)
            {
                m_playerInfoEvent.m_onDiscoverZoneChangeToTrue.Invoke();
            }
        }
        if (m_playerInfo.m_gathering != gathering)
        {
            m_playerInfo.m_gathering = gathering;
            if (gathering)
            {
                m_playerInfoEvent.m_onGatheringChangeToTrue.Invoke();
            }
        }


    }
}
