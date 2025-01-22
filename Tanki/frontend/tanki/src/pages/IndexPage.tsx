import { Button, Heading, Icon, IconButton, Spinner, Text, useDisclosure } from "@chakra-ui/react";
import { RoomList } from "../components/RoomList";
import { HubConnectionBuilder } from "@microsoft/signalr";
import { useEffect, useState } from "react";
import { Room } from "../models/Room";
import { RoomCreationForm } from "../components/RoomCreationForm";
import { PaginationControl } from "../components/PaginationControll";
import { FaPlus } from "react-icons/fa"
import { RoomService } from "../services/RoomService";

const hubUrl = "http://localhost:5074/ws/rooms";

const pageSize = 6;

export const IndexPage = () => {

    const { isOpen, onOpen, onClose } = useDisclosure();

    const [rooms, setRooms] = useState<Room[]>([]);
    const [loading, setLoading] = useState(true);
    const [page, setPage] = useState(1);
    const [roomCount, setRoomCount] = useState(0);

    const initRooms = async () =>
    {
        var rooms = await RoomService.getRooms(page, pageSize);

        if ( rooms != null) {
            setRooms(rooms);
        }
    }
    
    const onRoomsChanged = async () => {
        initRooms();
        
        var count = await RoomService.getRoomsCount();
        setRoomCount(count);
        setLoading(false);
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
            .then(onRoomsChanged);
    }, []);

    useEffect(() => {
        initRooms();
    }, [page]);

    return (
        <div className="w-full p-4">
            <div className="flex flex-col text-center
                items-center mt-20">

                <div className="w-full max-w-screen-md">
                    <div className="bg-green-500 pb-4 text-center justify-center
                        rounded-t">
                        <Heading color="white">
                            Choose room
                        </Heading>
                    </div>

                    <div className="bg-white rounded-b mb-4
                        border-b border-l border-r border-slate-300 shadow-xl">

                        <RenderTable />

                        <div className="flex justify-end p-2">
                            <IconButton colorScheme="green" onClick={onOpen}
                                icon={<FaPlus />} isRound aria-label="Plus" fontSize="21px">
                            </IconButton>
                        </div>

                        <RoomCreationForm isOpen={isOpen} onClose={onClose} />
                    </div>

                    <div className="flex justify-end">
                        <div className="bg-white flex rounded border
                         border-slate-300 shadow-xl">
                            <PaginationControl totalCount={Math.ceil(roomCount / pageSize)} page={page} setPage={setPage}/>
                        </div>
                    </div>

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