using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] int hp = 100;
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public int Hp { get => hp; set { hp = value; OnHpChanged(); } }

    public event UnityAction OnHpChange;

    private void OnHpChanged()
    {
        
    
        OnHpChange?.Invoke();
    }
}
