using Leap;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManagement : MonoBehaviour
{
    private AudioSource _AudioSource;
    private Controller _Controller;

    [SerializeField]
    private GameObject _GameObject;

    [SerializeField] private List<AudioClip> _AudioClips;

    private float _VelocityRotation;

    private void Start()
    {
        _AudioSource = this.GetComponent<AudioSource>();
        _Controller = new Controller();

        _VelocityRotation = 1f;
    }

    private void Update()
    {
        if (_Controller.IsConnected)
        {
            Frame frame = _Controller.Frame();

            Hand leftHand = Motions.GetLeftHand(frame.Hands);
            Hand rightHand = Motions.GetRightHand(frame.Hands);

            if (leftHand != null && rightHand != null)
            {
                if (Motions.GetHandGesture(leftHand, Gestures.ROCK) && Motions.GetHandGesture(rightHand, Gestures.ROCK))
                    Audio.Play(_AudioSource, _AudioClips[0]);

                else if (Motions.GetHandGesture(leftHand, Gestures.PEACE) && Motions.GetHandGesture(rightHand, Gestures.PEACE))
                    Audio.Play(_AudioSource, _AudioClips[1]);

                else if (Motions.GetHandGesture(leftHand, Gestures.FLAT) && Motions.GetHandGesture(rightHand, Gestures.FLAT))
                    if (Motions.GetDistanceBetweenHands(leftHand, rightHand) <= 0.2f)
                        Audio.Pause(_AudioSource);
            }


            if (rightHand != null)
            {
                if (Motions.GetHandGesture(rightHand, Gestures.POINT))
                {
                    if (leftHand != null)
                    {
                        if (_GameObject)
                        {
                            if (Motions.GetHandRoll(leftHand) > 1f)
                                _GameObject.transform.Rotate(new Vector3(0, 0, 1), _VelocityRotation);
                            else if (Motions.GetHandRoll(leftHand) < -1f)
                                _GameObject.transform.Rotate(new Vector3(0, 0, 1), -_VelocityRotation);

                            if (Motions.GetHandPitch(leftHand) > 1f)
                                _GameObject.transform.Rotate(new Vector3(0, 1, 0), _VelocityRotation);
                            else if (Motions.GetHandPitch(leftHand) < -1f)
                                _GameObject.transform.Rotate(new Vector3(0, 1, 0), -_VelocityRotation);
                        }
                    }
                }
            }



             /*   Motions.Pinch(hands[0].Fingers[Fingers.THUMB], hands[0].Fingers[Fingers.INDEX], 1f);

                if (Motions.Fist(hands[0]))
                {
                    if (_GameObject)
                    {
                        if (Motions.GetHandRoll(hands[0]) > 1f)
                            _GameObject.transform.Rotate(new Vector3(0, 0, 1), _VelocityRotation);
                        else if (Motions.GetHandRoll(hands[0]) < -1f)
                            _GameObject.transform.Rotate(new Vector3(0, 0, 1), -_VelocityRotation);
                    }

                    //Debug.Log(GetRoll(hands[0]));
                }

                Debug.Log("Rock: " + Motions.GetHandGesture(hands[0], Gestures.ROCK));*/
        }
    }
}