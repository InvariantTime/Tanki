import {
    Spinner,
    Table, TableContainer,
    Tbody, Th, Thead, Tr
} from "@chakra-ui/react";
import React from "react";
import { Room } from "../models/Room";

interface Props {
    rooms: Room[],
}

export const RoomList = ({ rooms }: Props) => {

    const RenderBody = () =>
    {
        return (
            rooms?.map((r, i) =>
                <Tr>
                    <Th>{i}</Th>
                    <Th>{r.name}</Th>
                    <Th>None</Th>
                    <Th isNumeric>0/10</Th>
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