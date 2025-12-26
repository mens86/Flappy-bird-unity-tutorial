using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    private List<float> means = new List<float>();
    public float finalGrade;
    public int currentLevel = 1;

    public static PlayerStats instance;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void AddToMean(float mean)
    {
        currentLevel += 1;
        means.Add(mean);
        Debug.Log(means.Count);
        if (means.Count == 5)
        {
            float sumOfMeans = 0;
            foreach (var m in means)
            {
                sumOfMeans += m;
            }
            finalGrade = sumOfMeans / 5 * 10;
        }
    }

}
