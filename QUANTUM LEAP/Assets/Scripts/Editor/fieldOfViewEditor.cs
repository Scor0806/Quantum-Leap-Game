using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (DemoAI))]
public class fieldOfViewEditor : Editor{
    
    private void OnSceneGUI(){
        DemoAI fow = (DemoAI)target;
        Handles.color = Color.white;
    }
}
