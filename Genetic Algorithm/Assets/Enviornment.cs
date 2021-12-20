using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enviornment : MonoBehaviour
{
    Material enviornment_material;

    // Start is called before the first frame update
    void Start()
    {
        enviornment_material = gameObject.GetComponent<Renderer>().material;
        StartCoroutine(cycleColors());
    }
    IEnumerator cycleColors(){
        Vector3 previousColor = new Vector3(enviornment_material.color.r,enviornment_material.color.g,enviornment_material.color.b);
        Vector3 currentColor = previousColor;
        float colorTransitionTime = 4.0f;
        while (true)
        {
            Vector3 newColor = new Vector3(UnityEngine.Random.Range(0.0f,1.0f),UnityEngine.Random.Range(0.0f,1.0f),UnityEngine.Random.Range(0.0f,1.0f));
            Vector3 deltaColor = (newColor-previousColor)*(1.0f/colorTransitionTime);
            while((newColor-currentColor).magnitude>0.1f){
                currentColor = currentColor+deltaColor*Time.deltaTime;
                enviornment_material.color = new Color(currentColor.x,currentColor.y,currentColor.z);
                yield return null;
            }
            previousColor = newColor;
        }
    }
}
