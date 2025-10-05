using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    public float speed = 10f;
    public float maxLifeTime = 3f;

    /*
    * Variables para dividir el meteorito en dos más pequeños
    */
    public GameObject smallMeteorPrefab; // Prefab del meteorito pequeño
    public float splitForce = 5f; // Qué tan rápido salen los pequeños
    public float angleOffset = 30f; // Separación entre los dos mini meteoritos

    public Vector3 targetVector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, maxLifeTime);

    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(speed * targetVector * Time.deltaTime);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IncreaseScore();
            Destroy(collision.gameObject);
            Destroy(gameObject);
            splitMeteor();
        }
        else if (collision.gameObject.CompareTag("smallEnemy"))
        {
            IncreaseScore();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void splitMeteor()
    {
        // Verificamos que haya un prefab asignado
        if (smallMeteorPrefab == null)
        {
            Debug.LogWarning("No se asignó el prefab de smallMeteorPrefab en el Inspector");
            return;
        }

        // Dirección de la bala (la bisectriz)
        Vector3 direction = targetVector.normalized;

        // Calculamos las dos direcciones usando un ángulo fijo
        Quaternion rotation1 = Quaternion.AngleAxis(angleOffset, Vector3.forward);
        Quaternion rotation2 = Quaternion.AngleAxis(-angleOffset, Vector3.forward);

        Vector3 dir1 = rotation1 * direction;
        Vector3 dir2 = rotation2 * direction;

        // Creamos los dos meteoritos pequeños en la posición de impacto (posición de la bala)
        GameObject small1 = Instantiate(smallMeteorPrefab, transform.position, Quaternion.identity);
        GameObject small2 = Instantiate(smallMeteorPrefab, transform.position, Quaternion.identity);

        // Les damos una fuerza para que salgan en direcciones opuestas
        Rigidbody rb1 = small1.GetComponent<Rigidbody>();
        Rigidbody rb2 = small2.GetComponent<Rigidbody>();

        if (rb1 != null) rb1.AddForce(dir1 * splitForce, ForceMode.Impulse);
        if (rb2 != null) rb2.AddForce(dir2 * splitForce, ForceMode.Impulse);           
    }


    private void IncreaseScore()
    {
        Player.SCORE++;
        Debug.Log(Player.SCORE);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "Puntos : " + Player.SCORE;
    }
    
}
