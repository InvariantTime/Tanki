import { Bullet, Transformable } from "../../models/GameModels";

export const drawBullet = (context: CanvasRenderingContext2D, bullet: Transformable) => {

    const position = bullet.position;

    context.fillStyle = "rgb(250, 190, 10)";

    context.beginPath();
    context.arc(position.x, position.y, 15, 0, Math.PI * 2);
    context.fill();
}