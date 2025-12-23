using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{

    public float moveSpeed = 10;
    public float deadZone = -45;

    public GameObject grade;
    public float spaceBetweenPipes;
    public PipeSpawnScript pipeSpawnScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pipeSpawnScript = GameObject.FindGameObjectWithTag("PipeSpawner").GetComponent<PipeSpawnScript>();
        SetGradePosition();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;
        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
    //prova
    void SetGradePosition()
    {
        spaceBetweenPipes = moveSpeed * pipeSpawnScript.spawnRate;
        float gradeMinXPosition = spaceBetweenPipes / 4;
        float gradeMaxXPosition = spaceBetweenPipes / 4 * 3;
        float gradeMinYPosition = -20;
        float gradeMaxYPosition = +20;
        grade.transform.localPosition = new Vector3(Random.Range(gradeMinXPosition, gradeMaxXPosition), 0, 0);
        grade.transform.position = new Vector3(grade.transform.position.x, Random.Range(gradeMinYPosition, gradeMaxYPosition), grade.transform.position.z);
    }


}
