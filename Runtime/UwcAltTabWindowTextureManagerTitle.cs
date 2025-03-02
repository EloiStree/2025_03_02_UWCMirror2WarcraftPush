using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace uWindowCapture
{

    public class UwcAltTabWindowTextureManagerTitle : UwcWindowTextureManager
    {
        void Start()
        {
            UwcManager.onWindowAdded.AddListener(OnWindowAdded);
            UwcManager.onWindowRemoved.AddListener(OnWindowRemoved);

            foreach (var pair in UwcManager.windows)
            {
                OnWindowAdded(pair.Value);
            }
        }

        public string m_title = "World of Warcraft";
        void OnWindowAdded(UwcWindow window)
        {
            if (window.title.StartsWith(m_title))
            {
                if (window.parentWindow != null) return; // handled by UwcWindowTextureChildrenManager
                if (!window.isVisible || !window.isAltTabWindow || window.isBackground) return;

                window.RequestCapture();
                AddWindowTexture(window);
            }
        }

        void OnWindowRemoved(UwcWindow window)
        {
            if (window.title.StartsWith(m_title))
                RemoveWindowTexture(window);
        }
    }
}