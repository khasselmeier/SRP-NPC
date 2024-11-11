using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinNPC : NPC
{
    public float pressedTime = 2f;
    public float invulnerabilityTime = 4f;
    private bool isPressed = false;
    private bool isInvulnerable = false;
    private Vector3 originalScale;

    protected override void Start()
    {
        base.Start();
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PressDown();
        }
    }

    public void PressDown()
    {
        if (!isPressed && !isInvulnerable)
        {
            StartCoroutine(PressDownRoutine());
        }
    }

    private IEnumerator PressDownRoutine()
    {
        isPressed = true;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);
        TakeDamage(maxHealth * 0.20f);

        yield return new WaitForSeconds(pressedTime);
        transform.localScale = originalScale;

        isPressed = false;
        StartCoroutine(InvulnerabilityRoutine());
    }

    private IEnumerator InvulnerabilityRoutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }

    public override void TakeDamage(float amount)
    {
        if (!isInvulnerable)
        {
            base.TakeDamage(amount);
        }
    }
}
