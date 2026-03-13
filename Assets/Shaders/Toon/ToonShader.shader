Shader "Unlit/ToonShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Main Texture", 2D) = "white" {}

        [HDR]_AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)

        [HDR]_SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
        _Glossiness("Glossiness", Float) = 32

        [HDR]_RimColor("Rim Color", Color) = (1,1,1,1)
        _RimAmount("Rim Amount", Range(0,1)) = 0.716
        _RimThreshold("Rim Threshold", Range(0,1)) = 0.1
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
            "Queue"="Geometry"
            "RenderPipeline"="UniversalPipeline"
        }

        Pass
        {
            Tags
            {
                "LightMode"="UniversalForward"
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // support multiple lights
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fwdadd_fullshadows

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : NORMAL;
                float2 uv : TEXCOORD0;
                float3 viewDir : TEXCOORD1;
                SHADOW_COORDS(2)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _Color;
            float4 _AmbientColor;

            float4 _SpecularColor;
            float _Glossiness;

            float4 _RimColor;
            float _RimAmount;
            float _RimThreshold;

            v2f vert (appdata v)
            {
                v2f o;

                o.pos = UnityObjectToClipPos(v.vertex);

                // normalized world normal improves outline detection
                o.worldNormal = normalize(UnityObjectToWorldNormal(v.normal));

                o.viewDir = normalize(WorldSpaceViewDir(v.vertex));

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                TRANSFER_SHADOW(o);

                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float3 normal = normalize(i.worldNormal);
                float3 viewDir = normalize(i.viewDir);

                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

                float NdotL = dot(lightDir, normal);

                float shadow = SHADOW_ATTENUATION(i);

                float lightIntensity = smoothstep(0,0.01,NdotL * shadow);

                float4 light = lightIntensity * _LightColor0;

                // Specular
                float3 halfVector = normalize(lightDir + viewDir);
                float NdotH = dot(normal, halfVector);

                float specularIntensity = pow(NdotH * lightIntensity,_Glossiness*_Glossiness);
                float specularIntensitySmooth = smoothstep(0.005,0.01,specularIntensity);

                float4 specular = specularIntensitySmooth * _SpecularColor;

                // Rim lighting
                float rimDot = 1 - dot(viewDir,normal);
                float rimIntensity = rimDot * pow(NdotL,_RimThreshold);
                rimIntensity = smoothstep(_RimAmount - 0.01,_RimAmount + 0.01,rimIntensity);

                float4 rim = rimIntensity * _RimColor;

                float4 sample = tex2D(_MainTex,i.uv);

                return (light + _AmbientColor + specular + rim) * _Color * sample;
            }

            ENDCG
        }

        // DepthNormals pass for fullscreen outline detection
Pass
{
    Name "DepthNormals"
    Tags { "LightMode" = "DepthNormals" }

    ZWrite On
    Cull Back

    HLSLPROGRAM
    #pragma vertex vertDepth
    #pragma fragment fragDepth

    #include "UnityCG.cginc"

    struct appdata
    {
        float4 vertex : POSITION;
        float3 normal : NORMAL;
    };

    struct v2f
    {
        float4 pos : SV_POSITION;
        float3 normal : TEXCOORD0;
    };

    v2f vertDepth(appdata v)
    {
        v2f o;
        o.pos = UnityObjectToClipPos(v.vertex);
        o.normal = normalize(UnityObjectToWorldNormal(v.normal));
        return o;
    }

    float4 fragDepth(v2f i) : SV_Target
    {
        float3 normal = normalize(i.normal);
        normal = normal * 0.5 + 0.5;
        return float4(normal, 1.0);
    }

    ENDHLSL
}

        // Shadow casting
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}