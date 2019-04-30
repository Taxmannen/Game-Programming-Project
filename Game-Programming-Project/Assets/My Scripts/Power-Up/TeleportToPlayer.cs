using System.Collections;
using UnityEngine;

public class TeleportToPlayer : Item
{
    public float dissolveSpeed = 0.5f;
    public float edgeSpeed = 0.2f;

    private Material mat;
    private PlayerController pc;
    private Coroutine coroutine;

    private void Start()
    {
        mat = player.GetComponent<SpriteRenderer>().material;
        pc = player.GetComponent<PlayerController>();
        UseItem();
    }

    public override void UseItem()
    {
        StartCoroutine(Teleport());
    }

    private IEnumerator Teleport()
    {
        AudioManager.INSTANCE.Play("Teleport", pitch: 1.1f);
        pc.SetUnableToMove(true);
        StartCoroutine(LerpAndChangeShader(0.25f, edgeSpeed, "_Edges"));

        yield return new WaitForSeconds(edgeSpeed);

        StartCoroutine(LerpAndChangeShader(1, dissolveSpeed, "_Level"));

        yield return new WaitForSeconds(dissolveSpeed);

        player.position = otherPlayer.position;
        AudioManager.INSTANCE.Play("Teleport", pitch: 1.1f);
        StartCoroutine(LerpAndChangeShader(0f, edgeSpeed, "_Edges"));

        yield return new WaitForSeconds(edgeSpeed);
   
        StartCoroutine(LerpAndChangeShader(0, dissolveSpeed, "_Level"));

        yield return new WaitForSeconds(dissolveSpeed);

        pc.SetUnableToMove(false);
        base.UseItem();
    }

    private IEnumerator LerpAndChangeShader(float newValue, float fadeTime, string name)
    {
        float value = mat.GetFloat(name);
        for (float time = 0.0f; time < 1.0f; time += Time.deltaTime / fadeTime)
        {
            float newColor = Mathf.Lerp(value, newValue, time);
            mat.SetFloat(name, newColor);
            yield return null;
        }
        mat.SetFloat(name, newValue);
    }
}