using UnityEngine;

public class PlayerReset : MonoBehaviour
{
   [SerializeField] private Transform resetTransform;
   [SerializeField] private GameObject player;
   [SerializeField] private Camera playerHead;
    
   [ContextMenu("Reset Position")]
   public void ResetPosition()
   {
      var rotationAngleY = resetTransform.transform.rotation.eulerAngles.y 
                           - playerHead.transform.rotation.eulerAngles.y;
      player.transform.Rotate(0, rotationAngleY, 0);

      var distanceDiff = resetTransform.position 
                         - playerHead.transform.position;
      player.transform.position += distanceDiff;
      
      Debug.Log("1");
   }
}
