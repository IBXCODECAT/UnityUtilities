using UnityEngine;

namespace Foresight.Utilities
{
    public static class MathUtilities
    {
        #region MapValue
        /// <summary>Maps a value that exists between a range to be within a 0 to 1 range.</summary>
        /// <param name="currentRangeMin">The minimum value of the range that the value currently exists.</param>
        /// <param name="currentRangeMax">The maximum value of the range that the value currently exists.</param>
        /// <param name="value">The value to be mapped within the new range.</param>
        /// <returns>Returns input value converted to a value within a 0 to 1 range.</returns>
        public static float MapValue01(float currentRangeMin, float currentRangeMax, float value)
        {
            return currentRangeMin + (currentRangeMax - currentRangeMin) * ((value - 0) / (1 - 0));
        }

        /// <summary>Maps a value that exists between a range to be within a new range.</summary>
        /// <param name="newRangeMin">The minimum value of the range within which the value will be mapped.</param>
        /// <param name="newRangeMax">The maximum value of the range within which the value will be mapped.</param>
        /// <param name="currentRangeMin">The minimum value of the range that the value currently exists.</param>
        /// <param name="currentRangeMax">The maximum value of the range that the value currently exists.</param>
        /// <param name="value">The value to be mapped within the new range.</param>
        /// <returns>Returns input value converted to a value within new range.</returns>
        public static float MapValue(float newRangeMin, float newRangeMax, float currentRangeMin, float currentRangeMax, float value)
        {
            return currentRangeMin + (currentRangeMax - currentRangeMin) * ((value - newRangeMin) / (newRangeMax - newRangeMin));
        }
        #endregion

        #region Clamp
        /// <summary>Clamps all axes of the entered Vector3 between the negative and positive of the entered clamp value.</summary>
        /// <param name="vector">The Vector3 to be clamped.</param>
        /// <param name="clampValue">The value at which the Vector3 will be clamped.</param>
        /// <returns>Returns the Vector3 after being clamped within the clamp range.</returns>
        public static Vector3 Clamp(Vector3 vector, float clampValue)
        {
            return new Vector3(Mathf.Clamp(vector.x, -clampValue, clampValue), Mathf.Clamp(vector.y, -clampValue, clampValue), Mathf.Clamp(vector.z, -clampValue, clampValue));
        }

        /// <summary>Clamps all axes of the entered Vector3 between the minClampValue and the maxClampValue.</summary>
        /// <param name="vector">The Vector3 to be clamped.</param>
        /// <param name="minClampValue">The minimum value at which the Vector3 will be clamped.</param>
        /// <param name="maxClampValue">The maximum value at which the Vector3 will be clamped.</param>
        /// <returns>Returns the Vector3 after being clamped between the minClampValue and maxClampValue.</returns>
        public static Vector3 Clamp(Vector3 vector, float minClampValue, float maxClampValue)
        {
            return new Vector3(Mathf.Clamp(vector.x, minClampValue, maxClampValue), Mathf.Clamp(vector.y, minClampValue, maxClampValue), Mathf.Clamp(vector.z, minClampValue, maxClampValue));
        }
        #endregion

        #region CheckDistance
        /// <summary>Calculates the distance between two Vector3 points.</summary>
        /// <param name="point1">The first point of the distance calculation.</param>
        /// <param name="point2"> The second point of the distance calculation.</param>
        /// <returns>Return the distance between the two points.</returns>
        public static float CheckDistance(Vector3 point1, Vector3 point2)
        {
            Vector3 heading;
            float distance;
            Vector3 direction;
            float distanceSquared;

            heading.x = point1.x - point2.x;
            heading.y = point1.y - point2.y;
            heading.z = point1.z - point2.z;

            distanceSquared = heading.x * heading.x + heading.y * heading.y + heading.z * heading.z;
            distance = Mathf.Sqrt(distanceSquared);

            direction.x = heading.x / distance;
            direction.y = heading.y / distance;
            direction.z = heading.z / distance;
            return distance;
        }

        /// <summary>Calculates the distance between two Vector2 points.</summary>
        /// <param name="point1">The first point of the distance calculation.</param>
        /// <param name="point2">The second point of the distance calculation.</param>
        /// <returns>Return the distance between the two points.</returns>
        public static float CheckDistance(Vector2 point1, Vector2 point2)
        {
            Vector2 heading;
            float distance;
            Vector2 direction;
            float distanceSquared;

            heading.x = point1.x - point2.x;
            heading.y = point1.y - point2.y;

            distanceSquared = heading.x * heading.x + heading.y * heading.y;
            distance = Mathf.Sqrt(distanceSquared);

            direction.x = heading.x / distance;
            direction.y = heading.y / distance;
            return distance;
        }
        #endregion

