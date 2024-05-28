using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    public enum State { None, SelectTarget };

    public static MouseHover instance { get; set; }

    new Collider2D collider;

    public State state { get; private set; }   
    public GameObject target { get; private set; }

    void Awake()
    {
        instance = this;
        collider = GetComponent<Collider2D>();
    }
  
    void Update()
    {
        if (state == State.None)
            return;

        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0f;

        transform.position = position;
    }

    public void SetState(State state)
    {
        this.state = state;

        if (state == State.None)
            collider.enabled = false;        
        else
            collider.enabled = true;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
    
}
