using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private GameObject nodePrefab;
    private List<GameObject> displayedPathNodes = new List<GameObject>();
    private bool isMoving = false;
    private float speed = 10f;

    /// <summary>
    /// If the platform is not moving, display the path (Vector3 list) that the platform will follow when calling FollowPath
    /// </summary>
    /// <param name="path"></param>
    public void DisplayPath(List<Vector3> path)
    {
        if (!isMoving)
        {
            path.ForEach((item) =>
            {
                Vector3 platformPosition = this.gameObject.transform.position;
                GameObject node = Instantiate(nodePrefab, new Vector3(platformPosition.x + item.x, 0, platformPosition.z + item.z), Quaternion.identity);
                this.displayedPathNodes.Add(node);
            });
        }
    }

    /// <summary>
    /// If the platform is not moving, remove the node of the currently displayed path
    /// </summary>
    public void RemoveDisplayedPath()
    {
        if (!isMoving)
        {
            this.displayedPathNodes.ForEach((node) =>
            {
                Destroy(node);
            });
            this.displayedPathNodes = new List<GameObject>();
        }
    }

    /// <summary>
    /// If there is a displayed path, move through the nodes and clean them
    /// </summary>
    IEnumerator FollowPath(Platform platform)
    {
        // Do something only if there is node displayed
        if (platform.displayedPathNodes.Count > 0)
        {
            // Set isMoving to true to avoid displaying new path or removing current path
            platform.isMoving = true;

            for (int i = 0; i < platform.displayedPathNodes.Count; i++)
            {
                GameObject node = platform.displayedPathNodes[i];

                // Create a duplicate of the node position to avoid taking Y position of the node into account while moving the platform
                Vector3 platformHeightNodePosition = new Vector3(node.transform.position.x, platform.transform.position.y, node.transform.position.z);

                // Move the platform and wait for it to have moved close enough to the node position before going out the loop
                while (Vector3.Distance(platform.transform.position, platformHeightNodePosition) > 0.05f)
                {
                    Vector3 moveTo = Vector3.MoveTowards(platform.transform.position, platformHeightNodePosition, platform.speed * Time.deltaTime);
                    platform.transform.position = moveTo;
                    yield return new WaitForEndOfFrame();
                };
                // Destroy the node GameObject
                Destroy(node);
            }
            // Reset displayed node list
            platform.displayedPathNodes = new List<GameObject>();
            // Set moving to false to allow displaying path again
            platform.isMoving = false;
        }
        yield return new WaitForEndOfFrame();
    }

    // TEMP à supprimer
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && this.isMoving == false)
        {
            StartCoroutine(FollowPath(this));
        }
    }
}
