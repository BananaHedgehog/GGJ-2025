using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject player;
    public GameObject[] targets;
    public GameObject closestTarget;
    float closestDist;
    float dist;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < targets.Length; i++)
            {                
                dist = Vector3.Distance(player.transform.position, targets[i].transform.position);

                if (i == 0)
                {
                    closestDist = dist;
                    closestTarget = targets[i];
                }
                else
                {
                    if (dist < closestDist)
                    {
                        closestDist = dist;
                        closestTarget = targets[i];
                    }
                }

                Debug.Log(i);
            }  
        }
    }
}
