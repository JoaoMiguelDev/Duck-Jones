shader_type canvas_item;

uniform float radius_px : hint_range(0.0, 200.0) = 32.0;
uniform vec4 circle_color : source_color = vec4(1.0, 0.0, 0.0, 0.8);
uniform vec2 rect_size = vec2(128.0, 128.0);
uniform float pixel_size : hint_range(1.0, 32.0) = 4.0; // tamanho do "pixel" retrô
uniform float thickness_px : hint_range(0.0, 20.0) = 4.0;

void fragment() {
	// Posição em pixels, centralizada
	vec2 pixel_pos = (UV - vec2(0.5)) * rect_size;

	// AQUI está a mágica: arredonda a posição para a grade de "pixels grandes"
	vec2 snapped_pos = floor(pixel_pos / pixel_size) * pixel_size;

	float dist = length(snapped_pos);

	// Sem smoothstep suave - usa step() para bordas duras (sem antialiasing)
	float outer_edge = step(dist, radius_px);
	float inner_edge = step(dist, radius_px - thickness_px);

	float ring = outer_edge - inner_edge;

	COLOR = vec4(circle_color.rgb, circle_color.a * ring);
}