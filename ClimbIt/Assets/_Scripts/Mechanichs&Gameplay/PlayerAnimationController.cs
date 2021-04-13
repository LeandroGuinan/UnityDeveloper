using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    
    private Animator _animator;
    private bool direction = true;
    private float horizontalInput;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        MoveCharacter(horizontalInput);
    }

    private void FixedUpdate()
    {
        ControlDirection();
    }

    
    /// <summary>
    /// allows to control the player Animation when he move through the scene
    /// </summary>
    /// <param name="playerInput">the input of the player</param>
    void MoveCharacter(float playerInput)
    {
        if (Mathf.Abs(playerInput) > 0.1f)
        {
            _animator.SetBool("IsMoving",true);
            _animator.SetFloat("Move",playerInput);
        }
        else
        {
            _animator.SetBool("IsMoving",false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            _animator.SetTrigger("Jump");
        }
    }

    
    /// <summary>
    /// help to control the avatar direction while he's moving
    /// </summary>
    void ControlDirection()
    {
        if (horizontalInput < 0 && direction)
        {
            this.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y * -1,
                transform.rotation.z, transform.rotation.w);
            direction = false;
        }

        if (horizontalInput > 0 && !direction)
        {
            this.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y * -1,
                transform.rotation.z, transform.rotation.w);
            direction = true;
        } 
    }
}
