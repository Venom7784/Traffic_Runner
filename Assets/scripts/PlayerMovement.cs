using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;


public class PlayerMovement : MonoBehaviour
{

    public PostProcessVolume postProcessVolume;
    private Vignette vignette;

    private float invulnerableTimeElapsed = 0f;

     [SerializeField] float MaxIntensity;
    [SerializeField] float frequency;


    public float playerSpeed = 2f;
    public float sideJumpSpeed = 10f;

  
    public float[] lanes = new float[] { 21f, 26.77f, 31f };
    private int currentLaneIndex = 1;
    private int previousLaneIndex = 1;

    private Animator animator;
    public bool canMove = true;
    private bool isVulnerable = false;
    private bool isPlayerChangingLanes = false;

    public float invulnerableDuration = 2f;
    public float laneChangeThreshold = 0.01f;


    public GameOverScreen gameOverScreen;
    public CoinCollector coinCollector;

    public CarSpeed carSpeed;
    void Start()
    {
        





        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found in children of: " + gameObject.name);
        }

        SegmentMover.allowMovement = true;
        CoinMover.allowMovement =true;
        CarMover.allowMovement =true;

        Vector3 pos = transform.position;
        pos.x = lanes[currentLaneIndex];
        transform.position = pos;

        if (postProcessVolume != null && postProcessVolume.profile.TryGetSettings(out vignette))
        {
            vignette.enabled.value = true;
        }
    }

    void Update()
    {
        
        if (!canMove || PauseManager.isPaused) return;

        // Move forward
        transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime, Space.World);

        // Input
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveToLane(currentLaneIndex - 1 );
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveToLane(currentLaneIndex + 1);
        }

        // Move to target lane position
        Vector3 targetPosition = new Vector3(lanes[currentLaneIndex], transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, sideJumpSpeed * Time.deltaTime);

        // Check if player is changing lanes
        isPlayerChangingLanes = Mathf.Abs(transform.position.x - lanes[currentLaneIndex]) > laneChangeThreshold;




        // ... your existing Update logic

        if (isVulnerable && vignette != null)
        {
            invulnerableTimeElapsed += Time.deltaTime;
            float sineValue = (Mathf.Sin(invulnerableTimeElapsed *frequency) + 1f) * 0.5f; // value oscillates between 0 and 1
            vignette.intensity.value = sineValue * MaxIntensity; // Adjust max intensity here (e.g., 0.4)
        }

        if(canMove)
        {
            EditableTexts.Instance.TextUpdate(coinCollector.GiveInfoCoin());
        }

        
    }

    private void MoveToLane(int newLaneIndex )
    {
        newLaneIndex = Mathf.Clamp(newLaneIndex, 0, lanes.Length - 1);

        if (newLaneIndex != currentLaneIndex)
        {
            previousLaneIndex = currentLaneIndex;
            currentLaneIndex = newLaneIndex;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        float PlayerX = transform.position.x;

        if (other.CompareTag("obstacle"))
        {
            float laneX = lanes[previousLaneIndex];
            float obstacleX = other.transform.position.x;
            float threshold = 2.5f; // Tolerance value for comparison

            bool isHitObstacleInSameLane = Mathf.Abs(laneX-obstacleX) <= threshold;

           

            if (!isVulnerable && isPlayerChangingLanes && !isHitObstacleInSameLane) // player gets vulnerable

            {
                // Revert to previous lane if hit during lane change
                Debug.Log("Hit while changing lanes → Reverting to previous lane and becoming vulnerable");
                currentLaneIndex = previousLaneIndex;
                isVulnerable = true;
                StartCoroutine(InvulnerabilityTimer());
            }



            else if (!isPlayerChangingLanes || isVulnerable  || isHitObstacleInSameLane) // player dies
            {
                // Hit again while vulnerable → Game Over
                Debug.Log("Hit while already vulnerable → Stopping movement");

                StartCoroutine(HandleGameOver());
                EditableTexts.Instance.CheckAndUpdateHighScore();
                gameOverScreen.Setup(coinCollector.GiveInfoCoin() , EditableTexts.Instance.getScoreInfo() , EditableTexts.Instance.LoadHighScore());


            }
        }
    }

    IEnumerator InvulnerabilityTimer()
    {
        invulnerableTimeElapsed = 0f;

        yield return new WaitForSeconds(invulnerableDuration);

        isVulnerable = false;
        if (vignette != null)
        {
            vignette.intensity.value = 0f; // Reset vignette
        }
    }

    IEnumerator HandleGameOver()
    {
        canMove = false;
        SegmentMover.allowMovement = false;
        CoinMover.allowMovement = false;
        CarMover.allowMovement = false;


        if (animator != null)
        {
            animator.SetTrigger("IsHit");

            // Wait for the transition to start
            yield return null;

            // Wait for the current state to be the hit animation
            while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Stumble"))
            {
                yield return null;
            }

            // Now wait for the animation to complete
            float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationLength);
        }

       
    }


}
