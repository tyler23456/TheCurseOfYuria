using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), (typeof(Animator)))]
public class FSMBehaviour : MonoBehaviour
{
    [SerializeField] FSMNode startingNode;

    public IActor actor { get; private set; }
    public new Rigidbody2D rigidbody2D { get; private set; }
    public Animator animator { get; private set; }
    public FSMNode currentNode { get; set; }
    
    void Awake()
    {
        actor = GetComponent<IActor>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentNode = startingNode;
    }

    
    void Update()
    {
        currentNode.UpdateNode(this);
    }
}
