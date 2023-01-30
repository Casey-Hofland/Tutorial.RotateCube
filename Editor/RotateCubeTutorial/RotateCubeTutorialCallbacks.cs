using System;
using Unity.Tutorials.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace Tutorial.RotateCube
{
    [CreateAssetMenu(fileName = nameof(RotateCubeTutorialCallbacks), menuName = "Tutorials/" + nameof(RotateCubeTutorialCallbacks))]
    public class RotateCubeTutorialCallbacks : ScriptableObject
    {
        public void StartTutorial(Unity.Tutorials.Core.Editor.Tutorial tutorial)
        {
            TutorialWindow.StartTutorial(tutorial);
        }

        private MeshFilter GetCube()
        {
            return Array.Find(FindObjectsOfType<MeshFilter>(true), meshFilter => meshFilter.sharedMesh == Resources.GetBuiltinResource<Mesh>("Cube.fbx"));
        }

        public bool DoesCubeExist()
        {
            return GetCube();
        }

        public bool AutoCompleteCube()
        {
            if (!DoesCubeExist())
            {
                Selection.activeGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            }
            
            return true;
        }

        public bool DoesCubeHaveRotatorScript()
        {
            return Array.Exists(GetCube().GetComponents<Component>(), component => component.GetType().Name == "Rotator");
        }

        public Quaternion lastRotation { get; set; }
        public float hasRotatedForSeconds { get; set; }

        public bool HasCubeRotatedForOneSecond()
        {
            if (hasRotatedForSeconds >= 4f)
            {
                return true;
            }

            var cube = GetCube();
            if (lastRotation == cube.transform.rotation)
            {
                hasRotatedForSeconds = 0f;
            }
            else
            {
                hasRotatedForSeconds += Time.deltaTime;
                lastRotation = cube.transform.rotation;
            }

            return false;
        }

        public bool AutoCompleteHasCubeRotatedForOneSecond()
        {
            hasRotatedForSeconds = 4f;
            return true;
        }
    }
}
