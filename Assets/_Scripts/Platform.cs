using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private GameObject enabledNodePrefab;
    [SerializeField]
    private GameObject disabledNodePrefab;

    private List<GameObject> displayedPathNodes = new List<GameObject>();
    private int enabledNodeCount = 0;
    private bool isMoving = false;
    private float speed = 5f;

    void Start()
    {
        VRControllerActionListener.OnRightControllerPrimaryButtonPressed += RunFollowPath;
    }
    
    private void OnDestroy()
    {
        VRControllerActionListener.OnRightControllerPrimaryButtonPressed -= RunFollowPath;
    }

    private void RunFollowPath()
    {
        StartCoroutine(FollowPath(this));
    }

    /// <summary>
    /// If the platform is not moving, display the path (Vector3 list) that the platform will follow when calling FollowPath
    /// </summary>
    /// <param name="path"></param>
    public void DisplayPath(List<Vector3> path)
    {
        if (!isMoving)
        {
            for (int i = 0; i < path.Count; i++)
            {
                Vector3 nodePosition = path[i];
                Vector3 platformPosition = this.gameObject.transform.position;
                GameObject node;
                // ! [DEMO]
                //if (i < path.Count / 2)
                //{
                node = Instantiate(enabledNodePrefab, new Vector3(platformPosition.x + nodePosition.x, platformPosition.y - 5, platformPosition.z + nodePosition.z), Quaternion.identity);
                //}
                //else
                //{
                //    node = Instantiate(disabledNodePrefab, new Vector3(platformPosition.x + nodePosition.x, platformPosition.y - 5, platformPosition.z + nodePosition.z), Quaternion.identity);
                //}
                this.displayedPathNodes.Add(node);
            }
            // ! [DEMO]
            //this.enabledNodeCount = path.Count / 2;
            this.enabledNodeCount = path.Count;
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

    public void incrementEnabledNode()
    {
        if (this.enabledNodeCount < this.displayedPathNodes.Count)
        {
            this.enabledNodeCount++;
            GameObject nodeToReplace = this.displayedPathNodes[this.enabledNodeCount - 1];
            GameObject newNode = Instantiate(enabledNodePrefab, nodeToReplace.transform.position, Quaternion.identity);
            Destroy(nodeToReplace);
            this.displayedPathNodes[this.enabledNodeCount - 1] = newNode;
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

                // If the node is enabled
                if (i < platform.enabledNodeCount)
                {
                    // Create a duplicate of the node position to avoid taking Y position of the node into account while moving the platform
                    Vector3 platformHeightNodePosition = new Vector3(node.transform.position.x, platform.transform.position.y, node.transform.position.z);

                    // Move the platform and wait for it to have moved close enough to the node position before going out the loop
                    while (Vector3.Distance(platform.transform.position, platformHeightNodePosition) > 0.05f)
                    {
                        Vector3 moveTo = Vector3.MoveTowards(platform.transform.position, platformHeightNodePosition, platform.speed * Time.deltaTime);
                        platform.transform.position = moveTo;
                        yield return new WaitForEndOfFrame();
                    };
                }

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
}
