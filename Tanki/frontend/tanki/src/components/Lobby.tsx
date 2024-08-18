import { Button, Center, Input, Spinner, Text, Tooltip, useDisclosure } from "@chakra-ui/react";
import { RoomList } from "./RoomList";
import { useEffect, useRef, useState } from "react";
import { RoomCreationForm } from "./RoomCreationForm";
import { FocusableElement } from "@chakra-ui/utils";
import { Room } from "../models/Room";

const url = "http://localhost:5074/api/room/";

export const Lobby = () => {

    const [loading, setLoading] = useState(true);
    const [rooms, setRooms] = useState<Room[]>([]);
    const { isOpen, onOpen, onClose } = useDisclosure();

    const cancelRef = useRef<HTMLButtonElement | FocusableElement>(null);

    const RenderTable = () => {
        if (loading === false)
            return <RoomList rooms={rooms}/>

        return (
            <div className="text-center">
                <Spinner className="text-gray-400"/>
            </div>
        );
    }

    const InitRooms = async () =>
    {
        const options = 
        {
            method: "GET"
        };

        const result = await fetch(url, options);
        
        if (result.ok === false)
        {
            alert("Unable to connect to server");
            return;
        }

        var rooms = await result.json();
        setRooms(rooms);
        setLoading(false);
    }

    useEffect(() =>
    {
        InitRooms();
    });

    return (
        <Center className="min-h-screen">
            <div className="p-4 h-full max-w-screen-md w-full bg-gray-800 rounded border border-gray-700 shadow-lg shadow-blue-950">

                <div className="mb-4">
                    <Input placeholder="search.." className="text-gray-400" borderColor="GrayText" />
                </div>

                <div className="mb-4">
                    {RenderTable()}
                </div>

                <RoomCreationForm isOpen={isOpen} onClose={onClose} cancelRef={cancelRef} />
                <Tooltip hasArrow label="add new room">
                    <Button float="right" colorScheme="blue" onClick={onOpen}>
                        <Text className="text-slate-200">+</Text>
                    </Button>
                </Tooltip>

            </div>
        </Center>
    );
}