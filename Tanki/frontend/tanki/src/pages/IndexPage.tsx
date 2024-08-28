import { Button, Heading, Spinner, useDisclosure } from "@chakra-ui/react";
import { RoomList } from "../components/RoomList";
import { HubConnectionBuilder } from "@microsoft/signalr";
import { useEffect, useState } from "react";
import { Room } from "../models/Room";
import { RoomCreationForm } from "../components/RoomCreationForm";

const hubUrl = "http://localhost:5074/ws/rooms";
const roomUrl = "http://localhost:5074/api/room";

export const IndexPage = () => {

    const [rooms, setRooms] = useState<Room[]>([]);
    const [loading, setLoading] = useState(true);
    const { isOpen, onOpen, onClose } = useDisclosure();

    const initRooms = async () => {
        const options =
        {
            method: "GET"
        }

        var result = await fetch(roomUrl, options);

        if (result.ok === false)
            return;

        var data = await result.json();
        setRooms(data);
        setLoading(false);
    }

    const onRoomsChanged = () => {
        initRooms();
    }

    const RenderTable = () => {
        if (loading) {
            return (
                <div className="text-center p-1">
                    <Spinner />
                </div>
            );
        }

        return <RoomList rooms={rooms} />
    }

    useEffect(() => {
        initConnection(onRoomsChanged)
            .then(initRooms);
    }, []);

    return (
        <div className="w-full p-4">
            <div className="flex flex-col text-center
                items-center gap-10 mt-20">
                <Heading>
                    Choose room
                </Heading>

                <div className="bg-white w-full rounded 
                border border-slate-300 shadow-xl max-w-screen-md">

                    <RenderTable />
  
                    <div className="float-right m-4">
                        <Button colorScheme="green" onClick={onOpen}>+</Button>
                    </div>

                    <RoomCreationForm isOpen={isOpen} onClose={onClose}/>
                </div>

            </div>

        </div>
    );
}

async function initConnection(onRoomsChanged: () => void) {
    const connection = new HubConnectionBuilder()
        .withUrl(hubUrl)
        .withAutomaticReconnect()
        .build();

    connection.on("onRoomsChanged", onRoomsChanged);

    return connection.start();
}