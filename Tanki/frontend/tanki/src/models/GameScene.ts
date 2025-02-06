import { Point } from "@chakra-ui/utils";
import { SceneData, VisualComposition } from "./GameModels";

export class GameScene
{
    private objects: VisualComposition[] = [];

    private worldSize: Point = {x: 800, y: 800};

    public update()
    {
    }

    public getObjects()
    {
        return this.objects;
    }

    public getWorldSize() : Point
    {
        return this.worldSize;
    }

    public changeState(data: SceneData)
    {
        this.objects = data.objects;
        
        this.worldSize.x = data.world.width;
        this.worldSize.y = data.world.height;
    }
}