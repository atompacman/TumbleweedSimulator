// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// MIT License
// Copyright (c) 2017 Stained Glass Guild
// See file "LICENSE.txt" at project root for complete license
// ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~
// Project: SimpleMobilePlaceholder
// File: CameraController.cs
// Creation: 2017-08
// Author: Jérémie Coulombe
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using JetBrains.Annotations;

using UnityEngine;

namespace SGG.TWS
{
   public sealed class CameraController : MonoBehaviour
   {
      #region Compile-time constants

      private const float CAM_MIN_ROTATION_SPEED = 0.75f;
      private const float CAM_MAX_ROTATION_SPEED = 3.5f;

      private const float MIN_SPEED_FOR_CAM_ROT = 10f;

      #endregion

      #region Private fields

      [SerializeField, UsedImplicitly]
      private Tumbleweed m_Tw;

      private bool m_ForceRotation;

      #endregion

      #region Properties

      private Vector2 CamDir2D
      {
         get
         {
            var dir = transform.forward;
            return new Vector2(dir.x, dir.z);
         }
      }

      #endregion

      #region Methods

      [UsedImplicitly]
      private void Start()
      {
         m_ForceRotation = false;
      }

      [UsedImplicitly]
      private void Update()
      {
         bool rotateCam = false;

         float camTwDirDot = Vector2.Dot(CamDir2D, m_Tw.RollingDir2D);

         if (m_Tw.Speed2D > MIN_SPEED_FOR_CAM_ROT)
         {
            rotateCam = true;
            m_ForceRotation = true;
         }
         else
         {
            if (camTwDirDot > 0.9f)
            {
               m_ForceRotation = false;
            }

            if (m_ForceRotation)
            {
               rotateCam = true;
            }
         }

         float rotSpeed = Mathf.Lerp(
            CAM_MAX_ROTATION_SPEED,
            CAM_MIN_ROTATION_SPEED,
            (camTwDirDot + 1) * 0.5f);
         float rotAngle = rotateCam ? Time.deltaTime * rotSpeed : 0;

         var newCamDir = Vector3.RotateTowards(CamDir2D, m_Tw.RollingDir2D, rotAngle, 0);
         newCamDir = new Vector3(newCamDir.x, 0, newCamDir.y);

         var twPos = m_Tw.transform.position;
         transform.position = twPos + newCamDir * -8 + Vector3.up * 2;
         transform.LookAt(m_Tw.transform);
      }

      #endregion
   }
}
