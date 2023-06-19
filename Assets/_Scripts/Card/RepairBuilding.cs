using UnityEngine;


public class RepairBuilding : MonoBehaviour, IEffectCard
{
    [SerializeField] private int repairAmount = 100;


    //private const string REPAIR_LAYER = "Building";


    public void DoEffect()
    {
        var ray = Helpers.MainCamera.ScreenPointToRay((Vector2)Input.mousePosition);
        var hit2D = Physics2D.GetRayIntersection(ray,LayerMask.NameToLayer("Building"));
        
        print(hit2D.collider);
        

        // if (hit2D.collider.TryGetComponent<HealthSystem>( out var healthSystem))
        // {
        //     healthSystem.Heal(repairAmount);
        // }
    }
}