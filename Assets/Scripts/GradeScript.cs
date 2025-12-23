using UnityEngine;

public class GradeScript : MonoBehaviour
{
    public Sprite[] voti;
    public SpriteRenderer spriteRenderer;
    public int gradeNumber;
    public LogicScript logicScript;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int randomIndex = Random.Range(0, 8);
        gradeNumber = randomIndex + 3;
        spriteRenderer.sprite = voti[randomIndex];
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 3) // il layer del bird
        {
            logicScript.AddGradeToMean(gradeNumber);
            Destroy(gameObject);
        }
    }
}
