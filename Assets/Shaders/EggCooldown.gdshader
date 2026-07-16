shader_type canvas_item;

// ========================
// User Controls
// ========================
uniform float progress : hint_range(0.0, 1.0) = 0.0;  // 0 = full cooldown, 1 = ready
uniform vec4 overlay_color : source_color = vec4(0.0, 0.0, 0.0, 0.7); // Cooldown overlay color

void fragment() {
    // Get the skill icon texture
    vec4 icon = texture(TEXTURE, UV);
    
    // Calculate the cutoff point (top to bottom)
    // progress = 0: full overlay (0% visible)
    // progress = 1: no overlay (100% visible)
    float cutoff = progress;  // CHANGED: removed 1.0 -
    
    // If the pixel is below the cutoff line, show the overlay
    if (UV.y > cutoff) {  // CHANGED: > instead of <
        // Show the overlay color (with transparency)
        COLOR = overlay_color;
    } else {
        // Show the skill icon
        COLOR = icon;
    }
}