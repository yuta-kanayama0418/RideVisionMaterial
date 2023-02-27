using UnityEngine;

/// <summary>
/// Execメソッドが実行された時、AudioQueue に AudioSource を追加します。
/// このコンポーネントと同じGameObjectに AudioSource が付いていることが前提です。 
/// </summary>

[RequireComponent(typeof(AudioSource))]
public class AudioQueueEnqueue : ExecutableBase {
    [SerializeField] private AudioQueue audioQueue;
    private AudioSource audioSource;
    
    
    public override void Exec() {
        audioSource = GetComponent<AudioSource>();
        if (audioQueue == null) {
            Debug.LogError($"{gameObject.name}: audioQueue is null.");
        }
        if (audioSource == null) {
            Debug.LogError($"audioSource is null.");
        }

        audioQueue.Enqueue(audioSource);

    }
}
