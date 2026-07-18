shader_type canvas_item;

// ========================
// Outline Controls
// ========================
uniform vec4 outline_color : source_color = vec4(1.0, 1.0, 1.0, 1.0); // Outline color
uniform float outline_size : hint_range(0.0, 10.0) = 3.0; // Outline thickness in pixels

void fragment() {
    // Get the original texture color
    vec4 original = texture(TEXTURE, UV);
    
    // Get the size of one pixel in UV space
    vec2 pixel_size = 1.0 / vec2(textureSize(TEXTURE, 0));
    
    // Check surrounding pixels for alpha
    float alpha = original.a;
    
    // Sample 8 surrounding pixels
    float alpha_sum = 0.0;
    float samples = 0.0;
    
    // Loop through surrounding pixels
    for (float x = -1.0; x <= 1.0; x += 1.0) {
        for (float y = -1.0; y <= 1.0; y += 1.0) {
            // Skip the center pixel
            if (x == 0.0 && y == 0.0) continue;
            
            // Sample the surrounding pixel
            vec2 offset = vec2(x, y) * pixel_size * outline_size;
            vec4 sample_color = texture(TEXTURE, UV + offset);
            alpha_sum += sample_color.a;
            samples += 1.0;
        }
    }
    
    // Calculate average alpha of surrounding pixels
    float surround_alpha = alpha_sum / samples;
    
    // If the current pixel is transparent but surrounding pixels have alpha, draw outline
    if (alpha < 0.1 && surround_alpha > 0.1) {
        COLOR = outline_color;
    } 
    // If the current pixel is transparent and no surrounding alpha, keep transparent
    else if (alpha < 0.1) {
        COLOR = vec4(0.0, 0.0, 0.0, 0.0);
    } 
    // Otherwise, show the original sprite
    else {
        COLOR = original;
    }
}