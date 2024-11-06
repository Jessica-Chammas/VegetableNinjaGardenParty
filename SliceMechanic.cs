using UnityEngine;
using TMPro;

public class SliceMechanic : MonoBehaviour
{
    public ScoreManager scoreManager;
    public LivesManager livesManager;
    public GameObject confettiPrefab;

    // Assign prefabs for each vegetable type sliced halves
    public GameObject eggplantLeftHalfPrefab;
    public GameObject eggplantRightHalfPrefab;
    public GameObject broccoliLeftHalfPrefab;
    public GameObject broccoliRightHalfPrefab;
    public GameObject carrotLeftHalfPrefab;
    public GameObject carrotRightHalfPrefab;
    public GameObject tomatoLeftHalfPrefab;
    public GameObject tomatoRightHalfPrefab;
    public GameObject rottenCucumberLeftHalfPrefab;
    public GameObject rottenCucumberRightHalfPrefab;
    


    private Vector3 swipeStart;
    private Vector3 swipeEnd;
    private int comboCount = 0;
    private float comboTimer = 0f;
    public float comboDuration = 2f;
    public int baseScore = 10;
    public int enhancerBonusScore = 100;
    public float enhancerMultiplierDuration = 5f;
    private bool isMultiplierActive = false;
    private float multiplierTimer = 0f;

    private SoundEffectsManager soundEffectsManager;

    void Start()
    {
        soundEffectsManager = FindObjectOfType<SoundEffectsManager>();

        if (scoreManager == null)
        {
            GameObject scoreManagerGO = GameObject.Find("Score");
            if (scoreManagerGO != null)
            {
                scoreManager = scoreManagerGO.GetComponent<ScoreManager>();
            }
            else
            {
                Debug.LogError("ScoreManager GameObject not found.");
            }
        }

        if (livesManager == null)
        {
            GameObject livesManagerGO = GameObject.Find("LivesManager");
            if (livesManagerGO != null)
            {
                livesManager = livesManagerGO.GetComponent<LivesManager>();
            }
            else
            {
                Debug.LogError("LivesManager GameObject not found.");
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            swipeStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            swipeStart.z = 0f;
        }

        if (Input.GetMouseButtonUp(0))
        {
            swipeEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            swipeEnd.z = 0f;
            CheckForSlice();
        }

        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
        }
        else
        {
            ResetCombo();
        }
    }

    void CheckForSlice()
    {
        RaycastHit2D[] hits = Physics2D.LinecastAll(swipeStart, swipeEnd);

        foreach (var hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("GoldenCarrot"))
                {
                    if (soundEffectsManager != null)
                    {
                        soundEffectsManager.PlayGoldenCarrotSound();
                    }
                    TriggerConfettiEffect(hit.collider.transform.position);
                    SplitVegetable(hit.collider.gameObject); // Split Golden Carrot
                    Destroy(hit.collider.gameObject);
                    OnGoldenCarrotSliced();
                }
                else if (hit.collider.gameObject.CompareTag("Vegetable"))
                {
                    TriggerConfettiEffect(hit.collider.transform.position);

                    if (soundEffectsManager != null)
                    {
                        soundEffectsManager.PlaySliceSound();
                    }

                    SplitVegetable(hit.collider.gameObject);
                    Destroy(hit.collider.gameObject);
                    OnVegetableSliced();
                }
                else if(hit.collider.gameObject.CompareTag("BadVegetable")){
                     if (soundEffectsManager != null)
                    {
                        soundEffectsManager.PlaySliceSound();
                    }
                    SplitVegetable(hit.collider.gameObject); // Split Rotten Cucumber
                    Destroy(hit.collider.gameObject);
                    OnBadVegetableSliced();
                }

                
            }
        }
    }

    void TriggerConfettiEffect(Vector3 position)
    {
        Instantiate(confettiPrefab, position, Quaternion.identity);
    }

    void SplitVegetable(GameObject vegetable)
{
    Vector3 position = vegetable.transform.position;

    switch (vegetable.name)
    {
        case "EggPlant_Sprite(Clone)":
            // Left half moves left, right half moves right
            Instantiate(eggplantLeftHalfPrefab, position + new Vector3(-0.1f, 0, 0), Quaternion.Euler(0, 0, -15)).GetComponent<Rigidbody2D>().velocity = new Vector2(-1, -2);
            Instantiate(eggplantRightHalfPrefab, position + new Vector3(0.1f, 0, 0), Quaternion.Euler(0, 0, 15)).GetComponent<Rigidbody2D>().velocity = new Vector2(1, -2);
            break;
        case "Broccoli_Sprite(Clone)":
            Instantiate(broccoliLeftHalfPrefab, position + new Vector3(-0.1f, 0, 0), Quaternion.Euler(0, 0, -15)).GetComponent<Rigidbody2D>().velocity = new Vector2(-1, -2);
            Instantiate(broccoliRightHalfPrefab, position + new Vector3(0.1f, 0, 0), Quaternion.Euler(0, 0, 15)).GetComponent<Rigidbody2D>().velocity = new Vector2(1, -2);
            break;
       case "GoldenCarrot_Sprite(Clone)":
            Instantiate(carrotLeftHalfPrefab, position + new Vector3(-0.1f, 0, 0), Quaternion.Euler(0, 0, -15)).GetComponent<Rigidbody2D>().velocity = new Vector2(-1, -2);
            Instantiate(carrotRightHalfPrefab, position + new Vector3(0.1f, 0, 0), Quaternion.Euler(0, 0, 15)).GetComponent<Rigidbody2D>().velocity = new Vector2(1, -2);
            break;
        case "Tomato_Sprite(Clone)":
            Instantiate(tomatoLeftHalfPrefab, position + new Vector3(-0.1f, 0, 0), Quaternion.Euler(0, 0, -15)).GetComponent<Rigidbody2D>().velocity = new Vector2(-1, -2);
            Instantiate(tomatoRightHalfPrefab, position + new Vector3(0.1f, 0, 0), Quaternion.Euler(0, 0, 15)).GetComponent<Rigidbody2D>().velocity = new Vector2(1, -2);
            break;
         case "RottenCucumber(Clone)": 
            Instantiate(rottenCucumberLeftHalfPrefab, position + new Vector3(-0.1f, 0, 0), Quaternion.Euler(0, 0, -15)).GetComponent<Rigidbody2D>().velocity = new Vector2(-1, -2);
            Instantiate(rottenCucumberRightHalfPrefab, position + new Vector3(0.1f, 0, 0), Quaternion.Euler(0, 0, 15)).GetComponent<Rigidbody2D>().velocity = new Vector2(1, -2);
            break;
        default:
            Debug.LogWarning("Unknown vegetable: " + vegetable.name);
            break;
    }
}




    void OnVegetableSliced()
    {
        if (scoreManager != null)
        {
            comboCount++;
            comboTimer = comboDuration;

            int scoreMultiplier = Mathf.Clamp(comboCount / 3, 1, 5);
            int pointsToAdd = baseScore * scoreMultiplier;
            scoreManager.AddScore(pointsToAdd);
        }
        else
        {
            Debug.LogError("ScoreManager is not assigned!");
        }
    }

    void OnGoldenCarrotSliced()
    {
        if (scoreManager != null)
        {
            scoreManager.AddScore(enhancerBonusScore);
        }
    }
    void OnBadVegetableSliced()
    {
        if (livesManager != null)
        {
            livesManager.MissVegetable(); // Deduct a life or apply other penalty
        }
    }

    void ResetCombo()
    {
        comboCount = 0;
    }
}
