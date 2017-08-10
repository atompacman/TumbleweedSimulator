// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// MIT License
// Copyright (c) 2017 Stained Glass Guild
// See file "LICENSE.txt" at project root for complete license
// ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~
// Project: SimpleMobilePlaceholder
// File: Utils.cs
// Creation: 2017-08
// Author: Jérémie Coulombe
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using UnityEngine;

namespace SGG.TWS
{
   public static class Utils
   {
      #region Static methods

      public static Vector3 Vec2DTo3D(Vector2 a_Vec2D)
      {
         return new Vector3(a_Vec2D.x, 0, a_Vec2D.y);
      }

      public static Vector2 Vec3DTo2D(Vector3 a_Vec3D)
      {
         return new Vector2(a_Vec3D.x, a_Vec3D.z);
      }

      public static Vector2 RotateVec2D(Vector2 a_Vec2D, float a_Angle)
      {
         return Vec3DTo2D(Quaternion.Euler(0, a_Angle, 0) * Vec2DTo3D(a_Vec2D));
      }

      #endregion
   }
}
