using System.Collections;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Destroy (gameObject, 1f);
    } 
}