        #region IncrementLoop
        /// <summary>
        /// An extension method that increments a double until it equals the maxValue, where then it will reset to 0.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">This value that you want to increment.</param>
        /// <param name="maxValue">The value you want the count value to reset at.</param>
        /// <returns></returns>
        public static double IncrementLoop<T>(this double count, T maxValue)
        {
            if (!double.TryParse(maxValue.ToString(), out double doubleValue))
                Debug.LogError($"{maxValue} cannot be converted into a number!");

            if (count < doubleValue)
                count++;
            else
                count = 0;

            return count;
        }

        /// <summary>
        /// An extension method that increments a float until it equals the maxValue, where then it will reset to 0.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">This value that you want to increment.</param>
        /// <param name="maxValue">The value you want the count value to reset at.</param>
        /// <returns></returns>
        public static float IncrementLoop<T>(this float count, T maxValue)
        {
            return (float)((double)count).IncrementLoop(maxValue);
        }

        /// <summary>
        /// An extension method that increments a int until it equals the maxValue, where then it will reset to 0.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">This value that you want to increment.</param>
        /// <param name="maxValue">The value you want the count value to reset at.</param>
        /// <returns></returns>
        public static int IncrementLoop<T>(this int count, T maxValue)
        {
            return (int)((double)count).IncrementLoop(maxValue);
        }

        /// <summary>
        /// An extension method that increments a byte until it equals the maxValue, where then it will reset to 0.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">This value that you want to increment.</param>
        /// <param name="maxValue">The value you want the count value to reset at.</param>
        /// <returns></returns>
        public static byte IncrementLoop<T>(this byte count, T maxValue)
        {
            return (byte)((double)count).IncrementLoop(maxValue);
        }
        #endregion

        #region IncrementClamped
        /// <summary>
        /// An extension method that increments a double until it equals the maxValue, where then it won't go any higher.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">This value that you want to increment.</param>
        /// <param name="maxValue">The value you want the count value to stop at.</param>
        /// <returns></returns>
        public static double IncrementClamped<T>(this double count, T maxValue)
        {
            if (!double.TryParse(maxValue.ToString(), out double doubleValue))
                Debug.LogError($"{maxValue} cannot be converted into a number!");

            if (count < doubleValue)
                count++;

            return count;
        }

        /// <summary>
        /// An extension method that increments a float until it equals the maxValue, where then it won't go any higher.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">This value that you want to increment.</param>
        /// <param name="maxValue">The value you want the count value to stop at.</param>
        /// <returns></returns>
        public static float IncrementClamped<T>(this float count, T maxValue)
        {
            return (float)((double)count).IncrementClamped(maxValue);
        }

        /// <summary>
        /// An extension method that increments a int until it equals the maxValue, where then it won't go any higher.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">This value that you want to increment.</param>
        /// <param name="maxValue">The value you want the count value to stop at.</param>
        /// <returns></returns>
        public static int IncrementClamped<T>(this int count, T maxValue)
        {
            return (int)((double)count).IncrementClamped(maxValue);
        }

        /// <summary>
        /// An extension method that increments a byte until it equals the maxValue, where then it won't go any higher.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">This value that you want to increment.</param>
        /// <param name="maxValue">The value you want the count value to stop at.</param>
        /// <returns></returns>
        public static byte IncrementClamped<T>(this byte count, T maxValue)
        {
            return (byte)((double)count).IncrementClamped(maxValue);
        }
        #endregion

        #region DecrementLoop
        /// <summary>
        /// An extension method that decrements a double until it equals the minValue, where then it will then be set to maxValue.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">This value that you want to decrement.</param>
        /// <param name="minValue">The value you want the count value to stop at.</param>
        /// <param name="maxValue">The value you want the count value to reset to.</param>
        /// <returns></returns>
        public static double DecrementLoop<T>(this double count, T minValue, T maxValue)
        {
            if (!double.TryParse(maxValue.ToString(), out double doubleMaxValue))
                Debug.LogError($"{maxValue} cannot be converted into a number!");

            if (!double.TryParse(minValue.ToString(), out double doubleMinValue))
                Debug.LogError($"{minValue} cannot be converted into a number!");

            if (count > doubleMinValue)
                count--;
            else
                count = doubleMaxValue;

            return count;
        }

