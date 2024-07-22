using System;
using Unity.Netcode;
using UnityEngine;

public class NetworkMovementComponent : NetworkBehaviour
{
    [SerializeField] private CharacterController cc;

    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;

    [SerializeField] private Transform camSocket;
    [SerializeField] private GameObject stateCam;

    private Transform stateCamTransform;

    private int tick = 0;
    private float tickRate = 1f / 60f;
    private float tickDeltaTime = 0f;

    private const int BUFFER_SIZE = 1024;

    private InputState[] inputStates = new InputState[BUFFER_SIZE];
    private TransformState[] transformStates = new TransformState[BUFFER_SIZE];

    public NetworkVariable<TransformState> ServerTransformState = new NetworkVariable<TransformState>();
    public TransformState previousTransformState;

    private void OnEnable()
    {
        ServerTransformState.OnValueChanged += OnServerStateChanged;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        stateCamTransform = stateCam.transform;
    }

    private void OnServerStateChanged(TransformState previousValue, TransformState newValue)
    {
        previousTransformState = previousValue;
    }

    public void ProcessLocalPlayerMovement(Vector3 direction, float lookX, float lookY)
    {
        tickDeltaTime += Time.deltaTime;
        if (tickDeltaTime > tickRate)
        {
            int bufferIndex = tick % BUFFER_SIZE;
            if (!IsServer)
            {
                MovePlayerServerRPC(tick, direction, lookX, lookY);
                MovePlayer(direction);
                RotatePlayer(lookX, lookY);
            }
            else
            {
                MovePlayer(direction);
                RotatePlayer(lookX, lookY);

                TransformState state = new TransformState()
                {
                    Tick = tick,
                    position = transform.position,
                    rotation = transform.rotation,
                    HasStartedMoving = true,
                };

                previousTransformState = ServerTransformState.Value;
                ServerTransformState.Value = state;
            }

            InputState inputState = new InputState()
            {
                Tick = tick,
                direction = direction,
                lookX = lookX,
                lookY = lookY
            };

            TransformState transformState = new TransformState()
            {
                Tick = tick,
                position = transform.position,
                rotation = transform.rotation,
                HasStartedMoving = true
            };

            inputStates[bufferIndex] = inputState;
            transformStates[bufferIndex] = transformState;

            tickDeltaTime -= tickRate;
            tick++;
        }
    }

    public void ProcessSimulatedPlayerMovement()
    {
        tickDeltaTime += Time.deltaTime;
        if (tickDeltaTime > tickRate)
        {
            if (ServerTransformState.Value.HasStartedMoving)
            {
                transform.position = ServerTransformState.Value.position;
                transform.rotation = ServerTransformState.Value.rotation;
            }

            tickDeltaTime -= tickRate;
            tick++;
        }
    }

    private void MovePlayer(Vector3 direction)
    {
        Vector3 movement = direction.x * stateCamTransform.right + direction.z * stateCamTransform.forward;
        movement.y = 0;

        if (!cc.isGrounded)
        {
            movement.y = -9.61f;
        }

        cc.Move(movement * speed * tickRate);
    }

    private void RotatePlayer(float lookX, float lookY)
    {
        stateCamTransform.RotateAround(stateCamTransform.position, stateCamTransform.right, -lookY * turnSpeed * tickRate);
        transform.RotateAround(transform.position, transform.up, lookX * turnSpeed * tickRate);
    }

    [ServerRpc]
    private void MovePlayerServerRPC(int tick, Vector3 direction, float lookX, float lookY)
    {
        MovePlayer(direction);
        RotatePlayer(lookX, lookY);

        TransformState state = new TransformState()
        {
            Tick = tick,
            position = transform.position,
            rotation = transform.rotation,
            HasStartedMoving = true,
        };

        previousTransformState = ServerTransformState.Value;
        ServerTransformState.Value = state;
    }
}
