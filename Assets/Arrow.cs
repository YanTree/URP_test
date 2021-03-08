using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] bool isTree = false;
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
        if (other.gameObject.name == "Hollow_Knight")
        {
            if(!isTree)
                gameObject.SetActive(false);
            else
                gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Hollow_Knight")
        {
            if (!isTree)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        }
    }
}
