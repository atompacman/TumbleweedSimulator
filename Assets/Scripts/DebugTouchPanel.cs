// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// MIT License
// Copyright (c) 2017 Stained Glass Guild
// See file "LICENSE.txt" at project root for complete license
// ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~
// Project: SimpleMobilePlaceholder
// File: DebugTouchPanel.cs
// Creation: 2017-08
// Author: Jérémie Coulombe
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using JetBrains.Annotations;

using UnityEngine;
using UnityEngine.UI;

namespace SGG.TWS
{
   public sealed class DebugTouchPanel : MonoBehaviour
   {
      #region Methods
      
      public void Activate()
      {
         SetPanelAlpha(0.4f);
      }

      public void Deactivate()
      {
         SetPanelAlpha(0.1f);
      }

      #endregion

      private void SetPanelAlpha(float a_Alpha)
      {
         var img = GetComponent<Image>();
         var color = img.color;
         color.a = a_Alpha;
         img.color = color;
      }
   }
}
