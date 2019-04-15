using System.Collections;
using UnityEngine;

public enum Direction { StartingPosition, EndingPosition }

public class MovingObject : MonoBehaviour
{
    public float speed = 5;
    public float delayBetweenSwitch = 0.25f;
    public Vector3 startPos;
    public Vector3 endPos;

    private Vector3 pos;
    private Direction currentDir = Direction.StartingPosition;

    private void Start()
    {
        pos = transform.position;
    }

    private void Update()
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + startPos, 0.25f);
        Gizmos.DrawSphere(transform.position + endPos, 0.25f);
    }
}
