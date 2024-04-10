using System;
using System.Collections;
using System.Collections.Generic;
using BoneGame.Message;
using BoneGame.System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Player : InputMonoBehaviour
{
    public float moveSpeed;
    
    private Vector2 m_Rotation;
    private Vector2 m_Look;
    private Vector2 m_Move;
    [SerializeField]
    private Direction NowDirection;

    [SerializeField] private float LeftLimit;
    [SerializeField] private float RightLimit;

    private bool CanControl;

    private void Start()
    {
        IsActive = true;
        Registration();
    }

    public void Update()
    {
        // Update orientation first, then move. Otherwise move orientation will lag
        // behind by one frame.
        Move(m_Move);
    }


    public override void MoveAction(InputAction.CallbackContext context)
    {
        m_Move = context.ReadValue<Vector2>();
    }
    
    public override void FireAction(InputAction.CallbackContext context)
    {
        Fire();
    }
    
    private void Move(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01)
            return;
        var scaledMoveSpeed = moveSpeed * Time.deltaTime;
        var move = Quaternion.Euler(0, 0, 0) * new Vector3(direction.x, 0, 0);

        var destination = transform.position + move * scaledMoveSpeed;

        if (destination.x < LeftLimit) return;
        if (destination.x > RightLimit) return;
        
        transform.position += move * scaledMoveSpeed;
        
        if (move.x > 0)
        {
            if (NowDirection != Direction.right)
            {
                // 右を向いていない時はy軸方向にrotationを180°回転させて右を向かせる。
                // Dotweenでtweenで動かす
                NowDirection = Direction.right;
                transform.DORotate(new Vector3(0, 180, 0), 0.5f);
              
            }
        }
        else if (NowDirection != Direction.left)
        {
            NowDirection = Direction.left;  
            transform.DORotate(new Vector3(0, 0, 0), 0.5f);
         
        }
    }
    

//OKボタンの挙動
    private void Fire()
    {
    }

    private enum Direction
    {
        left,
        right
    }
}