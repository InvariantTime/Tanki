import { GameScene } from "../models/GameScene";
import { drawTank } from "./Drawers/TankDrawer";
import { drawBullet } from "./Drawers/BulletDrawer";
import { Bullet, Tank, Transformable } from "../models/GameModels";


export const renderScene = (context: CanvasRenderingContext2D, scene: GameScene) => {

    const objects = scene.getObjects();

    for (var i = 0; i < objects.length; i++) {
        const drawer = getDrawDelegate(objects[i]);
        drawer(context, objects[i]);
    }
}

const getDrawDelegate = (obj: Transformable) => {

    if (obj as Tank)
        return drawTank;

    if (obj as Bullet)
        return drawBullet;

    return (context: CanvasRenderingContext2D, obj: Transformable) => { };
}