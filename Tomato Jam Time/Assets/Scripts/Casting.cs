using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    [SerializeField] Transform transform;
    [SerializeField] Rigidbody rigidBody;

    Transform origin;

    // Start is called before the first frame update
    void Start()
    {
        origin = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void castRod()
    {
        rigidBody.useGravity = true;
    }

    public void reelInRod()
    {
        transform = origin;
        rigidBody.useGravity = false;
    }
}
