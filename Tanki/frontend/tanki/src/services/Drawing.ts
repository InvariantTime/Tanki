import { Bullet } from "../models/GameModels";

const radius = 12;

export const drawBullet = (context: CanvasRenderingContext2D, bullet: Bullet) => {

    context.fillStyle = "rgb(255, 100, 10)";
    context.beginPath();
    context.arc(bullet.position.x, bullet.position.y, radius, 0, Math.PI * 2);
    context.fill();
    context.restore();
}