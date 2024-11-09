import {
    Table, TableContainer,
    Tbody, Text, Th, Thead, Tr
} from "@chakra-ui/react";
import { Room } from "../models/Room";
import { FaLock } from "react-icons/fa";
import { useNavigate } from "react-router-dom";
import { UUID } from "crypto";

interface Props {
    rooms: Room[],
}

const RoomName = ({ name, isLocked }: { name: string, isLocked: boolean }) => {
    return (
        <Th>
            <div className="flex">
                <Text marginRight="2">{name}</Text>
                {isLocked ? <FaLock /> : <></>}
            </div>
        </Th>
    );
}

export const RoomList = ({ rooms }: Props) => {

    const navigate = useNavigate();

    const onRoomClick = (id: UUID) =>
    {
        navigate(`session/${id}`);
    }

    const RenderBody = () => {
        return (
            rooms?.map((r, i) =>
                <Tr className="hover:bg-blue-100" onClick={() => onRoomClick(r.id) }>
                    <Th>{i}</Th>
                    <RoomName name={r.name} isLocked={r.isLocked} />
                    <Th>{r.hostName}</Th>
                    <Th isNumeric>
                        <Text color={r.playerCount >= r.maxPlayerCount ? "red" : "black"}>
                        {r.playerCount + '/' + r.maxPlayerCount}
                            </Text>
                    </Th>
                </Tr>
            )
        );
    }

    return (
        <TableContainer>
            <Table variant="striped" colorScheme="teal">
                <Thead>
                    <Tr>
                        <Th>i</Th>
                        <Th>Name</Th>
                        <Th>Host</Th>
                        <Th isNumeric>Players</Th>
                    </Tr>
                </Thead>

                <Tbody>
                    {RenderBody()}
                </Tbody>
            </Table>
        </TableContainer>
    );
}