using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonAnim : MonoBehaviour
{
    public void StartAnim()
    {
        StartCoroutine(nameof(PistonAnimation));
    }

    private IEnumerator PistonAnimation()
    {
        float timer = 0;
        GameObject head = transform.Find("Piston Head").gameObject;
        head.transform.localPosition = new Vector2(0, 0.8f);
        yield return new WaitForEndOfFrame();
        while (head.transform.localPosition != Vector3.zero)
        {
            timer += Time.deltaTime * 0.5f;
            head.transform.localPosition = new Vector2(0, Mathf.Lerp(head.transform.localPosition.y, 0, timer));
            yield return new WaitForEndOfFrame();
        }
    }
}
