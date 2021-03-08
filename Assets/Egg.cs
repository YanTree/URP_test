using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] GameManager GM;
    [SerializeField] GameObject arrowWater;
    [SerializeField] GameObject arrowStone;
    [SerializeField] GameObject arrowTree;
    [SerializeField] float rotationSpeed = 2.0f;
    [SerializeField] float moveSpeed = 1.5f;

    [SerializeField] float topPoint = -1.0f;
    [SerializeField] float buttomPoint = -1.4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed, Space.World);
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        if (transform.position.y >= topPoint || transform.position.y <= buttomPoint)
            moveSpeed = -moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Hollow_Knight")
        {
            GM.ScorePlusOne();
            FindObjectOfType<AudioManager>().Play("Egg");
            Destroy(gameObject,0.3f);
            DestroyArrow();
        }
    }

    private void DestroyArrow()
    {
        if (gameObject.name == "Egg_Water")
            Destroy(arrowWater, 0.5f);
        else if (gameObject.name == "Egg_Stone")
            Destroy(arrowStone, 0.5f);
        else
            Destroy(arrowTree, 0.5f);
    }
}
