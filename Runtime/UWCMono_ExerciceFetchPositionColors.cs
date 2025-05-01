using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UWCMono_ExerciceFetchPositionColors : MonoBehaviour
{

    public Texture2D m_currentTexture;

    [Header("Color 0")]
    [Range(0, 360)]
    public float m_angleCounterClockwise;
    [Range(0, 1)]
    public float m_mapPositionX;
    [Range(0, 1)]
    public float m_mapPositionY;

    [Header("Color 1")]
    public int m_worldPositionX;

    [Header("Color 2")]
    public int m_worldPositionY;

    [Header("Color 3")]
    [Range(0, 1)]
    public float m_life;
    [Range(0, 1)]
    public float m_experience;
    [Range(0, 120)]
    public int m_level;


    public void PushIn(Texture2D image) {

        m_currentTexture = image;

        FetchInTextureMetaInfoFromColor();
    }

    private void FetchInTextureMetaInfoFromColor()
    {
        Color32 colorMap2DAndAngle = m_currentTexture.GetPixel(0, 0);
        Color32 colorPositionX = m_currentTexture.GetPixel(0, 0);
        Color32 colorPositionY = m_currentTexture.GetPixel(0, 0);
        Color32 colorLifeXpAndLevel = m_currentTexture.GetPixel(0, 0);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
