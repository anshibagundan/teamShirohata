using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public OVRCameraRig ovrCameraRig;
    public float moveSpeed = 2.0f;

    private Transform centerEyeAnchor;

    void Start()
    {
        if (ovrCameraRig == null)
        {
            Debug.LogError("OVRCameraRigが設定されていません。");
            return;
        }

        // CenterEyeAnchorの参照を取得
        centerEyeAnchor = ovrCameraRig.centerEyeAnchor;
    }

    void Update()
    {
        if (centerEyeAnchor == null)
            return;

        /// キーボードの入力を取得
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        // CenterEyeAnchorの正面方向と右方向を基に移動ベクトルを計算
        Vector3 moveDirection = (centerEyeAnchor.forward * moveVertical + centerEyeAnchor.right * moveHorizontal).normalized;

        // 移動ベクトルに速度と時間を掛けて移動量を計算
        Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;

        // OVRCameraRigの位置を更新
        ovrCameraRig.transform.position += moveAmount;
    }
}