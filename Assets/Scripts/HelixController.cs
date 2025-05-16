using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    private Vector2 lastTapPos;
    private Vector3 startRotation;

    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float touchSensitivity = 0.1f;

    [SerializeField] private Transform startPlatformTransform;
    [SerializeField] private Transform endPlatformTransform;

    [SerializeField] private GameObject HelixPlatformPrefab;
    private float helixHeight;

    public List<Level> allLevels = new List<Level>();
    private List<GameObject> spawnedLevels = new List<GameObject>();

    private void Awake()
    {
        startRotation = transform.localEulerAngles;
        helixHeight = startPlatformTransform.localPosition.y - (endPlatformTransform.localPosition.y + 0.1f);
        LoadLevel(0);
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        if (Input.GetMouseButton(0))
        {
            Vector2 curTapPos = Input.mousePosition;

            if (lastTapPos == Vector2.zero)
                lastTapPos = curTapPos;

            float delta = lastTapPos.x - curTapPos.x;
            lastTapPos = curTapPos;

            transform.Rotate(Vector3.up * delta * touchSensitivity);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastTapPos = Vector2.zero;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontalInput) > 0.01f)
        {
            transform.Rotate(Vector3.up * -horizontalInput * rotationSpeed);
        }
    }

    public void LoadLevel(int levelNumber)
    {
        Level level = allLevels[Mathf.Clamp(levelNumber, 0, allLevels.Count - 1)];
        transform.localEulerAngles = startRotation;

        foreach (GameObject gO in spawnedLevels)
            Destroy(gO);

        float platformDistance = helixHeight / level.levels.Count;
        float spawnPosY = startPlatformTransform.localPosition.y;

        for(int i = 0; i < level.levels.Count; i++)
        {
            spawnPosY -= platformDistance;
            GameObject platform = Instantiate(HelixPlatformPrefab, transform);
            platform.transform.localPosition = new Vector3(0, spawnPosY, 0);
            spawnedLevels.Add(platform);

            int partToDisable = 12 - level.levels[i].platformPartCount;
            List<GameObject> disableParts = new List<GameObject>();

            while (disableParts.Count < partToDisable)
            {
                GameObject randomPart = platform.transform.GetChild(Random.Range(0, platform.transform.childCount)).gameObject;
                if (!disableParts.Contains(randomPart))
                {
                    randomPart.SetActive(false);
                    disableParts.Add(randomPart); 
                }
            }

            List<GameObject> leftParts = new List<GameObject>();
            foreach(Transform p in platform.transform)
            {
                if (p.gameObject.activeInHierarchy)
                {
                    leftParts.Add(p.gameObject);
                }
            }

            List<GameObject> deadParts = new List<GameObject>();
            
            while(deadParts.Count < level.levels[i].deadAreaPartCount)
            {
                GameObject randomPart = leftParts[Random.Range(0, leftParts.Count)];
                if (!deadParts.Contains(randomPart))
                {
                    randomPart.gameObject.AddComponent<PlayerDead>();
                    deadParts.Add(randomPart);
                }
            }
        }
    }
}