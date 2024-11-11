import { UUID } from "crypto";
import { Room } from "../models/Room";
import { STATUS_CODES } from "http";
import { NavigateFunction } from "react-router-dom";

const getRoomUrl = "http://localhost:5074/api/room?";
const getCountUrl = "http://localhost:5074/api/room/count"
const joinRoomUrl = "http://localhost:5074/api/session/join";

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

    public static async join(id: UUID, password: string, navigate: NavigateFunction) {

        const body = {
            id: id,
            password: password
        };

        const options: RequestInit = {
            method: "POST",
            credentials: "include",
            body: JSON.stringify(body),
            headers: {
                "CONTENT-TYPE": "application/json"
            }
        };

        const result = await fetch(joinRoomUrl, options);

        if (result.ok) {
            const session = await result.json();
            navigate(`/session/${session}`);
        }
        else if (result.status === 401) {
            navigate("/signin");
        }
        else {
            const error = await result.json();
            alert(error);
        }
    }
}