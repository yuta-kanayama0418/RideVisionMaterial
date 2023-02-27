using UnityEngine;

public class ParticleStopController : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particles;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "PlayerVehicleCollider") {

            foreach(ParticleSystem particle in particles){
                // Debug and Pause
                if (particle == null)
                    Debug.LogError("Not Attach to " + this.gameObject.name.ToString());

                particle.Stop();
            }
        }
    }
}
