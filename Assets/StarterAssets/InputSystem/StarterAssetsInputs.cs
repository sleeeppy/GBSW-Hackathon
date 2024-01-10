using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool aim;
		public bool shoot;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = false;
		public bool cursorInputForLook;

#if ENABLE_INPUT_SYSTEM
        private void Update()
        {
			MouseCursor();
        }
		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}
        private void OnLevelWasLoaded()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            int currentSceneIndex = currentScene.buildIndex;

            if (currentSceneIndex == 0)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else if (currentSceneIndex != 0)
            {
				Cursor.lockState = CursorLockMode.None;
            }
        }

        public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }
        public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnAim(InputValue value)
		{
			AimInput(value.isPressed);
		}

		public void OnShoot(InputValue value)
		{
			ShootInput(value.isPressed);
		}
#endif
        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }
		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void AimInput(bool newAimState)
		{
			aim = newAimState;
		}

		public void ShootInput(bool newShootState)
		{
			shoot = newShootState;
		}
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
        private void MouseCursor()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            int currentSceneIndex = currentScene.buildIndex;

            if (currentSceneIndex == 0)
            {
                cursorLocked = true;
                cursorInputForLook = true;
            }
            else if (currentSceneIndex == 2)
            {
                cursorLocked = false;
                cursorInputForLook = false;
            }
        }
    }
	
}