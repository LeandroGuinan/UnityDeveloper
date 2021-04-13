using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementLeftRight : MonoBehaviour
{
    public float speed;
    [SerializeField] private float timeToChageDirection;
    private bool directionControl = true;
    

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("controlMove");
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    /// <summary>
    /// allows to move enemy in a time interval.
    /// </summary>
    void MoveEnemy()
    {
        if (directionControl)
        {
            this.transform.Translate(Vector3.left * (speed * Time.deltaTime));
        }
        else
        {
            this.transform.Translate(Vector3.right * (speed * Time.deltaTime));
        }
    }

    IEnumerator controlMove()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(timeToChageDirection);
            if (directionControl)
            {
                directionControl = false;
            }
            else
            {
                directionControl = true;
            }
            
        }
    }
}
