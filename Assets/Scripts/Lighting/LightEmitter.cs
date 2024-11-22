using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEmitter : MonoBehaviour
{
    [SerializeField] Vector2 emitDirection;
    [SerializeField] LineRenderer lr;

    int indexes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int indexes = 0;
        lr.SetPositions(new Vector3[] { });
        lr.positionCount = 0;
        Emit(transform.position, emitDirection);
    }

    void Emit(Vector2 startPos, Vector2 emitDir, LightReflector last_hit = null){

        AddPosition(startPos);

        RaycastHit2D ray = Physics2D.CircleCast(startPos, 0.1f, emitDir.normalized, 20f);

        if (ray){
            if (ray.transform.TryGetComponent<LightReflector>(out LightReflector reflector)){
                if (reflector != last_hit) {
                    last_hit = reflector;
                    Emit(ray.point, reflector.Reflect(emitDir), reflector);
                }
            }
            else{
                AddPosition(startPos + (emitDir * 100f));
            }
        }
        else{
            AddPosition(startPos + (emitDir * 100f));
        }
    }

    void AddPosition(Vector2 pos){
        lr.positionCount += 1;
        lr.SetPosition(lr.positionCount -1, pos);
    }
}
