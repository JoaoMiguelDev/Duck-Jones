shader_type canvas_item;

// ========================
// Target Color
// ========================
uniform vec4 target_color : source_color = vec4(0.6627, 0.4078, 0.4078, 1.0); // #a96868

void fragment() {
    // Get the original texture color
    vec4 original_color = texture(TEXTURE, UV);
    
    // Preserve the original alpha (transparency)
    float alpha = original_color.a;
    
    // Apply the target color with original alpha
    COLOR = vec4(target_color.rgb, alpha);
}