import { Table, TableContainer, Tbody, Text, Th, Thead, Tr } from "@chakra-ui/react";
import { Player } from "../models/Player";

interface Props {
    players: Player[]
}

export const PlayerList = ({players}: Props) =>
{
    return (
        <div className="p-2">

            <Text as="b">Players</Text>

            <TableContainer>
                <Table variant="striped">
                    <Thead>
                        <Tr>
                            <Th>Place</Th>
                            <Th>Name</Th>
                            <Th isNumeric>Score</Th>
                        </Tr>
                    </Thead>
                    
                    <Tbody>
                        {players.map((player, index) =>
                            <Tr>
                                <Th>{index + 1}</Th>
                                <Th>{player.name}</Th>
                                <Th isNumeric>{player.score}</Th>
                            </Tr>
                        )}
                    </Tbody>
                </Table>
            </TableContainer>
        </div>
    );
}