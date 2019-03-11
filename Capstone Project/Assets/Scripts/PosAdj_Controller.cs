using UnityEngine;
using System.Collections;

public class PosAdj_Controller : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    bool objectMoving = false;
    bool objectSelected = false;
    Vector3 previousPosition;
    Transform selectedObj;


    public AnimationCurve animCurve;

    public Transform selectTransform;

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!objectSelected)
                {
                    print(hit.collider.name + " has been selected!");
                    selectedObj = hit.transform;
                    objectSelected = true;
                    previousPosition = selectedObj.position;
                    MoveTo(selectedObj.gameObject, selectTransform.position, 0.2f);
                    
                }
            }
        }


        if(objectSelected && !objectMoving)
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                objectSelected = false;
                MoveTo(selectedObj.gameObject, previousPosition, 0.2f);
            }
        }

    }




    IEnumerator AnimateMove(GameObject obj, Vector3 origin, Vector3 target, float duration)
    {
        objectMoving = true;
        float journey = 0f;
        while (journey <= duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);

            float curvePercent = animCurve.Evaluate(percent);
            obj.transform.position = Vector3.LerpUnclamped(origin, target, curvePercent);

            yield return null;
        }

        objectMoving = false;
    }

    void MoveTo(GameObject obj, Vector3 target, float duration)
    {
        StartCoroutine(AnimateMove(obj, obj.transform.position, target, duration));
    }
}