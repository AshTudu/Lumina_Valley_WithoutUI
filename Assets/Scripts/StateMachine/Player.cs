using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public Animator anim {  get; private set; }
    public Rigidbody2D rb { get; private set; }
    public PlayerInputSet input {  get; private set; }
    public SpriteRenderer spriteRenderer;

    private StateMachine stateMachine;
    public PlayerIdleState idleState {  get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerSprintState sprintState { get; private set; }
    public PlayerBasicAttackState basicAttackState { get; private set; }
    public PlayerAxeState axeState { get; private set; }
    public PlayerPickAxeState pickAxeState { get; private set; }
    public PlayerShovelState shovelState { get; private set; }
    public PlayerWateringState wateringState { get; private set; }


    public Vector2 lastMoveDir;
    public Vector2 moveInput {  get; private set; }


    [Header("Movement Details")]
    public float moveSpeed;
    public float sprintSpeed;

    [Header("Input Details")]
    public bool isSprinting;
    public bool isAttacking;

    [Header("Attack Details")]
    public float comboResetTime = 1.0f;

    [Header("Crop Logic")]
    public GameObject geneticCropPrefab;
    public CropData cropData;

    [Header("Tile Details")]
    public Tilemap groundTileMap;
    public Tilemap wetTileMap;
    public TileDatabase tiledatabase;

    private Dictionary<Vector3Int,Crop> cropGrid = new Dictionary<Vector3Int, Crop>();

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        stateMachine = new StateMachine();
        input = new PlayerInputSet();


        idleState = new PlayerIdleState(this, stateMachine);
        moveState = new PlayerMoveState(this, stateMachine);
        sprintState = new PlayerSprintState(this, stateMachine);
        basicAttackState = new PlayerBasicAttackState(this, stateMachine);
        axeState = new PlayerAxeState(this, stateMachine);
        pickAxeState = new PlayerPickAxeState(this, stateMachine);
        shovelState = new PlayerShovelState(this, stateMachine);
        wateringState = new PlayerWateringState(this, stateMachine);
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;

        input.Player.Sprint.performed += ctx => isSprinting = true;
        input.Player.Sprint.canceled += ctx => isSprinting = false;

    }
    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.ActiveCurrentState();

        if (Input.GetKeyDown(KeyCode.P))
        {
            PlantSeed();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            HarvestCrop();
        }


        CalculateLastMove();
        AdjustPlayerFacingDirection();
    }

    public void SetVelocity(float xVelocity , float yVelocity)
    {
        rb.velocity = new Vector2 (xVelocity, yVelocity);
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPos.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    public void CallAnimationTrigger()
    {
        stateMachine.CurrentState.CallAnimationTrigger();
    }

    public void CallAxeAnimTrigger()
    {
        stateMachine.CurrentState.CallAxeTrigger();
    }

    public void CallPickAxeTriggerCalled()
    {
        stateMachine.CurrentState.CallPickAxeTrigger();
    }

    public void CallShovelTriggerCalled()
    {
        stateMachine.CurrentState.CallShovelTrigger();
    }

    public void CallWateringTriggerCalled()
    {
        stateMachine.CurrentState.CallWateringTrigger();
    }

    private void CalculateLastMove()
    {
        Vector2 moveDir = new Vector2(moveInput.x, moveInput.y);

        if (moveDir.x != 0 || moveDir.y != 0)
        {
            if (Mathf.Abs(moveDir.x) > Mathf.Abs(moveDir.y))
            {
                lastMoveDir = new Vector2(Mathf.Sign(moveDir.x),0);
            }
            else
            {
                lastMoveDir = new Vector2(0, Mathf.Sign(moveDir.y));
            }
        }
    }

    public void SetLastMove(float xMove, float yMove)
    {
        anim.SetFloat("Horizontal", xMove);
        anim.SetFloat("Vertical", yMove);
    }

    //Trial crop growth logic

    public void WaterTile()
    {

        Vector3 actionPosition = transform.position;
        Vector3Int cellPosition = groundTileMap.WorldToCell(actionPosition);

        TileBase groundTile = groundTileMap.GetTile(cellPosition);

        if (groundTile != null)
        {
            TileBase existingWetTile = wetTileMap.GetTile(cellPosition);

            if (existingWetTile != null)
            {
                Debug.Log("This tile is already wet!. No need to water it again");
                return;
            }

            TileBase wetOverlayTile = tiledatabase.GetWetTile(groundTile);

            if (wetOverlayTile != null)
            {
                wetTileMap.SetTile(cellPosition, wetOverlayTile);
                Debug.Log("Placed a wet Overlay on top of the dirt");
            }
            else
            {
                Debug.Log("This type of Ground cannot be watered(no match in the database!)");
            }

        }
        else
        {
            Debug.Log("No Ground Tile Found Here");
        }
    }

    public void PlantSeed()
    {
        Vector3 actionPosition = transform.position;
        Vector3Int cellPosition = groundTileMap.WorldToCell(actionPosition);

        if (wetTileMap.HasTile(cellPosition))
        {
            if (!cropGrid.ContainsKey(cellPosition) && cropData != null)
            {
                Vector3 spawnPosition = groundTileMap.GetCellCenterWorld(cellPosition);

                GameObject newCropObject = Instantiate(geneticCropPrefab, spawnPosition, quaternion.identity);
                Crop newCrop = newCropObject.GetComponent<Crop>();

                newCrop.Initialized(cropData);
                cropGrid.Add(cellPosition, newCrop);

                Debug.Log("Test seed planted waiting for it to grow");
            }
            else
            {
                Debug.Log("There is already a crop planted here or no crop data assigned!");
            }
        }
        else
        {
            Debug.Log("The soil must be watered before planting a seed!");
        }

    }

    public void HarvestCrop()
    {
        Vector3 actionPosition = transform.position;
        Vector3Int cellPosition = groundTileMap.WorldToCell(actionPosition);

        if (cropGrid.TryGetValue(cellPosition,out Crop cropAtTile))
        {
            if (cropAtTile.IsFullyGrown())
            {
                if (cropAtTile.cropData.harvestedItemPrefab != null)
                {
                    Instantiate(cropAtTile.cropData.harvestedItemPrefab, groundTileMap.GetCellCenterWorld(cellPosition),Quaternion.identity);
                }

                Destroy(cropAtTile.gameObject);
                cropGrid.Remove(cellPosition);
                Debug.Log("Crop Successfully Harvested!");
            }
            else
            {
                Debug.Log("This crop is not fully grown yet!");
            }
        }
        else
        {
            Debug.Log("There is no crop planted here to harvest!");
        }
    }
}