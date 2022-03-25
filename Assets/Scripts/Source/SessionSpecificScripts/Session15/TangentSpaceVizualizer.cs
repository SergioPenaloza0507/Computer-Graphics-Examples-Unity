using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TangentSpaceVizualizer : MonoBehaviour
{
    
    [SerializeField] float vectorScale;

    private MeshFilter filter;
    
    #if UNITY_EDITOR

    private void OnValidate()
    {
        filter = GetComponent<MeshFilter>();
    }

    private void OnDrawGizmos()
    {
        Vector3[] vertices = filter.sharedMesh.vertices;
        Vector4[] tangents = filter.sharedMesh.tangents;
        Vector3[] normals = filter.sharedMesh.normals;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 positionWS = transform.position + Vector3.Scale(vertices[i], transform.lossyScale);
            Vector3 tangentWS = tangents[i];
            Vector3 normalWS = normals[i];
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(positionWS, positionWS + tangentWS * vectorScale);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(positionWS, positionWS + Vector3.Cross(tangentWS,normalWS) * vectorScale);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(positionWS, positionWS + normalWS * vectorScale);
            
        }
    }
#endif
}
