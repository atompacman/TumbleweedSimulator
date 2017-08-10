// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// MIT License
// Copyright (c) 2017 Stained Glass Guild
// See file "LICENSE.txt" at project root for complete license
// ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~
// Project: SimpleMobilePlaceholder
// File: Setup.cs
// Creation: 2017-08
// Author: Jérémie Coulombe
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using JetBrains.Annotations;

using UnityEngine;

namespace SGG.TWS
{
   public sealed class Setup : MonoBehaviour
   {
      #region Methods

      [UsedImplicitly]
      private void Start()
      {
         // Force landscape orientations
         Screen.autorotateToPortrait = false;
         Screen.autorotateToPortraitUpsideDown = false;
         Screen.orientation = ScreenOrientation.Landscape;
      }

      #endregion
   }
}
