﻿using System.Collections;
using UnityEngine;

public enum Direction { StartingPosition, EndingPosition }

public class MovingObject : MonoBehaviour
{
    #region Variables
    [SerializeField] private float speed = 5;
    [SerializeField] private float delayBetweenSwitch = 0.25f;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;

    [Header("Debug")]
    [SerializeField] private bool drawGizmos;

    private Vector3 pos;
    private Direction currentDir = Direction.StartingPosition;
    #endregion

    private void Start()
    {
        pos = transform.position;
    }

    private void FixedUpdate()
    {
        float step = speed * Time.deltaTime;
        if (currentDir == Direction.StartingPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, pos + startPos, step);
            if (Vector3.Distance(transform.position, pos + startPos) < 0.1f)
                StartCoroutine(ChangeDirection(Direction.EndingPosition, delayBetweenSwitch));
        }
        else if (currentDir == Direction.EndingPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, pos + endPos, step);
            if (Vector3.Distance(transform.position, pos + endPos) < 0.1f)
                StartCoroutine(ChangeDirection(Direction.StartingPosition, delayBetweenSwitch));
        }
    }

    private IEnumerator ChangeDirection(Direction dir, float delay)
    {
        yield return new WaitForSeconds(delay);
        currentDir = dir;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Vector3 drawPos = Application.isPlaying ? pos : transform.position;
            Gizmos.DrawSphere(drawPos + startPos, 0.25f);
            Gizmos.DrawSphere(drawPos + endPos, 0.25f);
        }
    }
}