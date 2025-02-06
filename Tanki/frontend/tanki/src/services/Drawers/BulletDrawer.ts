import { Vector2, VisualComposition } from "../../models/GameModels";

export const drawBullet = (context: CanvasRenderingContext2D, bullet: VisualComposition) => {

    context.fillStyle = "rgb(250, 190, 10)";

    var position = bullet.values["position"] as Vector2;

    context.translate(position.x, position.y);
    context.beginPath();
    context.arc(0, 0, 5, 0, Math.PI * 2);
    context.fill();

    context.translate(-position.x, -position.y);
}