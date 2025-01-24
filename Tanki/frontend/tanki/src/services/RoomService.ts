import { UUID } from "crypto";
import { Room } from "../models/Room";
import { STATUS_CODES } from "http";
import { NavigateFunction } from "react-router-dom";

const getRoomUrl = "http://localhost:5074/api/session/getRooms/";
const getCountUrl = "http://localhost:5074/api/session/count/"
const accessRoomUrl = "http://localhost:5074/api/session/access/";

export class RoomService
{
    public static async getRooms(page: number, pageSize: number) {

        const url = getRoomUrl + `?page=${page}&pageSize=${pageSize}`;

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

        try
        {
            const result = await fetch(accessRoomUrl, options);
            if (result.ok) {
                navigate(`/session/${id}`);
            }
            else {
                const error = await result.text();
                alert(error);
            }
        }
        catch (e)
        {
            alert(`internal error: ${e}`);
        }
    }
}