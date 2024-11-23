using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPowered : LightObject
{
    public bool isPowered;
    public Coroutine currentCoroutine = null;
    // Start is called before the first frame update
    new void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public virtual void PowerOn()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        isPowered = true;
        currentCoroutine = StartCoroutine(PowerDown());
    }

    public IEnumerator PowerDown()
    {
        yield return new WaitForSeconds(0.1f);
        isPowered = false;
        OnPowerDown();
    }

    public virtual void OnPowerDown()
    {

    }

    public override void OnHit(Vector2 hitPos, Vector2 emitDir, Vector2 hitNormal, LightObject last_hit = null, int recursions = 0)
    {
        PowerOn();
    }


}
