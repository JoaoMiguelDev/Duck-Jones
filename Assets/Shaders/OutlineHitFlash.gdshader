shader_type canvas_item;

// ========================
// Outline Controls
// ========================
uniform vec4 outline_color : source_color = vec4(1.0, 1.0, 1.0, 1.0); // Outline color
uniform float outline_size : hint_range(0.0, 10.0) = 3.0; // Outline thickness in pixels

// ========================
// Hit Flash Controls
// ========================
uniform vec4 hit_flash_color : source_color = vec4(1.0, 1.0, 1.0, 1.0);
uniform bool hit_flash_on = false;

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

    for (float x = -1.0; x <= 1.0; x += 1.0) {
        for (float y = -1.0; y <= 1.0; y += 1.0) {
            if (x == 0.0 && y == 0.0) continue;

            vec2 offset = vec2(x, y) * pixel_size * outline_size;
            vec4 sample_color = texture(TEXTURE, UV + offset);
            alpha_sum += sample_color.a;
            samples += 1.0;
        }
    }

    float surround_alpha = alpha_sum / samples;

    // --- Outline (pixel transparente com vizinhos opacos) ---
    if (alpha < 0.1 && surround_alpha > 0.1) {
        COLOR = outline_color;
    }
    // --- Totalmente transparente, sem outline ---
    else if (alpha < 0.1) {
        COLOR = vec4(0.0, 0.0, 0.0, 0.0);
    }
    // --- Sprite normal (aplica hit flash aqui, se ativo) ---
    else {
        vec3 final_rgb = hit_flash_on ? hit_flash_color.rgb : original.rgb;
        COLOR = vec4(final_rgb, original.a);
    }
}