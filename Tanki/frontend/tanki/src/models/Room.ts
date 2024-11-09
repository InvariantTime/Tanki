import { UUID } from "crypto";

export type Room =
{
    name: string,
    hostName: string,
    isLocked: boolean,
    playerCount: number,
    maxPlayerCount: number,
    id: UUID
};