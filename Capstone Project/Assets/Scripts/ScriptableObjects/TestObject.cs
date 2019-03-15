using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New TestObject", menuName = "Test")]
public class TestObject : ScriptableObject
{
    public MenuSelection.Menu menu;
    public string localizationFile;
}

