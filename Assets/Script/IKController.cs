using UnityEngine;

public class IKController : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] bool _ikActive = true;
    [SerializeField] Transform rightHandObj = null;
    [SerializeField] Transform lookObj = null;
    [SerializeField] Vector3 positionOffset = Vector3.zero;
    [SerializeField] Vector3 rotationOffset = Vector3.zero;

    void Start()
    {
        _animator = GetComponent<Animator>();   
    }

    private void OnAnimatorIK()
    {
        if (_animator)
        {
            if (_ikActive)
            {
                if (lookObj != null)
                {
                    _animator.SetLookAtWeight(1);
                    _animator.SetLookAtPosition(lookObj.position);
                }

                if (rightHandObj != null)
                {
                    _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

                    _animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position + positionOffset);
                    _animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation * Quaternion.Euler(rotationOffset));
                }
            }
            else
            {
                _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                _animator.SetLookAtWeight(0);
            }
        }
    }
}
