using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class DeformableMesh : MonoBehaviour
{
    private MeshFilter meshFilter;
    private Mesh carMesh;
    private Vector3[] originalVertices;
    private List<Vector3> newModifiedVertices;
    public float maximumDepression;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        carMesh = meshFilter.mesh;
        originalVertices = carMesh.vertices;
    }
    void UpdateMesh()
    {
        carMesh.RecalculateNormals();
    }

    public void ResetMesh()
    {
        UpdateMesh();
    }
    void LateUpdate()
    {
        UpdateMesh();
    }

    public void CalculateDepression(ContactPoint contact, float collisionSize)
    {
        newModifiedVertices = new List<Vector3>();

        for (int i = 0; i < originalVertices.Length; ++i)
        {
            // Calculate the vector from the contact point to the original vertex
            Vector3 contactToVertex = originalVertices[i] - contact.point;
            
            // Calculate the distance from the contact point to the vertex
            float distance = contactToVertex.magnitude;
            if (distance < collisionSize)
            {
                float depressionAmount = Mathf.Lerp(0f, maximumDepression, distance / collisionSize);
                Vector3 newVert = originalVertices[i] - contactToVertex.normalized * depressionAmount;
                newModifiedVertices.Add(newVert);
            }
            else
            {
                newModifiedVertices.Add(originalVertices[i]);
            }
        }
        carMesh.vertices = newModifiedVertices.ToArray();
    }
}
