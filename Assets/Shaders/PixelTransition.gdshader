shader_type canvas_item;

// ========================
// Screen Texture (required for Godot 4)
// ========================
uniform sampler2D SCREEN_TEXTURE : hint_screen_texture, filter_linear_mipmap;

// ========================
// Random function for pixelation pattern
// ========================
float rand(vec2 co){
    return fract(sin(dot(co.xy ,vec2(12.9898,96.233))) * 43758.5453);
}

// ========================
// User Controls
// ========================
uniform float progress : hint_range(0.0, 1.0) = 0.0;  // Progress bar: 0 to 1
uniform float resolution : hint_range(1.0, 50.0) = 5.0; // Pixel size
uniform vec4 pixel_color : source_color = vec4(0.0, 0.0, 0.0, 1.0); // Background color

void fragment()
{
	vec2 iResolution = 1.0 / SCREEN_PIXEL_SIZE;
	vec2 uv = FRAGCOORD.xy / iResolution.xy;
    
    // Calculate pixel grid position
    vec2 lowresxy = vec2(
    	floor(FRAGCOORD.x / resolution),
    	floor(FRAGCOORD.y / resolution)
    );
    
    // Use progress instead of sin(time)
    // progress = 0: fully transparent (shows scene)
    // progress = 0.5: 50% pixelated
    // progress = 1.0: fully pixelated (shows only pixel_color)
    if(progress > rand(lowresxy)){
        // Show the pixelated scene
        COLOR = texture(SCREEN_TEXTURE, uv);
        // Apply pixelation effect - sample from the center of each pixel block
        vec2 pixel_uv = floor(FRAGCOORD.xy / resolution) * resolution / iResolution.xy + (resolution * 0.5) / iResolution.xy;
        COLOR = texture(SCREEN_TEXTURE, pixel_uv);
    } else {
        // Show the background color (pixelated color)
        COLOR = pixel_color;
    }
}