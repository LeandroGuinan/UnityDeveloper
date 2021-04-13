using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private GameObject playerCharacter;
    private GameObject player;
    private float horizontalInput;
    private float rotationMulti = 0.15f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCharacter = GameObject.Find("PlayerCharacter");
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        RotateObj(horizontalInput);
        
    }

    /// <summary>
    /// rotate the object whit a direction in Game and restrict it.
    /// </summary>
    /// <param name="rotationDirection">the input of the user</param>
    void RotateObj(float rotationDirection)
    {
        this.transform.Rotate(player.transform.up * (rotationSpeed * Time.deltaTime * rotationDirection));

        if (Mathf.Abs(transform.rotation.y) > rotationMulti)
        {
            if (transform.rotation.y < 0)
            {
                this.transform.rotation = new Quaternion(transform.rotation.x,-rotationMulti,transform.rotation.z,transform.rotation.w);
            }
            else
            {
                this.transform.rotation = new Quaternion(transform.rotation.x,rotationMulti,transform.rotation.z,transform.rotation.w);
            }
        }
        //Debug.Log(transform.rotation.y);
    }
    
}
