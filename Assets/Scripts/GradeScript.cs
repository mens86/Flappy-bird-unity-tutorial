using UnityEditorInternal;
using UnityEngine;

public class GradeScript : MonoBehaviour
{
    public Sprite[] voti;
    public SpriteRenderer spriteRenderer;
    public int gradeNumber;
    public LogicScript logicScript;
    public GameObject Sparkles;
    public ParticleSystem circle;
    public GameObject EffectOnDestroy;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int randomIndex = Random.Range(0, 8);
        gradeNumber = randomIndex + 3;
        spriteRenderer.sprite = voti[randomIndex];
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        SetEffects();
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
            PlayGradeSound(gradeNumber);
            Instantiate(EffectOnDestroy, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
    private void PlayGradeSound(int gradeNumber)
    {
        if (gradeNumber < 6)
        {
            AudioManager.instance.PlaySFX("pop");
            AudioManager.instance.PlaySFX("gradeNo");
        }
        else if (gradeNumber >= 6 && gradeNumber < 8)
        {
            AudioManager.instance.PlaySFX("pop");
            AudioManager.instance.PlaySFX("gradeGood");
        }
        else if (gradeNumber >= 8 && gradeNumber < 10)
        {
            AudioManager.instance.PlaySFX("pop");
            AudioManager.instance.PlaySFX("gradeYeah");
        }
        else //è 10
        {
            AudioManager.instance.PlaySFX("pop");
            AudioManager.instance.PlaySFX("gradeNice");
        }
    }

    private void SetEffects()
    {
        if (gradeNumber >= 8)
        {
            Sparkles.SetActive(true); // se è un bel voto ci sono le stelline!
            var main = circle.main; //questo passaggio intermedio non ve lo spiego, fidatevi.
            main.startColor = Color.green;
        }
        else if (gradeNumber >= 6 && gradeNumber < 8)
        {
            var main = circle.main;
            main.startColor = Color.yellow;
        }
        else
        {
            var main = circle.main;
            main.startColor = Color.red;
        }
    }


}
