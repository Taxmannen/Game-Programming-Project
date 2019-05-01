using UnityEngine;

public class Marker : MonoBehaviour
{
    private Camera cam;
    private Transform player;
    private Vector2 bounds;

    SpriteRenderer sr;
    Vector3 pos;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bounds = new Vector2(sr.bounds.extents.x / 15, sr.bounds.extents.y / 15);
    }

    private void FixedUpdate()
    {
        pos = cam.WorldToViewportPoint(player.position);
        BoundsCheck();

        pos.x = Mathf.Clamp(pos.x, bounds.x, 1f - bounds.x);
        pos.y = Mathf.Clamp(pos.y, bounds.y, 1f - bounds.y);
        transform.position = cam.ViewportToWorldPoint(pos);
    }

    private void BoundsCheck()
    {
        if (pos.x > 0 - bounds.x && pos.x < 1 - bounds.x && pos.y > 0 - bounds.x && pos.y < 1 - bounds.x)
        {
            if (sr.enabled) sr.enabled = false;
        }
        else
        {
            if (!sr.enabled) sr.enabled = true;
        }
    }

    public void SetCameraAndPlayer(Camera cam, Transform player)
    {
        this.cam = cam;
        this.player = player;
    }
}