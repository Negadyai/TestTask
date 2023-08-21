using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player.PlayerController player))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
