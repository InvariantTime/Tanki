import { Tank, TankColors, Transformable } from "../../models/GameModels";

const tankW = 75;
const tankH = 100;
const headSize = 60;
const rollSize = 20;
const weaponW = 25;
const weaponH = 70;
const textOffset = 34;
const textW = 5;

export const drawTank = (context: CanvasRenderingContext2D, obj: Transformable) => {

    const tank = obj as Tank;

    const position = tank.position;
    const rotation = tank.rotation / 180 * Math.PI;
    const headRotation = tank.headRotation / 180 * Math.PI;

    context.translate(position.x + tankW / 2, position.y + tankH / 2);
    context.rotate(rotation);

    context.translate(-position.x - tankW / 2, -position.y - tankH / 2);

    context.fillStyle = "rgb(100, 100, 100)";
    context.fillRect(position.x - rollSize / 2, position.y, tankW + rollSize, tankH);


    context.fillStyle = getBodyColor(tank.color);
    context.fillRect(position.x, position.y, tankW, tankH);

    context.translate(position.x + tankW / 2, position.y + tankH / 2);
    context.rotate(headRotation);

    context.translate(-position.x - tankW / 2, -position.y - tankH / 2);

    context.fillStyle = getHeadColor(tank.color);
    context.fillRect(position.x + (tankW - headSize) / 2,
        position.y + (tankH - headSize) / 2, headSize, headSize);

    context.fillStyle = "rgb(100, 100, 100)";
    context.fillRect(position.x + (tankW - weaponW) / 2, position.y - tankH / 2, weaponW, weaponH);

    context.translate(position.x + tankW / 2, position.y + tankH / 2);
    context.rotate(-headRotation - rotation);
    context.translate(-position.x - tankW / 2, -position.y - tankH / 2);

    context.font = "30px Arial";
    context.fillStyle = "rgb(10, 10, 10)";
    context.fillText(tank.owner, position.x, position.y + tankH + textOffset, tankW + textW);

    context.restore();
}


const getBodyColor = (color: TankColors) => {

    switch (color) {
        case TankColors.Red:
            return "rgb(150, 10, 10)";
        
        case TankColors.Green:
            return "rgb(10, 150, 10)";

        case TankColors.Blue:
            return "rgb(10, 10, 150)";

        case TankColors.Yellow:
            return "rgb(150, 150, 10)";
    }
}

const getHeadColor = (color: TankColors) => {

    switch (color) {
        case TankColors.Red:
            return "rgb(120, 10, 10)";
        
        case TankColors.Green:
            return "rgb(10, 120, 10)";

        case TankColors.Blue:
            return "rgb(10, 10, 120)";

        case TankColors.Yellow:
            return "rgb(120, 120, 10)";
    }
}