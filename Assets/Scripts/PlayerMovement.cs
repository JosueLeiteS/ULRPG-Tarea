using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 4f;
    private Conversation conversation;
    private Rigidbody2D mRb;
    private Vector3 mDirection = Vector3.zero;
    public Vector3 mLastPosition = Vector3.zero;
    private Animator mAnimator;
    private PlayerInput mPlayerInput;
    private Transform hitBox;
    public int numberOfAttacks;
    private int actualAttack = 1;
    private bool isconversation;
    public Image image;
    public Sprite[] numbers;

    private void Start()
    {
        mRb = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mPlayerInput = GetComponent<PlayerInput>();

        hitBox = transform.Find("HitBox");

        ConversationManager.Instance.OnConversationStop += OnConversationStopDelegate;
        
    }

    private void OnConversationStopDelegate()
    {
        mPlayerInput.SwitchCurrentActionMap("Player");
    }

    private void Update()
    {
        if (mDirection != Vector3.zero)
        {
            mAnimator.SetBool("IsMoving", true);
            mAnimator.SetFloat("Horizontal", mDirection.x);
            mAnimator.SetFloat("Vertical", mDirection.y);
            mLastPosition = mDirection;
        }
        else
        {
            // Quieto
            mAnimator.SetBool("IsMoving", false);
        }
        if (Input.GetKeyDown(KeyCode.Return) && isconversation == true) {
            isconversation = false;
            mPlayerInput.SwitchCurrentActionMap("Conversation");
            ConversationManager.Instance.StartConversation(conversation);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            Debug.Log(actualAttack);
            if (actualAttack < numberOfAttacks)
            {
                actualAttack++;
                image.sprite = numbers[actualAttack - 1];
            }
            else
            {
                actualAttack = 1;
                image.sprite = numbers[actualAttack - 1];
            }
        }
    }

    private void FixedUpdate()
    {
        mRb.MovePosition(
            transform.position + (mDirection * speed * Time.fixedDeltaTime)
        );
    }

    public void OnMove(InputValue value)
    {
        mDirection = value.Get<Vector2>().normalized;
    }

    public void OnNext(InputValue value)
    {
        if (value.isPressed)
        {
            ConversationManager.Instance.NextConversation();
        }
    }

    public void OnCancel(InputValue value)
    {
        if (value.isPressed)
        {
            ConversationManager.Instance.StopConversation();
        }
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed && actualAttack == 1)
        {
            Debug.Log("Attack");
            mAnimator.SetTrigger("Attack");
            hitBox.gameObject.SetActive(true);
            AudioManager.instance.Play("Da�o");
        }
        else if (value.isPressed && actualAttack == 2)
        {
            Debug.Log("S");
            hitBox.gameObject.SetActive(true);
            mAnimator.SetTrigger("Spin");
            AudioManager.instance.Play("Da�o2");
        }
        else if(value.isPressed && actualAttack == 3)
        {
            Debug.Log("S");
            mAnimator.SetTrigger("Shotgun");
            //AudioManager.instance.Play("Da�o3");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

            if (other.transform.TryGetComponent<Conversation>(out conversation))
            {
                isconversation = true;
            }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isconversation = false;
    }
    public void DisableHitBox()
    {
        hitBox.gameObject.SetActive(false);
    }
}
