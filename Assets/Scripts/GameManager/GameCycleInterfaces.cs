using System;
using UnityEngine;

namespace ShootEmUp
{
   public interface IActivateable
   {
      bool IsActive { get; }
      void SetActive(bool isActive);
   }
   
   public interface IUserInputListener
   {
      void OnInputUpdated(Vector2 axis);
      void OnFireClicked();
   }

   public interface IStartGameListener
   {
      void Start();
   }

   public interface IPauseGameListener
   {
      void Pause();
      void Resume();
   }

   public interface IFinishGameListener
   {
      void Finish();
   }

   public interface IUpdateGameListener
   {
      void Update();
   }

   public interface IFixedUpdateGameListener
   {
      void FixedUpdate();
   }
}