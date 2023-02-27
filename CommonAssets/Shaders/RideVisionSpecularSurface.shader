
// ***********************************
// RideVisionSpecularSurfaceシェーダー
// ***********************************

// RideVision向けにカスタマイズした Standard (Specular Setup)ベースのシェーダーです。

// 特徴1:
// スペキュラはアルベドと同じ色味にします。（明るさは調整可能です。）
// 理由: RideVisionでは XRオブジェクトの光沢の色とカメラの色をマッチさせる（周囲になじませる）ためにIBLキューブマップを利用しますが、
// キューブマップの色をより強く反映させるためには スペキュラ = アルベド としたほうが良いという経験則のためです。

// 特徴2:
// ディザリングによる疑似半透明の機能
// 実装の意図: RideVisionコンテンツではオブジェクトを出現させたり消したりすることが多いです。
// 急に出現させると違和感になるため、ディザリングを使ってフェードインする機能があると好ましいです。

Shader "Custom/RideVisionSpecularSurface"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Alpha("Alpha", Range(0,1)) = 1
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Specular("Specular", Range(0,1)) = 0.2
        _Smoothness ("Smoothness", Range(0,1)) = 0.2
        _Emission("Emission", Range(0,1)) = 0.1 // アルベドとの乗算です
        [Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull", Float) = 2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull [_Cull]
        LOD 200

        CGPROGRAM
        #pragma surface surf StandardSpecular fullforwardshadows // fullForwardShadowsが必要かどうかは要検討。軽量なほうが良い。

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float4 screenPos;
        };

        half _Smoothness;
        fixed4 _Color;
        fixed _Alpha;
        fixed _Specular;
        fixed _Emission;
        
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandardSpecular o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

            
            // ディザリングによる疑似半透明の機能を追加します。

            // ディザリング処理です。 参考: https://light11.hatenadiary.com/entry/2018/01/30/232343
            float bayerMatrix[16] = {1.0/17.0 , 9.0/17.0 , 3.0/17.0 , 11.0/17.0 ,    13.0/17.0 , 5.0/17.0 , 15.0/17.0 , 7.0/17.0 ,      4.0/17.0 , 12.0/17.0 , 2.0/17.0 , 10.0/17.0,     16.0/17.0, 8.0/17.0, 14.0/17.0, 6.0/17.0};
            float2 screenPos = floor(IN.screenPos.xy / IN.screenPos.w * _ScreenParams.xy);
            int2 ditherXY = int2(screenPos.x % 4, screenPos.y % 4);
            // そのスクリーン画素のディザリングの閾値です
            float bayerVal = bayerMatrix[ditherXY.y * 4 + ditherXY.x];
            // ピクセルを破棄します
            clip(_Alpha - bayerVal);


            
            
            o.Albedo = c.rgb;
            o.Specular = c.rgb * _Specular;
            o.Smoothness = _Smoothness;
            o.Emission = c.rgb * _Emission;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
