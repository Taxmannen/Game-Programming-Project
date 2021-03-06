﻿using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 endPos;
    [SerializeField] private Vector2 scale;
    [SerializeField] private bool drawGizmos;

    private Vector3 pos;
    private bool go;

    private void Start()
    {
        pos = transform.position;
    }

    private void Update()
    {
        if (go)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, pos + endPos, step);
            if (Vector3.Distance(transform.position, pos + endPos) < 0.01f)
            {
                foreach (Collider2D c in gameObject.GetComponents<Collider2D>()) Destroy(c);
                Destroy(this);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) go = true;
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos) Gizmos.DrawCube(transform.position + endPos, scale);
    }
}
