Shader "Hidden/VHSShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LensDistortion("Lens distortion", float) = 1.2
        _ChromaticAberration("Chromatic aberration", float) = 0
        _ColorBleedIterations("Color bleed iterations", float) = 0
        _ColorBleedAmount("Color bleed amount", float) = 0
        _LineAmount("Line amount", float) = 1
        _LinesDisplacement("Lines displacement", float) = 0
        _LinesSpeed("Lines speed", float) = 0
        _Contrast("Contrast", Range(0,1)) = 1
        _SineLinesAmount("Sine lines amount", float) = 1
        _SineLinesSpeed("Sine lines speed", float) = 0
        _SineLinesThreshold("Sine lines threshold", Range(0,1)) = 0
        _SineLinesDisplacement("Sine lines displacement", float) = 0
        _NoiseTexture("Noise texture", 2D) = "white" {} 
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            #include "UnityCG.cginc"
 
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
 
            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _ChromaticAberration;
            float _ColorBleedAmount;
            float _ColorBleedIterations;
            float _LineAmount;
            float _LinesDisplacement;
            float _Contrast;
            float _Vignette;
            float _LensDistortion;
            float _LinesSpeed;
            float _SineLinesAmount;
            float _SineLinesDisplacement;
            float _SineLinesThreshold;
            float _SineLinesSpeed;
            sampler2D _NoiseTexture;
            float4 _NoiseTexture_ST;
 
            //from https://www.shadertoy.com/view/ldjGzV
            float2 screenDistort(float2 uv) {
                uv -= 0.5;
                uv = uv*_LensDistortion*(1./1.2+2.*uv.x*uv.x*uv.y*uv.y);
                uv += .5;
                return uv;
            }
 
            float rand (in float2 st) {
                return frac(sin(dot(st.xy, float2(12.9898,78.233)))*43758.5453123);
            }
 
 
            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                //UV distortion
                uv = screenDistort(uv);
 
                fixed colR = 0;
                fixed colG = 0;
                fixed colB = 0;
                float offset = 0;
 
                //Getting solid lines
                float lines = step(0.5, frac(uv.y * _LineAmount + _Time.y * _LinesSpeed)) * 2.0 - 1.0;
                float linesDispl = lines * _LinesDisplacement;
 
                //Offsetting and wrapping the whole screen overtime
                uv.y = frac(uv.y + lerp(0.0, 0.4, frac(_Time.z * 2.0) * step(0.97, rand(floor(_Time.z * 2.0)))));
 
                //Constantly changing random noise values
                float random = rand(uv + _Time.x);
                //Sampling the noise texture while also making it move constantly
                float noise = tex2D(_NoiseTexture, uv * _NoiseTexture_ST.xy + rand(_Time.x)).x;
 
                //Getting random values from -1 to 1 every few frames to randomly change the speed and direction of the sine lines
                float sineLinesTime = _Time.y * _SineLinesSpeed * (rand(floor(_Time.y)) * 2.0 - 1.0);
                float sineLines = sin(uv.y * _SineLinesAmount * UNITY_PI * 2.0 + sineLinesTime) * 0.5 + 0.5;
                //Lines with a random 0-1 value, to be used as mask
                float randLines = rand(round(uv.y * _SineLinesAmount + sineLinesTime));
                float sineLinesMask = step(randLines, _SineLinesThreshold);
                float sineLinesDispl = sineLines * sineLinesMask * _SineLinesDisplacement;
 
                //Multiple sampling for color bleeding
                for (int k = 0; k < _ColorBleedIterations; k++) {
                    offset += lerp(0.8, _ColorBleedAmount, sin(_Time.y) * 0.5 + 0.5);
                    colR += tex2D(_MainTex, uv + float2(offset + _ChromaticAberration + linesDispl + sineLinesDispl, 0) * _MainTex_TexelSize.xy).r;
                    colG += tex2D(_MainTex, uv + float2(offset + _ChromaticAberration - linesDispl + sineLinesDispl, 0) * _MainTex_TexelSize.xy).g;
                    colB += tex2D(_MainTex, uv + float2(offset + linesDispl + sineLinesDispl, 0) * _MainTex_TexelSize.xy).b;
                }
                colR /= _ColorBleedIterations;
                colG /= _ColorBleedIterations;
                colB /= _ColorBleedIterations;
                fixed4 col = fixed4(colR, colG, colB, 1.0);
 
                //Reducing contrast
                col = lerp(0.5, col, _Contrast);
                //Grain noise
                col *= max(0.7, random);
                //Top and bottom noise
                col += smoothstep(abs(uv.y * 2.0 - 1.0) - 0.8, abs(uv.y * 2.0 - 1.0) - 0.99, noise);
                //Passing lines noise
                col += step(0.99, 1.0 - randLines) * step(sineLines, noise) * 0.2;
                return col;
            }
            ENDCG
        }
    }
}