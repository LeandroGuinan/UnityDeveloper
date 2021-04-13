using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(
    typeof(Rigidbody),
    typeof(BoxCollider),
    typeof(TrailRenderer))]
public class PlayerController : MonoBehaviour
{
    //Move & Control:
    [SerializeField]private float moveSpeed;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;
    private bool isOnTheFloor;
    [SerializeField] private float forceJump;
    private float minY = 1.1f;
    
    
    //Mechanics
    private bool trailKiller;
    private bool timeStopCast;
    private TrailRenderer _trailRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        _trailRenderer = GetComponent<TrailRenderer>();
        timeStopCast = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        
        Movement(horizontalInput,verticalInput);
        StartPowerTrail();
        Jump();
    }
    

    /// <summary>
    /// allows move and jump through the scene.
    /// </summary>
    void Movement(float playerInputMoveHorizontal,float playerInputMoveVertical)
    {
        this.transform.Translate(Vector3.right*(moveSpeed*playerInputMoveHorizontal*Time.deltaTime));
        
        if (Mathf.Abs(playerInputMoveVertical) > 0.1f &&  _rigidbody.isKinematic && !timeStopCast)
        {
            this.transform.Translate(Vector3.up*(moveSpeed*playerInputMoveVertical*Time.deltaTime));
            
            //Security Check:
            if (this.transform.position.y < minY)
            {
                transform.position = new Vector3(this.transform.position.x, minY, 0);
            }
            isOnTheFloor = false;
        }
    }
    
    /// <summary>
    /// allows jump over the scene using Physics
    /// </summary>
    void Jump()
    {
        if (Input.GetButtonDown("Jump")  && isOnTheFloor & !_rigidbody.isKinematic && Time.timeScale == 1)
        {
            _rigidbody.AddForce(Vector3.up*forceJump,ForceMode.Impulse);
            isOnTheFloor = false;
        } 
    }

    /// <summary>
    /// starts the coroutine if the input allow it, it only happend if the player is on the floor
    /// & the input allow it.
    /// </summary>
    void StartPowerTrail()
    {
        if (Input.GetButtonDown("Fire1") && timeStopCast && Time.timeScale == 1)
        {
            StartCoroutine("powerOn");
        }
    }

    /// <summary>
    /// help to control the coroutine, here create all the effects, it controls time and space, take care
    /// </summary>
    /// <param name="command"></param>
    void PowerUpControl(bool command)
    {
        
        _trailRenderer.emitting = command;
        trailKiller = command;
        _rigidbody.isKinematic = command;
        _boxCollider.isTrigger = command;
        if (command)
        {
            Time.timeScale = 0.5f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isOnTheFloor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && trailKiller && _boxCollider.isTrigger)
        {
            Destroy(other.gameObject);
            
        }
    }

    /// <summary>
    /// activate and un-active the player power mechanic & control the power to make the slow motion effect.
    /// </summary>
    IEnumerator powerOn()
    {
        timeStopCast = false;
        PowerUpControl(true);
        yield return new WaitForSecondsRealtime(3);
        PowerUpControl(false);
        
        yield return new WaitForSecondsRealtime(5);
        timeStopCast = true;
    }
}
