using Leap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motions : MonoBehaviour
{
    public static bool Fist(Hand hand)
    {
        int fingers = 0;

        for (int i = 0; i < hand.Fingers.Count; i++)
            if (!hand.Fingers[i].IsExtended)
                fingers++;

        return fingers >= hand.Fingers.Count;
    }


    public static Hand GetLeftHand(List<Hand> hands)
    {
        for (int i = 0; i < hands.Count; i++)
            if (hands[i].IsLeft)
                return hands[i];

        return null;
    }

    public static Hand GetRightHand(List<Hand> hands)
    {
        for (int i = 0; i < hands.Count; i++)
            if (hands[i].IsRight)
                return hands[i];

        return null;
    }


    public static bool Pinch(Finger firstFinger, Finger secondFinger, float distance)
    {
        return Vector3.Distance(VectorToVector3(firstFinger.TipPosition), VectorToVector3(secondFinger.TipPosition)) <= distance;
    }

    public static float GetDistanceBetweenHands(Hand left, Hand right)
    {
        return Vector3.Distance(VectorToVector3(left.PalmPosition).normalized, VectorToVector3(right.PalmPosition).normalized);
    }


    public static float GetHandPitch(Hand hand)
    {
        return hand.Direction.Pitch;
    }


    public static float GetHandYaw(Hand hand)
    {
        return hand.Direction.Yaw;
    }


    public static float GetHandRoll(Hand hand)
    {
        return hand.PalmNormal.Roll;
    }


    public static Vector3 GetHandVelocity(Hand hand)
    {
        return VectorToVector3(hand.PalmVelocity);
    }


    public static List<Finger> GetExtendedFingers(Hand hand)
    {
        List<Finger> list = new List<Finger>();

        for (int i = 0; i < hand.Fingers.Count; i++)
            if (hand.Fingers[i].IsExtended)
                list.Add(hand.Fingers[i]);

        return list;
    }


    public static int GetExtendedFingersCount(Hand hand)
    {
        int count = 0;

        for (int i = 0; i < hand.Fingers.Count; i++)
            if (hand.Fingers[i].IsExtended)
                count++;

        return count;
    }


    public static bool GetHandGesture(Hand hand, int[] fingers)
    {
        int count = 0;

        for (int i = 0; i < hand.Fingers.Count; i++)
        {
            if (hand.Fingers[i].IsExtended)
            {
                bool correct = false;

                for (int j = 0; j < fingers.Length; j++)
                {
                    if (i == fingers[j])
                    {
                        correct = true;
                        count++;
                        continue;
                    }
                }

                if (!correct)
                    return false;
            }
        }

        return count == fingers.Length;
    }


    public static Vector3 VectorToVector3(Vector vector)
    {
        return new Vector3(vector.x, vector.y, vector.z).normalized;
    }
}