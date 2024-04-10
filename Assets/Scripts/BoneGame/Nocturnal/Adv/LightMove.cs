using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BoneGame.Data.Adv
{
    public class LightMove : MonoBehaviour
    {
          public float moveSpeed;
            
            private Vector2 m_Move;
        
            
            public void Update()
            {
                // Update orientation first, then move. Otherwise move orientation will lag
                // behind by one frame.
                Move(m_Move);
            }
        
            public void OnMove(InputAction.CallbackContext context)
            {
                m_Move = context.ReadValue<Vector2>();
            }
            
            
            private void Move(Vector2 direction)
            {
                if (direction.sqrMagnitude < 0.01)
                    return;
                var scaledMoveSpeed = moveSpeed * Time.deltaTime;
                var move = Quaternion.Euler(0, 0, 0) * new Vector3(direction.x, 0, 0);
                
                transform.position += move * scaledMoveSpeed;
            }
    }
}