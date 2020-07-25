using UnityEngine;

public class AnimateCamera : MonoBehaviour
{
    Vector3 _newPosition;
    Quaternion _newRotation;
    [SerializeField] Vector2 min;
    [SerializeField] Vector2 max;
    [SerializeField] Vector2 yRotationRange;
    [SerializeField] [Range(0.01f, 0.1f)] private float lerpSpeed = 0.05f; 

    private void Awake()
    {
        _newPosition = transform.position;
        _newRotation = transform.rotation;
    }

    private void Update()
    {
        if (transform.position.x <= 90f)
        {
            Destroy(gameObject.GetComponent<AnimateCamera>()); 
        }
        transform.position = Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * lerpSpeed);        
        transform.rotation = Quaternion.Lerp(transform.rotation, _newRotation, Time.deltaTime * lerpSpeed);        

        if(Vector3.Distance(transform.position, _newPosition) < 1f)
        {
            GetNewPosition();
        }
    }

    private void GetNewPosition()
    {
        float xPos = Random.Range(min.x, max.x);
        float zPos = Random.Range(min.y, max.y);
        _newRotation = Quaternion.Euler(0, Random.Range(yRotationRange.x, yRotationRange.y), 0);
        _newPosition = new Vector3(xPos, 0, zPos);
    }
}
