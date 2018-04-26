using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {


    public AnimationCurve curve; 


    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.contacts[0].point.y < transform.position.y)
        {
            StartCoroutine("Sample");
        }
    }

    IEnumerator Sample()
    {
        var pos = transform.position;

        for (float t = 0; t < curve.keys[curve.length - 1].time; t += Time.deltaTime)
        {
            transform.position = new Vector2(pos.x, pos.y + curve.Evaluate(t));
            yield return null;
        }
    }
}
