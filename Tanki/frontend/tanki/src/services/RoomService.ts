import { Room } from "../models/Room";

const getRoomUrl = "http://localhost:5074/api/room?";
const getCountUrl = "http://localhost:5074/api/room/count"

export class RoomService
{
    public static async getRooms(page: number, pageSize: number) {

        const url = getRoomUrl + new URLSearchParams({
            "page": `${page}`,
            "pageSize": `${pageSize}`
        }).toString();

        const options:RequestInit =
        {
            method: "GET",
            credentials: "include",
        }

        var result = await fetch(url, options);

        if (result.ok === false)
            return null;

        var json: Room[] = await result.json();
        return json;
    }

    public static async getRoomsCount(): Promise<number> {

        const options: RequestInit = {
            method: "GET",
            credentials: "include"
        };

        var result = await fetch(getCountUrl, options);

        if (result.ok) {
            var json = result.json();
            return json;
        }

        return 0;
    }

    public static createRoom() {
        
    }
}