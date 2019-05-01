using UnityEngine;

public class Marker : MonoBehaviour
{
    private Camera cam;
    private Transform player;
    private Transform otherPlayer;
    private Vector2 bounds;

    SpriteRenderer sr;
    Vector3 pos;
    float diff = 0f;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bounds = new Vector2(sr.bounds.extents.x / 15, sr.bounds.extents.y / 15);
    }

    private void FixedUpdate()
    {
        pos = cam.WorldToViewportPoint(otherPlayer.position);
        BoundsCheck();

        pos.x = Mathf.Clamp(pos.x, bounds.x + diff, (1 - diff) - bounds.x);
        pos.y = Mathf.Clamp(pos.y, bounds.y + diff, (1 - diff) - bounds.y);
        transform.position = cam.ViewportToWorldPoint(pos);

  
    }
    private void Update()
    {
        var dir = otherPlayer.position - player.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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

    public void SetCameraAndPlayer(Camera cam, Transform player, Transform otherPlayer)
    {
        this.cam = cam;
        this.player = player;
        this.otherPlayer = otherPlayer;
    }
}