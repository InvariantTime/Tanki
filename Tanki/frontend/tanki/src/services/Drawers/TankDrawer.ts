import { Tank } from "../../models/GameModels";

const tankW = 75;
const tankH = 100;
const headSize = 60;
const rollSize = 20;
const weaponW = 25;
const weaponH = 70;

export const drawTank = (context: CanvasRenderingContext2D, tank: Tank) => {

    const position = tank.position;
    const rotation = tank.rotation;

    context.translate(position.x + tankW / 2, position.y + tankH / 2);
    context.rotate(rotation);

    context.translate(-position.x - tankW / 2, -position.y - tankH / 2);

    context.fillStyle = "rgb(100, 100, 100)";
    context.fillRect(position.x - rollSize / 2, position.y, tankW + rollSize, tankH);


    context.fillStyle = "rgb(50, 50, 50)";
    context.fillRect(position.x, position.y, tankW, tankH);

    context.translate(position.x + tankW / 2, position.y + tankH / 2);
    context.rotate(tank.headRotation);

    context.translate(-position.x - tankW / 2, -position.y - tankH / 2);

    context.fillStyle = "rgb(30, 30, 30)";
    context.fillRect(position.x + (tankW - headSize) / 2,
        position.y + (tankH - headSize) / 2, headSize, headSize);

    context.fillStyle = "rgb(100, 100, 100)";
    context.fillRect(position.x + (tankW - weaponW) / 2, position.y - tankH / 2, weaponW, weaponH);

    context.restore();
}