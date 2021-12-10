float3 YCbCrToSRGB(float y, float2 cbcr)
{
    float b = y + cbcr.x * 1.772 - 0.886;
    float r = y + cbcr.y * 1.402 - 0.701;
    float g = y + dot(cbcr, float2(-0.3441, -0.7141)) + 0.5291;
    return float3(r, g, b);
}

void yCbCrToSRGB_float(float y, float2 cbcr, out float3 rgb){
    rgb = YCbCrToSRGB(y, cbcr);
}
