using UnityEngine;

public class WowMono_Pixels16ToWowBasicInfo : MonoBehaviour {


    public Color16Group m_color16Group = new Color16Group();

    [System.Serializable]
    public class Color16Group { 
    public Color32[] m_colorClockwise = new Color32[16];
    public ColorWowMapCoordinate m_cr0_mapCoordinate = new ColorWowMapCoordinate();
    public ColorWowToWorldXY m_cr1cr2_toWorldXY = new ColorWowToWorldXY();
    public ColorWowPlayer m_cr3_playerLifeLevel = new ColorWowPlayer();
    public ColorWowToTeamLife9 m_cr4_targetInfo = new ColorWowToTeamLife9();
    public ColorWowXpModulo999999 m_cr5_xpModuloInfo = new ColorWowXpModulo999999();
    public ColorWowDoubleColorToGUID m_cr6cr7_doubleColorToGUIDPlayer = new ColorWowDoubleColorToGUID();

    public ColorWowTo24Bits m_cl0_Custom24bits = new ColorWowTo24Bits();
    public ColorWowTo24Bits m_cl1_Custom24bits = new ColorWowTo24Bits();
    public WowColorTargetBinaryInfo m_cl2_targetBinaryInfo = new WowColorTargetBinaryInfo();
    public WowColorPlayerBinaryInfo m_cl3_playerBinaryInfo = new WowColorPlayerBinaryInfo();
    public ColorWowGatherObjectId m_cl4_gatherObjectId = new ColorWowGatherObjectId();
    public ColorWowTargetInfo m_cl5_targetInfo = new ColorWowTargetInfo();
    public ColorWowDoubleColorToGUID m_cl6cl7_doubleColorToGUIDTarget = new ColorWowDoubleColorToGUID();


    public ColorWowTo24Bits m_player24bits;
    public ColorWowTo24Bits m_target24bits;


    public void SetColor16(Color32[] source)
    {
        if (source == null || source.Length != 16)
        {
            return;
        }
        m_colorClockwise = source;
        m_cr0_mapCoordinate.SetWithColor(source[0]);
        m_cr1cr2_toWorldXY.SetWithColor(source[1], source[2]);
        m_cr3_playerLifeLevel.SetWithColor(source[3]);
        m_cr4_targetInfo.SetWithColor(source[4]);
        m_cr5_xpModuloInfo.SetWithColor(source[5]);
        m_cl0_Custom24bits.SetWithColor(source[15]);
        m_cl1_Custom24bits.SetWithColor(source[14]);
        m_target24bits.SetWithColor(source[13]);
        m_player24bits.SetWithColor(source[12]);
        m_cl2_targetBinaryInfo.SetWithBits(m_target24bits.bits24);
        m_cl3_playerBinaryInfo.SetWithBits(m_player24bits.bits24);
        m_cl4_gatherObjectId.SetWithColor(source[11]);
        m_cl5_targetInfo.SetWithColor(source[10]);

        m_cr6cr7_doubleColorToGUIDPlayer.SetWithColor(source[6], source[7]);
        m_cl6cl7_doubleColorToGUIDTarget.SetWithColor(source[9], source[8]);

    }
    }

    public void SetColor16(Color32[] source) {
        m_color16Group.SetColor16(source);
    }

}
