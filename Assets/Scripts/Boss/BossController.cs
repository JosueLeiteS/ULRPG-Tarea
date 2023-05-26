using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BossController : MonoBehaviour
{
    #region Public Properties
    public float WakeDistance = 8f;
    public float Speed = 1f;
    public float SwingDistance = 0.75f;
    public float StabDistance = 1.5f;
    public float ChargeDistance = 4f;
    public int AttackCounter = 0;
    public int AttacksForCharge = 5;
    public bool StabBool = false;
    public bool ChargeReady = false;
    public bool Spinning = false;

    #endregion

    #region Components
    public Transform Player;
    public Rigidbody2D rb { private set; get; }
    public Animator animator { private set; get; }
    
    public bool AttackingEnd { set; get; } = false;
    public Transform hitBox { private set; get; }

    #endregion

    #region Private Properties
    private FSM<BossController> mFSM;

    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hitBox = transform.Find("HitBox");

        mFSM = new FSM<BossController>(new Boss.IdleState(this));
        mFSM.Begin();
    }

    private void FixedUpdate()
    {
        mFSM.Tick(Time.fixedDeltaTime);
    }

    public void SetAttackingEnd()
    {
        AttackingEnd = true;

        if (AttackCounter == AttacksForCharge) {
            ChargeReady = true;
            AttackCounter = 0;
        }
    }

    public void StartSpin()
    {
        Spinning = true;
    }

    public void EndSpin()
    {
        Spinning = false;
    }

}
