Shader "Custom/AdvancedWater"
{
    Properties
    {
        [Header(Wave Settings)]
        _WaveSpeed ("Wave Speed", Range(0, 3)) = 1.0
        _WaveAmplitude ("Wave Amplitude", Range(0, 2)) = 0.3
        _WaveFrequency ("Wave Frequency", Range(0.1, 5)) = 1.0
        _WaveSteepness ("Wave Steepness", Range(0, 1)) = 0.5

        [Header(Surface Color)]
        _ShallowColor ("Shallow Color", Color) = (0.1, 0.55, 0.6, 0.8)
        _DeepColor ("Deep Color", Color) = (0.0, 0.1, 0.2, 1.0)
        _DepthFadeDistance ("Depth Fade Distance", Range(0.1, 20)) = 4.0

        [Header(Normal Mapping)]
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _NormalTiling ("Normal Tiling", Float) = 2.0
        _NormalSpeed ("Normal Scroll Speed", Float) = 0.15
        _NormalStrength ("Normal Strength", Range(0, 2)) = 1.0

        [Header(Foam)]
        _FoamColor ("Foam Color", Color) = (1,1,1,1)
        _FoamDistance ("Foam Distance", Range(0, 5)) = 0.5
        _FoamNoiseTiling ("Foam Noise Tiling", Float) = 8.0
        _FoamCutoff ("Foam Cutoff", Range(0,1)) = 0.5

        [Header(Lighting)]
        _Smoothness ("Smoothness", Range(0,1)) = 0.9
        _FresnelPower ("Fresnel Power", Range(0.1, 8)) = 3.0
        _SpecularIntensity ("Specular Intensity", Range(0, 5)) = 1.5

        [Header(Refraction)]
        _RefractionStrength ("Refraction Strength", Range(0, 0.2)) = 0.05
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "RenderPipeline"="UniversalPipeline" }
        LOD 300

        GrabPass { "_WaterGrabTexture" }

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On
            Cull Back

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile_fragment _ _SHADOWS_SOFT
            #pragma multi_compile_fog

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            TEXTURE2D(_NormalMap); SAMPLER(sampler_NormalMap);
            TEXTURE2D(_CameraDepthTexture); SAMPLER(sampler_CameraDepthTexture);
            TEXTURE2D(_WaterGrabTexture); SAMPLER(sampler_WaterGrabTexture);

            CBUFFER_START(UnityPerMaterial)
                float _WaveSpeed, _WaveAmplitude, _WaveFrequency, _WaveSteepness;
                float4 _ShallowColor, _DeepColor;
                float _DepthFadeDistance;
                float _NormalTiling, _NormalSpeed, _NormalStrength;
                float4 _FoamColor;
                float _FoamDistance, _FoamNoiseTiling, _FoamCutoff;
                float _Smoothness, _FresnelPower, _SpecularIntensity;
                float _RefractionStrength;
            CBUFFER_END

            struct Attributes
            {
                float3 positionOS : POSITION;
                float3 normalOS   : NORMAL;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 positionWS  : TEXCOORD0;
                float2 uv          : TEXCOORD1;
                float3 normalWS    : TEXCOORD2;
                float4 screenPos   : TEXCOORD3;
                float waveHeight   : TEXCOORD4;
            };

            // ---- Gerstner wave function: returns displacement and accumulates normal ----
            float3 GerstnerWave(float2 pos, float2 dir, float steepness, float wavelength, float speed, float t, inout float3 tangent, inout float3 binormal)
            {
                float k = 2.0 * PI / wavelength;
                float c = sqrt(9.8 / k) * speed;
                float2 d = normalize(dir);
                float f = k * (dot(d, pos) - c * t);
                float a = steepness / k;

                tangent += float3(
                    -d.x * d.x * steepness * sin(f),
                    d.x * steepness * cos(f),
                    -d.x * d.y * steepness * sin(f)
                );
                binormal += float3(
                    -d.x * d.y * steepness * sin(f),
                    d.y * steepness * cos(f),
                    -d.y * d.y * steepness * sin(f)
                );

                return float3(
                    d.x * a * cos(f),
                    a * sin(f),
                    d.y * a * cos(f)
                );
            }

            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                float3 positionWS = TransformObjectToWorld(IN.positionOS);
                float t = _Time.y * _WaveSpeed;

                float3 tangent = float3(1,0,0);
                float3 binormal = float3(0,0,1);
                float3 displacement = float3(0,0,0);

                // Sum of 4 Gerstner waves at varying directions/wavelengths for natural chop
                displacement += GerstnerWave(positionWS.xz, float2(1.0, 0.4), _WaveSteepness, 4.0 / _WaveFrequency, 1.0, t, tangent, binormal);
                displacement += GerstnerWave(positionWS.xz, float2(0.6, -1.0), _WaveSteepness * 0.7, 2.3 / _WaveFrequency, 1.3, t, tangent, binormal);
                displacement += GerstnerWave(positionWS.xz, float2(-0.8, 0.3), _WaveSteepness * 0.5, 1.5 / _WaveFrequency, 1.7, t, tangent, binormal);
                displacement += GerstnerWave(positionWS.xz, float2(0.2, 0.9), _WaveSteepness * 0.3, 0.8 / _WaveFrequency, 2.1, t, tangent, binormal);

                displacement *= _WaveAmplitude;

                positionWS += displacement;

                float3 normalWS = normalize(cross(binormal, tangent));

                OUT.positionWS = positionWS;
                OUT.normalWS = normalWS;
                OUT.uv = IN.uv;
                OUT.waveHeight = displacement.y;
                OUT.positionHCS = TransformWorldToHClip(positionWS);
                OUT.screenPos = ComputeScreenPos(OUT.positionHCS);

                return OUT;
            }

            float3 SampleNormalMap(float2 uv, float tiling, float2 scrollDir, float speed)
            {
                float2 uv1 = uv * tiling + _Time.y * speed * scrollDir;
                float2 uv2 = uv * tiling * 1.4 - _Time.y * speed * 0.7 * scrollDir.yx;
                float3 n1 = UnpackNormal(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, uv1));
                float3 n2 = UnpackNormal(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, uv2));
                return normalize(n1 + n2);
            }

            float hash21(float2 p)
            {
                p = frac(p * float2(123.34, 456.21));
                p += dot(p, p + 45.32);
                return frac(p.x * p.y);
            }

            float valueNoise(float2 p)
            {
                float2 i = floor(p);
                float2 f = frac(p);
                float a = hash21(i);
                float b = hash21(i + float2(1,0));
                float c = hash21(i + float2(0,1));
                float d = hash21(i + float2(1,1));
                float2 u = f * f * (3.0 - 2.0 * f);
                return lerp(a, b, u.x) + (c - a) * u.y * (1.0 - u.x) + (d - b) * u.x * u.y;
            }

            float4 frag(Varyings IN) : SV_Target
            {
                float2 screenUV = IN.screenPos.xy / IN.screenPos.w;

                // ---- Depth-based color (soft edge near shore) ----
                float sceneRawDepth = SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, screenUV).r;
                float sceneEyeDepth = LinearEyeDepth(sceneRawDepth, _ZBufferParams);
                float surfaceEyeDepth = IN.screenPos.w;
                float depthDifference = saturate((sceneEyeDepth - surfaceEyeDepth) / _DepthFadeDistance);

                float4 waterColor = lerp(_ShallowColor, _DeepColor, depthDifference);

                // ---- Normal mapping ----
                float3 tangentNormal = SampleNormalMap(IN.uv, _NormalTiling, float2(1,0.3), _NormalSpeed);
                tangentNormal = lerp(float3(0,0,1), tangentNormal, _NormalStrength);

                float3 upNormal = normalize(IN.normalWS + float3(tangentNormal.x, 0, tangentNormal.y) * 0.6);

                // ---- View / lighting vectors ----
                float3 viewDir = normalize(GetCameraPositionWS() - IN.positionWS);
                Light mainLight = GetMainLight();
                float3 lightDir = normalize(mainLight.direction);

                // Fresnel
                float fresnel = pow(1.0 - saturate(dot(upNormal, viewDir)), _FresnelPower);

                // Specular (Blinn-Phong)
                float3 halfDir = normalize(lightDir + viewDir);
                float specAngle = saturate(dot(upNormal, halfDir));
                float specular = pow(specAngle, lerp(8.0, 256.0, _Smoothness)) * _SpecularIntensity;

                // ---- Refraction via grab pass ----
                float2 refractionOffset = tangentNormal.xy * _RefractionStrength;
                float3 refractedScene = SAMPLE_TEXTURE2D(_WaterGrabTexture, sampler_WaterGrabTexture, screenUV + refractionOffset).rgb;

                float3 baseColor = lerp(refractedScene, waterColor.rgb, waterColor.a);

                // ---- Foam near shoreline and wave crests ----
                float shoreFoam = 1.0 - saturate(depthDifference / max(_FoamDistance, 0.001));
                float crestFoam = smoothstep(0.6, 1.0, IN.waveHeight / max(_WaveAmplitude, 0.0001));
                float foamNoise = valueNoise(IN.uv * _FoamNoiseTiling + _Time.y * 0.2);
                float foamMask = saturate(max(shoreFoam, crestFoam) - (1.0 - foamNoise) * 0.3);
                foamMask = step(_FoamCutoff, foamMask) * foamMask;

                float3 litColor = baseColor + mainLight.color * specular;
                litColor = lerp(litColor, litColor + mainLight.color * 0.3, fresnel);
                float3 finalColor = lerp(litColor, _FoamColor.rgb, foamMask);

                float alpha = saturate(waterColor.a + foamMask + specular * 0.3);

                return float4(finalColor, alpha);
            }
            ENDHLSL
        }

        // Simple shadow caster pass so the water can still receive/cast shadows if needed
        Pass
        {
            Name "ShadowCaster"
            Tags { "LightMode"="ShadowCaster" }
            ZWrite On
            ColorMask 0

            HLSLPROGRAM
            #pragma vertex ShadowVert
            #pragma fragment ShadowFrag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes { float3 positionOS : POSITION; };
            struct Varyings { float4 positionHCS : SV_POSITION; };

            Varyings ShadowVert(Attributes IN)
            {
                Varyings OUT;
                float3 positionWS = TransformObjectToWorld(IN.positionOS);
                OUT.positionHCS = TransformWorldToHClip(positionWS);
                return OUT;
            }

            half4 ShadowFrag(Varyings IN) : SV_Target { return 0; }
            ENDHLSL
        }
    }

    FallBack "Universal Render Pipeline/Lit"
}
