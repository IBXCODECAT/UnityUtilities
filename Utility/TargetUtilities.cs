using System.Collections.Generic;
using UnityEngine;

namespace Foresight.Utilities
{
    public class TargetUtilities
    {
        #region GetClosestObject
        /// <summary>Calculates the closest object from a given array of GameObjects.</summary>
        /// <param name="objects">Array of objects to compare distances.</param>
        /// <param name="originPos">Position to check distance from.</param>
        /// <returns>Transform of closest object.</returns>
        public static Transform GetClosestObject(GameObject[] objects, Vector3 originPos)
        {
            Transform closestObject = null;
            float minDist = Mathf.Infinity;
            foreach (GameObject obj in objects)
            {
                float dist = MathUtilities.CheckDistance(obj.transform.position, originPos);
                if (dist < minDist)
                {
                    closestObject = obj.transform;
                    minDist = dist;
                }
            }
            return closestObject;
        }

        /// <summary>Calculates the closest object from a given array of Transforms.</summary>
        /// <param name="objects">Array of objects to compare distances.</param>
        /// <param name="originPos">Position to check distance from.</param>
        /// <returns>Transform of closest object.</returns>
        public static Transform GetClosestObject(Transform[] objects, Vector3 originPos)
        {
            Transform closestObject = null;
            float minDist = Mathf.Infinity;
            foreach (Transform obj in objects)
            {
                float dist = MathUtilities.CheckDistance(obj.transform.position, originPos);
                if (dist < minDist)
                {
                    closestObject = obj.transform;
                    minDist = dist;
                }
            }
            return closestObject;
        }

        /// <summary>Calculates the closest object from a given List of GameObjects.</summary>
        /// <param name="objects">List of objects to compare distances.</param>
        /// <param name="originPos">Position to check distance from.</param>
        /// <returns>Transform of closest object.</returns>
        public static Transform GetClosestObject(List<GameObject> objects, Vector3 originPos)
        {
            Transform closestObject = null;
            float minDist = Mathf.Infinity;
            foreach (GameObject obj in objects)
            {
                float dist = MathUtilities.CheckDistance(obj.transform.position, originPos);
                if (dist < minDist)
                {
                    closestObject = obj.transform;
                    minDist = dist;
                }
            }
            return closestObject;
        }

        /// <summary>Calculates the closest object from a given List of Transforms.</summary>
        /// <param name="objects">List of objects to compare distances.</param>
        /// <param name="originPos">Position to check distance from.</param>
        /// <returns>Transform of closest object.</returns>
        public static Transform GetClosestObject(List<Transform> objects, Vector3 originPos)
        {
            Transform closestObject = null;
            float minDist = Mathf.Infinity;
            foreach (Transform obj in objects)
            {
                float dist = MathUtilities.CheckDistance(obj.transform.position, originPos);
                if (dist < minDist)
                {
                    closestObject = obj.transform;
                    minDist = dist;
                }
            }
            return closestObject;
        }
        #endregion

        #region GetClosestObjects
        /// <summary>Calculates the closest objects from a given List of GameObjects.</summary>
        /// <param name="objects">List of objects to compare distances.</param>
        /// <param name="originPos">Position to check distance from.</param>
        /// <param name="amount">Number of objects to return.</param>
        /// <returns>List of Transforms of the closest objects.</returns>
        public static List<Transform> GetClosestObjects(List<GameObject> objects, Vector3 originPos, int amount)
        {
            List<GameObject> tempObjects = new List<GameObject>();
            List<Transform> closestsTargets = new List<Transform>();

            foreach (GameObject go in objects)
                tempObjects.Add(go);

            for (int i = 0; i < amount; i++)
            {
                Transform closestObject = GetClosestObject(tempObjects, originPos);
                closestsTargets.Add(closestObject);
                tempObjects.Remove(closestObject.gameObject);
            }
            return closestsTargets;
        }

