using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField] private Image _targetImage;
    [SerializeField] private Image _inSightTargetIndicatorImage;
    [SerializeField] private RectTransform _offScreenTargetIndicator;
    [SerializeField] private float _outOfSightOffset = 20f;
    [SerializeField] private float _minDistanceToShow;

    private float outOfSightOffest { get { return _outOfSightOffset /* canvasRect.localScale.x*/; } }
    private GameObject target;
    private Camera mainCamera;
    private RectTransform canvasRect;
    private RectTransform rectTransform;
    private PlayerTransformController _playerTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }


    public void InitTargetIndicator(GameObject target, Camera mainCamera, Canvas canvas, Sprite targetSprite)
    {
        _targetImage.sprite = targetSprite;
        this.target = target;
        this.mainCamera = mainCamera;
        canvasRect = canvas.GetComponent<RectTransform>();
        _playerTransform = FindObjectOfType<PlayerTransformController>();
    }

    public void UpdateTargetIndicator()
    {
        SetIndicatorPosition();
        SetInnerTargetImageRotation(_targetImage);
        //Adjust distance display
        //Turn on or off when in range/out of range
        //Do stuff if picked as main target
    }

    private void SetInnerTargetImageRotation(Image targetImage)
    {
        targetImage.transform.eulerAngles = new Vector3(0, 0, 0);
    }


    protected void SetIndicatorPosition()
    {
        if (Vector3.Distance(target.transform.position, _playerTransform.GetPosition()) > _minDistanceToShow)
        {
            _offScreenTargetIndicator.gameObject.SetActive(false);
            return;
        }
        else
        { 
            _offScreenTargetIndicator.gameObject.SetActive(true);
        }

        //Get the position of the target in relation to the screenSpace 
        Vector3 indicatorPosition = mainCamera.WorldToScreenPoint(target.transform.position);
        //Debug.Log("GO: "+ gameObject.name + "; slPos: " + indicatorPosition + "; cvWidt: " + canvasRect.rect.width + "; cvHeight: " + canvasRect.rect.height);

        //In case the target is both in front of the camera and within the bounds of its frustrum
        if (indicatorPosition.z >= 0f & indicatorPosition.x <= canvasRect.rect.width * canvasRect.localScale.x
         & indicatorPosition.y <= canvasRect.rect.height * canvasRect.localScale.x & indicatorPosition.x >= 0f & indicatorPosition.y >= 0f)
        {

            //Set z to zero since it's not needed and only causes issues (too far away from Camera to be shown!)
            indicatorPosition.z = 0f;

            //Target is in sight, change indicator parts around accordingly
            targetOutOfSight(false, indicatorPosition);
        }

        //In case the target is in front of the ship, but out of sight
        else if (indicatorPosition.z >= 0f)
        {
            //Set indicatorposition and set targetIndicator to outOfSight form.
            indicatorPosition = OutOfRangeindicatorPositionB(indicatorPosition);
            targetOutOfSight(true, indicatorPosition);
        }
        else
        {
            //Invert indicatorPosition! Otherwise the indicator's positioning will invert if the target is on the backside of the camera!
            indicatorPosition *= -1f;

            //Set indicatorposition and set targetIndicator to outOfSight form.
            indicatorPosition = OutOfRangeindicatorPositionB(indicatorPosition);
            targetOutOfSight(true, indicatorPosition);

        }

        //Set the position of the indicator
        rectTransform.position = indicatorPosition;

    }

    private Vector3 OutOfRangeindicatorPositionB(Vector3 indicatorPosition)
    {
        //Set indicatorPosition.z to 0f; We don't need that and it'll actually cause issues if it's outside the camera range (which easily happens in my case)
        indicatorPosition.z = 0f;

        //Calculate Center of Canvas and subtract from the indicator position to have indicatorCoordinates from the Canvas Center instead the bottom left!
        Vector3 canvasCenter = new Vector3(canvasRect.rect.width / 2f, canvasRect.rect.height / 2f, 0f) * canvasRect.localScale.x;
        indicatorPosition -= canvasCenter;

        //Calculate if Vector to target intersects (first) with y border of canvas rect or if Vector intersects (first) with x border:
        //This is required to see which border needs to be set to the max value and at which border the indicator needs to be moved (up & down or left & right)
        float divX = (canvasRect.rect.width / 2f - outOfSightOffest) / Mathf.Abs(indicatorPosition.x);
        float divY = (canvasRect.rect.height / 2f - outOfSightOffest) / Mathf.Abs(indicatorPosition.y);

        //In case it intersects with x border first, put the x-one to the border and adjust the y-one accordingly (Trigonometry)
        if (divX < divY)
        {
            float angle = Vector3.SignedAngle(Vector3.right, indicatorPosition, Vector3.forward);
            indicatorPosition.x = Mathf.Sign(indicatorPosition.x) * (canvasRect.rect.width * 0.5f - outOfSightOffest) * canvasRect.localScale.x;
            indicatorPosition.y = Mathf.Tan(Mathf.Deg2Rad * angle) * indicatorPosition.x;
        }
        //In case it intersects with y border first, put the y-one to the border and adjust the x-one accordingly (Trigonometry)
        else
        {
            float angle = Vector3.SignedAngle(Vector3.up, indicatorPosition, Vector3.forward);

            indicatorPosition.y = Mathf.Sign(indicatorPosition.y) * (canvasRect.rect.height / 2f - outOfSightOffest) * canvasRect.localScale.y;
            indicatorPosition.x = -Mathf.Tan(Mathf.Deg2Rad * angle) * indicatorPosition.y;
        }

        //Change the indicator Position back to the actual rectTransform coordinate system and return indicatorPosition
        indicatorPosition += canvasCenter;
        return indicatorPosition;
    }



    private void targetOutOfSight(bool isOutOfSight, Vector3 indicatorPosition)
    {
        //In Case the indicator is OutOfSight
        if (isOutOfSight)
        {
            if (_offScreenTargetIndicator.gameObject.activeSelf == false) 
                _offScreenTargetIndicator.gameObject.SetActive(true);

            if (_inSightTargetIndicatorImage && _inSightTargetIndicatorImage.isActiveAndEnabled == true) 
                _inSightTargetIndicatorImage.enabled = false;

            //Set the rotation of the OutOfSight direction indicator
            _offScreenTargetIndicator.rotation = Quaternion.Euler(RotationOutOfSightTargetindicator(indicatorPosition));

            //outOfSightArrow.rectTransform.rotation  = Quaternion.LookRotation(indicatorPosition- new Vector3(canvasRect.rect.width/2f,canvasRect.rect.height/2f,0f)) ;
            /*outOfSightArrow.rectTransform.rotation = Quaternion.LookRotation(indicatorPosition);
            viewVector = indicatorPosition- new Vector3(canvasRect.rect.width/2f,canvasRect.rect.height/2f,0f);
            
            //Debug.Log("CanvasRectCenter: " + canvasRect.rect.center);
            outOfSightArrow.rectTransform.rotation *= Quaternion.Euler(0f,90f,0f);*/
        }
        //In case that the indicator is InSight, turn on the inSight stuff and turn off the OOS stuff.
        else
        {
            if (_offScreenTargetIndicator.gameObject.activeSelf == true) 
                _offScreenTargetIndicator.gameObject.SetActive(false);

            if (_inSightTargetIndicatorImage && _inSightTargetIndicatorImage.isActiveAndEnabled == false) 
                _inSightTargetIndicatorImage.enabled = true;
        }
    }


    private Vector3 RotationOutOfSightTargetindicator(Vector3 indicatorPosition)
    {
        //Calculate the canvasCenter
        Vector3 canvasCenter = new Vector3(canvasRect.rect.width / 2f, canvasRect.rect.height / 2f, 0f) * canvasRect.localScale.x;

        //Calculate the signedAngle between the position of the indicator and the Direction up.
        float angle = Vector3.SignedAngle(Vector3.up, indicatorPosition - canvasCenter, Vector3.forward);

        //return the angle as a rotation Vector
        return new Vector3(0f, 0f, angle);
    }
}