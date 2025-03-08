using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour,IKitchenObjectParent
{
    public static Player Instance;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public _BaseCounters selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 4f;
    [SerializeField] private InputManager inputManager;
    private bool isWalking,canMove;
    Vector3 lastInteractDirection;
    [SerializeField] private LayerMask counterLayer;
    private _BaseCounters selectedCounter;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    private KitchenObjects kitchenObjects;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("there is more than one instance");
        }
    }
    private void Start()
    {
        inputManager.onInteract += InputManager_onInteract;
        inputManager.onInteractAlternate += InputManager_onInteractAlternate;
    }

    private void InputManager_onInteractAlternate(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void InputManager_onInteract(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        PlayerMovement();
        HandleInteraction();
    }

    void PlayerMovement()
    {
        Vector3 inputVector = inputManager.GetVectorMovementNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float playerCollisionRadius = .6f, playerHeight = 2f;
        float moveDistance = moveSpeed * Time.deltaTime;
        canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerCollisionRadius
            , moveDir, moveDistance);
        //if (!canMove)
        //{
        //    // try to move in X direction while Z direction is blocked
        //    Vector3 dirX = new Vector3(moveDir.x, 0, 0).normalized;
        //    canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerCollisionRadius
        //     , dirX, moveDistance);
        //    if (canMove)
        //    {
        //        moveDir = dirX;
        //    }
        //    else
        //    {
        //        //try to move in Z direction while X direction is blocked
        //        Vector3 dirZ = new Vector3(0, 0, moveDir.z).normalized;
        //        canMove = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerCollisionRadius
        //         , dirZ, moveDistance);
        //        if (canMove)
        //        {
        //            moveDir = dirZ;
        //        }
        //    }
        //}
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
        isWalking = moveDir != Vector3.zero;
    }

    void HandleInteraction()
    {
        Vector3 inputVector = inputManager.GetVectorMovementNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float interactDistance = 2f;
        float radius = .5f,maxDistance = .4f;
        if(moveDir != Vector3.zero)
        {
            lastInteractDirection = moveDir;
        }

        if (Physics.CapsuleCast(transform.position, transform.position + Vector3.up * interactDistance,
           radius, lastInteractDirection,out RaycastHit rayCastHit,maxDistance,counterLayer))
        {
            if (rayCastHit.transform.TryGetComponent<_BaseCounters>(out _BaseCounters baseCounter))
            {
                if (selectedCounter != baseCounter)
                {
                    SetSelectCounter(baseCounter);
                }
            }
            else
            {
                SetSelectCounter(null);
            }
        }
        else
        {
            SetSelectCounter(null);
        }

        //if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance, counterLayer))
        //{
        //    if (raycastHit.transform.TryGetComponent<_BaseCounters>(out _BaseCounters counter))
        //    {
        //        if (selectedCounter != counter)
        //        {
        //            SetSelectCounter(counter);
        //        }
        //    }
        //    else
        //    {
        //        SetSelectCounter(null);
        //    }
        //}
        //else
        //{
        //    SetSelectCounter(null);
        //}

    }

    public bool IsWalking()
    {
        return isWalking;
    }

    void SetSelectCounter(_BaseCounters selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = this.selectedCounter
        });
    }

    public Transform SetKitchObjectHolderTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetNewKitchenObject(KitchenObjects kitchenObjects)
    {
        this.kitchenObjects = kitchenObjects;
    }

    public KitchenObjects GetKitchenObjects()
    {
        return kitchenObjects;
    }

    public void ClearKitchenObjects()
    {
        kitchenObjects = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObjects != null;
    }
}