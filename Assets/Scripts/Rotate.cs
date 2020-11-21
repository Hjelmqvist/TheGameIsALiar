using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public enum RotateType
    {
        Reverse = -1,
        Nothing = 0,
        Normal = 1
    }

    [SerializeField] float duration = 1;
    [SerializeField] RotateType y = RotateType.Normal, z = RotateType.Nothing;
    Coroutine rotating = null;

    void Update()
    {
        if (Input.GetButtonDown("Rotate") && rotating == null)
            rotating = StartCoroutine(RotateCoroutine());

        IEnumerator RotateCoroutine()
        {
            float rotateInput = Input.GetAxis("Rotate");
            if (Mathf.Abs(rotateInput) == 1)
            {
                Vector3 rotChange = new Vector3(0, 90 * (int)y * rotateInput, 90 * (int)z * rotateInput);
                Vector3 startRot = transform.eulerAngles;
                Vector3 targetRot = startRot + rotChange;

                for (float i = 0; i < 1; i += Time.deltaTime / duration)
                {
                    transform.eulerAngles = Vector3.Lerp(startRot, targetRot, i);
                    yield return null;
                }
                transform.eulerAngles = targetRot;
            }
            rotating = null;
        }
    }
}