        /// <summary>
        /// An extension method that decrements a float until it equals the minValue, where then it will then be set to maxValue.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">This value that you want to decrement.</param>
        /// <param name="minValue">The value you want the count value to stop at.</param>
        /// <param name="maxValue">The value you want the count value to reset to.</param>
        /// <returns></returns>
        public static float DecrementLoop<T>(this float count, T minValue, T maxValue)
        {
            return (float)((double)count).DecrementLoop(minValue, maxValue);
        }

        /// <summary>
        /// An extension method that decrements a int until it equals the minValue, where then it will then be set to maxValue.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">This value that you want to decrement.</param>
        /// <param name="minValue">The value you want the count value to stop at.</param>
        /// <param name="maxValue">The value you want the count value to reset to.</param>
        /// <returns></returns>
        public static int DecrementLoop<T>(this int count, T minValue, T maxValue)
        {
            return (int)((double)count).DecrementLoop(minValue, maxValue);
        }

        /// <summary>
        /// An extension method that decrements a byte until it equals the minValue, where then it will then be set to maxValue.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">This value that you want to decrement.</param>
        /// <param name="minValue">The value you want the count value to stop at.</param>
        /// <param name="maxValue">The value you want the count value to reset to.</param>
        /// <returns></returns>
        public static byte DecrementLoop<T>(this byte count, T minValue, T maxValue)
        {
            return (byte)((double)count).DecrementLoop(minValue, maxValue);
        }
        #endregion
        
        #region DecrementClamped
        /// <summary>
        /// An extension method that decrements a double until it equals 0, where then it stops.
        /// </summary>
        /// <param name="count">This value that you want to decrement.</param>
        /// <returns></returns>
        public static double DecrementClamped(this double count)
        {
            if (count > 0)
                count--;

            return count;
        }

        /// <summary>
        /// An extension method that decrements a double until it equals the minValue, where then it stops.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">This value that you want to decrement.</param>
        /// <param name="minValue">The value you want the count value to stop at.</param>
        /// <returns></returns>
        public static double DecrementClamped<T>(this double count, T minValue)
        {
            if (!double.TryParse(minValue.ToString(), out double doubleValue))
                Debug.LogError($"{minValue} cannot be converted into a number!");

            if (count > doubleValue)
                count--;            

            return count;
        }

        /// <summary>
        /// An extension method that decrements an int until it equals 0, where then it stops.
        /// </summary>
        /// <param name="count">This value that you want to decrement.</param>
        /// <returns></returns>
        public static int DecrementClamped(this int count)
        {
            return (int)((double)count).DecrementClamped();
        }

        /// <summary>
        /// An extension method that decrements an int until it equals the minValue, where then it stops.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">This value that you want to decrement.</param>
        /// <param name="minValue">The value you want the count value to stop at.</param>
        /// <returns></returns>
        public static int DecrementClamped<T>(this int count, T minValue)
        {
            return (int)((double)count).DecrementClamped(minValue);
        }

        /// <summary>
        /// An extension method that decrements a float until it equals 0, where then it stops.
        /// </summary>
        /// <param name="count">This value that you want to decrement.</param>
        /// <returns></returns>
        public static float DecrementClamped(this float count)
        {
            return (float)((double)count).DecrementClamped();
        }

        /// <summary>
        /// An extension method that decrements a float until it equals the minValue, where then it stops.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">This value that you want to decrement.</param>
        /// <param name="minValue">The value you want the count value to stop at.</param>
        /// <returns></returns>
        public static float DecrementClamped<T>(this float count, T minValue)
        {
            return (float)((double)count).DecrementClamped(minValue);
        }

        /// <summary>
        /// An extension method that decrements a byte until it equals 0, where then it stops.
        /// </summary>
        /// <param name="count">This value that you want to decrement.</param>
        /// <returns></returns>
        public static byte DecrementClamped(this byte count)
        {
            return (byte)((double)count).DecrementClamped();
        }

        /// <summary>
        /// An extension method that decrements a byte until it equals the minValue, where then it stops.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count">This value that you want to decrement.</param>
        /// <param name="minValue">The value you want the count value to stop at.</param>
        /// <returns></returns>
        public static byte DecrementClamped<T>(this byte count, T minValue)
        {
            return (byte)((double)count).DecrementClamped(minValue);
        }
        #endregion
    }
}
