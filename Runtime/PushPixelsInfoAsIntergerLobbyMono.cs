
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static FetchBasicWowPixelCoordinate;

public class PushPixelsInfoAsIntergerLobbyMono : MonoBehaviour
{

    public FetchBasicWowPixelCoordinate m_pixelAccess;
    public UnityEvent<int> m_onPushIntegerToLobby;
    public UnityEvent<int,DateTime> m_onPushIntegerToLobbyWithLocalDateUTC;

    public float m_pushAllFrequency = 5f;

    public List<PlayerSendAsInteger> m_playerSendAsInteger = new List<PlayerSendAsInteger>();

    [System.Serializable]
    public class PlayerSendAsInteger
    {
        public int m_playerTag;
        public int m_i01MapX;
        public int m_i02MapY;
        public int m_i03Angle360;
        public int m_i04WorldX;
        public int m_i05WorldY;
        public int m_i06PlayerLevel;
        public int m_i07PlayerLifePercent;
        public int m_i08PlayerXpPercent;
        public int m_i10PlayerIdPartOne;
        public int m_i11PlayerIdPartTwo;
        public int m_i12PlayerIdPartThree;
    }

    void Start()
    {

        InvokeRepeating(nameof(PushAll), 0.5f, m_pushAllFrequency);
    }

    private void PushAll()
    {
        if (m_pixelAccess == null) return;
        if (m_pixelAccess.m_fourPixelsBasicWowInfo == null) return;


        List<FourPixelsBasicWowInfo> fourPixelsBasicWowInfo = m_pixelAccess.m_fourPixelsBasicWowInfo;
        int count =  fourPixelsBasicWowInfo.Count;
        if (count <= 0) return;

        if (count != m_playerSendAsInteger.Count)
        {
            m_playerSendAsInteger = new List<PlayerSendAsInteger>();
            for (int i = 0; i < count; i++)
            {
                m_playerSendAsInteger.Add(new PlayerSendAsInteger());
            }
        }
        for (int i = 0; i < count; i++)
        {
            FourPixelsBasicWowInfo target = fourPixelsBasicWowInfo[i];
            if (i > 18)
                throw new System.Exception("Too many players, only 19 players supported in integer format");
            int playerIndex = (i+1) * 100000000;
            m_playerSendAsInteger[i].m_playerTag = playerIndex;
            m_playerSendAsInteger[i].m_i01MapX = playerIndex                + 1000000;
            m_playerSendAsInteger[i].m_i02MapY = playerIndex                + 2000000;
            m_playerSendAsInteger[i].m_i03Angle360 = playerIndex            + 3000000;
            m_playerSendAsInteger[i].m_i04WorldX = playerIndex              + 4000000;
            m_playerSendAsInteger[i].m_i05WorldY = playerIndex              + 5000000;
            m_playerSendAsInteger[i].m_i06PlayerLevel = playerIndex         + 6000000;
            m_playerSendAsInteger[i].m_i07PlayerLifePercent = playerIndex   + 7000000;
            m_playerSendAsInteger[i].m_i08PlayerXpPercent = playerIndex     + 8000000;

            m_playerSendAsInteger[i].m_i01MapX += (int)(target.m_rMapX * 100f);
            m_playerSendAsInteger[i].m_i02MapY += (int)(target.m_gMapY * 100f);
            m_playerSendAsInteger[i].m_i03Angle360 += (int)(target.m_bRotationAngle * 100f);

            m_playerSendAsInteger[i].m_i06PlayerLevel += (int)(target.m_rPlayerLevel );
            m_playerSendAsInteger[i].m_i07PlayerLifePercent += (int)(target.m_gPercentLife * 10000f);
            m_playerSendAsInteger[i].m_i08PlayerXpPercent += (int)(target.m_bPercentXp * 10000f);

            m_playerSendAsInteger[i].m_i04WorldX += (int)(Mathf.Abs(target.m_worldPositionX));
            if (target.m_worldPositionX < 0)
                m_playerSendAsInteger[i].m_i04WorldX *= -1;
            
            m_playerSendAsInteger[i].m_i05WorldY += (int)(Mathf.Abs(target.m_worldPositionY));
            if (target.m_worldPositionY < 0)
                m_playerSendAsInteger[i].m_i05WorldY *= -1;

            m_playerSendAsInteger[i].m_i10PlayerIdPartOne = playerIndex     + 10000000;
            m_playerSendAsInteger[i].m_i11PlayerIdPartTwo = playerIndex     + 11000000;
            m_playerSendAsInteger[i].m_i12PlayerIdPartThree = playerIndex   + 12000000;
            byte[] bytes = new byte[6];
            bytes[0] = target.m_playerPartOne.r;
            bytes[1] = target.m_playerPartOne.g;
            bytes[2] = target.m_playerPartOne.b;
            bytes[3] = target.m_playerPartTwo.r;
            bytes[4] = target.m_playerPartTwo.g;
            bytes[5] = target.m_playerPartTwo.b;

            m_playerSendAsInteger[i].m_i10PlayerIdPartOne += bytes[0] * 1000;
            m_playerSendAsInteger[i].m_i10PlayerIdPartOne += bytes[1] ;
            m_playerSendAsInteger[i].m_i11PlayerIdPartTwo += bytes[2] * 1000;
            m_playerSendAsInteger[i].m_i11PlayerIdPartTwo += bytes[3];
            m_playerSendAsInteger[i].m_i12PlayerIdPartThree += bytes[4] * 1000;
            m_playerSendAsInteger[i].m_i12PlayerIdPartThree += bytes[5];

        }

        DateTime localDate = DateTime.UtcNow;
        for (int i = 0; i < count; i++)
        {
            FourPixelsBasicWowInfo target = fourPixelsBasicWowInfo[i];
            if (i > 18)
                throw new System.Exception("Too many players, only 19 players supported in integer format");


            Push(m_playerSendAsInteger[i].m_playerTag, localDate);
            Push(m_playerSendAsInteger[i].m_i01MapX, localDate);
            Push(m_playerSendAsInteger[i].m_i02MapY, localDate);
            Push(m_playerSendAsInteger[i].m_i03Angle360, localDate);
            Push(m_playerSendAsInteger[i].m_i04WorldX, localDate);
            Push(m_playerSendAsInteger[i].m_i05WorldY, localDate);
            Push(m_playerSendAsInteger[i].m_i06PlayerLevel, localDate);
            Push(m_playerSendAsInteger[i].m_i07PlayerLifePercent, localDate);
            Push(m_playerSendAsInteger[i].m_i08PlayerXpPercent, localDate);
            Push(m_playerSendAsInteger[i].m_i10PlayerIdPartOne, localDate);
            Push(m_playerSendAsInteger[i].m_i11PlayerIdPartTwo, localDate);
            Push(m_playerSendAsInteger[i].m_i12PlayerIdPartThree, localDate);
        }
    }

    private void Push(int integerToPush,DateTime nowUtc)
    {
        m_onPushIntegerToLobby?.Invoke(integerToPush);
        m_onPushIntegerToLobbyWithLocalDateUTC?.Invoke(integerToPush, nowUtc);
    }
}
