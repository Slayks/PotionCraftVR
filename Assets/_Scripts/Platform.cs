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
    private float speed = 0.0001f;

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
                GameObject node = Instantiate(nodePrefab, new Vector3(platformPosition.x + item.x, platformPosition.y + item.y, platformPosition.z + item.z), Quaternion.identity);
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
    public void FollowPath()
    {
        // Do something only if there is node displayed
        if (this.displayedPathNodes.Count > 0)
        {
            // Set isMoving to true to avoid displaying new path or removing current path
            this.isMoving = true;
            
            this.displayedPathNodes.ForEach((node) =>
            {
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                // FIXME Faire une coroutine sinon ça freeze
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                // Move the platform and wait for it to have moved close enough to the node position before going out the loop
                while (Vector3.Distance(this.transform.position, node.transform.position) > 0.05f) {
                    Vector3 moveTo = Vector3.MoveTowards(this.transform.position, node.transform.position, this.speed * Time.deltaTime);
                    this.transform.position = moveTo;
                };
                // Destroy the node GameObject
                Destroy(node);
            });
            // Reset displayed node list
            this.displayedPathNodes = new List<GameObject>();
            // Set moving to false to allow displaying path again
            this.isMoving = false;
        }
    }

    // TEMP à supprimer
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && this.isMoving == false)
        {
            this.FollowPath();
        }
    }
}
