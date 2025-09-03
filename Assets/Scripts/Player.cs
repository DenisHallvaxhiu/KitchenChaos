using JetBrains.Annotations;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField] private GameScript gameScript;

    private bool isWalking;
    private void Update()
    {
        Vector2 inputVector = gameScript.GetMovementVectorNormalized();

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
