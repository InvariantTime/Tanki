import { Bullet, Tank, TankColors, Transformable } from "./GameModels";

export class GameScene
{
    private objects: Transformable[] = [
        {color: TankColors.Blue, position:{x:100, y:100}, rotation:10, owner:"tank", headRotation:0} as Tank,
        {position: {x: 100, y: 150}, rotation:0} as Bullet
    ];

    private obj: Transformable[] =[];

    public update()
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