        /// <summary>Calculates the closest objects from a given List of Transforms.</summary>
        /// <param name="objects">List of objects to compare distances.</param>
        /// <param name="originPos">Position to check distance from.</param>
        /// <param name="amount">Number of objects to return.</param>
        /// <returns>List of Transforms of the closest objects.</returns>
        public static List<Transform> GetClosestObjects(List<Transform> objects, Vector3 originPos, int amount)
        {
            List<Transform> tempObjects = new List<Transform>();
            List<Transform> closestsTargets = new List<Transform>();

            foreach (Transform t in objects)
                tempObjects.Add(t);

            for (int i = 0; i < amount; i++)
            {
                Transform closestObject = GetClosestObject(tempObjects, originPos);
                closestsTargets.Add(closestObject);
                tempObjects.Remove(closestObject);
            }
            return closestsTargets;
        }

        /// <summary>Calculates the closest objects from a given Array of GameObjects.</summary>
        /// <param name="objects">Array of objects to compare distances.</param>
        /// <param name="originPos">Position to check distance from.</param>
        /// <param name="amount">Number of objects to return.</param>
        /// <returns>Array of Transforms of the closest objects.</returns>
        public static Transform[] GetClosestObjects(GameObject[] objects, Vector3 originPos, int amount)
        {
            List<GameObject> tempObjects = new List<GameObject>();
            List<Transform> closestsTargets = new List<Transform>();

            foreach (GameObject go in objects)
                tempObjects.Add(go);

            for (int i = 0; i < amount; i++)
            {
                Transform closestObject = GetClosestObject(tempObjects, originPos);
                closestsTargets.Add(closestObject);
                tempObjects.Remove(closestObject.gameObject);
            }
            return closestsTargets.ToArray();
        }

        /// <summary>Calculates the closest objects from a given Array of Transforms.</summary>
        /// <param name="objects">Array of objects to compare distances.</param>
        /// <param name="originPos">Position to check distance from.</param>
        /// <param name="amount">Number of objects to return.</param>
        /// <returns>Array of Transforms of the closest objects.</returns>
        public static Transform[] GetClosestObjects(Transform[] objects, Vector3 originPos, int amount)
        {
            List<Transform> tempObjects = new List<Transform>();
            List<Transform> closestsTargets = new List<Transform>();

            foreach (Transform t in objects)
                tempObjects.Add(t);

            for (int i = 0; i < amount; i++)
            {
                Transform closestObject = GetClosestObject(tempObjects, originPos);
                closestsTargets.Add(closestObject);
                tempObjects.Remove(closestObject);
            }
            return closestsTargets.ToArray();
        }
        #endregion

        #region GetPoint
        /// <summary>Calculates a point along a ray, hitting specified layers.</summary>
        /// <param name="origin">Starting position of the ray.</param>
        /// <param name="direction">Direction the ray will be pointing.</param>
        /// <param name="distance">Distance the ray will span.</param>
        /// <param name="mask">Layers with which the ray can collide.</param>
        /// <returns>Returns the closest point along the ray that meets a detectable layer.</returns>
        public static Vector3 GetPoint(Vector3 origin, Vector3 direction, float distance, LayerMask mask)
        {
            RaycastHit hit;
            Ray ray = new Ray(origin, direction);
            if (Physics.Raycast(ray, out hit, distance, mask))
            {
                Vector3 point = new Vector3(origin.x, origin.y, hit.point.z + (hit.normal.z * 1.25f));
                return point;
            }
            else return ray.GetPoint(distance);
        }

        /// <summary>Calculates a point along a ray at the specific distance.</summary>
        /// <param name="origin">Starting position of the ray.</param>
        /// <param name="direction">Direction the ray will be pointing.</param>
        /// <param name="distance">Distance of the point on the ray from the origin.</param>
        /// <returns>Returns a point along the ray at the given distance.</returns>
        public static Vector3 GetPoint(Vector3 origin, Vector3 direction, float distance)
        {
            Ray ray = new Ray(origin, direction);
            return ray.GetPoint(distance);
        }
        #endregion
    }
}
