// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// MIT License
// Copyright (c) 2017 Stained Glass Guild
// See file "LICENSE.txt" at project root for complete license
// ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~
// Project: SimpleMobilePlaceholder
// File: TumbleweedController.cs
// Creation: 2017-08
// Author: Jérémie Coulombe
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using System.Collections.Generic;

using JetBrains.Annotations;

using UnityEngine;
using UnityEngine.UI;

namespace SGG.TWS
{
   /// <summary>
   /// By default, screen touching is mapped on the mouse, so we use the mouse as the input source
   /// so that the actual mouse can be used for debugging.
   /// </summary>
   public sealed class TumbleweedController : MonoBehaviour
   {
      #region Nested types

      private struct SwipeBeginInfo
      {
         public float Time;
         public Vector2 NormPos;
         public float CamAngle;
      }

      private enum MoveState
      {
         IDLE,
         ANALYZING,
         BRAKING,
         ACCELERATING
      }

      #endregion

      #region Compile-time constants

      private const float ACCEL_POWER = 3000f;

      private const float BREAK_POWER = 15f;

      private const float MIN_HOLD_TIME_FOR_BREAK = 0.2f;

      private const float MAX_NORM_DIST_FOR_BREAK = 0.05f;

      private const int TOUCH_OR_MOUSE_LEFT_CLICK = 0;

      private const float MAX_SWIPE_DELTA_LENGTH = 0.1f;

      #endregion

      #region Runtime constants

      private static readonly Dictionary<MoveState, Color> MOVE_STATE_TO_DEBUG_TEXT_COLOR =
         new Dictionary<MoveState, Color>
         {
            {MoveState.IDLE, Color.black},
            {MoveState.ANALYZING, Color.yellow},
            {MoveState.BRAKING, Color.red},
            {MoveState.ACCELERATING, Color.green}
         };

      #endregion

      #region Private fields

      [SerializeField, UsedImplicitly]
      private Text m_DebugStateText;

      private Vector2 m_PrevNormTouchPos;

      private SwipeBeginInfo m_Sbi;

      private MoveState m_PrevMoveState;

      #endregion

      #region Properties

      private Tumbleweed Tw
      {
         get { return GetComponent<Tumbleweed>(); }
      }

      #endregion

      #region Methods

      [UsedImplicitly]
      private void Start()
      { }

      [UsedImplicitly]
      private void Update()
      {
         // If player is touching screen
         if (Input.GetMouseButton(TOUCH_OR_MOUSE_LEFT_CLICK))
         {
            UpdateSwipe();
         }
         else
         {
            m_PrevMoveState = MoveState.IDLE;
         }

         // Update move state debug text
         m_DebugStateText.text = m_PrevMoveState.ToString();
         m_DebugStateText.color = MOVE_STATE_TO_DEBUG_TEXT_COLOR[m_PrevMoveState];
      }

      private void UpdateSwipe()
      {
         // Get normalized touch position
         Vector2 normTouchPos = Input.mousePosition / Screen.width;

         // If first frame of touch
         if (Input.GetMouseButtonDown(TOUCH_OR_MOUSE_LEFT_CLICK))
         {
            // Remember swipe begin info
            m_Sbi = new SwipeBeginInfo
            {
               Time = Time.time,
               NormPos = normTouchPos,
               CamAngle = Camera.main.transform.rotation.eulerAngles.y
            };
         }

         // Wait some time at the beginning of the swipe to detect if it's a brake
         bool canBrake = Time.time - m_Sbi.Time > MIN_HOLD_TIME_FOR_BREAK;

         var force2D = Vector2.zero;

         // If touch has almost stayed at the same place for a certain amount of time, interpret
         // this as a break
         if (canBrake &&
             m_PrevMoveState != MoveState.ACCELERATING &&
             Vector2.Distance(normTouchPos, m_Sbi.NormPos) < MAX_NORM_DIST_FOR_BREAK)
         {
            // Compute the breaking force, which is the opposite of the 2D rolling direction
            force2D = Tw.RollingDir2D * -BREAK_POWER;

            // force2D *= 3 / Mathf.Min(Tw.Speed2D, 3);

            m_PrevMoveState = MoveState.BRAKING;
         }
         else
         {
            if (m_PrevMoveState == MoveState.ACCELERATING || m_PrevMoveState == MoveState.ANALYZING)
            {
               // Set force in the direction of the swipe
               force2D = normTouchPos - m_PrevNormTouchPos;

               // This is to prevent player from using two fingers to go fast very quickly
               if (force2D.magnitude < MAX_SWIPE_DELTA_LENGTH)
               {
                  // Rotate the force according to camera angle at the start of the swipe
                  force2D = Utils.RotateVec2D(force2D, m_Sbi.CamAngle);

                  // Adjust acceleration power
                  force2D *= ACCEL_POWER;
               }
               else
               {
                  force2D = Vector2.zero;
               }
            }

            m_PrevMoveState = canBrake ? MoveState.ACCELERATING : MoveState.ANALYZING;
         }

         // Add force to the tw
         GetComponent<Rigidbody>().AddForce(Utils.Vec2DTo3D(force2D));

         m_PrevNormTouchPos = normTouchPos;
      }

      #endregion
   }
}
