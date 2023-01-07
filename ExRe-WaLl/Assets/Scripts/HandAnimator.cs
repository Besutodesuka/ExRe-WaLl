using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace MediaPipe.HandPose {

public class HandAnimator : MonoBehaviour
{
    #region Editable attributes

    [SerializeField] WebCamInput _webcam = null;
    [SerializeField] ResourceSet _resources = null;
    [SerializeField] bool _useAsyncReadback = true;
    [Space]
    [SerializeField] GameObject _Screen = null;
    [SerializeField] RawImage _monitorUI = null;
    [SerializeField] GameObject _cursor = null;
    
    
    #endregion

    #region Private members

    RectTransform _ScreenTransform;
    RectTransform _CursorTransform;
    HandPipeline _pipeline;
    #endregion

    #region MonoBehaviour implementation

    void Start(){
      _pipeline = new HandPipeline(_resources);
      _ScreenTransform = _Screen.GetComponent<RectTransform>();
      _CursorTransform = _cursor.GetComponent<RectTransform>();
    }

    void OnDestroy()
      => _pipeline.Dispose();

    void LateUpdate()
    {
        // Feed the input image to the Hand pose pipeline.
        _pipeline.UseAsyncReadback = _useAsyncReadback;
        // Texture2D originalTexture = (Texture2D)_webcam.inputImageTexture;
        _pipeline.ProcessImage(_webcam.inputImageTexture);
        if (_pipeline.Score() >= 0.10f){

          menuActionController.cursor_positions = _pipeline.GetKeyPoint(9);
        }else{
          //change to curser inactive animated
        }
        // UI update
        _monitorUI.texture = _webcam.inputImageTexture;
    }
    #endregion
    void movecursor(){
                //change to curser active animated

          // visualize hand
          // Vector3 finger_positions = _pipeline.GetKeyPoint(8);// tip of index finger relative position 
          // menuActionController.cursor_positions = new Vector3(
          //   Mathf.Max((1-finger_positions.x)/2 * _ScreenTransform.rect.width,0),
          //   Mathf.Max(finger_positions.y  * _ScreenTransform.rect.height  + _ScreenTransform.rect.height/2,0),
          //   5
          // );
          // Debug.LogFormat("position: {0}",cursor_positions);

    }
}

} // namespace MediaPipe.HandPose
