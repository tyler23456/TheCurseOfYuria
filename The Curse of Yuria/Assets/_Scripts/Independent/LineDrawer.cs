using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(LineRenderer))]
public class LineDrawer : MonoBehaviour
{
    const int drawTime = 1;
    const int lineCount = 60;
    const float height = 6f;
    const float gravity = -18f;

    [SerializeField] bool destroyWhenFinishedDrawing = true;
    [SerializeField] LineRenderer lineRenderer;

    Transform user;
    Transform target;

    float drawRateAccumulator = 0f;
    int visibleLinesCount = 1;
    float drawRate = 0f;

    public Color color { get; set; } = Color.grey;
    public Action onFinishedDrawing { get; set; } = () => { };

    public void Initialize(Transform user, Transform target)
    {
        lineRenderer = GetComponent<LineRenderer>();
        drawRateAccumulator = 0f;
        visibleLinesCount = 1;
        drawRate = drawTime / (float)lineCount / 2f;

        this.user = user;
        this.target = target;
    }

    LaunchData CalculateLaunchData(Vector2 user, Vector2 target)
    {
        Vector2 displacement = target - user;

        float safeHeight = displacement.y + 1;
        safeHeight = MathF.Max(height, safeHeight);
        float time = Mathf.Sqrt(-2 * safeHeight / gravity) + Mathf.Sqrt(2 * (displacement.y - safeHeight) / gravity);
        Vector2 velocity;
        velocity.y = Mathf.Sqrt(-2 * gravity * safeHeight);
        velocity.x = displacement.x / time;
        return new LaunchData(time, velocity);
    }

    void DrawPath(Vector2 user, Vector2 target, int visibleLinesCount)
    {
        LaunchData launchData = CalculateLaunchData(user, target);
        lineRenderer.positionCount = visibleLinesCount;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float simulationTime = i / (float)lineCount * launchData.time;
            Vector2 displacement = launchData.velocity * simulationTime + Vector2.up * gravity * simulationTime * simulationTime / 2f;
            Vector2 drawPoint = user + displacement;
            lineRenderer.SetPosition(i, drawPoint);
        }
    }

    void Update()
    {
        if (user == null || target == null)
            return;

        drawRateAccumulator += Time.deltaTime;

        if (drawRateAccumulator > drawRate)
        {
            drawRateAccumulator = 0f;
            visibleLinesCount++;

            if (visibleLinesCount > lineCount)
            {
                onFinishedDrawing.Invoke();

                if (destroyWhenFinishedDrawing)
                    Destroy(gameObject);
            }
            visibleLinesCount = Mathf.Clamp(visibleLinesCount, 1, lineCount);
        }
        DrawPath(user.position, target.position, visibleLinesCount);
    }

    struct LaunchData
    {
        public readonly float time;
        public readonly Vector2 velocity;
        
        public LaunchData(float time, Vector2 velocity)
        {
            this.time = time;
            this.velocity = velocity;
        }
    }

}
