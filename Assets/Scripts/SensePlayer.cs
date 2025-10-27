// MoveTo.cs
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SensePlayer : MonoBehaviour
{
    public LayerMask wallLayer; // Assign walls to a specific layer
    public Transform[] goal;
    public int currentGoal = 0;
    private void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal[currentGoal].position;
    }
    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal[currentGoal].position;
        if (Vector3.Distance(transform.position, goal[currentGoal].position) < 7.0f)
        {
            currentGoal += 1;
            if (currentGoal >= goal.Length) 
            {
                currentGoal = 0;
            }
        }
    }
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        SceneManager.LoadScene(3);
    //        //currentGoal = 0; // Reset to the first goal or any specific behavior
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 directionToPlayer = other.transform.position - transform.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            // Cast a ray towards the player
            if (Physics.Raycast(transform.position, directionToPlayer.normalized,
                out RaycastHit hit, distanceToPlayer, wallLayer))
            {
                // Something is blocking the view - do nothing
                return;
            }

            // Player is visible - game over
            GameOver();
        }
    }

    private void GameOver()
    {
        // Your game over logic
        SceneManager.LoadScene(3);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    //void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        Debug.Log("Player Lost");
    //        //currentGoal = 1; // Move to the next goal or any specific behavior
    //    }
    //}
}
