using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    // fitness score to resemble to enviornment
    public float fitness_score = 1.0f;
    public Color color = new Color(1.0f,1.0f,1.0f);
    public Material material;
    private void Awake() {
        material = gameObject.GetComponent<Renderer>().material;
        
    }
    public void setColor(Color color){
        this.color = color;
        material.color = color;
    }
}
