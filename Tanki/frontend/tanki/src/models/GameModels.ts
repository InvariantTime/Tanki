
export type Vector2 = 
{
    x: number,
    y: number
};

export type VisualComposition =
{
    object: string,
    values: {[id: string]: any}
};

export type SceneData =
{
    world: World,
    objects: VisualComposition[]
};

export type World =
{
    width: number,
    height: number
}

export enum TankColors {
    Red,
    Green,
    Yellow,
    Blue
}