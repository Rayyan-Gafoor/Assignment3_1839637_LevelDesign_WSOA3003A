using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMove : MonoBehaviour
{
    public float speed;
    //public Transform Enemy;
    //private Vector2 target;
    public Transform target;
    public float lifetime;

    public void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, 0.02f);
        Destroy(gameObject, lifetime);
    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }*/
    //public void OnTriggerEnter2D(Collider2D collision)
    
    /*private void Start()
    {
        
        target = new Vector3(Enemy.transform.position.x, Enemy.transform.position.y, Enemy.transform.position.z);
    }
    private void Update()
    {
        transform.position= Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime);
    }
   */

}
