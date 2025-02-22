using UnityEngine;
using UnityEngine.UI;

public class ChildFinder
{
    public static T getComponent<T>(Transform parent, string childName) where T : Component
    {
        Transform childTransform = parent.Find(childName);
        return childTransform.GetComponent<T>();
    }
}