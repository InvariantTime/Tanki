import { TankColors, VisualComposition, Vector2 } from "../../models/GameModels";

const tankW = 45;
const tankH = 45;
const headSize = 30;
const rollSize = 7;
const weaponW = 11;
const weaponH = 35;
const textOffset = 34;
const textW = 5;

export const drawTank = (context: CanvasRenderingContext2D, tank: VisualComposition) => {

    const position = tank.values["position"] as Vector2;
    const rotation =  (tank.values["rotation"] as number) / 180 * Math.PI;
    const headRotation = (tank.values["head"] as number) / 180 * Math.PI;

    context.translate(position.x + tankW / 2, position.y + tankH / 2);
    context.rotate(rotation);

    context.translate(-position.x - tankW / 2, -position.y - tankH / 2);

    context.fillStyle = "rgb(100, 100, 100)";
    context.fillRect(position.x - rollSize / 2, position.y, tankW + rollSize, tankH);


    context.fillStyle = getBodyColor(TankColors.Red);
    context.fillRect(position.x, position.y, tankW, tankH);

    context.translate(position.x + tankW / 2, position.y + tankH / 2);
    context.rotate(headRotation);

    context.translate(-position.x - tankW / 2, -position.y - tankH / 2);

    context.fillStyle = getHeadColor(TankColors.Red);
    context.fillRect(position.x + (tankW - headSize) / 2,
        position.y + (tankH - headSize) / 2, headSize, headSize);

    context.fillStyle = "rgb(100, 100, 100)";
    context.fillRect(position.x + (tankW - weaponW) / 2, position.y - tankH / 2, weaponW, weaponH);
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