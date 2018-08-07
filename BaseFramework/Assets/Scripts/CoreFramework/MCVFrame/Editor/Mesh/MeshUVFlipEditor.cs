using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(MeshUVFlip))]
public class MeshUVFlipEditor : Editor {

    // Use this for initialization
    MeshUVFlip t;
    void OnEnable()
    {
        t = target as MeshUVFlip;
    }
    public override void OnInspectorGUI()
    {
        t._mesh = EditorGUILayout.ObjectField("Mesh", t._mesh ,typeof(Mesh)) as Mesh; 

        if (GUILayout.Button("DoFlip"))
        {
            DoFlip();
        }

    }
    void DoFlip()
    {
       Mesh _mesh = t._mesh;
       Vector2[] uvs = new Vector2[_mesh.uv.Length];
       for(int i = 0; i< uvs.Length;i++)
        {

            Vector2 uv = _mesh.uv[i];
            uvs[i] = new Vector2(1 - uv.x, uv.y);
        }


        Mesh newmesh = new Mesh();
        newmesh.vertices = _mesh.vertices;
        newmesh.triangles = _mesh.triangles;

        newmesh.uv = uvs;

        AssetDatabase.CreateAsset(newmesh, "Assets/ClientScripts/Client/RenderTextureHelper/Models/Meshs/Full.asset");
    }
}
