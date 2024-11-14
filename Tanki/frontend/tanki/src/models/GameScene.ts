import { Bullet, Tank, TankColors, Transformable } from "./GameModels";

export class GameScene
{
    private objects: Transformable[] = [];

    public run()
    {
    }

    public getObjects() : Transformable[]
    {
        return this.objects;
    }

    public changeState()
    {

    }
}