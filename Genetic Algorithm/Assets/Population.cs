using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Population : MonoBehaviour
{
    public Text generationNumber;
    private int generation_iteration=0;
    public int poplationSize = 100;
    public GameObject enviornment;
    protected List<Rabbit> population_rabbit = new List<Rabbit>();
    // Start is called before the first frame update
    void Start()
    {
        Bounds boundaries = enviornment.GetComponent<Renderer>().bounds;
        
        for(int i=0;i<poplationSize;i++){
            Rabbit rabbit = CreateRabbit(boundaries);
            population_rabbit.Add(rabbit);
        }
        StartCoroutine(EvaluationCycle());
    }

    public Rabbit CreateRabbit(Bounds bounds){
       Vector3 randomPos = new Vector3(UnityEngine.Random.Range(-0.5f,0.5f)*bounds.size.x,UnityEngine.Random.Range(-0.5f,0.5f)*bounds.size.y,UnityEngine.Random.Range(-0.5f,0.5f)*bounds.size.z);
        Vector3 worldPos = enviornment.transform.position + randomPos;
        GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        Rabbit rabbit = temp.AddComponent<Rabbit>();
          


                  float height = temp.GetComponent<MeshFilter>().mesh.bounds.size.y;
        worldPos.y = worldPos.y+height/2.0f;
        temp.transform.position = worldPos;
        temp.GetComponent<Rabbit>().setColor(new Color(UnityEngine.Random.Range(0.0f,1.0f),
                                                       UnityEngine.Random.Range(0.0f,1.0f),
                                                       UnityEngine.Random.Range(0.0f,1.0f)
        ));
        return rabbit;
    }
    float EvaluateFitness(Color enviornment,Color bunnyColor){
        float fitness_Score_bunny = (new Vector3(enviornment.r,enviornment.g,enviornment.b)-new Vector3(bunnyColor.r,bunnyColor.g,bunnyColor.b)).magnitude;
        return fitness_Score_bunny;
    }
    public void EvaluatePoplation(){
        // Fitness evaluation
        for(int i=0;i<population_rabbit.Count;i++){
            float fitness_evolved = EvaluateFitness(enviornment.GetComponent<MeshRenderer>().material.color,population_rabbit[i].GetComponent<MeshRenderer>().material.color);
            population_rabbit[i].fitness_score = fitness_evolved;
        }
        //Sort based on fitness score
        population_rabbit.Sort(
            delegate(Rabbit rabbit1,Rabbit rabbit2){
                if(rabbit1.fitness_score>rabbit2.fitness_score){
                    return 1;
                }else if(rabbit1.fitness_score==rabbit2.fitness_score){
                    return 0;
                }else{
                    return -1;
                }
            }
        );
        // Kill some
        int halfofpopulation = (int) population_rabbit.Count/2;
        if(halfofpopulation%2!=0){
            halfofpopulation+=1;
        }
        for(int i=halfofpopulation;i<poplationSize;i++){
            Destroy(population_rabbit[i].gameObject);
            population_rabbit[i] = null;
        }
        population_rabbit.RemoveRange(halfofpopulation,population_rabbit.Count-halfofpopulation);
        // Breed 
        Breed();
        generation_iteration++;
        generationNumber.text = generation_iteration.ToString();
    }

    void Breed(){
        //New Babies
        List<Rabbit> babylist = new List<Rabbit>();
        for(int i=1;i<population_rabbit.Count;i+=2){
            int father_index = i-1;
            int mother_index = i;
            float genesplit = UnityEngine.Random.Range(0.0f,1.0f);
            Bounds bounds = enviornment.GetComponent<Renderer>().bounds;
            Rabbit rabbitchild1 = CreateRabbit(bounds);
            Rabbit rabbitchild2 = CreateRabbit(bounds);
            if(genesplit<=0.16f){
                Color tempBabyColor = new Color(population_rabbit[father_index].color.r,
                                                population_rabbit[father_index].color.g,
                                                population_rabbit[mother_index].color.b);
                tempBabyColor = EvaluateMutation(tempBabyColor);
                rabbitchild1.setColor(tempBabyColor);
                Color tempBabyColor1 = new Color(population_rabbit[mother_index].color.r,
                                                population_rabbit[mother_index].color.g,
                                                population_rabbit[father_index].color.b);
                tempBabyColor1 = EvaluateMutation(tempBabyColor1);
                rabbitchild2.setColor(tempBabyColor1);
            }
            else if(genesplit<=0.32f){
                Color tempBabyColor = new Color(population_rabbit[father_index].color.r,
                                                population_rabbit[mother_index].color.g,
                                                population_rabbit[father_index].color.b);
                tempBabyColor = EvaluateMutation(tempBabyColor);

                rabbitchild1.setColor(tempBabyColor);
                Color tempBabyColor1 = new Color(population_rabbit[mother_index].color.r,
                                                population_rabbit[father_index].color.g,
                                                population_rabbit[mother_index].color.b);
                tempBabyColor1 = EvaluateMutation(tempBabyColor1);
                rabbitchild2.setColor(tempBabyColor1);
            }
            else if(genesplit<=0.48f){
                Color tempBabyColor = new Color(population_rabbit[father_index].color.r,
                                                population_rabbit[mother_index].color.g,
                                                population_rabbit[mother_index].color.b);
                tempBabyColor = EvaluateMutation(tempBabyColor);
                rabbitchild1.setColor(tempBabyColor);
                Color tempBabyColor1 = new Color(population_rabbit[mother_index].color.r,
                                                population_rabbit[father_index].color.g,
                                                population_rabbit[father_index].color.b);
                tempBabyColor1 = EvaluateMutation(tempBabyColor1);
                rabbitchild2.setColor(tempBabyColor1);
            }
            else if(genesplit<=0.64f){
                Color tempBabyColor = new Color(population_rabbit[mother_index].color.r,
                                                population_rabbit[father_index].color.g,
                                                population_rabbit[father_index].color.b);
                tempBabyColor = EvaluateMutation(tempBabyColor);
                rabbitchild1.setColor(tempBabyColor);
                Color tempBabyColor1 = new Color(population_rabbit[father_index].color.r,
                                                population_rabbit[father_index].color.g,
                                                population_rabbit[mother_index].color.b);
                tempBabyColor1 = EvaluateMutation(tempBabyColor1);
                rabbitchild2.setColor(tempBabyColor1);
            }
            else if(genesplit<=0.8f){
                Color tempBabyColor = new Color(population_rabbit[mother_index].color.r,
                                                population_rabbit[mother_index].color.g,
                                                population_rabbit[father_index].color.b);
                tempBabyColor = EvaluateMutation(tempBabyColor);
                rabbitchild1.setColor(tempBabyColor);
                Color tempBabyColor1 = new Color(population_rabbit[father_index].color.r,
                                                population_rabbit[father_index].color.g,
                                                population_rabbit[mother_index].color.b);
                tempBabyColor1 = EvaluateMutation(tempBabyColor1);
                rabbitchild2.setColor(tempBabyColor1);
            }
            else{
                Color tempBabyColor = new Color(population_rabbit[mother_index].color.r,
                                                population_rabbit[father_index].color.g,
                                                population_rabbit[mother_index].color.b);
                tempBabyColor = EvaluateMutation(tempBabyColor);
                rabbitchild1.setColor(tempBabyColor);
                Color tempBabyColor1 = new Color(population_rabbit[father_index].color.r,
                                                population_rabbit[mother_index].color.g,
                                                population_rabbit[father_index].color.b);
                tempBabyColor1 = EvaluateMutation(tempBabyColor1);
                rabbitchild2.setColor(tempBabyColor1);
            }
            babylist.Add(rabbitchild1);
            babylist.Add(rabbitchild2);
        }
        population_rabbit.AddRange(babylist);
    }

    public Color EvaluateMutation(Color rabbit){
        float rateof_mutation = 0.1f;
        Vector3 mutatedColor = new Vector3(rabbit.r,rabbit.g,rabbit.r);
        for(int i=0;i<3;i++){
            if(UnityEngine.Random.Range(0.0f,1.0f)<=rateof_mutation){
                mutatedColor[i] = UnityEngine.Random.Range(0.0f,1.0f);
            }
        }
        return new Color(mutatedColor.x,mutatedColor.y,mutatedColor.z);
    }

    IEnumerator EvaluationCycle(){
        while (true)
        {
             yield return new WaitForSeconds(0.5f);
             EvaluatePoplation();
        }
    }
}
