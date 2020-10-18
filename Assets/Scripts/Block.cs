using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
