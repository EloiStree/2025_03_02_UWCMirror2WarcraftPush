
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
        public int m_i09PartyLife;
        public int m_i10PlayerIdPartOne;
        public int m_i11PlayerIdPartTwo;
        public int m_i12PlayerIdPartThree;
        public int m_i13TargetIdPartOne;
        public int m_i14TargetIdPartTwo;
        public int m_i15TargetIdPartThree;
        public int m_i16TargetLifePercent;
        public int m_i17TargetPowerPercent;
        public int m_i18TargetWindowHandle6Digit;
        public int m_i19TargetWindowHandle6DigitAdditional;
    }

    void Start()
    {

        InvokeRepeating(nameof(PushAll), 0.5f, m_pushAllFrequency);
    }

    private void PushAll()
    {
        if (m_pixelAccess == null) return;
        if (m_pixelAccess.m_fourPixelsBasicWowInfo == null) return;


        List<LeftRightCellPixelInfo> fourPixelsBasicWowInfo = m_pixelAccess.m_fourPixelsBasicWowInfo;
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
            LeftRightCellPixelInfo target = fourPixelsBasicWowInfo[i];
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
            m_playerSendAsInteger[i].m_i08PlayerXpPercent = playerIndex + 8000000;
            m_playerSendAsInteger[i].m_i09PartyLife = playerIndex + 9000000;
            m_playerSendAsInteger[i].m_i16TargetLifePercent = playerIndex  + 16000000;
            m_playerSendAsInteger[i].m_i17TargetPowerPercent = playerIndex + 17000000;
            m_playerSendAsInteger[i].m_i18TargetWindowHandle6Digit = playerIndex + 18000000;
            m_playerSendAsInteger[i].m_i19TargetWindowHandle6DigitAdditional = playerIndex + 19000000;


            int handle = target.m_windowHandle;
            if (handle >= 1000000)
            {
                m_playerSendAsInteger[i].m_i18TargetWindowHandle6Digit += handle%1000000;
                m_playerSendAsInteger[i].m_i19TargetWindowHandle6DigitAdditional += handle/1000000;
            }
            else
            {
                m_playerSendAsInteger[i].m_i18TargetWindowHandle6Digit += handle;
                m_playerSendAsInteger[i].m_i19TargetWindowHandle6DigitAdditional += 0;
            }


            m_playerSendAsInteger[i].m_i01MapX += (int)(target.m_rMapX * 100f);
            m_playerSendAsInteger[i].m_i02MapY += (int)(target.m_gMapY * 100f);
            m_playerSendAsInteger[i].m_i03Angle360 += (int)(target.m_bRotationAngle * 100f);

            m_playerSendAsInteger[i].m_i06PlayerLevel += (int)(target.m_rPlayerLevel );
            m_playerSendAsInteger[i].m_i07PlayerLifePercent += (int)(target.m_gPercentLife * 10000f);
            m_playerSendAsInteger[i].m_i08PlayerXpPercent += (int)(target.m_bPercentXp * 10000f);


            m_playerSendAsInteger[i].m_i16TargetLifePercent += (int)(target.m_targetLifePercent * 10000f);
            m_playerSendAsInteger[i].m_i17TargetPowerPercent += (int)(target.m_targetPowerPercent * 10000f);


            int targetBlue = target.m_leftTop5_targetLifeLevelPowerInfo.b;

            m_playerSendAsInteger[i].m_i04WorldX += (int)(Mathf.Abs(target.m_worldPositionX));
            if (target.m_worldPositionX < 0)
                m_playerSendAsInteger[i].m_i04WorldX *= -1;
            
            m_playerSendAsInteger[i].m_i05WorldY += (int)(Mathf.Abs(target.m_worldPositionY));
            if (target.m_worldPositionY < 0)
                m_playerSendAsInteger[i].m_i05WorldY *= -1;

            m_playerSendAsInteger[i].m_i10PlayerIdPartOne = playerIndex + 10000000;
            m_playerSendAsInteger[i].m_i11PlayerIdPartTwo = playerIndex + 11000000;
            m_playerSendAsInteger[i].m_i12PlayerIdPartThree = playerIndex + 12000000;

            m_playerSendAsInteger[i].m_i13TargetIdPartOne = playerIndex + 13000000;
            m_playerSendAsInteger[i].m_i14TargetIdPartTwo = playerIndex + 14000000;
            m_playerSendAsInteger[i].m_i15TargetIdPartThree = playerIndex + 15000000;

            byte[] bytes = new byte[6];
            bytes[0] = target.m_rightTop6_playerIdPart1.r;
            bytes[1] = target.m_rightTop6_playerIdPart1.g;
            bytes[2] = target.m_rightTop6_playerIdPart1.b;
            bytes[3] = target.m_rightTop7_playerIdPart2.r;
            bytes[4] = target.m_rightTop7_playerIdPart2.g;
            bytes[5] = target.m_rightTop7_playerIdPart2.b;

            m_playerSendAsInteger[i].m_i10PlayerIdPartOne += bytes[0] * 1000;
            m_playerSendAsInteger[i].m_i10PlayerIdPartOne += bytes[1] ;
            m_playerSendAsInteger[i].m_i11PlayerIdPartTwo += bytes[2] * 1000;
            m_playerSendAsInteger[i].m_i11PlayerIdPartTwo += bytes[3];
            m_playerSendAsInteger[i].m_i12PlayerIdPartThree += bytes[4] * 1000;
            m_playerSendAsInteger[i].m_i12PlayerIdPartThree += bytes[5];


            bytes[0] = target.m_leftTop6_targetPart1.r;
            bytes[1] = target.m_leftTop6_targetPart1.g;
            bytes[2] = target.m_leftTop6_targetPart1.b;
            bytes[3] = target.m_leftTop7_targetPart2.r;
            bytes[4] = target.m_leftTop7_targetPart2.g;
            bytes[5] = target.m_leftTop7_targetPart2.b;

            m_playerSendAsInteger[i].m_i13TargetIdPartOne += bytes[0] * 1000;
            m_playerSendAsInteger[i].m_i13TargetIdPartOne += bytes[1];
            m_playerSendAsInteger[i].m_i14TargetIdPartTwo += bytes[2] * 1000;
            m_playerSendAsInteger[i].m_i14TargetIdPartTwo += bytes[3];
            m_playerSendAsInteger[i].m_i15TargetIdPartThree += bytes[4] * 1000;
            m_playerSendAsInteger[i].m_i15TargetIdPartThree += bytes[5];



            int playerLife =( (int)(target.m_playerLife*9f))        *100000;
            int party1 =    ( (int)(target.m_partyPlayerLife1 * 9f))  *10000;
            int party2 =    ( (int)(target.m_partyPlayerLife2 * 9f))  *1000;
            int party3 =    ( (int)(target.m_partyPlayerLife3 * 9f))  *100;
            int party4 =    ( (int)(target.m_partyPlayerLife4 * 9f))  *10;
            int petLife =   ( (int)(target.m_petLife * 9f))         *1;
            
            m_playerSendAsInteger[i].m_i09PartyLife +=  playerLife + party1 + party2 + party3 + party4 + petLife;




        }

        DateTime localDate = DateTime.UtcNow;
        for (int i = 0; i < count; i++)
        {
            LeftRightCellPixelInfo target = fourPixelsBasicWowInfo[i];
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
            Push(m_playerSendAsInteger[i].m_i09PartyLife, localDate);
            Push(m_playerSendAsInteger[i].m_i10PlayerIdPartOne, localDate);
            Push(m_playerSendAsInteger[i].m_i11PlayerIdPartTwo, localDate);
            Push(m_playerSendAsInteger[i].m_i12PlayerIdPartThree, localDate);
            Push(m_playerSendAsInteger[i].m_i13TargetIdPartOne, localDate);
            Push(m_playerSendAsInteger[i].m_i14TargetIdPartTwo, localDate);
            Push(m_playerSendAsInteger[i].m_i15TargetIdPartThree, localDate);
            Push(m_playerSendAsInteger[i].m_i16TargetLifePercent, localDate);
            Push(m_playerSendAsInteger[i].m_i17TargetPowerPercent, localDate);
            Push(m_playerSendAsInteger[i].m_i18TargetWindowHandle6Digit, localDate);
            Push(m_playerSendAsInteger[i].m_i19TargetWindowHandle6DigitAdditional, localDate);


        }
    }

    private void Push(object m_i19TargetWindowHandleAdditional, DateTime localDate)
    {
        throw new NotImplementedException();
    }

    private byte CharToDigit0To9(char charToParse)
    {
        int value16 = 0;
        switch (charToParse)
        {
            case '0': value16 = 0; break;
            case '1': value16 = 1; break;
            case '2': value16 = 2; break;
            case '3': value16 = 3; break;
            case '4': value16 = 4; break;
            case '5': value16 = 5; break;
            case '6': value16 = 6; break;
            case '7': value16 = 7; break;
            case '8': value16 = 8; break;
            case '9': value16 = 9; break;
            case 'A': value16 = 10; break;
            case 'B': value16 = 11; break;
            case 'C': value16 = 12; break;
            case 'D': value16 = 13; break;
            case 'E': value16 = 14; break;
            case 'F': value16 = 15; break;
            default: break;
        }

        return (byte)((((float)value16) / 16f) * 9f);
    }

    private void Push(int integerToPush,DateTime nowUtc)
    {
        m_onPushIntegerToLobby?.Invoke(integerToPush);
        m_onPushIntegerToLobbyWithLocalDateUTC?.Invoke(integerToPush, nowUtc);
    }
}
