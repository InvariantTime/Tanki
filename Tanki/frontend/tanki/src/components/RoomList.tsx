import {
    Table, TableContainer,
    Tbody,
    Text,
    Tfoot, Th, Thead, Tr
} from "@chakra-ui/react";
import React from "react";
import { Room } from "../models/Room";

interface Props {
    rooms: Room[]
}

export const RoomList = ({ rooms }: Props) => {

    return (
        <div>
            <TableContainer className="mb-10">
                <Table variant="striped" colorScheme="teal">
                    <Thead>
                        <Tr>
                            <Th><Text className="text-slate-200">i</Text></Th>
                            <Th><Text className="text-slate-200">Name</Text></Th>
                            <Th><Text className="text-slate-200">Admin</Text></Th>
                            <Th isNumeric><Text className="text-slate-200">Players</Text></Th>
                        </Tr>
                    </Thead>

                    <Tbody>
                        {
                            rooms?.map((r, i) =>
                                <Tr>
                                    <Th><Text className="text-slate-400">{i + 1}</Text></Th>
                                    <Th><Text className="text-slate-400">{r.name}</Text></Th>
                                    <Th><Text className="text-slate-400">None</Text></Th>
                                    <Th isNumeric><Text className="text-slate-400">0/10</Text></Th>
                                </Tr>
                            )
                        }
                    </Tbody>
                </Table>
            </TableContainer>
        </div>
    );
}