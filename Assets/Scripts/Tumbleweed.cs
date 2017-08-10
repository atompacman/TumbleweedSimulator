// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// MIT License
// Copyright (c) 2017 Stained Glass Guild
// See file "LICENSE.txt" at project root for complete license
// ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~   ~
// Project: SimpleMobilePlaceholder
// File: Tumbleweed.cs
// Creation: 2017-08
// Author: Jérémie Coulombe
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

using UnityEngine;

namespace SGG.TWS
{
   public sealed class Tumbleweed : MonoBehaviour
   {
      #region Properties

      public Vector3 Position
      {
         get { return transform.position; }
      }

      public Vector2 Velocity2D
      {
         get { return Utils.Vec3DTo2D(Velocity3D); }
      }

      public Vector3 Velocity3D
      {
         get { return GetComponent<Rigidbody>().velocity; }
      }

      public Vector2 RollingDir2D
      {
         get { return Velocity2D.normalized; }
      }

      public Vector3 RollingDir3D
      {
         get { return Velocity3D.normalized; }
      }

      public float Speed2D
      {
         get { return Velocity2D.magnitude; }
      }

      public float Speed3D
      {
         get { return Velocity3D.magnitude; }
      }

      public TumbleweedController Controller
      {
         get { return GetComponent<TumbleweedController>(); }
      }

      #endregion
   }
}
