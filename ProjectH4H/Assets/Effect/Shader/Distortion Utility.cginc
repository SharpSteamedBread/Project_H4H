#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Hashes.hlsl"

float SimpleNoise(float2 uv)
{
    float2 i = floor(uv);
    float2 f = frac(uv);
    f = f * f * (3.0 - 2.0 * f);
    uv = abs(frac(uv) - 0.5);
    float2 c0 = i + float2(0.0, 0.0);
    float2 c1 = i + float2(1.0, 0.0);
    float2 c2 = i + float2(0.0, 1.0);
    float2 c3 = i + float2(1.0, 1.0);
    float r0;
    Hash_Tchou_2_1_float(c0, r0);
    float r1;
    Hash_Tchou_2_1_float(c1, r1);
    float r2;
    Hash_Tchou_2_1_float(c2, r2);
    float r3;
    Hash_Tchou_2_1_float(c3, r3);
    float bottomOfGrid = lerp(r0, r1, f.x);
    float topOfGrid = lerp(r2, r3, f.x);
    float t = lerp(bottomOfGrid, topOfGrid, f.y);
    return t;
}

void SimpleNoiseDeterministic(float2 UV, float Scale, out float Out)
{
    float freq, amp;
    Out = 0.0f;
    freq = pow(2.0, float(0));
    amp = pow(0.5, float(3 - 0));
    Out += SimpleNoise(float2(UV.xy * (Scale / freq))) * amp;
    freq = pow(2.0, float(1));
    amp = pow(0.5, float(3 - 1));
    Out += SimpleNoise(float2(UV.xy * (Scale / freq))) * amp;
    freq = pow(2.0, float(2));
    amp = pow(0.5, float(3 - 2));
    Out += SimpleNoise(float2(UV.xy * (Scale / freq))) * amp;
}

float2 TilingAndOffset(float2 UV, float2 Tiling, float2 Offset)
{
    return UV * Tiling + Offset;
}

struct Bindings_Distortion_843269bc1b18d634ba1775dadb712013_float
{
    float2 NDCPosition;
    float3 TimeParameters;
};

float2 Distortion(float speed, float scale, float4 grabPosition)
{
    float slowSpeed = speed * 0.01;

    float moveBySlowTime = _Time.y * slowSpeed;

    float4 grabPositionPlanner = grabPosition + moveBySlowTime.xxxx;

    float noise;

    SimpleNoiseDeterministic(grabPositionPlanner.xy, scale, noise);

    float noiseScale = noise * 0.08;

    return TilingAndOffset(grabPosition.xy, float2(1, 1), noiseScale.xx);
}
