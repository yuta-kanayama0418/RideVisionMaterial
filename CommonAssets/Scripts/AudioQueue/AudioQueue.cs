using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音のキューです。
/// キューの先頭にある音を再生し、再生が終わったらキューにある次の音を再生します。
/// 再生が終わったGameObjectを Inactive にします。
/// </summary>
public class AudioQueue : MonoBehaviour {
    private readonly Queue<AudioSource> queue = new Queue<AudioSource>();
    private AudioSource current;

    public void Enqueue(AudioSource audioSource) {
        Debug.Log($"AudioQueue: {audioSource.gameObject.name} enqueued.");
        queue.Enqueue(audioSource);
        if (audioSource.playOnAwake) {
            Debug.LogWarning($"{audioSource.gameObject.name} の PlayOnAwake をオフにしてください。再生タイミングは AudioQueue が管理します。");
        }
        
    }

    private void Update() {
        if (queue.Count == 0) return;
        
        // キューに残りがある かつ 今音を再生していない時、次を再生します。
        if (current == null || !current.isPlaying) {
            DequeueAndPlay();
        }
    }

    private void DequeueAndPlay() {
        if(current != null) current.gameObject.SetActive(false);
        current = queue.Dequeue();
        current.gameObject.SetActive(true);
        current.Play();
        Debug.Log($"AudioQueue: {current.gameObject.name} started.");
    }
}
