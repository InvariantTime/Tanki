import { GameScene } from "../models/GameScene";
import { drawTank } from "./Drawers/TankDrawer";
import { drawBullet } from "./Drawers/BulletDrawer";
import { VisualComposition } from "../models/GameModels";
import { Point } from "@chakra-ui/utils";

const wallSize = 10;
const wallColor = "rgb(30, 30, 30)";
const aspectRatioCoef = 0.55;

export const renderScene = (context: CanvasRenderingContext2D, scene: GameScene, viewport: Point) => {

    var world = scene.getWorldSize();

    transformViewport(context, world, viewport);

    drawWalls(context, world);
    
    const objects = scene.getObjects();
    
    for (var i = 0; i < objects.length; i++) {
        const drawer = getDrawDelegate(objects[i]);
        drawer(context, objects[i]);
    }
    
    context.restore();
    context.resetTransform();
}

const getDrawDelegate = (obj: VisualComposition) => {

    if (obj.object == "tank")
        return drawTank;

    if (obj.object == "bullet")
        return drawBullet;

    return (context: CanvasRenderingContext2D, obj: VisualComposition) => { };
}

const drawWalls = (context: CanvasRenderingContext2D, world: Point) =>
{
    context.fillStyle = wallColor;

    context.fillRect(-wallSize, 0, wallSize, world.y);
    context.fillRect(world.x, 0, wallSize, world.y);

    context.fillRect(0, -wallSize, world.x, wallSize);
    context.fillRect(0, world.y, world.x, wallSize);
}

const transformViewport = (context: CanvasRenderingContext2D, world: Point, viewport: Point) =>
{
    var sides = {x: world.x / 2, y: world.y / 2};
    var aspectRatio = 1;

    if (world.x > viewport.x || world.y > viewport.y)
    {
        aspectRatio = Math.max(viewport.x / world.x, viewport.y / world.y) * (viewport.x / viewport.y) * aspectRatioCoef;
    }
    
    
    context.translate(viewport.x / 2, viewport.y / 2);
    context.scale(aspectRatio, aspectRatio);
    context.translate(-sides.x, -sides.y);
}