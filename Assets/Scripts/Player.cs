using JetBrains.Annotations;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private float moveSpeed = 7f;

    private bool isWalking;
    private void Update()
    {
        

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        isWalking = moveDir != Vector3.zero;
        transform.position += moveDir * Time.deltaTime * moveSpeed;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp( transform.forward,moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
