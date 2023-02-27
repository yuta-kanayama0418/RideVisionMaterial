using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 対象の MeshRendererのマテリアルのプロパティの値をフェード（ゆっくり変更）させます。
/// マテリアルは sharedMaterialが対象なので、同じマテリアルを使うオブジェクトはすべて変更されることに注意してください。
/// 例えば、フェードさせたいオブジェクトが100個あって、そのすべてが同じマテリアルである場合、そのうちの1個のみを target に指定すれば事足ります。
/// </summary>
public class MaterialFadeInOut : ExecutableBase {
    [SerializeField] private MeshRenderer[] targets;
    [SerializeField] private string materialPropertyName;
    [SerializeField] private bool doExecOnAwake = true;
    [SerializeField] private float startValue;
    [SerializeField] private float endValue = 1;
    [SerializeField] private float fadeDuration = 4;
    
    [SerializeField, Tooltip("フェードが終わったとき、このオブジェクトのSetActiveをfalseにします。")]
    private GameObject[] disableObjsOnFadeEnd;
    
    private List<Material> materials;
    private int propertyId;
    private readonly FadeInOutCalculator calculator = new FadeInOutCalculator();

    private void Awake() {
        // targetの sharedMaterial のリストを構築します。
        materials = new List<Material>();
        for (int i = 0; i < targets.Length; i++) {
            var target = targets[i];
            if (target == null) {
                Debug.LogError("Targetを設定してください。");
                continue;
            }
            var targetRenderer = target.GetComponent<Renderer>();
            if (targetRenderer == null) {
                Debug.LogError("TargetにRendererがありません。");
                continue;
            }
            materials.AddRange(targetRenderer.sharedMaterials);
        }
        
        // 設定によってはここで実行します。
        if (doExecOnAwake) Exec();
    }

    public override void Exec() {
        propertyId = Shader.PropertyToID(materialPropertyName);
        calculator.Start(startValue, endValue, fadeDuration);
        UpdateMaterials();
    }

    private void Update() {
        if (!calculator.IsFading) return;
        UpdateMaterials();
        // フェードの終了時
        if (!calculator.IsFading) {
            foreach (var obj in disableObjsOnFadeEnd) {
                if (obj != null) {
                    obj.SetActive(false);
                }
            }
        }
    }

    private void UpdateMaterials() {
        float nextVal = calculator.CalcNext(Time.deltaTime);
        foreach (var mat in materials) {
            mat.SetFloat(propertyId, nextVal);
        }
    }
}
