import { AbsoluteCenter, Button, Center, Input, Spinner, Text, Tooltip, useDisclosure } from "@chakra-ui/react";
import { RoomList } from "./RoomList";
import { useEffect, useRef, useState } from "react";
import { RoomCreationForm } from "./RoomCreationForm";
import { Room } from "../models/Room";
import { Connection } from "../models/Connection";

const url = "http://localhost:5074/api/room/";

interface Props {
    connection: Connection
}

export const Lobby = ({ connection }: Props) => {

    const [loading, setLoading] = useState(true);
    const { isOpen, onOpen, onClose } = useDisclosure();

    const [rooms, setRooms] = useState<Room[]>([]);
    const [currentRooms, setCurrentRooms] = useState<Room[]>([]);

    const InitRooms = async () => {
        setLoading(true);
        var rooms = await GetRooms();

        setRooms(rooms);
        setLoading(false);
    }

    const OnRoomsChanged = () => {
        InitRooms();
    }


    useEffect(() => {
        //  connection.RoomListChanged.bind(OnRoomsChanged);
        //InitRooms();

    }, [connection]);

    return (
        <div className="p-8 bg-white
             rounded border border-slate-300 shadow-xl">

            <div className="mb-4">
                <Input placeholder="search.." borderColor="GrayText" />
            </div>

            <div className="mb-4">
                <RoomList rooms={rooms} loading={loading} />
            </div>

            <RoomCreationForm isOpen={isOpen} onClose={onClose} />

            <Tooltip hasArrow label="add new room">
                <Button colorScheme="green" onClick={onOpen}>
                    <Text>+</Text>
                </Button>
            </Tooltip>
        </div>
    );
}

const GetRooms = async (): Promise<Room[]> => {
    const options =
    {
        method: "GET"
    };

    const result = await fetch(url, options);

    if (result.ok === false) {
        alert("Unable to connect to server");
        return [];
    }

    var rooms = await result.json();
    return rooms;
}