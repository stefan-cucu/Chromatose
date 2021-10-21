using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PortalScript))]
public class PortalScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PortalScript portalScript = (PortalScript)target;
        if(GUILayout.Button("Create portal at destination"))
        {
            GameObject portal = portalScript.gameObject;
            GameObject newPortal = Instantiate(portal);
            newPortal.transform.position = portalScript.destination;
            newPortal.transform.SetParent(portal.transform);
            newPortal.name = portal.name + " destination";
        }
    }
}
