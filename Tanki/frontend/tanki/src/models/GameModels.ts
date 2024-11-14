import { Point } from "@chakra-ui/utils";

export type Transformable = {
    position: Point,
    rotation: number
}

export type Tank = Transformable &
{
    headRotation: number,
    owner: string,
    color: TankColors
};

export type Bullet = Transformable &
{
};

export enum TankColors {
    Red,
    Green,
    Yellow,
    Blue
}