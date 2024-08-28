import { Point } from "@chakra-ui/utils";

export type Tank =
{
    position: Point,
    rotation: number,
    headRotation: number,
    owner: string
};
