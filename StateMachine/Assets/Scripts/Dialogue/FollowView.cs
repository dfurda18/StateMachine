using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowView : MonoBehaviour
{
    public Transform lookAt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(new Vector3(lookAt.position.x, this.transform.position.y, lookAt.transform.position.z));
    }
}
